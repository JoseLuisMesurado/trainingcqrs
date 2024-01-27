using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Training.Application.Permissions.Commands;
using Training.Core;
using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.XUnitTest.Mocks;

namespace Training.XUnitTest.CommandsTest
{
    public class UpdatePermissionCommandTest
    {
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock = new();
        private readonly Mock<IPermissionElasticRepository> _permissionElasticRepository = new();
        
        [Fact]
        public async Task UpdatePermissionTest()
        {
            var permissionList = MockRepositories.PermissionGetAllWithInclude();
            _permissionRepositoryMock.Setup(r => r.Update(It.IsAny<Permission<Guid>>())).ReturnsAsync((Permission<Guid> leaveType) =>
            {
                foreach (var item in permissionList)
                {
                    if (item.Id == leaveType.Id)
                    {
                        item.PermissionTypeId = leaveType.PermissionTypeId;
                        item.PermissionType.Id = leaveType.PermissionTypeId;
                        item.PermissionType.Name = "upddate ";
                    }
                }
                return leaveType;
            });
            _permissionRepositoryMock.Setup(r=>r.FindBy(It.IsAny<Guid>())).ReturnsAsync(MockRepositories.PermissionFindBy());
            _permissionRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<Func<IQueryable<Permission<Guid>>, IIncludableQueryable<Permission<Guid>, object>>>()))
                .ReturnsAsync(MockRepositories.PermissionGetById());

            var handler = new UpdatePermissionCommandHandler(_permissionRepositoryMock.Object,  _permissionElasticRepository.Object);
            await handler.Handle(new UpdatePermissionCommand
            {
                Id = new Guid(),
                PermissionTypeId = 3,
                EmployeeId=new Guid(),
            }, CancellationToken.None);

            Assert.Equal(3, permissionList.Count);
            var updated = permissionList.FirstOrDefault(x => x.Id == new Guid());
        }
    }
}

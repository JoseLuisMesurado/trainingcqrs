using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Training.Application.Permissions.Commands;
using Training.Core;
using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.XUnitTest.Mocks;

namespace Training.XUnitTest.CommandsTest
{
    public class AddPermissionCommandTest
    {
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock = new();
        private readonly Mock<IPermissionElasticRepository> _permissionElasticRepository = new();

        [Fact]
        public async Task AddPermissionTest()
        {
            var permissionList = MockRepositories.PermissionGetAllWithInclude();
            _permissionRepositoryMock.Setup(r => r.Add(It.IsAny<Permission<Guid>>())).ReturnsAsync((Permission<Guid> leaveType) =>
                        {
                            permissionList.Add(leaveType);
                            return leaveType;
                        });

            _permissionRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<Func<IQueryable<Permission<Guid>>, IIncludableQueryable<Permission<Guid>, object>>>()))
                .ReturnsAsync(MockRepositories.PermissionGetById());


           var handler = new AddPermissionCommandHandler(_permissionRepositoryMock.Object, _permissionElasticRepository.Object);
           await handler.Handle(new AddPermissionCommand
           {
               PermissionTypeId = 3,

           }, CancellationToken.None);

            Assert.Equal(4, permissionList.Count);
            

        }
    }
}

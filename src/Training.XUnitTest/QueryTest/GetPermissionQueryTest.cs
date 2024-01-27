using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Training.Application.Permissions.Queries;
using Training.Core;
using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.XUnitTest.Mocks;

namespace Training.XUnitTest.QueryTest
{
    public class GetPermissionQueryTest
    {
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock = new();
        private readonly Mock<IPermissionElasticRepository> _permissionElasticRepository = new();

        [Fact]
        public async Task GetPermissionList_WithOutParams_ReturnPermissionResponse()
        {
            _permissionRepositoryMock.Setup(m=> m.GetAllWithInclude(It.IsAny<Func<IQueryable<Permission<Guid>>, IIncludableQueryable<Permission<Guid>, object>>>()))
                .ReturnsAsync(MockRepositories.PermissionGetAllWithInclude());

            var handler = new GetPermissionsQueryHandler(_permissionRepositoryMock.Object, _permissionElasticRepository.Object);

            var toreturn = await handler.Handle(new GetPermissionsQuery(), CancellationToken.None);

            Assert.Equal(3, toreturn.Count);
        }
    }
}

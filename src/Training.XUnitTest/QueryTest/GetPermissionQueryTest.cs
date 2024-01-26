using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Training.Application.Permissions.Queries;
using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.XUnitTest.Mocks;

namespace Training.XUnitTest.QueryTest
{
    public class GetPermissionQueryTest
    {
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock = new();

        [Fact]
        public async Task GetPermissionList_WithOutParams_ReturnPermissionResponse()
        {
            _permissionRepositoryMock.Setup(m=> m.GetAllWithInclude(It.IsAny<Func<IQueryable<Permission<Guid>>, IIncludableQueryable<Permission<Guid>, object>>>()))
                .ReturnsAsync(MockRepositories.PermissionGetAllWithInclude());

            var handler = new GetPermissionQueryHandler(_permissionRepositoryMock.Object);

            var toreturn = await handler.Handle(new GetPermissionQuery(), CancellationToken.None);

            Assert.Equal(3, toreturn.Response.Count);
        }
    }
}

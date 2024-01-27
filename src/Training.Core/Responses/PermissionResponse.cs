using System.Linq.Expressions;
using Training.Core.Entities;

namespace Training.Core.Responses
{
    public class PermissionResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime GrantedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PermissionType { get; set; }
    }

    public static class PermissionSelectExpression
    {
        private static Expression<Func<Permission<Guid>, PermissionResponse>> dTO = p => new PermissionResponse
        {
            Id = p.Id,
            PermissionTypeId = p.PermissionTypeId,
            EmployeeId = p.EmployeeId,
            GrantedDate = p.GrantedDate,
            ExpirationDate = p.ExpirationDate,
            PermissionType = p.PermissionType.Name
        };

        public static Expression<Func<Permission<Guid>, PermissionResponse>> DTO { get => dTO; set => dTO = value; }
    }
}

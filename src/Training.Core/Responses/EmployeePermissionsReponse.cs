
namespace Training.Core;

public class EmployeePermissionsReponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime GrantedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}

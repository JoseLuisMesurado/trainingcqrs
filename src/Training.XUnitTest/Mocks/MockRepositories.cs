using Training.Core.Entities;

namespace Training.XUnitTest.Mocks
{
    public static class MockRepositories
    {
        public static List<Permission<Guid>> PermissionGetAllWithInclude()
        {
            var permissionList = new List<Permission<Guid>>()
            {
                new() {
                    Id = new Guid(),
                    PermissionTypeId = 1,
                    EmployeeId = new Guid(),
                    GrantedDate = DateTime.UtcNow.Date,
                    GrantedExpirationDate = DateTime.UtcNow.AddDays(3),
                    PermissionType =  new PermissionType<short>{
                                        Id= 1,
                                        Name="Admin"
                                        }
                    },
                new() {Id = new Guid(),
                    PermissionTypeId = 2,
                    EmployeeId = new Guid(),
                    GrantedDate = DateTime.UtcNow.Date,
                    GrantedExpirationDate = DateTime.UtcNow.AddDays(3),
                    PermissionType =  new PermissionType<short>{
                                        Id= 2,
                                        Name="Root"
                                        }
                    },
                new() {
                    Id = new Guid(),
                    PermissionTypeId = 3,
                    EmployeeId = new Guid(),
                    GrantedDate = DateTime.UtcNow.Date,
                    GrantedExpirationDate = DateTime.UtcNow.AddDays(3),
                    PermissionType =  new PermissionType<short>{
                                        Id= 3,
                                        Name="Permision 1"
                                        }

                    }
            };
            return permissionList;
        }

        public static Permission<Guid> PermissionGetById()
        {
            return new Permission<Guid>
            {
                Id = new Guid(),
                EmployeeId = new Guid(),
                PermissionTypeId = 3,
                GrantedDate = DateTime.UtcNow.Date,
                GrantedExpirationDate = DateTime.UtcNow.AddDays(3),
                PermissionType = new PermissionType<short>
                {
                    Id = 3,
                    Name = "Permision 1"
                }

            };
        }

        public static Permission<Guid> PermissionFindBy()
        {
            return new Permission<Guid>
            {
                Id = new Guid(),
                EmployeeId = new Guid(),
                PermissionTypeId = 3,
                GrantedDate = DateTime.UtcNow.AddDays(3),
                PermissionType = new PermissionType<short>
                {
                    Id = 3,
                    Name = "Permision 1"
                }
            };
        }
    }
}

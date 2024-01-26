export interface PermissionResponse {
    id: number;
    permissionTypeId: number;
    employeeFirstName: string;
    employeeLastName: string;
    grantedDate: Date;
    permissionType: string;
}
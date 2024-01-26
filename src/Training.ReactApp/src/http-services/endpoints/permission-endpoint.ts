import { BACKEND_URL } from "../../environments/appenvironment";

export const permissionEndpoints = {
    getpermissions: BACKEND_URL + '/permission',
    addpermission: BACKEND_URL + '/permission',
    updatepermission: BACKEND_URL + '/permission'
}
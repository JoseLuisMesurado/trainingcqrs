import { PermissionInterfaceDto } from "../interfaces/dto/permission-interface.dto";
import api from "./apiclient-cfg/axios-cfg";
import { PortalResponse } from "./base/apiresponse";
import { permissionEndpoints } from "./endpoints/permission-endpoint";
import { PermissionResponse } from "./reponses/permission-response";

export default class PermissionHttpService {
    static getPermission = () => {
        return api.get<PortalResponse<PermissionResponse>>(permissionEndpoints.getpermissions);
    }
    static addPermission = (values: PermissionInterfaceDto) => {
        return api.post(permissionEndpoints.addpermission,values);
    }
    static updatePermission = (values: PermissionInterfaceDto) => {
        return api.put(permissionEndpoints.updatepermission,values);
    }
}
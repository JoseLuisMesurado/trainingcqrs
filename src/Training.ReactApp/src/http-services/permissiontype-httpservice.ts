import api from "./apiclient-cfg/axios-cfg";
import { PortalResponse } from "./base/apiresponse";
import { permissionTypeEndpoints } from "./endpoints/permissiontype-endpoint";
import { PermissionTypeResponse } from "./reponses/permissiontype-response";

export default class PermissionTypeHttpService {
    static getPermissionType = () => {
        return api.get<PortalResponse<PermissionTypeResponse>>(permissionTypeEndpoints.getpermissiontypes);
    }
}
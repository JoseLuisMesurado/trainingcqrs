import { CommonStateInterface } from "../base/commonstate-interface";
import { PermissionInterfaceDto } from "../dto/permission-interface.dto";


export interface PermissionStateInterface extends CommonStateInterface {
    permissions: Array<PermissionInterfaceDto>,
}
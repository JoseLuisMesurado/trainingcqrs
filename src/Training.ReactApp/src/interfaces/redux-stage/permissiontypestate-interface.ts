import { CommonStateInterface } from "../base/commonstate-interface";
import { PermissionTypeInterfaceDto } from "../dto/permissiontype-interface.dto";

export interface PermissionTypeStateInterface extends CommonStateInterface {
    permissiontypes: Array<PermissionTypeInterfaceDto>,
}
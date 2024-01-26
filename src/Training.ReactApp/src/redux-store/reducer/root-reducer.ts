import permissionSlice from "./permission-slice";
import permissionTypeSlice  from "./permissiontype-slice";

const rootReducer = () => {
    return {
        permission: permissionSlice,
        permissiontype: permissionTypeSlice
    }
}
export default rootReducer;
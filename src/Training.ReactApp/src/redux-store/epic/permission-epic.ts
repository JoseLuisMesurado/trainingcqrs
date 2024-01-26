import {catchError, from, map, mergeMap, of} from "rxjs";
import {ofType} from "redux-observable";
import { 
    getPermissionRequestAction, 
    setPermissionSuccessAction,
    setPermissionFailedAction,
} from "../reducer/permission-slice";

import PermissionHttpService from "../../http-services/permission-httpservice";

export const permissionRequestEpic = (action$: any, state$: any) => {
    return action$.pipe(
        ofType(getPermissionRequestAction),
        mergeMap((action: any) =>
            from( PermissionHttpService.getPermission()
            ).pipe(
                map((response: any) => {
                    if (response.data) {
                        return setPermissionSuccessAction(response.data.response);
                    } else {
                        throw response;
                    }
                }),
                catchError((err) => {
                    let result = {
                        message: err
                    }
                    return of(setPermissionFailedAction(result));
                })
            )
        )
    );
};

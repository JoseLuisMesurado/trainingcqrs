import {catchError, from, map, mergeMap, of} from "rxjs";
import {ofType} from "redux-observable";
import { setPermissionTypeFailedAction, getPermissionTypeRequestAction, setPermissionTypeSuccessAction } from "../reducer/permissiontype-slice";
import PermissionTypeHttpService from "../../http-services/permissiontype-httpservice";

export const permissionTypeRequestEpic = (action$: any, state$: any) => {
    return action$.pipe(
        ofType(getPermissionTypeRequestAction),
        mergeMap((action: any) =>
            from( PermissionTypeHttpService.getPermissionType()
            ).pipe(
                map((response: any) => {
                    if (response.data) {
                        return setPermissionTypeSuccessAction(response.data.response);
                    } else {
                        throw response;
                    }
                }),
                catchError((err) => {
                    let result = {
                        message: err
                    }
                    return of(setPermissionTypeFailedAction(result));
                })
            )
        )
    );
};
import {combineEpics} from "redux-observable";
import { permissionRequestEpic } from "./permission-epic";
import { permissionTypeRequestEpic } from "./permisiontype-epic";

export const rootEpic = combineEpics(
    permissionRequestEpic,
    permissionTypeRequestEpic
);
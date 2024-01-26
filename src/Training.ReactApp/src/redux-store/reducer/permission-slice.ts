import type {PayloadAction} from '@reduxjs/toolkit'
import {createSlice} from '@reduxjs/toolkit'
import { PermissionStateInterface } from '../../interfaces/redux-stage/permissionstate-interface';

const initialState: PermissionStateInterface = {
    permissions: [],
    isLoading: false,
    isSuccessful: false,
    error: {}
}

export const permissionSlice = createSlice({
    name: 'permission',
    initialState,
    reducers: {
        getPermissionRequestAction: (state: any) => {
            state.isLoading = true;
        },
        setPermissionSuccessAction: (state: any, action: PayloadAction<[]>) => {
            state.permissions = action.payload;
            state.isLoading = false;
            state.isSuccessful = true;
        },
        setPermissionFailedAction: (state: any, action: PayloadAction<{}>) => {
            state.isSuccessful = false;
            state.result = action.payload;
        },
    },
})

export const {
    getPermissionRequestAction,
    setPermissionSuccessAction,
    setPermissionFailedAction,
} = permissionSlice.actions

export default permissionSlice.reducer
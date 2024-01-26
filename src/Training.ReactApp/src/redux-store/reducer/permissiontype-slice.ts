import type {PayloadAction} from '@reduxjs/toolkit'
import {createSlice} from '@reduxjs/toolkit'
import { PermissionTypeStateInterface } from '../../interfaces/redux-stage/permissiontypestate-interface';

const initialState: PermissionTypeStateInterface = {
    permissiontypes: [],
    isLoading: false,
    isSuccessful: false,
    error: {}
}

export const permissionTypeSlice = createSlice({
    name: 'permissiontype',
    initialState,
    reducers: {
        getPermissionTypeRequestAction: (state: any) => {
            state.isLoading = true;
        },
        setPermissionTypeSuccessAction: (state: any, action: PayloadAction<[]>) => {
            state.permissiontypes = action.payload;
            state.isLoading = false;
            state.isSuccessful = true;
        },
        setPermissionTypeFailedAction: (state: any, action: PayloadAction<{}>) => {
            state.isSuccessful = false;
            state.result = action.payload;
        },
    },
})

export const {
    getPermissionTypeRequestAction,
    setPermissionTypeSuccessAction,
    setPermissionTypeFailedAction
} = permissionTypeSlice.actions

export default permissionTypeSlice.reducer
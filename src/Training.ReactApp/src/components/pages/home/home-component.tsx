import { Box, Button, Container, Stack } from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams } from '@mui/x-data-grid';
import { useDispatch, useSelector } from 'react-redux/es/exports';
import { getPermissionRequestAction } from '../../../redux-store/reducer/permission-slice';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';

const columns: GridColDef[] = [
    { field: 'id',  hideable: true },
    { field: 'employeeFirstName', headerName: 'First Name', width: 150 },
    { field: 'employeeLastName', headerName: 'Last Name', width: 150 },
    { field: 'permissionType', headerName: 'Permission Type', width: 150 },
    { field: 'grantedDate', headerName: 'Granted Date', width: 200 },
    { field: 'actions', headerName: '', flex: 1, align : "right",
        renderCell: (params: GridRenderCellParams<Number>) => (
          <strong>
            <Link to={`/updatepermission/${params.id}`}>
                <Button
                variant="contained"
                size="small"
                style={{ marginLeft: 16 }}
                tabIndex={params.hasFocus ? 0 : -1}
                >
                Edit Permission
                </Button>
            </Link>
          </strong>
        ),
      },
];
export const HomeComponent = () => {
    const permissionState = useSelector((state: any) => state.permission);
    const dispatch = useDispatch()
    useEffect(() => {
        dispatch(getPermissionRequestAction())
    }, [dispatch])

    return (
        <Container fixed >
            <Box component="main" sx={{ padding: 2 }}>
            <h1> EMPLOYEES PERMISSIONS</h1>
                <div style={{ height: 400, width: '100%' }}>
                    <DataGrid rows={permissionState.permissions} columns={columns} />
                </div>
                <Stack direction="row" margin={2} justifyContent="flex-end">
                    <Button variant="contained" href="/requestpermission">Request Permission</Button>
                </Stack>
            </Box>
        </Container>
    )
}
export default HomeComponent;
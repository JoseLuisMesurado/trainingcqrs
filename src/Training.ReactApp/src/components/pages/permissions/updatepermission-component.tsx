import { Box, Button, Container, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, TextField } from "@mui/material";
import { ChangeEvent, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { getPermissionTypeRequestAction } from "../../../redux-store/reducer/permissiontype-slice";
import { PermissionInterfaceDto } from "../../../interfaces/dto/permission-interface.dto";
import PermissionHttpService from "../../../http-services/permission-httpservice";
import { getPermissionRequestAction } from "../../../redux-store/reducer/permission-slice";

export function UpdatePermissionComponent() {
    
    const dispatch = useDispatch()
    useEffect(() => {
        dispatch(getPermissionRequestAction())
        dispatch(getPermissionTypeRequestAction())
        // Safe to add dispatch to the dependencies array
    }, [dispatch])

    const { pathname } = useLocation();
    const permisionId = parseInt(pathname.replace("/updatepermission/", ""));
    const permissionState = useSelector((state: any) => state.permission);
    const permissionTypeState = useSelector((state: any) => state.permissiontype)
    const currentPermission = permissionState.permissions?.find((item:any) => item.id === permisionId);
    
    const [permissionId] = useState(currentPermission.id);
    const [permissionTypeId,setPermissionTypeId] = useState(currentPermission.permissionTypeId);
    const [firstName,setFirstName] = useState(currentPermission.employeeFirstName);
    const [lastName,setLastName] = useState(currentPermission.employeeLastName);
    
    const handleChange = (event: SelectChangeEvent) => {
        setPermissionTypeId(event.target.value)
    }; 

    const handleFirstName = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setFirstName(event.target.value);
    const handleLastName = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setLastName(event.target.value);
    const handleSaveClick = () => {

        const udaptePermission : PermissionInterfaceDto = {
            id: Number(permissionId),
            employeeFirstName:firstName,
            employeeLastName:lastName,
            permissionTypeId: Number(permissionTypeId),
        };
        PermissionHttpService.updatePermission(udaptePermission).then((response)=>{
            if(response.status=== 200){
                setPermissionTypeId("");
                setFirstName("");
                setLastName("");
                dispatch(getPermissionRequestAction());
            }
        });
    };
    
   
    

    return (
        <Container>
            <h1> UPDATE PERMISSION FOR EMPLOYEES</h1>
            <Box sx={{ minWidth: 120 }}>
                <FormControl fullWidth margin="normal">
                    <InputLabel id="demo-simple-select-label">Permission Type</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={permissionTypeId}
                        label="text"
                        onChange={handleChange}
                    >
                        { permissionTypeState.permissiontypes.map((item:any) => 
                            <MenuItem key={"selectitem"+ item.id} value={item.id}>{item.name}</MenuItem>   
                            )}
                    </Select>
                </FormControl>
            </Box>
            <Box
                component="form"
                sx={{
                    maxWidth: '100%',
                }}
                noValidate
                autoComplete="off"
            >
                <TextField fullWidth label="First Name" id="employee-firstname" margin="normal" value={firstName} onChange={handleFirstName} />
                <TextField fullWidth label="Last Name" id="employee-lasttname" margin="normal" value={lastName} onChange={handleLastName}/>
            </Box>
            <Stack spacing={2} direction="row" margin={2} justifyContent="flex-end">
                <Button variant="outlined" href="/home">Cancel</Button>
                <Button variant="contained" onClick={handleSaveClick}>Save </Button>
            </Stack>
        </Container>
    )
}

export default UpdatePermissionComponent;
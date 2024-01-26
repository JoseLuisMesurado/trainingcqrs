import {
    Box,
    Button,
    Container,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    SelectChangeEvent,
    Stack,
    TextField
} from "@mui/material";
import { ChangeEvent, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getPermissionTypeRequestAction } from "../../../redux-store/reducer/permissiontype-slice";
import { PermissionInterfaceDto } from "../../../interfaces/dto/permission-interface.dto";
import PermissionHttpService from "../../../http-services/permission-httpservice";
import { getPermissionRequestAction } from "../../../redux-store/reducer/permission-slice";

export const AddPermissionComponent = () => {
    const permissionTypeState = useSelector((state: any) => state.permissiontype);
    const dispatch = useDispatch()
    useEffect(() => {
        dispatch(getPermissionTypeRequestAction())
        // Safe to add dispatch to the dependencies array
    }, [dispatch])

    const [permissionTypeId,setPermissionTypeId] = useState('');
    const [firstName,setFirstName] = useState('');
    const [lastName,setLastName] = useState('');

    const handleChange = (event: SelectChangeEvent) => {
        setPermissionTypeId(event.target.value)
    }; 

    const handleFirstName = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setFirstName(event.target.value);
    const handleLastName = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setLastName(event.target.value);
    const handleSaveClick = () => {

        const addPermission : PermissionInterfaceDto = {
            id:0,
            employeeFirstName:firstName,
            employeeLastName:lastName,
            permissionTypeId: Number(permissionTypeId),
        };
        PermissionHttpService.addPermission(addPermission).then((response)=>{
            if(response.status=== 201){
                setPermissionTypeId("");
                setFirstName("");
                setLastName("");
                dispatch(getPermissionRequestAction());
            }
        });
    };
    
    return (
        <Container>
            <h1> REQUEST PERMISSION FOR EMPLOYEES</h1>
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
export default AddPermissionComponent;
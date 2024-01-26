import { AppBar, Toolbar, Typography } from "@mui/material";

export function HeaderComponent() {
    return (
        <AppBar component="nav" color="primary">
            <Toolbar>
                <Typography>Header Training</Typography>
            </Toolbar>
        </AppBar>
    )
}

export default HeaderComponent;
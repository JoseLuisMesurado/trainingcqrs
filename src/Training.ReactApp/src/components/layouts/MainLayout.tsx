import { Box, Link, Toolbar, Typography } from "@mui/material";
import FooterComponent from "../common/FooterComponent";
import HeaderComponent from "../common/HeaderComponent";

function Copyright() {
    return (
      <Typography variant="body2" color="white" align="center">
        <Link  color="inherit" href="https://www.linkedin.com/in/jos%C3%A9-luis-mesurado-44a76859/" target="blank">
          JOSE LUIS MESURADO - linkedin
        </Link>{' '}
      </Typography>
    );
  }
export function MainLayout({ children }: any) {
    return (<>
        <header>
            <HeaderComponent />
        </header>
        <Box component="main" sx={{ paddingTop: 2 }}>
        <Toolbar />
            {children}
        </Box>
        <footer>
            <FooterComponent><Copyright /></FooterComponent>
        </footer>
    </>)
}

export default MainLayout;
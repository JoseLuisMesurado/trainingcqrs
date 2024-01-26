import { Box, useTheme } from "@mui/material";
import { PropsWithChildren } from "react";

export const FooterComponent = ({ children }: PropsWithChildren<unknown>) => {
    const theme = useTheme();
        if (!children) {
        return null;
    }
    return (
        <Box
          sx={{
            background: theme.palette.primary.main,
            color: theme.palette.secondary.dark,
            padding: theme.spacing(2),
            textAlign: 'center',
            bottom:0,
          }}
        >
          { children }
        </Box>
      );
}

export default FooterComponent;


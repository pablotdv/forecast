import * as React from 'react';
import { AppRouterCacheProvider } from '@mui/material-nextjs/v14-appRouter';
import { ThemeProvider } from '@mui/material/styles';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import DashboardIcon from '@mui/icons-material/Dashboard';
import theme from '@/theme';

export const metadata = {
  title: 'Weather Forecast',
  description: 'App to show weather forecast',
};

export default function RootLayout(props: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body>
        <AppRouterCacheProvider options={{ enableCssLayer: true }}>
          <ThemeProvider theme={theme}>
            <CssBaseline />
            <AppBar position="fixed" sx={{ zIndex: 2000 }}>
              <Toolbar sx={{ backgroundColor: 'background.paper' }}>
                <DashboardIcon sx={{ color: '#444', mr: 2, transform: 'translateY(-2px)' }} />
                <Typography variant="h6" noWrap component="div" color="black">
                  Weather Forecast
                </Typography>
              </Toolbar>
            </AppBar>
            <Box
              component="main"
              sx={{
                flexGrow: 1,
                bgcolor: 'background.default',                
                mt: ['48px', '56px', '64px'],
                p: 3,
              }}
            >
              {props.children}
            </Box>
          </ThemeProvider>
        </AppRouterCacheProvider>
      </body>
    </html>
  );
}

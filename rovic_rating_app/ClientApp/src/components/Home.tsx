import { Navigate, Route, Routes} from "react-router";
import * as React from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import Link from '@mui/material/Link';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { ListItemButton, ListItemIcon, ListItemText, colors } from "@mui/material";
import Start from "./Start";
import { BrowserRouter } from "react-router-dom";
import DashboardIcon from '@mui/icons-material/Dashboard';
import SignIn from "./SingIn";
import LocalMoviesIcon from '@mui/icons-material/LocalMovies';
import MovieFilterIcon from '@mui/icons-material/MovieFilter';
import Movie from "./Movie";
import Album from "./Album";
import LibraryMusicIcon from '@mui/icons-material/LibraryMusic';
import Tag from "./Tag";
import BookmarkIcon from '@mui/icons-material/Bookmark';
import RateMovie from "./RateMovie";
import RateAlbum from "./RateAlbum";
import LibraryAddIcon from '@mui/icons-material/LibraryAdd';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import Panel from "./Panel";

const isAuthorized = localStorage.getItem('token') ? true : false;
const role = localStorage.getItem('role');

const drawerWidth: number = 240;

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    '& .MuiDrawer-paper': {
      position: 'relative',
      whiteSpace: 'nowrap',
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
      boxSizing: 'border-box',
      ...(!open && {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(9),
        },
      }),
    },
  }),
);

const defaultTheme = createTheme({
  palette: {
      background: {
        default: colors.grey[300]
      },
      text: {
        primary: colors.grey[900]
      },
      primary: {
          main: colors.grey[900]
      },
      secondary: {
          main: colors.grey[900]
      }
  }
});

export default function Home() {
  const [open, setOpen] = React.useState(true);
  const [page, setPage] = React.useState("Start");
  const toggleDrawer = () => {
    setOpen(!open);
  };

  if(isAuthorized === false) {
    return <Navigate to="/signin" />
  }

  return (
    <ThemeProvider theme={defaultTheme}>
      <Box sx={{ display: 'flex' }}>
        <CssBaseline />
        <AppBar position="absolute" open={open}>
          <Toolbar
            sx={{
              pr: '24px', // keep right padding when drawer closed
            }}
          >
            <IconButton
              edge="start"
              color="inherit"
              aria-label="open drawer"
              onClick={toggleDrawer}
              sx={{
                marginRight: '36px',
                ...(open && { display: 'none' }),
              }}
            >
              <MenuIcon />
            </IconButton>
            <Typography
              component="h1"
              variant="h5"
              color="inherit"
              align="center"
              noWrap
              sx={{ flexGrow: 1 }}
            >
              ROVIC
            </Typography>
          </Toolbar>
        </AppBar>
        <Drawer variant="permanent" open={open} PaperProps={{ sx: {
          backgroundColor: colors.grey[500],
          color: colors.grey[900]
        }}}>
          <Toolbar
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'flex-end',
              px: [1],
              backgroundColor: colors.grey[500]
            }}
          >
            <IconButton onClick={toggleDrawer}>
              <ChevronLeftIcon />
            </IconButton>
          </Toolbar>
          <Divider />
          <List component="nav">
            <ListItemButton onClick={ (event) => {
              setPage("Start")
            }}>
            <ListItemIcon>
              <DashboardIcon />
            </ListItemIcon>
              <ListItemText primary="Home" />
            </ListItemButton>

            <ListItemButton onClick={ (event) => {
              setPage("Movies")
            }}>
            <ListItemIcon>
              <LocalMoviesIcon />
            </ListItemIcon>
              <ListItemText primary="Your movies" />
            </ListItemButton>

            <ListItemButton onClick={ (event) => {
              setPage("AddMovie")
            }}>
            <ListItemIcon>
              <MovieFilterIcon />
            </ListItemIcon>
              <ListItemText primary="Rate a movie" />
            </ListItemButton>

            <ListItemButton onClick={ (event) => {
              setPage("Albums")
            }}>
            <ListItemIcon>
              <LibraryMusicIcon />
            </ListItemIcon>
              <ListItemText primary="Your albums" />
            </ListItemButton>

            <ListItemButton onClick={ (event) => {
              setPage("AddAlbum")
            }}>
            <ListItemIcon>
              <LibraryAddIcon />
            </ListItemIcon>
              <ListItemText primary="Rate a album" />
            </ListItemButton>

            <ListItemButton onClick={ (event) => {
              setPage("Tags")
            }}>
            <ListItemIcon>
              <BookmarkIcon />
            </ListItemIcon>
              <ListItemText primary="Your tags" />
            </ListItemButton>

            <Divider sx={{ my: 1 }} />

            <ListItemButton onClick={ (event) => {
              setPage("Panel")
            }} sx={{
              visibility: (role === "Administrator") ? "visible" : "hidden"
            }}>
            <ListItemIcon>
              <ManageAccountsIcon />
            </ListItemIcon>
              <ListItemText primary="Panel" />
            </ListItemButton>

          </List>
        </Drawer>
        <Box
          component="main"
          sx={{
            backgroundColor: (theme) =>
              theme.palette.mode === 'light'
                ? theme.palette.grey[300]
                : theme.palette.grey[800],
            flexGrow: 1,
            height: '100vh',
            overflow: 'auto',
          }}
        >
          <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                {(page === "Start") ? <Start /> : (page === "Movies") ? <Movie /> :
                (page === "Albums") ? <Album /> : (page === "Tags") ? <Tag /> : 
                (page === "AddMovie") ? <RateMovie /> : (page === "AddAlbum") ? <RateAlbum /> :
                (page === "Panel") ? <Panel /> : <Start />}
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

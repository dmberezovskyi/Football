import clsx from 'clsx';
import Logo from 'src/components/Logo';
import React, {  } from 'react';
import useOffSetTop from 'src/hooks/useOffSetTop';
import { NavLink as RouterLink } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import {
  Box,
  AppBar,
  Toolbar,
  Container,
} from '@material-ui/core';
import { useNavigate } from 'react-router-dom'
import { AppRoutes } from '../../routes/paths';
import {useTranslation} from 'react-i18next'
import { useSnackbar } from 'notistack';
import { Icon } from '@iconify/react';
import closeFill from '@iconify-icons/eva/close-fill';
import { MIconButton, MButton } from 'src/components/@material-extend';
import userManager from 'src/services/auth/userManager'


const APP_BAR_MOBILE = 64;
const APP_BAR_DESKTOP = 96;

const useStyles = makeStyles(theme => ({
  root: {
    boxShadow: 'none'
  },
  toolbar: {
    height: APP_BAR_MOBILE,
    transition: theme.transitions.create(['height', 'background-color'], {
      easing: theme.transitions.easing.easeInOut,
      duration: theme.transitions.duration.shorter
    }),
    [theme.breakpoints.up('md')]: {
      height: APP_BAR_DESKTOP
    }
  },
  toolbarContainer: {
    lineHeight: 0,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between'
  },
  toolbarShadow: {
    left: 0,
    right: 0,
    bottom: 0,
    height: 24,
    zIndex: -1,
    content: "''",
    margin: 'auto',
    borderRadius: '50%',
    position: 'absolute',
    width: `calc(100% - 48px)`,
    boxShadow: theme.customShadows.z8
  },
  onScroll: {
    '& $toolbar': {
      backgroundColor: theme.palette.background.default
    },
    [theme.breakpoints.up('md')]: {
      '& $toolbar': { height: APP_BAR_DESKTOP - 20 }
    }
  },
  signInButton: {
    marginRight: '10px'
  },
  notification: {
    marginTop: '80px'
  }
}));


function LandingNavbar() {
  const classes = useStyles();
  const offset = useOffSetTop(100);
  const navigate = useNavigate();
  const { t } = useTranslation();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();

  return (
    <AppBar
      color="transparent"
      className={clsx(classes.root, { [classes.onScroll]: offset })}
    >
      <Toolbar disableGutters className={classes.toolbar}>
        <Container maxWidth="lg" className={classes.toolbarContainer}>
          <RouterLink to="/">
            <Logo />
          </RouterLink>
          <Box sx={{ flexGrow: 1 }} />


          <MButton variant="contained" color="inherit" 
          className={classes.signInButton} 
          onClick={async () => {
            try{
              await userManager.signinRedirect();
            }
            catch(error){
              console.log(error);
              enqueueSnackbar(t("labels.oops", "Oops! Something went wrong"), {
                variant: 'error',
                preventDuplicate: true,
                className: classes.notification,
                action: key => (
                  <MIconButton size="small" onClick={() => closeSnackbar(key)}>
                    <Icon icon={closeFill} />
                  </MIconButton>
                )
              });
            }
          }}>
            {t('labels.signIn', "Sign In")}
          </MButton>
          <MButton 
          variant="contained" 
          color="primary"
          onClick={() => navigate(AppRoutes.auth.register)}
          >
            {t('labels.signUp', "Sign Up")}
          </MButton>

        </Container>
      </Toolbar>
      {offset && <span className={classes.toolbarShadow} />}
    </AppBar>
  );
}

export default LandingNavbar;

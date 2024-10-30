import React, {FunctionComponent, useEffect} from "react";
import { useNavigate } from 'react-router-dom';
import useAuth from 'src/hooks/useAuth';
import { useSnackbar } from 'notistack';
import { MIconButton } from 'src/components/@material-extend';
import { Icon } from '@iconify/react';
import closeFill from '@iconify-icons/eva/close-fill';
import { AppRoutes } from '../../routes/paths'
import LoadingScreen from '../LoadingScreen'
import userManager from 'src/services/auth/userManager'

const LogoutComponent: FunctionComponent = () => {
    const navigate = useNavigate();
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();
    const { logout } = useAuth();
    const IconButton = MIconButton as any;

    useEffect(() => {
        const Logout = async () => {
            try {
                await userManager.signoutRedirectCallback();
                const logoutId = getParameter("logoutId");

                if(logoutId !== null)
                  logout(logoutId);
            }
            catch(err) {
                enqueueSnackbar('Sorry, something went wrong', {
                    variant: 'error',
                    action: key => (
                      <IconButton size="small" onClick={() => closeSnackbar(key)}>
                        <Icon icon={closeFill} />
                      </IconButton>
                    )
                  });
            }
            finally {
                navigate(AppRoutes.landing);
            }
        }

        Logout();
    }, []);

    const getParameter = (paramName: string): string | null => {
        let params = new URLSearchParams(window.location.search);
        return params.get(paramName);
      }
    return <LoadingScreen className={undefined}/>
}

export default LogoutComponent;

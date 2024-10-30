import React, {FunctionComponent, useEffect} from "react";
import { useNavigate } from 'react-router-dom';
import { useSnackbar } from 'notistack';
import { MIconButton } from 'src/components/@material-extend';
import { Icon } from '@iconify/react';
import closeFill from '@iconify-icons/eva/close-fill';
import { AppRoutes } from '../../routes/paths'
import LoadingScreen from '../LoadingScreen'
import useAuth from 'src/hooks/useAuth'

const CallbackComponent: FunctionComponent = () => {
    const navigate = useNavigate();
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();
    const IconButton = MIconButton as any;
    const { callback } = useAuth();

    useEffect(() => {
        const Callback = async () => {
            try {
                await callback();
                navigate(AppRoutes.user.account);
            }
            catch(err) {
                navigate(AppRoutes.landing);
                enqueueSnackbar('Sorry, something went wrong', {
                    variant: 'error',
                    action: key => (
                      <IconButton size="small" onClick={() => closeSnackbar(key)}>
                        <Icon icon={closeFill} />
                      </IconButton>
                    )
                  });
            }
        }

        Callback();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    
    return <LoadingScreen className={undefined}/>
}

export default CallbackComponent;

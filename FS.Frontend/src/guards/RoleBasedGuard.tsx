import React, { FunctionComponent } from 'react'
import { Container, Alert, AlertTitle } from '@material-ui/core';
import useAuth from 'src/hooks/useAuth'
import { UserRole } from 'src/types'
import { useTranslation } from 'react-i18next'
import LoadingScreen from 'src/components/LoadingScreen'

// ----------------------------------------------------------------------

interface RoleBasedGuardProps {
  children: JSX.Element,
  accessibleRoles: UserRole[]
}

const useCurrentRole = (): UserRole => {
  const { userInfo } = useAuth();

  return userInfo?.role as UserRole;
};

const RoleBasedGuard: FunctionComponent<RoleBasedGuardProps> = ({accessibleRoles, children}) => {
  const currentRole = useCurrentRole();
  const { t } = useTranslation();

  if(currentRole === undefined)
    return <LoadingScreen />
    
  if (!accessibleRoles.includes(currentRole)) {
    return (
      <Container>
        <Alert severity="error">
          <AlertTitle>{t('error.accessDenied')}</AlertTitle>
          {t('error.messageYouDontHavePermissions')}
        </Alert>
      </Container>
    );
  }

  return <>{children}</>;
}

export default RoleBasedGuard;
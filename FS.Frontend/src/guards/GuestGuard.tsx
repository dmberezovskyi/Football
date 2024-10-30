import React, { FunctionComponent } from 'react'
import { Navigate } from 'react-router-dom';
// hooks
import useAuth from '../hooks/useAuth';
// routes
import { AppRoutes } from '../routes/paths';

interface GuestGuardProps {
  children: JSX.Element
}

 const GuestGuard: FunctionComponent<GuestGuardProps> = ({ children }) => {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return <Navigate to={AppRoutes.dashboard} />;
  }

  return <>{children}</>;
}

export default GuestGuard;
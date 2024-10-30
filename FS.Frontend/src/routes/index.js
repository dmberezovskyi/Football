import { AppRoutes as paths } from './paths';
import LoadingScreen from 'src/components/LoadingScreen';
import { Navigate, useRoutes } from 'react-router-dom';
import { Suspense, lazy} from 'react';
import GuestGuard from 'src/guards/GuestGuard';
import LogoOnlyLayout from '../layouts/LogoOnlyLayout';
import LandingLayout from '../layouts/landing'
import DashboardLayout from 'src/layouts/dashboard'
import AuthGuard from 'src/guards/AuthGuard';
import RoleBasedGuard from 'src/guards/RoleBasedGuard';
import { SidebarConfigType, UserRole } from 'src/types'
// ----------------------------------------------------------------------


const Loadable = (Component) => (props) => {
  return (
    <Suspense fallback={<LoadingScreen />}>
      <Component {...props} />
    </Suspense>
  );
};

export default function Router() {
  return useRoutes([
    {
      path: 'auth',
      children: [
        {
          path: 'login',
          element: (
            <GuestGuard>
              <Login />
            </GuestGuard>
          )
        },
        {
          path: 'register',
          element: (
            <GuestGuard>
              <Register />
            </GuestGuard>
          )
        },
        { path: 'callback', element: <Callback /> },
        { path: 'logout', element: <Logout /> },
        { path: 'reset-password', element: <ResetPassword /> },
        { path: 'verify', element: <Verify /> },
        { path: 'auth', element: <Navigate to={paths.auth.login} />}
      ]
    },
    //App routes
    {
      path: 'user',
      element: (
        <AuthGuard>
          <DashboardLayout/>
        </AuthGuard>
      ),
      children: [
        { path: '/', element: <Navigate to='/user/account' replace/> },
        { path: 'account', element: <Account />}
      ]
    },
    {
      path: 'dashboard',
      element: (
        <AuthGuard>
          <DashboardLayout/>
        </AuthGuard>
      ),
      children: [
        { path: '/', element: <Dashboard />}
      ]
    },
    //Main routes
    {
      path: '*',
      element: <LogoOnlyLayout />,
      children: [
        { path: 'coming-soon', element: <ComingSoon /> },
        { path: 'maintenance', element: <Maintenance /> },
        { path: '500', element: <Page500 /> },
        { path: '404', element: <NotFound /> },
        { path: '*', element: <Navigate to="/404" replace /> }
      ]
    },
    {
      path: 'admin',
      element: (
        <RoleBasedGuard accessibleRoles={[UserRole.Admin]}>
          <DashboardLayout sidebarConfigType={SidebarConfigType.Admin}/>
        </RoleBasedGuard>
      ),
      children: [
        { path: 'dashboard', element: <AdminDashboard /> },
        { path: 'users', element: <Users /> }
      ]
    },
    {
      path: '/',
      element: <LandingLayout />,
      children: [
        { path: '/', element: <Landing />}
      ]
    },
    { path: '*', element: <Navigate to="/404" replace /> }
  ]);
}

//Auth
const Login = Loadable(lazy(() => import('src/pages/auth/Login')));
const Register = Loadable(lazy(() => import('src/pages/auth/Register')));
const Callback = Loadable(lazy(() => import('src/components/Auth/Callback')));
const Logout = Loadable(lazy(() => import('src/components/Auth/Logout')));
const ResetPassword = Loadable(lazy(() => import('src/pages/auth/ResetPassword')));
const Verify = Loadable(lazy(() => import('src/pages/auth/VerifyCode')));

//Error pages
const NotFound = Loadable(lazy(() => import('src/pages/Page404')));
const Page500 = Loadable(lazy(() => import('src/pages/Page500')));
const ComingSoon = Loadable(lazy(() => import('src/pages/ComingSoonView')));
const Maintenance = Loadable(lazy(() => import('src/pages/MaintenanceView')));

//Landing
const Landing = Loadable(lazy(() => import('src/pages/LandingPage')));

const Account = Loadable(lazy(() => import('src/pages/user/AccountView')));
const Dashboard = Loadable(lazy(() => import('src/pages/MainDashboard')));

//Admin
const AdminDashboard = Loadable(lazy(() => import('src/pages/admin/AdminDashboard')));
const Users = Loadable(lazy(() => import('src/pages/admin/UserList')));
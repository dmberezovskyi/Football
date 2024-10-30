// ----------------------------------------------------------------------

function path(root: string, sublink: string): string {
  return `${root}${sublink}`;
}

const ROOTS = {
  auth: '/auth',
  admin: '/admin'
};

export const AppRoutes = {
  landing: '/',
  auth: {
    root: ROOTS.auth,
    login: path(ROOTS.auth, '/login'),
    register: path(ROOTS.auth, '/register'),
    callback: path(ROOTS.auth, '/callback'),
    logout: path(ROOTS.auth, '/logout'),
    resetPassword: path(ROOTS.auth, '/reset-password'),
    verify: path(ROOTS.auth, '/verify')
  },
  user: {
    account: '/user/account'
  },
  dashboard: '/dashboard',
  admin: {
    root: ROOTS.admin,
    dashboard: path(ROOTS.admin, '/dashboard'),
    users: path(ROOTS.admin, '/users')
  },
  comingSoon: '/coming-soon',
  maintenance: '/maintenance'
};


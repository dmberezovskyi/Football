// components
import React from 'react';
import SvgIconStyle from '../../components/SvgIconStyle';
import { AppRoutes } from 'src/routes/paths';
import { SidebarConfigType } from 'src/types'

// ----------------------------------------------------------------------

const getIcon = (name: string) => (
  <SvgIconStyle src={`/static/icons/navbar/${name}.svg`} sx={{ width: '100%', height: '100%' }} />
);

const ICONS = {
  blog: getIcon('ic_blog'),
  cart: getIcon('ic_cart'),
  chat: getIcon('ic_chat'),
  mail: getIcon('ic_mail'),
  user: getIcon('ic_user'),
  calendar: getIcon('ic_calendar'),
  ecommerce: getIcon('ic_ecommerce'),
  analytics: getIcon('ic_analytics'),
  dashboard: getIcon('ic_dashboard'),
  kanban: getIcon('ic_kanban')
};

const generalSidebarConfig = [
  {
      subheader: 'general',
      items: [
          {
              title: 'dashboard',
              icon: ICONS.dashboard,
              path: AppRoutes.dashboard
          }
      ]
  }
];

const adminSidebarConfig = [
  {
      subheader: 'administration',
      items: [
          {
              title: 'Dashboard',
              icon: ICONS.dashboard,
              path: AppRoutes.admin.dashboard
          },
          {
              title: 'User management',
              icon: ICONS.user,
              path: AppRoutes.admin.users
          }
      ]
  }
];

const getSidebarConfig = (sidebarConfigType: SidebarConfigType) => {
  switch(sidebarConfigType)
  {
    case SidebarConfigType.Admin:
      return adminSidebarConfig;
  }

  return generalSidebarConfig;
}

export default getSidebarConfig;

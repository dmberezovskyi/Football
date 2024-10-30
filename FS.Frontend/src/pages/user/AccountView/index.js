import General from './General';
import { Icon } from '@iconify/react';
import Page from '../../../components/Page';
import ChangePassword from './ChangePassword';
import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import roundVpnKey from '@iconify-icons/ic/round-vpn-key';
import HeaderDashboard from 'src/components/HeaderDashboard';
import roundAccountBox from '@iconify-icons/ic/round-account-box';
import { makeStyles } from '@material-ui/core/styles';
import { Container, Tab, Box, Tabs } from '@material-ui/core';
import { useTranslation } from 'react-i18next'
import { getUserProfile } from 'src/redux/slices/user'
import useAuth from 'src/hooks/useAuth'
// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {},
  tabBar: {
    marginBottom: theme.spacing(5)
  }
}));

// ----------------------------------------------------------------------

function AccountView() {
  const dispatch = useDispatch();
  const {t} = useTranslation();
  const classes = useStyles();
  const [currentTab, setCurrentTab] = useState('general');
  const { userId} = useAuth();

  useEffect(() => {
    dispatch(getUserProfile(userId))
  }, [dispatch, userId]);

  const ACCOUNT_TABS = [
    {
      value: "general",
      label: ` ${t("labels.accountTabs.general")}`,
      icon: <Icon icon={roundAccountBox} width={20} height={20} />,
      component: <General />
    },
    {
      value: 'change_password',
      label: ` ${t("labels.accountTabs.changePassword")}`,
      icon: <Icon icon={roundVpnKey} width={20} height={20} />,
      component: <ChangePassword />
    }
  ];

  const handleChangeTab = (event, newValue) => {
    setCurrentTab(newValue);
  };
  return (
    <Page title="Account Settings" className={classes.root}>
      <Container>
        <HeaderDashboard
          heading={t("labels.headerDashboard.accountHeading")}
          links={[
            { name: t("labels.headerDashboard.dashboard"), href: "#" },
            { name: t("labels.headerDashboard.accountSettings") }
          ]}
        />

        <Tabs
          value={currentTab}
          scrollButtons="auto"
          variant="scrollable"
          allowScrollButtonsMobile
          onChange={handleChangeTab}
          className={classes.tabBar}
        >
          {ACCOUNT_TABS.map(tab => (
            <Tab
              disableRipple
              key={tab.value}
              label={tab.label}
              icon={tab.icon}
              value={tab.value}
            />
          ))}
        </Tabs>

        {ACCOUNT_TABS.map(tab => {
          const isMatched = tab.value === currentTab;
          return isMatched && <Box key={tab.value}>{tab.component}</Box>;
        })}
      </Container>
    </Page>
  );
}

export default AccountView;

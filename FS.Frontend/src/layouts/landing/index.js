import React from 'react';
import LandingNavbar from './LandingNavbar';
import { Outlet } from 'react-router-dom';
// ----------------------------------------------------------------------


function LandingLayout() {
  return (
    <>
      <LandingNavbar />

      <div>
        <Outlet />
      </div>
    </>
  );
}

export default LandingLayout;

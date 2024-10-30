import React, { FunctionComponent } from 'react';
import NotistackProvider from './components/NotistackProvider'
import ThemeConfig from './theme';
import RtlLayout from './components/RtlLayout';
import Router from 'src/routes';
import useAuth from 'src/hooks/useAuth'
import LoadingScreen from './components/LoadingScreen';


const App: FunctionComponent = () => {
  const { isInitialized } = useAuth();

  return (
    <ThemeConfig>
      <RtlLayout>
        <NotistackProvider>
          {isInitialized ? <Router /> : <LoadingScreen />}
        </NotistackProvider>
      </RtlLayout>
    </ThemeConfig>
  );
}

export default App;

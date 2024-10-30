import 'lazysizes';
import 'src/_api_';
import 'src/utils/i18n';
import 'src/utils/highlight';
import 'intersection-observer';
import 'simplebar/src/simplebar.css';
import 'mapbox-gl/dist/mapbox-gl.css';
import 'slick-carousel/slick/slick.css';
import 'react-image-lightbox/style.css';
import 'react-quill/dist/quill.snow.css';
import 'slick-carousel/slick/slick-theme.css';
import 'lazysizes/plugins/attrchange/ls.attrchange';
import 'lazysizes/plugins/object-fit/ls.object-fit';
import 'lazysizes/plugins/parent-fit/ls.parent-fit';
import 'react-draft-wysiwyg/dist/react-draft-wysiwyg.css';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import * as serviceWorker from './sWorker';
import { HelmetProvider } from 'react-helmet-async'
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/lib/integration/react';
import LocalizationProvider from '@material-ui/lab/LocalizationProvider';
import LoadingScreen from './components/LoadingScreen';
import { BrowserRouter } from 'react-router-dom';
import AdapterDateFns from '@material-ui/lab/AdapterDateFns';
import { store, persistor } from './redux/store';
import { AuthProvider } from 'src/contexts/JWTContext';

const Loading = <LoadingScreen className={undefined} />;

ReactDOM.render(
    <HelmetProvider>
        <Provider store={store}>
            <PersistGate loading={Loading} persistor={persistor}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <BrowserRouter>
                        <AuthProvider>
                            <App />
                        </AuthProvider>
                    </BrowserRouter>
                </LocalizationProvider>
            </PersistGate>
        </Provider>
    </HelmetProvider>
    , document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();

import { combineReducers } from 'redux';
import storage from 'redux-persist/lib/storage';
import darkModeReducer from './slices/dark-mode';
import userReducer from './slices/user';
import notificationsReducer from './slices/notifications';
import settingsReducer from './slices/settings';
import adminReducer from './slices/admin';
import { PersistConfig } from 'redux-persist/es/types';

// ----------------------------------------------------------------------

const rootPersistConfig: PersistConfig<any> = {
  key: 'root',
  storage: storage,
  version: 1,
  whitelist: ['settings'],
  transforms: []
};


const rootReducer = combineReducers({
  theme: darkModeReducer,
  user: userReducer,
  notifications: notificationsReducer,
  settings: settingsReducer,
  admin: adminReducer
});

export { rootPersistConfig, rootReducer };

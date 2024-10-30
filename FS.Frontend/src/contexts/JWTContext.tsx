
import React, { createContext, useEffect, useReducer } from 'react';
// utils
import axios from '../utils/axios';
import { QueryResponse, UserRole, QueryPart, User, UserInfo } from 'src/types';
import userManager from 'src/services/auth/userManager'
import { User as OpenIdUser} from 'oidc-client';

export type AuthState = {
  error: any,
  isAuthenticated: boolean | null,
  openIdUser: OpenIdUser | null
  role: UserRole | null,
  userId: string | null;
  email: string | null | undefined;
  isInitialized: boolean;
  userVersion: number | null;
  userInfo: UserInfo | null;

  login(email: string, password: string, returnUrl: string): Promise<LoginResponse>;
  register(data: RegistrationData): Promise<void>;
  logout(logoutId: string): Promise<void>;
  loadUserInfo(): Promise<void>;
  callback(): Promise<void>;
  resetPassword(): Promise<void>;
}

interface ActionPayload<T> {
  type: string;
  payload: T;
}
interface LoginResponse {
  redirectUrl: string;
}

export interface RegistrationData {
  firstName: string;
  lastName: string;
  email: string,
  password: string;
  birthDate: Date;
  phone: string;
  role: UserRole
}

const initialState: AuthState = {
  isAuthenticated: null,
  openIdUser: null,
  error: null,
  role: null,
  email: null,
  isInitialized: false,
  userInfo: null,
  userVersion: null,
  userId: null,

  login:(email: string, password: string, returnUrl: string) => Promise.resolve({} as LoginResponse),
  register: (data: RegistrationData) => Promise.resolve(),
  logout:(logoutId: string) => Promise.resolve(),
  loadUserInfo: () => Promise.resolve(),
  callback: () => Promise.resolve(),
  resetPassword: () => Promise.resolve()
};

type InitializePayload = {
  isAuthenticated: boolean,
  user: User | null
}

type UserInfoPayload = {
  userInfo: UserInfo;
  userVersion: number;
  userId: string;
}

interface LoginResponse {
  redirectUrl: string;
}

const ACTIONS = {
  INITIALIZE: "INITIALIZE",
  SET_USER_INFO: "SET_USER_INFO",
  LOGIN: "LOGIN",
  LOGOUT: "LOGOUT",
  REGISTER: "REGISTER",
}

const handlers = {

  [ACTIONS.INITIALIZE]: (state: AuthState, action: ActionPayload<InitializePayload>) => {
    const { isAuthenticated, user } = action.payload;

    return {
      ...state,
      isAuthenticated,
      isInitialized: true,
      user
    };
  },
  [ACTIONS.LOGIN]: (state: AuthState, action: ActionPayload<{user: User}>) => {
    const { user } = action.payload;

    if(!user || !user.profile) return;

    const { id } = user.profile as any;

    return {
      ...state,
      isAuthenticated: true,
      user,
      userId: id,
    };
  },
  [ACTIONS.LOGOUT]: (state: AuthState) => ({
    ...state,
    isAuthenticated: false,
    user: null
  }),
  [ACTIONS.REGISTER]: (state: AuthState, action: ActionPayload<any>) => {
    const { user } = action.payload;

    return {
      ...state,
      isAuthenticated: true,
      user
    };
  },
  [ACTIONS.SET_USER_INFO]: (state: AuthState, action: ActionPayload<UserInfoPayload>) => {
    const { userInfo, userId, userVersion } = action.payload;
    const { role, email } = userInfo;

    return {
      ...state,
      userInfo,
      userId,
      userVersion,
      email,
      role
    }
  }
} as any;

const reducer = (state: AuthState, action: ActionPayload<any>) => 
  (handlers[action.type] ? handlers[action.type](state, action) : state);

const AuthContext = createContext({
  ...initialState
});


const setAuthHeader = (accessToken: string | null) => {
  if (accessToken) {
    axios.defaults.headers.common.Authorization = `Bearer ${accessToken}`;
  } else {
    delete axios.defaults.headers.common.Authorization;
  }
};

const AuthProvider = ({ children }: {children: JSX.Element}) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  const loadUserInfo = async (userId: string) => {
    try {
      const responce = await axios.get<QueryResponse<User>>(`/api/users`, {
        params: {
          parts: [QueryPart.UserInfo],
          ids: [userId]
        }
      });

      const { userInfo, version, id } = responce.data.items[0];

      dispatch({
        type: ACTIONS.SET_USER_INFO,
        payload: {
          userInfo,
          userId: id,
          userVersion: version
        } as UserInfoPayload
      });
    } catch (error) {

    }
  };

  useEffect(() => {
    const initialize = async () => {
      try {
        const user = await userManager.getUser();

        if (user && !user.expired) {
          setAuthHeader(user.access_token);

          dispatch({
            type: ACTIONS.INITIALIZE,
            payload: {
              isAuthenticated: true,
              user
            }
          });

          const { id } = user.profile;
          loadUserInfo(id);
        }
        else {
          dispatch({
            type: ACTIONS.INITIALIZE,
            payload: {
              isAuthenticated: false,
              user: null
            }
          });
        }

      } catch (err) {
        console.error(err);
        dispatch({
          type: ACTIONS.INITIALIZE,
          payload: {
            isAuthenticated: false,
            user: null
          }
        });
      }
    };

    initialize();
  }, []);

  const login = async (email: string, password: string, returnUrl: string): Promise<LoginResponse> => {
    const response = await axios.post<LoginResponse>(`/api/auth/login`,
      {
        email,
        password,
        returnUrl
      }
    );

    return response.data;
  };

  const register = async (data: RegistrationData) => {
    await axios.post(`/api/registrations`, {
      ...data
    });
  };

  const logout = async (logoutId: string) => {
    
    try {
      await axios.post(`/api/auth/logout`, null, {
        params: {
          logoutId
        }
      });

      dispatch({ 
        type: ACTIONS.LOGOUT,
        payload: null 
      });
    }
    catch (error) {
      //TODO Add error handling
    }
    finally {
      setAuthHeader(null);
    }
  };


  const callback = async (): Promise<void> => {
    const user = await userManager.signinRedirectCallback();
    if (!user || (Object.keys(user).length === 0 && user.constructor === Object)) return;

    dispatch({
      type: ACTIONS.LOGIN,
      payload: {
        user
      }
    });

    setAuthHeader(user.access_token);
    
    const { id } = user.profile;
    loadUserInfo(id);

    userManager.clearStaleState();
  };

  const resetPassword = () => { };

  return (
    <AuthContext.Provider
      value={{
        ...state,
        login,
        logout,
        register,
        resetPassword,
        callback
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export { AuthContext, AuthProvider };

import { QueryResponse, Error, UserSnippet, UserProfile} from 'src/types';
import { createSlice, PayloadAction, Dispatch } from '@reduxjs/toolkit';
import { QueryPart, User, UserInfo } from 'src/types';
import axios from "src/utils/axios"
import ValidationError from 'src/infrastructure/errors/ValidationError'

// ----------------------------------------------------------------------
type UserState = {
  id: string | undefined,
  version: number | undefined,
  userInfo: UserInfo,
  userProfile: UserProfile,

  error: any,
  isLoading: boolean
}

interface UserActionPayload<T> {
  version: number;
  id: string;

  userActionPayload: T;
}


const initialState: UserState = {
  id: undefined,
  version: undefined,
  userInfo: {} as UserInfo,
  userProfile: {} as UserProfile,

  error: false,
  isLoading: false,
};

const slice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    hasError(state: UserState, action) {
      state.error = action.payload;
      state.isLoading = false;
    },

    startUserLoading(state: UserState){
      state.isLoading = true;
    },

    setUserProfile(state: UserState, action: PayloadAction<UserActionPayload<UserProfile>>) {
      setUserMeta(state, action);

      state.userProfile = action.payload.userActionPayload;
      state.isLoading = false;
    },

    setUserInfo(state: UserState, action: PayloadAction<UserActionPayload<UserInfo>>){
      setUserMeta(state, action);

      state.userInfo = action.payload.userActionPayload;
    }
  }
});

const setUserMeta = (state: UserState, action: PayloadAction<UserActionPayload<any>>) => {
  state.id = action.payload.id;
  state.version = action.payload.version;
}


// Reducer
export default slice.reducer;

export const getUserProfile = (userId: string) => {
  return async (dispatch: Dispatch<any>) => {
    try {
      const responce = await axios.get<QueryResponse<User>>(`/api/users`, {
        params: {
          parts: [QueryPart.Profile],
          ids: [userId]
        }
      });

      const { profile, version, id } = responce.data.items[0];

      dispatch(slice.actions.setUserProfile(
        {userActionPayload: profile, version, id}
      ))
    } catch (error) {
      dispatch(slice.actions.hasError(error))
    }
  }
}

export const getUserInfo = (userId: string) => {
  return async (dispatch: Dispatch<any>) => {
    try {
      const responce = await axios.get<QueryResponse<User>>(`/api/users`, {
        params: {
          parts: [QueryPart.UserInfo],
          ids: [userId]
        }
      });

      const { userInfo, version, id } = responce.data.items[0];

      dispatch(slice.actions.setUserInfo(
        {userActionPayload: userInfo, version, id})
        )
    } catch (error) {
      dispatch(slice.actions.hasError(error))
    }
  }
}

interface UpdateUserRequest {
  id: string;
  version: number;

  profile: UserProfile
}

export const updateUserProfile = (updateUserRequest: UpdateUserRequest) => {
  return async (dispatch: Dispatch<any>) => {
    try {
      await axios.put<User>(`/api/users/${updateUserRequest.id}`, {
        ...updateUserRequest
      });

    } catch (error) {
      let errors = error.response.data.errors;
      
      throw new ValidationError(errors)
    }
  }
}

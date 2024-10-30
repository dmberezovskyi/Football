import { QueryPart } from './../../types';
import { createSlice, PayloadAction, Dispatch } from '@reduxjs/toolkit';
import { User } from 'src/types';
import axios from "src/utils/axios"

type AdminState = {
    error: any,
    users: User[],
    totalUsers: number
  }
  
  const initialState: AdminState = {
    error: false,
    users: [],
    totalUsers: 0
  };
  
  const slice = createSlice({
  name: 'admin',
    initialState,
    reducers: {

      hasError(state: AdminState, action) {
        state.error = action.payload;
      },
  
      setUsers(state: AdminState, action: PayloadAction<UsersResponse>) {
        state.users = action.payload.items;
        state.totalUsers = action.payload.total;
      }
    }
  });

// Reducer
export default slice.reducer;

export interface UsersResponse {
  items: User[],
  total: number
};

export const getUsers = (take: number, skip: number, parts: QueryPart[]) => {
    return async (dispatch: Dispatch<any>) => {
      try {
        const responce = await axios.get<UsersResponse>('/api/users',
        {
          params: { take, skip, parts }
        });
        dispatch(slice.actions.setUsers(responce.data))
      } catch (error) {
        dispatch(slice.actions.hasError(error))
      }
    }
  }
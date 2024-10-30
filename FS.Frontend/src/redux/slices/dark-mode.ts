import { createSlice } from '@reduxjs/toolkit';

// ----------------------------------------------------------------------

interface DarkModeState {
  darkMode: boolean
}

const initialState: DarkModeState = {
  darkMode: false
};

const slice = createSlice({
  name: 'darkMode',
  initialState,
  reducers: {
    toggleTheme(state) {
      state.darkMode = !state.darkMode;
    }
  }
});

// Reducer
export default slice.reducer;

// Actions
export const { toggleTheme } = slice.actions;

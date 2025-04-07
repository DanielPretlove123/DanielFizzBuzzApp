import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { GameSession, GameSessionDetails } from '../api/types';

interface GameSessionState {
  currentSession: GameSession | null;
  sessionDetails: GameSessionDetails | null;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
}

const initialState: GameSessionState = {
  currentSession: null,
  sessionDetails: null,
  status: 'idle',
  error: null,
};

export const gameSessionSlice = createSlice({
  name: 'gameSession',
  initialState,
  reducers: {
    setCurrentSession: (state, action: PayloadAction<GameSession>) => {
      state.currentSession = action.payload;
    },
    setSessionDetails: (state, action: PayloadAction<GameSessionDetails>) => {
      state.sessionDetails = action.payload;
    },
    clearSession: (state) => {
      state.currentSession = null;
      state.sessionDetails = null;
    },
    setLoading: (state) => {
      state.status = 'loading';
      state.error = null;
    },
    setSuccess: (state) => {
      state.status = 'succeeded';
    },
    setError: (state, action: PayloadAction<string>) => {
      state.status = 'failed';
      state.error = action.payload;
    },
  },
});

export const {
  setCurrentSession,
  setSessionDetails,
  clearSession,
  setLoading,
  setSuccess,
  setError,
} = gameSessionSlice.actions;

export default gameSessionSlice.reducer;
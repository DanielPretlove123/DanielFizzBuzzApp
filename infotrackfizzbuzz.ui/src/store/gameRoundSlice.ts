import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { GameChallenge, SubmitAnswerResponse, EndGameResponse } from '../api/types';

interface GameRoundState {
  currentChallenge: GameChallenge | null;
  lastAnswer: SubmitAnswerResponse | null;
  gameResults: EndGameResponse | null;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
}

const initialState: GameRoundState = {
  currentChallenge: null,
  lastAnswer: null,
  gameResults: null,
  status: 'idle',
  error: null,
};

export const gameRoundSlice = createSlice({
  name: 'gameRound',
  initialState,
  reducers: {
    setCurrentChallenge: (state, action: PayloadAction<GameChallenge>) => {
      state.currentChallenge = action.payload;
    },
    setLastAnswer: (state, action: PayloadAction<SubmitAnswerResponse>) => {
      state.lastAnswer = action.payload;
    },
    setGameResults: (state, action: PayloadAction<EndGameResponse>) => {
      state.gameResults = action.payload;
    },
    clearGameRound: (state) => {
      state.currentChallenge = null;
      state.lastAnswer = null;
      state.gameResults = null;
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
  setCurrentChallenge,
  setLastAnswer,
  setGameResults,
  clearGameRound,
  setLoading,
  setSuccess,
  setError,
} = gameRoundSlice.actions;

export default gameRoundSlice.reducer;
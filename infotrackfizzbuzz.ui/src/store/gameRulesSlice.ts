import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { GameRule } from '../api/types';

interface GameRulesState {
  rules: GameRule[];
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
}

const initialState: GameRulesState = {
  rules: [],
  status: 'idle',
  error: null,
};

export const gameRulesSlice = createSlice({
  name: 'gameRules',
  initialState,
  reducers: {
    setRules: (state, action: PayloadAction<GameRule[]>) => {
      state.rules = action.payload;
    },
    addRule: (state, action: PayloadAction<GameRule>) => {
      state.rules.push(action.payload);
    },
    updateRule: (state, action: PayloadAction<GameRule>) => {
      const index = state.rules.findIndex(rule => rule.id === action.payload.id);
      if (index !== -1) {
        state.rules[index] = action.payload;
      }
    },
    removeRule: (state, action: PayloadAction<number>) => {
      state.rules = state.rules.filter(rule => rule.id !== action.payload);
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
  setRules,
  addRule,
  updateRule,
  removeRule,
  setLoading,
  setSuccess,
  setError,
} = gameRulesSlice.actions;

export default gameRulesSlice.reducer;
import { configureStore } from '@reduxjs/toolkit';
import gameRulesReducer from './gameRulesSlice';
import gameSessionReducer from './gameSessionSlice';
import gameRoundReducer from './gameRoundSlice';

export const store = configureStore({
  reducer: {
    gameRules: gameRulesReducer,
    gameSession: gameSessionReducer,
    gameRound: gameRoundReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
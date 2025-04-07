// src/hooks/useGameSession.ts
import { useMutation, useQuery } from 'react-query';
import { useDispatch } from 'react-redux';
import { startGameSession, getGameSessionDetails } from '../api/gameSessions';
import {
  setCurrentSession,
  setSessionDetails,
  setLoading,
  setSuccess,
  setError
} from '../store/gameSessionSlice';

export const useGameSession = (sessionId?: string) => {
  const dispatch = useDispatch();
 
  const startSession = useMutation(startGameSession, {
    onMutate: () => {
      dispatch(setLoading());
    },
    onSuccess: (data) => {
      dispatch(setCurrentSession(data));
      dispatch(setSuccess());
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to start game session'));
    },
  });

  const sessionDetails = useQuery(
    ['sessionDetails', sessionId],
    () => {
      if (!sessionId) {
        throw new Error('Session ID is required');
      }
      return getGameSessionDetails(sessionId);
    },
    {
      enabled: !!sessionId,
      onSuccess: (data) => {
        dispatch(setSessionDetails(data));
        dispatch(setSuccess());
      },
      onError: (error: any) => {
        dispatch(setError(error.message || 'Failed to fetch session details'));
      },
    }
  );

  return {
    startSession: startSession.mutate,
    sessionDetails,
    isStarting: startSession.isLoading,
    startError: startSession.error,
    sessionData: startSession.data
  };
};
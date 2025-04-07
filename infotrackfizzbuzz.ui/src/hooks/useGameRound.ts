import { useMutation, useQuery, useQueryClient } from 'react-query';
import { useDispatch } from 'react-redux';
import { getChallenge, submitAnswer, endGame } from '../api/gameRounds';
import {
  setCurrentChallenge,
  setLastAnswer,
  setGameResults,
  setLoading,
  setSuccess,
  setError
} from '../store/gameRoundSlice';
import { SubmitAnswerRequest, EndGameRequest } from '../api/types';
import { getOrGenerateUuid, isValidUuid } from '../utils/uuid';

export const useGameRound = (sessionId: string | null) => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  
  const validSessionId = sessionId ? getOrGenerateUuid(sessionId) : null;

  const { 
    data: challenge, 
    isLoading: challengeLoading, 
    refetch: refetchChallenge 
  } = useQuery(
    ['challenge', validSessionId],
    () => getChallenge(validSessionId!),
    {
      enabled: !!validSessionId,
      onSuccess: (data) => {
        dispatch(setCurrentChallenge(data));
        dispatch(setSuccess());
      },
      onError: (error: any) => {
        dispatch(setError(error.message || 'Failed to fetch challenge'));
      },
    }
  );

  const submitAnswerMutation = useMutation((request: SubmitAnswerRequest) => {
    // Ensure the sessionId in the request is valid
    return submitAnswer({
      ...request,
      sessionId: getOrGenerateUuid(request.sessionId)
    });
  }, {
    onMutate: () => {
      dispatch(setLoading());
    },
    onSuccess: (data) => {
      dispatch(setLastAnswer(data));
      dispatch(setSuccess());
      refetchChallenge();
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to submit answer'));
    },
  });

  const endGameMutation = useMutation((request: EndGameRequest) => {
    return endGame({
      ...request,
      sessionId: getOrGenerateUuid(request.sessionId)
    });
  }, {
    onMutate: () => {
      dispatch(setLoading());
    },
    onSuccess: (data) => {
      dispatch(setGameResults(data));
      dispatch(setSuccess());
      queryClient.invalidateQueries(['sessionDetails', validSessionId]);
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to end game'));
    },
  });

  return {
    challenge,
    challengeLoading,
    submitAnswer: submitAnswerMutation.mutate,
    submitAnswerResult: submitAnswerMutation.data,
    endGame: endGameMutation.mutate,
    isSubmitting: submitAnswerMutation.isLoading,
    isEnding: endGameMutation.isLoading,
    submitError: submitAnswerMutation.error,
    endError: endGameMutation.error,
  };
};
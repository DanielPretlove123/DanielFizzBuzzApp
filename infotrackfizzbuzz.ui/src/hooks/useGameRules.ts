import { useQuery, useMutation, useQueryClient } from 'react-query';
import { useDispatch } from 'react-redux';
import { getGameRules, createGameRule, updateGameRule, deleteGameRule } from '../api/gameRules';
import { 
  setRules, 
  addRule, 
  updateRule, 
  removeRule, 
  setLoading, 
  setSuccess, 
  setError 
} from '../store/gameRulesSlice';
import { CreateGameRuleRequest, UpdateGameRuleRequest } from '../api/types';

export const useGameRules = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();

  const { data, isLoading, isError, error } = useQuery('gameRules', getGameRules, {
    onSuccess: (data) => {
      dispatch(setRules(data));
      dispatch(setSuccess());
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to fetch game rules'));
    },
  });

  const createMutation = useMutation((rule: CreateGameRuleRequest) => createGameRule(rule), {
    onMutate: () => {
      dispatch(setLoading());
    },
    onSuccess: (newRule) => {
      dispatch(addRule(newRule));
      dispatch(setSuccess());
      queryClient.invalidateQueries('gameRules');
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to create game rule'));
    },
  });

  const updateMutation = useMutation((rule: UpdateGameRuleRequest) => updateGameRule(rule), {
    onMutate: () => {
      dispatch(setLoading());
    },
    onSuccess: (_, variables) => {
      dispatch(updateRule(variables as any));
      dispatch(setSuccess());
      queryClient.invalidateQueries('gameRules');
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to update game rule'));
    },
  });

  const deleteMutation = useMutation((id: number) => deleteGameRule(id), {
    onMutate: () => {
      dispatch(setLoading());
    },
    onSuccess: (_, id) => {
      dispatch(removeRule(id as number));
      dispatch(setSuccess());
      queryClient.invalidateQueries('gameRules');
    },
    onError: (error: any) => {
      dispatch(setError(error.message || 'Failed to delete game rule'));
    },
  });

  return {
    rules: data || [],
    isLoading,
    isError,
    error,
    createRule: createMutation.mutate,
    updateRule: updateMutation.mutate,
    deleteRule: deleteMutation.mutate,
  };
};
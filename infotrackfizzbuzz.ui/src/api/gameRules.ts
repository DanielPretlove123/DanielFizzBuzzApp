import axios from 'axios';
import { GameRule, CreateGameRuleRequest, UpdateGameRuleRequest } from './types';

const API_URL = 'https://localhost:5001/api/GameRules';


export const getGameRules = async (): Promise<GameRule[]> => {
  const response = await axios.get<GameRule[]>(API_URL);
  return response.data;
};


export const createGameRule = async (rule: CreateGameRuleRequest): Promise<GameRule> => {
  const response = await axios.post<GameRule>(API_URL, rule);
  return response.data;
};


export const updateGameRule = async (rule: UpdateGameRuleRequest): Promise<void> => {
  await axios.put(`${API_URL}/${rule.id}`, rule);
};

export const deleteGameRule = async (id: number): Promise<void> => {
  await axios.delete(`${API_URL}/${id}`);
};
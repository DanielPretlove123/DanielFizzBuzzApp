import { v4 as uuidv4 } from 'uuid';

export const generateUuid = (): string => {
  return uuidv4();
};

export const isValidUuid = (uuid: string): boolean => {
  if (!uuid) return false;
  
  const uuidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
  return uuidRegex.test(uuid);
};

export const getOrGenerateUuid = (uuid?: string): string => {
  if (isValidUuid(uuid || '')) {
    return uuid!;
  }
  return generateUuid();
};
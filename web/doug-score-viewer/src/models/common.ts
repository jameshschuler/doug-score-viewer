import { AppErrorType } from './enums/error';

export interface APIResponse<T> {
    data?: T;
    error?: AppError;
}

export interface AppError {
    message: string;
    errorType: AppErrorType
}
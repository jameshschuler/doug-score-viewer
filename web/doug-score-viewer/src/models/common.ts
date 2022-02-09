export interface APIResponse<T> {
    data?: T;
    error?: AppError;
}

export interface AppError {
    message: string;
}
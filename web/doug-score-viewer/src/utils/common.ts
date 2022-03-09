import { AppErrorType } from '../models/enums/error';

export function handleErrorResponse ( errorType: AppErrorType, message: string ) {
    return {
        error: {
            errorType,
            message
        }
    }
}
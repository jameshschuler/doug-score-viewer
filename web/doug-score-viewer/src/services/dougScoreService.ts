import { APIResponse } from '../models/common';
import { AppErrorType } from '../models/enums/error';
import { FeaturedDougScoresResponse, SearchDougScoresResponse } from '../models/response';

// TODO: implement caching for this response
export async function getFeaturedDougScores (): Promise<APIResponse<FeaturedDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/featured`;
        const response = await fetch( url );
        const data = ( await response.json() ) as APIResponse<FeaturedDougScoresResponse>;

        if ( data.data?.dougScores.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: "No featured DougScores were found."
                }
            }
        }

        return { data: data.data };
    } catch ( err ) {
        return {
            error: {
                errorType: AppErrorType.BadRequest,
                message: 'Unable to load featured DougScores. Please try again later!'
            }
        };
    }
}

// TODO: implement caching for this response
export async function getHighestDougScores (): Promise<APIResponse<SearchDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/search?sortBy=TotalDougScore`;
        const response = await fetch( url );
        const data = ( await response.json() ) as APIResponse<SearchDougScoresResponse>;

        if ( data.data?.dougScores.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: "No DougScores were found."
                }
            }
        }

        return { data: data.data };
    } catch ( err ) {
        return {
            error: {
                errorType: AppErrorType.BadRequest,
                message: 'Unable to load DougScores. Please try again later!'
            }
        };
    }
}
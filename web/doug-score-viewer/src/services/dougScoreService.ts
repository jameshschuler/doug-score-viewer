import { APIResponse } from '../models/common';
import { FeaturedDougScoresResponse, SearchDougScoresResponse } from '../models/response';

export async function getFeaturedDougScores (): Promise<APIResponse<FeaturedDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/featured`;
        const response = await fetch( url );
        const data = ( await response.json() ) as APIResponse<FeaturedDougScoresResponse>;

        return { data: data.data };
    } catch ( err ) {
        return {
            error: {
                message: 'Unable to load featured DougScores. Please try again later!'
            }
        };
    }
}

export async function getHighestDougScores (): Promise<APIResponse<SearchDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/search?sortBy=TotalDougScore`;
        const response = await fetch( url );
        const data = ( await response.json() ) as APIResponse<SearchDougScoresResponse>;

        return { data: data.data };
    } catch ( err ) {
        return {
            error: {
                message: 'Unable to load DougScores. Please try again later!'
            }
        };
    }
}


export interface AppError {
    message?: string;
}
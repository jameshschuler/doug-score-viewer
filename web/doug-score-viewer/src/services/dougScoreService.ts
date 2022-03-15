import { APIResponse } from '../models/common';
import { AppErrorType } from '../models/enums/error';
import { SearchDougScoreRequest } from '../models/request';
import { FeaturedDougScoresResponse, SearchDougScoresResponse } from '../models/response';
import { cacheResponse, getCachedResponse } from '../utils/cache';
import { handleErrorResponse } from '../utils/common';
import { isNullEmptyOrWhitespace } from '../utils/strings';

export async function getFeaturedDougScores (): Promise<APIResponse<FeaturedDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/featured`;

        let responseData = await getCachedResponse( 'getFeaturedDougScores', url );
        if ( responseData === null ) {
            const response = await fetch( url );
            await cacheResponse( 'getFeaturedDougScores', url, response );

            responseData = ( await response.json() ) as APIResponse<FeaturedDougScoresResponse>;
        }

        if ( responseData.data?.dougScores.length === 0 ) {
            return handleErrorResponse( AppErrorType.NotFound, "No featured DougScores were found." );
        }

        return { data: responseData.data };
    } catch ( err ) {
        return handleErrorResponse( AppErrorType.BadRequest, "Unable to load featured DougScores. Please try again later!" );
    }
}

// TODO: implement caching for this response
export async function getDougScores ( sortBy: string ): Promise<APIResponse<SearchDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/search?sortBy=${sortBy}`;
        const response = await fetch( url );
        const responseData = ( await response.json() ) as APIResponse<SearchDougScoresResponse>;

        if ( responseData.data?.dougScores.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: "No DougScores were found."
                }
            }
        }

        return { data: responseData.data };
    } catch ( err ) {
        return {
            error: {
                errorType: AppErrorType.BadRequest,
                message: 'Unable to load DougScores. Please try again later!'
            }
        };
    }
}

export async function searchDougScores ( { minYear, maxYear, make, model, originCountries }: SearchDougScoreRequest ): Promise<APIResponse<SearchDougScoresResponse>> {
    try {
        let url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/search?minYear=${minYear}&maxYear=${maxYear}`;

        url += !isNullEmptyOrWhitespace( make ) ? `&make=${make}` : '';
        url += !isNullEmptyOrWhitespace( model ) ? `&make=${model}` : '';

        if ( originCountries.length > 0 ) {
            url += `&originCountries=${originCountries.map( s => s )}`;
        }

        const response = await fetch( url );
        const responseData = ( await response.json() ) as APIResponse<SearchDougScoresResponse>;

        if ( responseData.data?.dougScores.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: "No DougScores were found matching the given search criteria."
                }
            }
        }

        return { data: responseData.data };
    } catch ( err ) {
        return {
            error: {
                errorType: AppErrorType.BadRequest,
                message: 'Unable to load DougScores. Please try again later!'
            }
        };
    }
}
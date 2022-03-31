import { messages } from '../constants';
import { APIResponse } from '../models/common';
import { AppErrorType } from '../models/enums/error';
import { FeaturedDougScoresResponse, SearchDougScoresResponse } from '../models/response';
import { SearchQuery } from '../models/searchQuery';
import { cacheResponse, Caches, getCachedResponse } from '../utils/cache';
import { handleErrorResponse } from '../utils/common';
import { isNullEmptyOrWhitespace } from '../utils/strings';

export async function getFeaturedDougScores (): Promise<APIResponse<FeaturedDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/featured`;

        let responseData = await getCachedResponse<FeaturedDougScoresResponse>( Caches.FeaturedDougScores, url );
        if ( responseData === null ) {
            const response = await fetch( url );
            responseData = ( await response.json() ) as APIResponse<FeaturedDougScoresResponse>;

            if ( responseData.error ) {
                await cacheResponse( Caches.FeaturedDougScores, url, response );
            }
        }

        if ( responseData.data?.dougScores.length === 0 ) {
            return handleErrorResponse( AppErrorType.NotFound, "No featured DougScores were found." );
        }

        return { data: responseData.data };
    } catch ( err ) {
        return handleErrorResponse( AppErrorType.BadRequest, "Unable to load featured DougScores. Please try again later!" );
    }
}

export async function getDougScores ( sortBy: Caches ): Promise<APIResponse<SearchDougScoresResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/search?sortBy=${sortBy}`;

        let responseData = await getCachedResponse<SearchDougScoresResponse>( sortBy, url );
        if ( responseData === null ) {
            const response = await fetch( url );
            responseData = ( await response.json() ) as APIResponse<SearchDougScoresResponse>;

            if ( responseData.error ) {
                await cacheResponse( sortBy, url, response );
            }
        }

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

async function searchDougScores ( { minYear, maxYear, make, model, originCountries, sortByOption }: SearchQuery ): Promise<APIResponse<SearchDougScoresResponse>> {
    try {
        let url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/dougscore/search?minYear=${minYear}&maxYear=${maxYear}&pageSize=24`;

        url += !isNullEmptyOrWhitespace( make ) ? `&make=${make}` : '';
        url += !isNullEmptyOrWhitespace( model ) ? `&model=${model}` : '';

        let originCountriesParam = originCountries.filter( ( c ) => c.selected ).map( ( c ) => c.name );
        if ( originCountriesParam.length > 0 ) {
            url += `&originCountries=${originCountriesParam.map( s => s )}`;
        }

        const [ sortBy, sortOrder ] = sortByOption.split( '|' );
        url += `&sortBy=${sortBy}&sortOrder=${sortOrder}`;

        const response = await fetch( url );
        const responseData = ( await response.json() ) as APIResponse<SearchDougScoresResponse>;

        if ( responseData.data?.dougScores.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: messages.noSearchResults
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

export default {
    searchDougScores
}
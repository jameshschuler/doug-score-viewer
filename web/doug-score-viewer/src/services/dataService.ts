import { APIResponse, Option } from '../models/common';
import { AppErrorType } from '../models/enums/error';
import { AvailableMake, AvailableMakesResponse, AvailableModelsResponse, OptionsResponse } from '../models/response';
import { handleErrorResponse } from '../utils/common';
import { isNullEmptyOrWhitespace } from '../utils/strings';

export async function getMakeOptions (): Promise<APIResponse<OptionsResponse>> {
    try {
        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/data/makes`;
        const response = await fetch( url );
        const responseData = ( await response.json() ) as APIResponse<AvailableMakesResponse>;

        if ( responseData.data?.makes.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: "No options were found."
                }
            }
        }

        const options = responseData.data?.makes.map( ( { count, name }: AvailableMake ) => {
            return {
                text: `${name} (${count})`,
                value: name
            } as Option
        } );

        return { data: { options } };
    } catch ( err ) {
        return {
            error: {
                errorType: AppErrorType.BadRequest,
                message: 'Unable to load options. Please try again later!'
            }
        };
    }
}

export async function getModelOptions ( make?: string ): Promise<APIResponse<OptionsResponse>> {
    try {
        if ( isNullEmptyOrWhitespace( make ) ) {
            return handleErrorResponse( AppErrorType.NotFound, 'No make was provided.' );
        }

        const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/data/models?make=${make}`;
        const response = await fetch( url );
        const responseData = ( await response.json() ) as APIResponse<AvailableModelsResponse>;

        if ( responseData.data?.models.length === 0 ) {
            return {
                error: {
                    errorType: AppErrorType.NotFound,
                    message: "No options were found."
                }
            }
        }

        const options = responseData.data?.models.map( ( name: string ) => {
            return {
                text: name,
                value: name
            } as Option
        } );

        return { data: { options } };
    } catch ( err ) {
        return {
            error: {
                errorType: AppErrorType.BadRequest,
                message: 'Unable to load options. Please try again later!'
            }
        };
    }
}
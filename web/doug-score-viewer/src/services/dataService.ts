import { APIResponse, Option } from '../models/common';
import { AppErrorType } from '../models/enums/error';
import { AvailableMake, AvailableMakesResponse, OptionsResponse } from '../models/response';

// TODO: cache this?
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

        const options = responseData.data?.makes.map( ( { name }: AvailableMake ) => {
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
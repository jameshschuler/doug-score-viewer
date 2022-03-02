export function getYearOptions (): Array<Option> {
    const currentYear = new Date().getUTCFullYear();
    const range = ( start: number, stop: number, step: number ): Option[] => {
        return Array.from( { length: ( stop - start ) / step + 1 }, ( _, i ) => {
            const year = start + ( i * step );
            return {
                text: year.toString(),
                value: year
            }
        } );
    };

    return range( currentYear, 1960, -1 );
}

interface Option {
    text: string;
    value: number;
}
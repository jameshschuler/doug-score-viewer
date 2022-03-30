import { Country, SelectableCountry } from '../models/country';
import { Countries } from './countries';
import { TotalDougScoreDesc } from './sortOptions';

export const initialSearchQuery = {
    make: "",
    model: "",
    minYear: "1960",
    maxYear: new Date().getUTCFullYear().toString(),
    originCountries: [
        ...( Countries.map( ( c: Country ) => {
            return {
                ...c,
                selected: true,
            };
        } ) as SelectableCountry[] ),
    ],
    sortByOption: TotalDougScoreDesc,
}
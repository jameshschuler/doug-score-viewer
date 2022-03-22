import { SelectableCountry } from './country';

export interface SearchQuery {
    minYear: string;
    maxYear: string;
    model?: string;
    make?: string;
    originCountries: SelectableCountry[];
    sortByOption: string;
}
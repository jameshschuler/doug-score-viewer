import { Countries } from '../constants/countries';
import { SortBy } from '../constants/sortOptions';
import { Country } from '../models/country';
import { SearchQuery } from '../models/searchQuery';
import { isNullEmptyOrWhitespace } from './strings';

export function getFlagIcon ( flagIconName: string ): string {
    const url = new URL( `../assets/flags/${flagIconName}.png`, import.meta.url ).href;
    return url;
}

export function getCountry ( countryName: string ): Country {
    const country = Countries.find( e => e.name === countryName );
    return country!;
}

export function getDougScoreBracket ( dougScore: number ): string {
    if ( dougScore >= 65 ) {
        return 'has-border-great';
    } else if ( dougScore > 50 ) {
        return 'has-border-okay';
    } else {
        return 'has-border-sad';
    }
}

export function getScoreBracket ( score: number ): string {
    let result;
    if ( score >= 30 ) {
        result = 'has-border-great';
    } else if ( score >= 20 ) {
        result = 'has-border-okay';
    } else {
        result = 'has-border-sad';
    }

    return result;
}

export function getUrlSearchParams ( query: SearchQuery | null ) {
    if ( query === null ) {
        return;
    }

    const { make, model, minYear, maxYear, sortByOption, originCountries } = query;
    let params = `?minYear=${minYear}&maxYear=${maxYear}`;
    params += !isNullEmptyOrWhitespace( make ) ? `&make=${make}` : '';
    params += !isNullEmptyOrWhitespace( model ) ? `&model=${model}` : '';
    params += !isNullEmptyOrWhitespace( sortByOption ) ? `&sortBy=${sortByOption}` : SortBy.TotalDougScoreDesc;

    const selectedCountries = originCountries.filter( c => c.selected );
    if ( selectedCountries.length > 0 ) {
        params += `&originCountries=${selectedCountries.map( s => s.name )}`;
    }

    return params;
}
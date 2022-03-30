import { LocationQuery } from 'vue-router';
import { store } from '.';
import { Countries } from '../constants/countries';
import { SortBy } from '../constants/sortOptions';
import { Country } from '../models/country';
import { SearchQuery } from '../models/searchQuery';
import { searchDougScores } from '../services/dougScoreService';
import { isNullEmptyOrWhitespace } from '../utils/strings';

export async function handleSearchFromUrl ( query: LocationQuery ) {
    if ( Object.keys( query ).length === 0 || store.searchResults !== null ) {
        return;
    }

    store.setLoading( true );

    const { make, model, minYear, maxYear, sortBy, originCountries } = query;
    const selectedCountryNames = !isNullEmptyOrWhitespace( originCountries ) ?
        originCountries!.toString().split( ',' ) : [];

    let countries = Countries.map( ( country: Country ) => {
        return {
            ...country,
            selected: selectedCountryNames.includes( country.name )
        };
    } )

    const searchQuery = {
        make: !isNullEmptyOrWhitespace( make ) ? make!.toString() : '',
        model: !isNullEmptyOrWhitespace( model ) ? model!.toString() : '',
        minYear: !isNullEmptyOrWhitespace( minYear ) ? minYear!.toString() : '1960',
        maxYear: !isNullEmptyOrWhitespace( maxYear ) ? maxYear!.toString() : new Date().getUTCFullYear().toString(),
        sortByOption: !isNullEmptyOrWhitespace( sortBy ) ? sortBy!.toString() : SortBy.TotalDougScoreDesc,
        originCountries: countries
    };
    const response = await searchDougScores( searchQuery );

    if ( !response.error && response.data ) {
        store.setSearchResults( response.data );
        store.setCurrentSearchQuery( searchQuery );
    }

    store.setLoading( false );
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
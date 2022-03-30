import { LocationQuery } from 'vue-router';
import { store } from '.';
import { Countries } from '../constants/countries';
import { TotalDougScoreDesc } from '../constants/sortOptions';
import { Country, SelectableCountry } from '../models/country';
import { searchDougScores } from '../services/dougScoreService';
import { isNullEmptyOrWhitespace } from '../utils/strings';

export async function handleSearchFromUrl ( query: LocationQuery ) {
    if ( Object.keys( query ).length === 0 ) {
        return;
    }

    store.setLoading( true );

    const { make, model, minYear, maxYear, sortBy, originCountries } = query;

    let countries = new Array<SelectableCountry>();
    if ( !isNullEmptyOrWhitespace( originCountries ) ) {
        const selectedCountryNames = originCountries!.toString().split( ',' );
        countries = Countries.map( ( country: Country ) => {
            return {
                ...country,
                selected: selectedCountryNames.includes( country.name )
            };
        } )
    }

    const searchQuery = {
        make: !isNullEmptyOrWhitespace( make ) ? make!.toString() : '',
        model: !isNullEmptyOrWhitespace( model ) ? model!.toString() : '',
        minYear: !isNullEmptyOrWhitespace( minYear ) ? minYear!.toString() : '1960',
        maxYear: !isNullEmptyOrWhitespace( maxYear ) ? maxYear!.toString() : new Date().getUTCFullYear().toString(),
        sortByOption: !isNullEmptyOrWhitespace( sortBy ) ? sortBy!.toString() : TotalDougScoreDesc,
        originCountries: countries
    };
    const response = await searchDougScores( searchQuery );

    if ( !response.error && response.data ) {
        store.setSearchResults( response.data );
        store.setCurrentSearchQuery( searchQuery );
    }

    store.setLoading( false );
}
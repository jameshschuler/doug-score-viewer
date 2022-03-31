import { reactive } from 'vue';
import { LocationQuery } from 'vue-router';
import { Countries } from '../constants';
import { SortBy } from '../constants/sortOptions';
import { AppError } from '../models/common';
import { Country, SelectableCountry } from '../models/country';
import { SearchDougScoresResponse } from '../models/response';
import { SearchQuery } from '../models/searchQuery';
import dougScoreService from '../services/dougScoreService';
import { isNullEmptyOrWhitespace } from '../utils/strings';

interface StoreState {
    currentSearchQuery: SearchQuery | null;
    error: AppError | null,
    isSearchDrawerOpen: boolean;
    loading: boolean;
    searchResults: SearchDougScoresResponse | null;
    currentCountries: SelectableCountry[];
    searching: boolean;
    getCurrentSortByOption: () => SortBy;
    handleSearchFromUrl: ( query: LocationQuery ) => Promise<void>;
    searchDougScores: ( query: SearchQuery ) => Promise<void>;
    setCurrentSearchQuery: ( searchQuery: SearchQuery ) => void;
    setError: ( appError: AppError ) => void;
    setLoading: ( value: boolean ) => void;
    setSearchResults: ( data?: SearchDougScoresResponse ) => void;
    toggleSearchDrawer: ( state?: boolean ) => void,
}

export const store = reactive<StoreState>( {
    error: null,
    isSearchDrawerOpen: false,
    loading: true,
    searchResults: null,
    currentCountries: [],
    currentSearchQuery: null,
    searching: false,
    getCurrentSortByOption (): SortBy {
        if ( !isNullEmptyOrWhitespace( this.currentSearchQuery?.sortByOption ) ) {
            return this.currentSearchQuery?.sortByOption as SortBy;
        }

        return SortBy.TotalDougScoreDesc;
    },
    async handleSearchFromUrl ( query: LocationQuery ) {
        if ( Object.keys( query ).length === 0 || this.searchResults !== null ) {
            return;
        }

        this.setLoading( true );

        const { make, model, minYear, maxYear, sortBy, originCountries } = query;
        const selectedCountryNames = !isNullEmptyOrWhitespace( originCountries ) ?
            originCountries!.toString().split( ',' ) : [];

        let countries = Countries.map( ( country: Country ) => {
            return {
                ...country,
                selected: selectedCountryNames.includes( country.name )
            };
        } );

        const searchQuery = {
            make: !isNullEmptyOrWhitespace( make ) ? make!.toString() : '',
            model: !isNullEmptyOrWhitespace( model ) ? model!.toString() : '',
            minYear: !isNullEmptyOrWhitespace( minYear ) ? minYear!.toString() : '1960',
            maxYear: !isNullEmptyOrWhitespace( maxYear ) ? maxYear!.toString() : new Date().getUTCFullYear().toString(),
            sortByOption: !isNullEmptyOrWhitespace( sortBy ) ? sortBy!.toString() : SortBy.TotalDougScoreDesc,
            originCountries: countries
        };
        const response = await dougScoreService.searchDougScores( searchQuery );

        if ( response.error ) {
            this.setError( response.error );
        } else {
            this.setSearchResults( response.data );
        }

        this.setCurrentSearchQuery( searchQuery );
        this.setLoading( false );
    },
    async searchDougScores ( query: SearchQuery ) {
        this.searching = true;

        const response = await dougScoreService.searchDougScores( query );
        this.toggleSearchDrawer( false );

        if ( response.error ) {
            this.setError( response.error );
        } else {
            this.setSearchResults( response.data );
        }

        this.setCurrentSearchQuery( query );
        this.searching = false;
    },
    setError ( appError: AppError ) {
        this.error = appError ?? null;
    },
    setLoading ( value: boolean ) {
        this.loading = value;
    },
    setCurrentSearchQuery ( searchQuery: SearchQuery ) {
        this.currentSearchQuery = searchQuery ?? null;
        if ( this.currentSearchQuery ) {
            this.currentCountries = this.currentSearchQuery.originCountries.filter( c => c.selected );
        }
    },
    toggleSearchDrawer ( state?: boolean ) {
        if ( state !== undefined ) {
            this.isSearchDrawerOpen = state;
        } else {
            this.isSearchDrawerOpen = !this.isSearchDrawerOpen;
        }
    },
    setSearchResults ( data?: SearchDougScoresResponse ) {
        this.searchResults = data ?? null;
    }
} );
import { reactive } from 'vue';
import { AppError } from './models/common';
import { SelectableCountry } from './models/country';
import { SearchDougScoresResponse } from './models/response';
import { SearchQuery } from './models/searchQuery';

interface StoreState {
    currentSearchQuery: SearchQuery | null;
    error: AppError | null,
    isSearchDrawerOpen: boolean;
    loading: boolean;
    searchResults: SearchDougScoresResponse | null;
    currentCountries: SelectableCountry[];
    setCurrentSearchQuery: ( searchQuery: SearchQuery ) => void;
    setError: ( appError: AppError ) => void;
    setLoading: ( value: boolean ) => void;
    setSearchResults: ( data?: SearchDougScoresResponse ) => void;
    toggleSearchDrawer: Function,
}

export const store = reactive<StoreState>( {
    error: null,
    isSearchDrawerOpen: false,
    loading: true,
    searchResults: null,
    currentCountries: [],
    currentSearchQuery: null,
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
    toggleSearchDrawer () {
        this.isSearchDrawerOpen = !this.isSearchDrawerOpen;
    },
    setSearchResults ( data?: SearchDougScoresResponse ) {
        this.searchResults = data ?? null;
    }
} );
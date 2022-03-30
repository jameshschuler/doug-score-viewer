import { reactive } from 'vue';
import { SortBy } from '../constants/sortOptions';
import { AppError } from '../models/common';
import { SelectableCountry } from '../models/country';
import { SearchDougScoresResponse } from '../models/response';
import { SearchQuery } from '../models/searchQuery';
import { isNullEmptyOrWhitespace } from '../utils/strings';

interface StoreState {
    currentSearchQuery: SearchQuery | null;
    error: AppError | null,
    isSearchDrawerOpen: boolean;
    loading: boolean;
    searchResults: SearchDougScoresResponse | null;
    currentCountries: SelectableCountry[];
    getCurrentSortByOption: () => SortBy;
    setCurrentSearchQuery: ( searchQuery: SearchQuery ) => void;
    setError: ( appError: AppError ) => void;
    setLoading: ( value: boolean ) => void;
    setSearchResults: ( data?: SearchDougScoresResponse ) => void;
    setSortByOption: ( sortByOption: SortBy ) => void;
    toggleSearchDrawer: Function,
}

export const store = reactive<StoreState>( {
    error: null,
    isSearchDrawerOpen: false,
    loading: true,
    searchResults: null,
    currentCountries: [],
    currentSearchQuery: null,
    getCurrentSortByOption (): SortBy {
        if ( !isNullEmptyOrWhitespace( store.currentSearchQuery?.sortByOption ) ) {
            return store.currentSearchQuery?.sortByOption as SortBy;
        }

        return SortBy.TotalDougScoreDesc;
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
    toggleSearchDrawer () {
        this.isSearchDrawerOpen = !this.isSearchDrawerOpen;
    },
    setSearchResults ( data?: SearchDougScoresResponse ) {
        this.searchResults = data ?? null;
    },
    setSortByOption ( sortByOption: SortBy ) {
        if ( this.currentSearchQuery !== null ) {
            this.currentSearchQuery.sortByOption = sortByOption;
        }
    }
} );
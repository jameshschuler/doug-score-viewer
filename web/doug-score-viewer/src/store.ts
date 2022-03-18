import { reactive } from 'vue';
import { AppError } from './models/common';
import { SearchDougScoresResponse } from './models/response';
import { SearchQuery } from './models/searchQuery';

interface StoreState {
    currentSearchQuery: SearchQuery | null;
    error: AppError | null,
    isSearchDrawerOpen: boolean;
    searchResults: SearchDougScoresResponse | null;
    setCurrentSearchQuery: ( searchQuery: SearchQuery ) => void;
    setError: ( appError: AppError ) => void;
    setSearchResults: ( data?: SearchDougScoresResponse ) => void;
    toggleSearchDrawer: Function,
}

export const store = reactive<StoreState>( {
    error: null,
    isSearchDrawerOpen: false,
    searchResults: null,
    currentSearchQuery: null,
    setError ( appError: AppError ) {
        this.error = appError ?? null;
    },
    setCurrentSearchQuery ( searchQuery: SearchQuery ) {
        this.currentSearchQuery = searchQuery ?? null;
    },
    toggleSearchDrawer () {
        this.isSearchDrawerOpen = !this.isSearchDrawerOpen;
    },
    setSearchResults ( data?: SearchDougScoresResponse ) {
        this.searchResults = data ?? null;
    }
} );
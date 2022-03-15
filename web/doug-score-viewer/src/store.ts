import { reactive } from 'vue';
import { SearchDougScoresResponse } from './models/response';

interface StoreState {
    isSearchDrawerOpen: boolean;
    searchResults: SearchDougScoresResponse | null,
    setSearchResults: ( data?: SearchDougScoresResponse ) => void;
    toggleSearchDrawer: Function,
}

export const store = reactive<StoreState>( {
    isSearchDrawerOpen: false,
    searchResults: null,
    toggleSearchDrawer () {
        this.isSearchDrawerOpen = !this.isSearchDrawerOpen;
    },
    setSearchResults ( data?: SearchDougScoresResponse ) {
        this.searchResults = data ?? null;
    }
} );
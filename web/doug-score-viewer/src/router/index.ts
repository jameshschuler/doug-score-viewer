import { createRouter, createWebHistory, RouteLocationNormalized, RouteRecordRaw } from "vue-router";
import { TotalDougScoreDesc } from '../constants/sortOptions';
import About from '../pages/About.vue';
import Error from '../pages/Error.vue';
import Landing from '../pages/Landing.vue';
import NotFound from '../pages/NotFound.vue';
import SearchResults from '../pages/SearchResults.vue';
import { searchDougScores } from '../services/dougScoreService';
import { store } from '../store';
import { isNullEmptyOrWhitespace } from '../utils/strings';

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'Landing',
        component: Landing
    },
    {
        path: '/about',
        name: 'About',
        component: About
    },
    {
        path: '/search/results',
        name: 'SearchResults',
        component: SearchResults,
        beforeEnter: async ( to: RouteLocationNormalized ) => {

            // TODO: move to handleSearchFromUrl(to);
            // TODO: validate at least one key is valid?
            const { make, model, minYear, maxYear, sortBy, originCountries } = to.query;
            if ( Object.keys( to.query ).length !== 0 ) {
                store.setLoading( true );
                const searchQuery = {
                    make: !isNullEmptyOrWhitespace( make ) ? make!.toString() : '',
                    model: !isNullEmptyOrWhitespace( model ) ? model!.toString() : '',
                    minYear: !isNullEmptyOrWhitespace( minYear ) ? minYear!.toString() : '1960',
                    maxYear: !isNullEmptyOrWhitespace( maxYear ) ? maxYear!.toString() : new Date().getUTCFullYear().toString(),
                    sortByOption: !isNullEmptyOrWhitespace( sortBy ) ? sortBy!.toString() : TotalDougScoreDesc,
                    originCountries: [] // TODO:
                };
                const response = await searchDougScores( searchQuery );

                if ( !response.error && response.data ) {
                    store.setSearchResults( response.data );
                    store.setCurrentSearchQuery( searchQuery );
                    store.setLoading( false );
                }
            }

            return true;
        },
    },
    {
        path: '/error',
        name: 'Error',
        component: Error
    },
    {
        path: '/:pathMatch(.*)*',
        name: 'NotFound',
        component: NotFound
    },
];

const router = createRouter( {
    history: createWebHistory(),
    routes
} );

export default router;
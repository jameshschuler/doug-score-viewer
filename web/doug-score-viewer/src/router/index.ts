import { createRouter, createWebHistory, RouteLocationNormalized, RouteRecordRaw } from "vue-router";
import About from '../pages/About.vue';
import Error from '../pages/Error.vue';
import Landing from '../pages/Landing.vue';
import NotFound from '../pages/NotFound.vue';
import SearchResults from '../pages/SearchResults.vue';
import { handleSearchFromUrl } from '../store/actions';

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
            await handleSearchFromUrl( to.query );

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
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import About from '../pages/About.vue';
import Landing from '../pages/Landing.vue';
import SearchResults from '../pages/SearchResults.vue';

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
        path: '/results',
        name: 'SearchResults',
        component: SearchResults
    },
];

const router = createRouter( {
    history: createWebHistory(),
    routes
} );

export default router;
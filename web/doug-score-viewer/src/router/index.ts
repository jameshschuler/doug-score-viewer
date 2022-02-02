import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import About from '../pages/About.vue';
import Landing from '../pages/Landing.vue';

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
];

const router = createRouter( {
    history: createWebHistory(),
    routes
} );

export default router;
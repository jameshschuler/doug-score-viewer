import { APIResponse } from '../models/common';
import { FeaturedDougScoresResponse } from '../models/response';

export async function cacheResponse ( cacheName: string, url: string, response: Response ): Promise<void> {
    if ( !isCacheSupported ) return;

    const cache = await caches.open( cacheName );

    await cache.put( url, response.clone() );

    const expiry = getExpiryDate();
    localStorage.setItem( cacheName, expiry );
}

export async function getCachedResponse ( cacheName: string, url: string ): Promise<APIResponse<FeaturedDougScoresResponse> | null> {
    if ( !isCacheSupported ) return null;

    const cache = await caches.open( cacheName );
    const cachedResponse = await cache.match( url );

    if ( cachedResponse && !isCacheExpired( cacheName ) ) {
        return await cachedResponse.json();
    }

    return null;
}

function getExpiryDate (): string {
    const expiry = new Date();
    const currentHour = expiry.getHours();

    if ( currentHour >= 4 ) {
        expiry.setDate( expiry.getDate() + 1 );
        expiry.setHours( 4, 0, 0 );
    } else {
        expiry.setHours( 4, 0, 0 );
    }

    return expiry.toLocaleString();
}

function isCacheSupported () {
    return 'caches' in window;
}

function isCacheExpired ( key: string ): boolean {
    const item = localStorage.getItem( key );

    if ( !item ) {
        return true;
    }

    if ( new Date() > new Date( item ) ) {
        return true;
    }

    return false;
}
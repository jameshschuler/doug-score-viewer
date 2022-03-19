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

// TODO: write tests for this
function getExpiryDate (): string {
    const expiry = new Date();
    if ( expiry.getUTCHours() > 0 && expiry.getUTCMinutes() > 5 ) {
        expiry.setUTCDate( expiry.getUTCDate() + 1 );
        expiry.setUTCHours( 0, 5, 0 );
    } else {
        expiry.setUTCDate( expiry.getUTCDate() );
        expiry.setUTCHours( 0, 5, 0 );
    }

    return expiry.toUTCString();
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
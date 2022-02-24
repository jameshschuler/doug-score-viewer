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
    const currentHour = expiry.getUTCHours();

    // If it's current past 10:05 AM UTC then set the expiry date to tomorrow at 10:05 AM UTC
    // else set the expiry to today at 10:05 AM UTC
    if ( currentHour >= 10 ) {
        expiry.setDate( expiry.getUTCDate() + 1 );
        expiry.setUTCHours( 2, 5, 0 );
    } else {
        expiry.setDate( expiry.getUTCDate() );
        expiry.setHours( 2, 5, 0 );
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
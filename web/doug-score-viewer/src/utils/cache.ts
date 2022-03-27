import { APIResponse } from '../models/common';

export enum Caches {
    FeaturedDougScores = 'FeaturedDougScores',
    TotalDougScore = 'TotalDougScore',
    WeekendScore = 'WeekendScore',
    DailyScore = 'DailyScore'
}

export async function cacheResponse ( cacheName: Caches, url: string, response: Response ): Promise<void> {
    if ( !isCacheSupported ) return;

    const cache = await caches.open( cacheName );

    await cache.put( url, response.clone() );

    let expiry = '';
    if ( cacheName === Caches.FeaturedDougScores ) {
        expiry = getFeaturedExpiryDate();
    } else {
        expiry = getExpiryDate( 7 );
    }

    localStorage.setItem( cacheName, expiry );
}

export async function getCachedResponse<T> ( cacheName: Caches, url: string ): Promise<APIResponse<T> | null> {
    if ( !isCacheSupported ) return null;

    const cache = await caches.open( cacheName );
    const cachedResponse = await cache.match( url );

    if ( cachedResponse && !isCacheExpired( cacheName ) ) {
        return await cachedResponse.json();
    }

    return null;
}

// TODO: write tests for this
function getFeaturedExpiryDate (): string {
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

function getExpiryDate ( numDays: number = 1 ): string {
    const expiry = new Date();
    expiry.setUTCDate( expiry.getUTCDate() + numDays );

    return expiry.toUTCString();
}

function isCacheSupported () {
    return 'caches' in window;
}

function isCacheExpired ( key: Caches ): boolean {
    const item = localStorage.getItem( key );

    if ( !item ) {
        return true;
    }

    if ( new Date() > new Date( item ) ) {
        return true;
    }

    return false;
}
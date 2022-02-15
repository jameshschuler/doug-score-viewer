// TODO: Call this method before fetch() actually runs? 
// TODO: Add service worker

export async function cacheResponse ( cacheName: string, url: string, response: Response ) {
    let isCacheSupported = 'caches' in window;
    if ( !isCacheSupported ) {
        console.error( 'Darn! Caches not available' );
        return;
    }
    const cache = await caches.open( cacheName );

    // TODO: we also want to automatically update the cache periodically 
    const cachedResponse = await cache.match( url );
    if ( cachedResponse ) {
        console.log( 'Found a cache to return!' )
        return await cachedResponse.json();
    } else {
        await cache.put( url, response.clone() );
        console.log( 'Request was cached?' );
    }
}
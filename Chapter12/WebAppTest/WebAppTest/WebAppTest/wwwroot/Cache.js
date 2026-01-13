export function clearDotNetResourcesCache() {
    if ('caches' in window) {
        caches.open('dotnet-resources-/').then(cache => {
            cache.keys().then(keys => {
                keys.forEach(request => cache.delete(request));
            });
        }).then(() => {
            console.log('dotnet-resources cache cleared.');
        });
    } else {
        console.error('Caching is not supported in this browser.');
    }
}
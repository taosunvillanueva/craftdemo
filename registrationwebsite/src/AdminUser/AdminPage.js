import React, { Suspense } from 'react';

const LazyAuthentication = React.lazy(() => import('./AdminLandingPage'));

const AdminPage = () => (
    <div>
        <Suspense fallback={<div>Loading...</div>}>
            <LazyAuthentication />
        </Suspense>
    </div>
);

export default AdminPage;
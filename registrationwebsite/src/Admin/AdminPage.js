import React, { Suspense } from 'react';

// const LazyAuthentication = React.lazy(() => import('./LazyAuthentication'));
const LazyAuthentication = React.lazy(() => import('../AdminUser/AdminLandingPage'));

const AdminPage = () => (
    <div>
        <Suspense fallback={<div>Loading...</div>}>
            <LazyAuthentication />
        </Suspense>
    </div>
);

export default AdminPage;
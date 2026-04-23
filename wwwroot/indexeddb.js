(function () {
    window.indexedb = {
        saveCart: function (dbName, storeName, version, items) {
            return new Promise((resolve, reject) => {
                const request = indexedDB.open(dbName, version);

                request.onerror = () => reject(request.error);
                request.onsuccess = () => {
                    const db = request.result;
                    const tx = db.transaction(storeName, 'readwrite');
                    const store = tx.objectStore(storeName);
                    store.clear();
                    items.forEach(item => store.add(item));
                    tx.oncomplete = () => resolve();
                    tx.onerror = () => reject(tx.error);
                };

                request.onupgradeneeded = (event) => {
                    const db = event.target.result;
                    if (!db.objectStoreNames.contains(storeName)) {
                        db.createObjectStore(storeName);
                    }
                    if (!db.objectStoreNames.contains('wallet')) {
                        db.createObjectStore('wallet');
                    }
                };
            });
        },

        loadCart: function (dbName, storeName, version) {
            return new Promise((resolve, reject) => {
                const request = indexedDB.open(dbName, version);

                request.onerror = () => reject(request.error);
                request.onsuccess = () => {
                    const db = request.result;
                    const tx = db.transaction(storeName, 'readonly');
                    const store = tx.objectStore(storeName);
                    const getAll = store.getAll();
                    getAll.onsuccess = () => resolve(getAll.result);
                    getAll.onerror = () => reject(getAll.error);
                };
            });
        },

        clearCart: function (dbName, storeName, version) {
            return new Promise((resolve, reject) => {
                const request = indexedDB.open(dbName, version);

                request.onerror = () => reject(request.error);
                request.onsuccess = () => {
                    const db = request.result;
                    const tx = db.transaction(storeName, 'readwrite');
                    const store = tx.objectStore(storeName);
                    store.clear();
                    tx.oncomplete = () => resolve();
                    tx.onerror = () => reject(tx.error);
                };
            });
        },

        saveWalletBalance: function (dbName, storeName, version, balance) {
            return new Promise((resolve, reject) => {
                const request = indexedDB.open(dbName, version);

                request.onerror = () => reject(request.error);
                request.onsuccess = () => {
                    const db = request.result;
                    const tx = db.transaction(storeName, 'readwrite');
                    const store = tx.objectStore(storeName);
                    store.put(balance, 'balance');
                    tx.oncomplete = () => resolve();
                    tx.onerror = () => reject(tx.error);
                };
            });
        },

        loadWalletBalance: function (dbName, storeName, version) {
            return new Promise((resolve, reject) => {
                const request = indexedDB.open(dbName, version);

                request.onerror = () => reject(request.error);
                request.onsuccess = () => {
                    const db = request.result;
                    const tx = db.transaction(storeName, 'readonly');
                    const store = tx.objectStore(storeName);
                    const get = store.get('balance');
                    get.onsuccess = () => resolve(get.result);
                    get.onerror = () => reject(get.error);
                };
            });
        }
    };
})();
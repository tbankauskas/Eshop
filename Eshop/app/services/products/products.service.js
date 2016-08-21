angular.
    module('services').
    factory('Products', ['$resource',
        function ($resource) {
            return $resource('api/products', {}, {
                query: {
                    method: 'GET',
                    isArray: true
                },
                filterProducts: {
                    method: 'POST',
                    isArray: true,
                    params: {filters: 'filters' }
                }
            });
        }
    ]).
    factory('ProductTypes', ['$resource',
        function ($resource) {
            return $resource('api/productTypes', {}, {
                query: {
                    method: 'GET',
                    isArray: true
                }
            });
        }
    ]);

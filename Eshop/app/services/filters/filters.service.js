angular.
    module('services').
    factory('Filters', ['$resource',
        function ($resource) {
            return $resource('api/filters', {}, {
                query: {
                    method: 'GET',
                    isArray: true
                },
                filter: {
                    method: 'POST',
                    isArray: true,
                    params: { productTypes: 'productTypes' }
                }

            });
        }
    ])
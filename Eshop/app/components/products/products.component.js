angular.
    module('products').
    component('products', {
        templateUrl: 'app/components/products/products.template.html',
        controller: function ProductsController($http) {

            this.getProductDetails = function (obj) {
                $http.post('/Index/GetProductDetails', { product: obj }).
                    then(function (obj) {
                        angular.element('#singleProduct').html(obj.data);

                        angular.element('.main-content .page').removeClass('visible');
                        angular.element('#singleProduct').addClass('visible');
                    });
            };
        },
        bindings: {
            itemsSource: '='
        }
    });
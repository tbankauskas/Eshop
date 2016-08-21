angular.
    module('filters').
    component('filters', {
        templateUrl: 'app/components/filters/filters.template.html',
        controller: ['ProductTypes', 'Filters', 'Products',
            function FiltersController(ProductTypes, Filters, Products) {
                self = this;
                self.price;
                this.productTypes = ProductTypes.query(function (data) {
                    self.isLoaded = true;
                });

                self.productTypesSelect = function (types) {
                    self.filters = Filters.filter(types)
                }

                self.clearFilters = function () {
                    self.productTypes.forEach(function (e) { e.isSelected = false; });
                    self.filters = [];
                    self.isLoading = true;
                    self.filterSource = Products.query(function (data) { self.isLoading = false; });
                }

                self.filter = function () {
                    self.isLoading = true;
                    var filters = { 'productTypes': self.productTypes, 'filters': self.filters, 'price': self.price }
                    self.filterSource = Products.filterProducts(filters, function (data) {
                        self.isLoading = false;
                    });
                }
            }
        ],
        bindings: {
            filterSource: '=',
            isLoading: "="
        }
    });

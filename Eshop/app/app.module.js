var app = angular.module('eshopApp', ['filters', 'products', 'services']);

app.controller('RootController', function (Products) {
    var self = this;
    this.isLoading = true;
    this.products = Products.query(function (data) {
        self.isLoading = false;
    });
});
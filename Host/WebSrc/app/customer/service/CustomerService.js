"use strict";
var CustomerService = (function () {
    function CustomerService($http, $q) {
        this.signUpApiPath = "api/customer/newaccount";
        this.httpService = $http;
        this.qService = $q;
    }
    CustomerService.prototype.addNewCustomer = function (customer) {
        var self = this;
        self.httpService.post(self.signUpApiPath, customer)
            .then(function (result) {
            alert(result);
        }, function (error) {
            alert(error);
        });
    };
    CustomerService.CustomerServiceFactory = function ($http, $q) {
        return new CustomerService($http, $q);
    };
    return CustomerService;
}());
exports.CustomerService = CustomerService;

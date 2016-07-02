"use strict";
var Customer_ts_1 = require("../model/Customer.ts");
var CustomerSignUpController = (function () {
    function CustomerSignUpController($scope, customerService) {
        var self = this;
        self.$scope = $scope;
        self.dataSvc = customerService;
        self.$scope.addVideo = function () {
            var customer = new Customer_ts_1.Customer();
            customer.CustomerName = $scope.CustomerName;
            customer.Email = $scope.Email;
            customer.Password = $scope.Password;
            self.dataSvc.addNewCustomer(customer);
        };
    }
    return CustomerSignUpController;
}());
exports.CustomerSignUpController = CustomerSignUpController;
CustomerSignUpController.$inject = ['$scope', 'customerService'];

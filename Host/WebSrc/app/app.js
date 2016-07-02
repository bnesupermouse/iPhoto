/// <reference path="./scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-route.d.ts" />
var Extensions;
(function (Extensions) {
    var Customer = (function () {
        function Customer() {
        }
        return Customer;
    }());
    Extensions.Customer = Customer;
})(Extensions || (Extensions = {}));
var OneStopCustomerApp;
(function (OneStopCustomerApp) {
    var Config = (function () {
        function Config($routeProvider) {
            $routeProvider
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .otherwise({ redirectTo: '/' });
        }
        return Config;
    }());
    OneStopCustomerApp.Config = Config;
    Config.$inject = ['$routeProvider'];
    var CustomerDataSvc = (function () {
        function CustomerDataSvc($http, $q) {
            this.techCtmApiPath = "api/customer/newaccount";
            this.httpService = $http;
            this.qService = $q;
        }
        CustomerDataSvc.prototype.addCustomer = function (customer) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.techCtmApiPath, customer)
                .then(function (result) {
                deferred.resolve();
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        CustomerDataSvc.CustomerDataSvcFactory = function ($http, $q) {
            return new CustomerDataSvc($http, $q);
        };
        return CustomerDataSvc;
    }());
    OneStopCustomerApp.CustomerDataSvc = CustomerDataSvc;
    var AddCustomerCtrl = (function () {
        function AddCustomerCtrl($scope, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$scope.addCustomer = function () {
                var ctm = new Extensions.Customer();
                ctm.CustomerName = self.$scope.CustomerName;
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.addCustomer(ctm).then(function () {
                    alert("Successful");
                });
            };
            self.init();
        }
        AddCustomerCtrl.prototype.init = function () {
            var self = this;
        };
        return AddCustomerCtrl;
    }());
    OneStopCustomerApp.AddCustomerCtrl = AddCustomerCtrl;
    AddCustomerCtrl.$inject = ['$scope', 'customerDataSvc'];
    var app = angular.module("webApp", ['ngRoute']);
    app.config(Config);
    app.factory('customerDataSvc', ['$http', '$q', CustomerDataSvc.CustomerDataSvcFactory]);
    app.controller('AddNewCustomerCtrl', AddCustomerCtrl);
})(OneStopCustomerApp || (OneStopCustomerApp = {}));

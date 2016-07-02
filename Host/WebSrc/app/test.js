var DataModels;
(function (DataModels) {
    var Customer = (function () {
        function Customer() {
        }
        return Customer;
    }());
    DataModels.Customer = Customer;
})(DataModels || (DataModels = {}));
/// <reference path="../model/Customer.ts" />
var Services;
(function (Services) {
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
    Services.CustomerService = CustomerService;
})(Services || (Services = {}));
/// <reference path="../service/CustomerService.ts" />
/// <reference path="../model/Customer.ts" />
var Controllers;
(function (Controllers) {
    var CustomerSignUpController = (function () {
        function CustomerSignUpController($scope, customerService) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = customerService;
            self.$scope.addVideo = function () {
                var customer = new DataModels.Customer();
                customer.CustomerName = $scope.CustomerName;
                customer.Email = $scope.Email;
                customer.Password = $scope.Password;
                self.dataSvc.addNewCustomer(customer);
            };
        }
        return CustomerSignUpController;
    }());
    Controllers.CustomerSignUpController = CustomerSignUpController;
    CustomerSignUpController.$inject = ['$scope', 'customerService'];
})(Controllers || (Controllers = {}));
var Controllers;
(function (Controllers) {
    var MainController = (function () {
        function MainController() {
        }
        return MainController;
    }());
    Controllers.MainController = MainController;
})(Controllers || (Controllers = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./MainController.ts" />
/// <reference path="./customer/controller/CustomerSignUpController.ts" />
/// <reference path="./customer/service/CustomerService.ts" />
/// <reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var webApp;
(function (webApp) {
    var Config = (function () {
        function Config($routeProvider) {
            $routeProvider
                .when("/", { templateUrl: "index.html", controller: "MainController" })
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "CustomerSignUpController" })
                .otherwise({ redirectTo: '/' });
        }
        return Config;
    }());
    webApp.Config = Config;
    Config.$inject = ['$routeProvider'];
    var app = angular.module("webApp", ['ngRoute']);
    app.config(Config);
    app.factory('CustomerService', ['$http', '$q', Services.CustomerService.CustomerServiceFactory]);
    app.controller('MainController', MainController);
    app.controller('CustomerSignUpController', Controllers.CustomerSignUpController);
    app.config(Config);
})(webApp || (webApp = {}));

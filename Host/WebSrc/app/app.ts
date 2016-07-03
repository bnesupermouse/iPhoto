/// <reference path="./all.ts" />
module OneStopCustomerApp {
    export class Config {
        constructor($routeProvider: ng.route.IRouteProvider) {
            $routeProvider
                .when("/", { controller: "IndexPageCtrl" })
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .when("/signin", { templateUrl: "customer/view/signin.html", controller: "CustomerSignOnCtrl" })
                .otherwise({ redirectTo: '/' });
        }
    }
    Config.$inject = ['$routeProvider'];
    Controllers.AddCustomerCtrl.$inject = ['$scope', 'customerDataSvc'];
    Controllers.SignOnCustomerCtrl.$inject = ['$scope', 'customerDataSvc'];
    Controllers.MainPageCtrl.$inject = ['$scope', 'mainPageDataSvc'];
    //test
    var app = angular.module("webApp", ['ngRoute']);
    app.config(Config);
    app.factory('customerDataSvc', ['$http', '$q', Services.CustomerDataSvc.CustomerDataSvcFactory]);
    app.factory('mainPageDataSvc', ['$http', '$q', Services.MainPageDataSvc.MainPageDataSvcFactory]);
    app.controller('AddNewCustomerCtrl', Controllers.AddCustomerCtrl);
    app.controller('CustomerSignOnCtrl', Controllers.SignOnCustomerCtrl);
    app.controller('IndexPageCtrl', Controllers.MainPageCtrl);
}
/// <reference path="./scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-route.d.ts" />
module Extensions {
    export class Customer {
        public CustomerId: number;
        public Email: string;
        public CustomerName: string;
        public Password: string;
        public Gender: number;
        public Age: number;
        public Address: string;
        public Phone: string;
        public OpenDate: string;
        public LastLoginTime: string;
        public Status: number;
    }

    export interface IAddCustomercope extends ng.IScope {
        CustomerName: string;
        Email: string;
        Password: string;
        addCustomer(): void;
    }
}

module OneStopCustomerApp {
    export class Config {
        constructor($routeProvider: ng.route.IRouteProvider) {
            $routeProvider
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .otherwise({ redirectTo: '/' });
        }
    }
    Config.$inject = ['$routeProvider'];

    export class CustomerDataSvc {
        private techCtmApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        addCustomer(customer: Extensions.Customer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.techCtmApiPath, customer)
                .then(function (result) {
                    deferred.resolve();
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.techCtmApiPath = "api/customer/newaccount";
            this.httpService = $http;
            this.qService = $q;
        }

        public static CustomerDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): CustomerDataSvc {
            return new CustomerDataSvc($http, $q);
        }

    }

    export class AddCustomerCtrl {
        $scope: Extensions.IAddCustomercope;
        dataSvc: CustomerDataSvc;

        constructor($scope: Extensions.IAddCustomercope, dataSvc: CustomerDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.dataSvc = dataSvc;

            self.$scope.addCustomer = function () {
                let ctm = new Extensions.Customer();
                ctm.CustomerName = self.$scope.CustomerName;
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.addCustomer(ctm).then(function () {
                    alert("Successful");
                });

            };

            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
    AddCustomerCtrl.$inject = ['$scope', 'customerDataSvc'];

    var app = angular.module("webApp", ['ngRoute']);
    app.config(Config);
    app.factory('customerDataSvc', ['$http', '$q', CustomerDataSvc.CustomerDataSvcFactory]);
    app.controller('AddNewCustomerCtrl', AddCustomerCtrl);
}
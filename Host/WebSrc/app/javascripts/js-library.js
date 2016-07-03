/// <reference path="../../all.ts" />
var DataModels;
(function (DataModels) {
    var Customer = (function () {
        function Customer() {
        }
        return Customer;
    }());
    DataModels.Customer = Customer;
})(DataModels || (DataModels = {}));

/// <reference path="../../all.ts" />
var Services;
(function (Services) {
    var CustomerDataSvc = (function () {
        function CustomerDataSvc($http, $q) {
            this.techCtmApiPath = "api/customer/newaccount";
            this.signOnApiPath = "api/customer/signon";
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
        CustomerDataSvc.prototype.signOnCustomer = function (customer) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.signOnApiPath, customer)
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
    Services.CustomerDataSvc = CustomerDataSvc;
})(Services || (Services = {}));

/// <reference path="../../all.ts" />
var Controllers;
(function (Controllers) {
    var AddCustomerCtrl = (function () {
        function AddCustomerCtrl($scope, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$scope.addCustomer = function () {
                var ctm = new DataModels.Customer();
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
    Controllers.AddCustomerCtrl = AddCustomerCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var Controllers;
(function (Controllers) {
    var MainPageCtrl = (function () {
        function MainPageCtrl($scope, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.init();
        }
        MainPageCtrl.prototype.init = function () {
            var self = this;
            self.dataSvc.getMainPage().then(function (data) {
                self.$scope.Header = data;
            });
        };
        return MainPageCtrl;
    }());
    Controllers.MainPageCtrl = MainPageCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    var Offer = (function () {
        function Offer() {
        }
        return Offer;
    }());
    DataModels.Offer = Offer;
})(DataModels || (DataModels = {}));

/// <reference path="../all.ts" />
var Services;
(function (Services) {
    var MainPageDataSvc = (function () {
        function MainPageDataSvc($http, $q) {
            this.indexApiPath = "api/mainpage/index";
            this.httpService = $http;
            this.qService = $q;
        }
        MainPageDataSvc.prototype.getMainPage = function () {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.indexApiPath)
                .then(function (data) {
                deferred.resolve();
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        MainPageDataSvc.MainPageDataSvcFactory = function ($http, $q) {
            return new MainPageDataSvc($http, $q);
        };
        return MainPageDataSvc;
    }());
    Services.MainPageDataSvc = MainPageDataSvc;
})(Services || (Services = {}));

/// <reference path="../../all.ts" />
var Controllers;
(function (Controllers) {
    var SignOnCustomerCtrl = (function () {
        function SignOnCustomerCtrl($scope, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$scope.signOnCustomer = function () {
                var ctm = new DataModels.Customer();
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.signOnCustomer(ctm).then(function () {
                    alert("Successful");
                });
            };
            self.init();
        }
        SignOnCustomerCtrl.prototype.init = function () {
            var self = this;
        };
        return SignOnCustomerCtrl;
    }());
    Controllers.SignOnCustomerCtrl = SignOnCustomerCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    var PhotoType = (function () {
        function PhotoType() {
        }
        return PhotoType;
    }());
    DataModels.PhotoType = PhotoType;
    var NavigationHeader = (function () {
        function NavigationHeader() {
        }
        return NavigationHeader;
    }());
    DataModels.NavigationHeader = NavigationHeader;
})(DataModels || (DataModels = {}));

/// <reference path="./scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="./customer/model/Customer.ts" />
/// <reference path="./customer/service/CustomerDataSvc.ts" />
/// <reference path="./customer/controller/AddCustomerCtrl.ts" />
/// <reference path="./main/MainPageCtrl.ts" />
/// <reference path="./main/MainPage.ts" />
/// <reference path="./main/MainPageDataSvc.ts" />
/// <reference path="./customer/controller/SignOnCustomerCtrl.ts" />
/// <reference path="./common/IBaseScope.ts" /> 

/// <reference path="./all.ts" />
var OneStopCustomerApp;
(function (OneStopCustomerApp) {
    var Config = (function () {
        function Config($routeProvider) {
            $routeProvider
                .when("/", { controller: "IndexPageCtrl" })
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .when("/signin", { templateUrl: "customer/view/signin.html", controller: "CustomerSignOnCtrl" })
                .otherwise({ redirectTo: '/' });
        }
        return Config;
    }());
    OneStopCustomerApp.Config = Config;
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
})(OneStopCustomerApp || (OneStopCustomerApp = {}));

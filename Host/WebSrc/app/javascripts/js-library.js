/// <reference path="../../all.ts" />
var DataModels;
(function (DataModels) {
    var Customer = (function () {
        function Customer() {
        }
        return Customer;
    }());
    DataModels.Customer = Customer;
    var CookieInfo = (function () {
        function CookieInfo() {
        }
        return CookieInfo;
    }());
    DataModels.CookieInfo = CookieInfo;
})(DataModels || (DataModels = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    var PhotoType = (function () {
        function PhotoType() {
        }
        return PhotoType;
    }());
    DataModels.PhotoType = PhotoType;
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
            this.cookieInfo = new DataModels.CookieInfo();
        }
        CustomerDataSvc.prototype.addCustomer = function (customer) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.techCtmApiPath, customer)
                .then(function (result) {
                self.cookieInfo.sid = result.data.SessionId;
                self.cookieInfo.skey = result.data.SessionKey;
                self.cookieInfo.cid = result.data.CustomerId;
                self.cookieInfo.cname = result.data.CustomerName;
                deferred.resolve(self.cookieInfo);
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
                self.cookieInfo.sid = result.data.SessionId;
                self.cookieInfo.skey = result.data.SessionKey;
                self.cookieInfo.cid = result.data.CustomerId;
                self.cookieInfo.cname = result.data.CustomerName;
                deferred.resolve(self.cookieInfo);
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
        function AddCustomerCtrl($scope, $cookies, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$scope.addCustomer = function () {
                var ctm = new DataModels.Customer();
                ctm.CustomerName = self.$scope.CustomerName;
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.addCustomer(ctm).then(function (res) {
                    $cookies.put("sid", String(res.sid));
                    $cookies.put("skey", String(res.skey));
                    $cookies.put("cid", String(res.cid));
                    $cookies.put("cname", String(res.cname));
                    self.$scope.CustomerName = self.$cookies.get("cname");
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
        function MainPageCtrl($scope, $cookies, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.init();
        }
        MainPageCtrl.prototype.init = function () {
            var self = this;
            self.dataSvc.getMainPage().then(function (data) {
                self.$scope.CustomerName = self.$cookies.get("cname");
                self.$scope.PhotoTypes = data.PhotoTypes;
                self.$scope.Offers = data.Offers;
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
    var MainContent = (function () {
        function MainContent() {
        }
        return MainContent;
    }());
    DataModels.MainContent = MainContent;
})(DataModels || (DataModels = {}));

/// <reference path="../all.ts" />
var Services;
(function (Services) {
    var MainPageDataSvc = (function () {
        function MainPageDataSvc($http, $q) {
            this.indexApiPath = "api/mainpage/index";
            this.MainPageContent = new DataModels.MainContent();
            this.httpService = $http;
            this.qService = $q;
        }
        MainPageDataSvc.prototype.getMainPage = function () {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.indexApiPath)
                .then(function (result) {
                self.MainPageContent.CustomerName = result.data.CustomerName;
                self.MainPageContent.PhotoTypes = result.data.PhotoTypes;
                self.MainPageContent.Offers = result.data.Offers;
                deferred.resolve(self.MainPageContent);
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
        //static $inject = ['$scope', '$cookies', 'dataSvc'];
        function SignOnCustomerCtrl($scope, $cookies, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$scope.signOnCustomer = function () {
                var ctm = new DataModels.Customer();
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.signOnCustomer(ctm).then(function (res) {
                    $cookies.put("sid", String(res.sid));
                    $cookies.put("skey", String(res.skey));
                    $cookies.put("cid", String(res.cid));
                    $cookies.put("cname", String(res.cname));
                    self.$scope.CustomerName = self.$cookies.get("cname");
                    alert(JSON.stringify(self.$cookies.get("cname")));
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
var Controllers;
(function (Controllers) {
    var PhotoTypeCtrl = (function () {
        function PhotoTypeCtrl($scope, $routeParams, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.init();
        }
        PhotoTypeCtrl.prototype.init = function () {
            var self = this;
            self.dataSvc.getPhotoTypeOffers(self.$routeParams.phototypeid).then(function (data) {
                self.$scope.Offers = data.OfferList;
                self.$scope.PhotoTypeId = data.PhotoTypeId;
                self.$scope.PhotoTypeName = data.PhotoTypeName;
            });
        };
        return PhotoTypeCtrl;
    }());
    Controllers.PhotoTypeCtrl = PhotoTypeCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var Services;
(function (Services) {
    var PhotoTypeDataSvc = (function () {
        function PhotoTypeDataSvc($http, $q) {
            this.getPhotoTypeOffersApiPath = "api/phototype/getphototypeoffers";
            this.OfferList = new Array();
            this.PhotoTypeId = 0;
            this.PhotoTypeName = "";
            this.httpService = $http;
            this.qService = $q;
        }
        PhotoTypeDataSvc.prototype.getPhotoTypeOffers = function (photoTypeId) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.getPhotoTypeOffersApiPath + "/" + photoTypeId)
                .then(function (result) {
                self.PhotoTypeId = result.data.PhotoTypeId;
                self.PhotoTypeName = result.data.PhotoTypeName;
                self.OfferList = result.data.OfferList;
                deferred.resolve(self);
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        PhotoTypeDataSvc.PhotoTypeDataSvcFactory = function ($http, $q) {
            return new PhotoTypeDataSvc($http, $q);
        };
        return PhotoTypeDataSvc;
    }());
    Services.PhotoTypeDataSvc = PhotoTypeDataSvc;
})(Services || (Services = {}));

/// <reference path="../all.ts" />
var Controllers;
(function (Controllers) {
    var OfferDetailsCtrl = (function () {
        function OfferDetailsCtrl($scope, $routeParams, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.init();
        }
        OfferDetailsCtrl.prototype.init = function () {
            var self = this;
            self.dataSvc.getOfferDetails(self.$routeParams.offerid).then(function (data) {
                self.$scope.OfferDetails = data.OfferDetails;
            });
        };
        return OfferDetailsCtrl;
    }());
    Controllers.OfferDetailsCtrl = OfferDetailsCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var Services;
(function (Services) {
    var OfferDetailsDataSvc = (function () {
        function OfferDetailsDataSvc($http, $q) {
            this.getOfferDetailsApiPath = "api/offer/getofferdetails";
            this.OfferDetails = new DataModels.Offer();
            this.httpService = $http;
            this.qService = $q;
        }
        OfferDetailsDataSvc.prototype.getOfferDetails = function (offerId) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.getOfferDetailsApiPath + "/" + offerId)
                .then(function (result) {
                self.OfferDetails = result.data;
                deferred.resolve(self);
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        OfferDetailsDataSvc.OfferDetailsDataSvcFactory = function ($http, $q) {
            return new OfferDetailsDataSvc($http, $q);
        };
        return OfferDetailsDataSvc;
    }());
    Services.OfferDetailsDataSvc = OfferDetailsDataSvc;
})(Services || (Services = {}));

/// <reference path="./scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-cookies.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="./customer/model/Customer.ts" />
/// <reference path="./common/IBaseScope.ts" />
/// <reference path="./customer/service/CustomerDataSvc.ts" />
/// <reference path="./customer/controller/AddCustomerCtrl.ts" />
/// <reference path="./main/MainPageCtrl.ts" />
/// <reference path="./main/MainPage.ts" />
/// <reference path="./main/MainPageDataSvc.ts" />
/// <reference path="./customer/controller/SignOnCustomerCtrl.ts" />
/// <reference path="./phototype/PhotoTypeCtrl.ts" />
/// <reference path="./phototype/PhotoTypeDataSvc.ts" />
/// <reference path="./offer/OfferDetailsCtrl.ts" />
/// <reference path="./offer/OfferDetailsDataSvc.ts" /> 

/// <reference path="./all.ts" />
var OneStopCustomerApp;
(function (OneStopCustomerApp) {
    var Config = (function () {
        function Config($routeProvider) {
            $routeProvider
                .when("/", { controller: "IndexPageCtrl" })
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .when("/signin", { templateUrl: "customer/view/signin.html", controller: "CustomerSignOnCtrl" })
                .when("/phototype/:phototypeid", { templateUrl: "phototype/phototype.html", controller: "GetPhotoTypeCtrl" })
                .when("/offerdetails/:offerid", { templateUrl: "offer/details.html", controller: "GetOfferDetailsCtrl" })
                .otherwise({ redirectTo: '/' });
        }
        return Config;
    }());
    OneStopCustomerApp.Config = Config;
    Config.$inject = ['$routeProvider'];
    Controllers.AddCustomerCtrl.$inject = ['$scope', '$cookies', 'customerDataSvc'];
    Controllers.SignOnCustomerCtrl.$inject = ['$scope', '$cookies', 'customerDataSvc'];
    Controllers.MainPageCtrl.$inject = ['$scope', '$cookies', 'mainPageDataSvc'];
    Controllers.PhotoTypeCtrl.$inject = ['$scope', '$routeParams', 'photoTypeDataSvc'];
    Controllers.OfferDetailsCtrl.$inject = ['$scope', '$routeParams', 'offerDetailsDataSvc'];
    //test
    var app = angular.module("webApp", ['ngRoute', 'ngCookies']);
    app.config(Config);
    app.factory('customerDataSvc', ['$http', '$q', Services.CustomerDataSvc.CustomerDataSvcFactory]);
    app.factory('mainPageDataSvc', ['$http', '$q', Services.MainPageDataSvc.MainPageDataSvcFactory]);
    app.factory('photoTypeDataSvc', ['$http', '$q', Services.PhotoTypeDataSvc.PhotoTypeDataSvcFactory]);
    app.factory('offerDetailsDataSvc', ['$http', '$q', Services.OfferDetailsDataSvc.OfferDetailsDataSvcFactory]);
    app.controller('AddNewCustomerCtrl', Controllers.AddCustomerCtrl);
    app.controller('CustomerSignOnCtrl', Controllers.SignOnCustomerCtrl);
    app.controller('IndexPageCtrl', Controllers.MainPageCtrl);
    app.controller('GetPhotoTypeCtrl', Controllers.PhotoTypeCtrl);
    app.controller('GetOfferDetailsCtrl', Controllers.OfferDetailsCtrl);
})(OneStopCustomerApp || (OneStopCustomerApp = {}));

// Type definitions for Angular JS 1.4 (ngCookies module)
// Project: http://angularjs.org
// Definitions by: Diego Vilar <http://github.com/diegovilar>, Anthony Ciccarello <http://github.com/aciccarello>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped
/// <reference path="angular.d.ts" />

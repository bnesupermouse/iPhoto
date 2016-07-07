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

/// <reference path="../../all.ts" />
var DataModels;
(function (DataModels) {
    var Photographer = (function () {
        function Photographer() {
        }
        return Photographer;
    }());
    DataModels.Photographer = Photographer;
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
            this.addPhApiPath = "api/photographer/newaccount";
            this.signOnPhApiPath = "api/photographer/signon";
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
        CustomerDataSvc.prototype.addPhotographer = function (photographer) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.addPhApiPath, photographer)
                .then(function (result) {
                self.cookieInfo.sid = result.data.SessionId;
                self.cookieInfo.skey = result.data.SessionKey;
                self.cookieInfo.cid = result.data.PhotographerId;
                self.cookieInfo.cname = result.data.PhotographerName;
                deferred.resolve(self.cookieInfo);
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        CustomerDataSvc.prototype.signOnPhotographer = function (photographer) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.signOnPhApiPath, photographer)
                .then(function (result) {
                self.cookieInfo.sid = result.data.SessionId;
                self.cookieInfo.skey = result.data.SessionKey;
                self.cookieInfo.cid = result.data.PhotographerId;
                self.cookieInfo.cname = result.data.PhotographerName;
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
    var ManageAccountCtrl = (function () {
        //static $inject = ['$scope', '$cookies', 'dataSvc'];
        function ManageAccountCtrl($scope, $cookies, $location, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$location = $location;
            var sid = $cookies.get("sid");
            if (sid == null) {
                self.$location.path("/signin");
            }
            else {
                self.$scope.CustomerName = $cookies.get("cname");
                self.$scope.AcccountId = $cookies.get("cid");
                self.$scope.CustomerType = $cookies.get("ctype");
            }
            self.init();
        }
        ManageAccountCtrl.prototype.init = function () {
            var self = this;
        };
        return ManageAccountCtrl;
    }());
    Controllers.ManageAccountCtrl = ManageAccountCtrl;
})(Controllers || (Controllers = {}));

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
                if (self.$scope.CustomerType == 1) {
                    var ctm = new DataModels.Customer();
                    ctm.CustomerName = self.$scope.NewCustomerName;
                    ctm.Email = self.$scope.Email;
                    ctm.Password = self.$scope.Password;
                    dataSvc.addCustomer(ctm).then(function (res) {
                        $cookies.put("ctype", String(1));
                        $cookies.put("sid", String(res.sid));
                        $cookies.put("skey", String(res.skey));
                        $cookies.put("cid", String(res.cid));
                        $cookies.put("cname", String(res.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        alert("Successful");
                    });
                }
                else {
                    var phg = new DataModels.Photographer();
                    phg.PhotographerName = self.$scope.NewCustomerName;
                    phg.Email = self.$scope.Email;
                    phg.Password = self.$scope.Password;
                    dataSvc.addPhotographer(phg).then(function (res) {
                        $cookies.put("ctype", String(2));
                        $cookies.put("sid", String(res.sid));
                        $cookies.put("skey", String(res.skey));
                        $cookies.put("cid", String(res.cid));
                        $cookies.put("cname", String(res.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        alert("Successful");
                    });
                }
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
        function SignOnCustomerCtrl($scope, $cookies, $location, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$location = $location;
            self.$scope.signOnCustomer = function () {
                if (self.$scope.CustomerType == 1) {
                    var ctm = new DataModels.Customer();
                    ctm.Email = self.$scope.Email;
                    ctm.Password = self.$scope.Password;
                    dataSvc.signOnCustomer(ctm).then(function (res) {
                        $cookies.put("ctype", String(1));
                        $cookies.put("sid", String(res.sid));
                        $cookies.put("skey", String(res.skey));
                        $cookies.put("cid", String(res.cid));
                        $cookies.put("cname", String(res.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        self.$location.path("/");
                    });
                }
                else {
                    var phg = new DataModels.Photographer();
                    phg.Email = self.$scope.Email;
                    phg.Password = self.$scope.Password;
                    dataSvc.signOnPhotographer(phg).then(function (res) {
                        $cookies.put("ctype", String(2));
                        $cookies.put("sid", String(res.sid));
                        $cookies.put("skey", String(res.skey));
                        $cookies.put("cid", String(res.cid));
                        $cookies.put("cname", String(res.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        self.$location.path("/");
                    });
                }
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
        function OfferDetailsCtrl($scope, $cookies, $routeParams, $location, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.placeOrder = function () {
                var placeOrder = new DataModels.PlaceOrder();
                self.$scope.AcccountId = $cookies.get("cid");
                placeOrder.CustomerId = self.$scope.AcccountId;
                placeOrder.OfferId = self.$scope.OfferDetails.OfferId;
                dataSvc.placeOrder(placeOrder).then(function (res) {
                    var orderId = res;
                    self.$location.path("/orderpayment/" + orderId);
                });
            };
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
            this.placeOrderApiPath = "api/offer/placeorder";
            this.OrderId = 0;
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
        OfferDetailsDataSvc.prototype.placeOrder = function (placeOrder) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.placeOrderApiPath, placeOrder)
                .then(function (result) {
                self.OrderId = result.data.OrderId;
                deferred.resolve(self.OrderId);
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

/// <reference path="../all.ts" />
var Controllers;
(function (Controllers) {
    var OrderPaymentCtrl = (function () {
        function OrderPaymentCtrl($scope, $cookies, $routeParams, $location, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.payOrder = function () {
                var payOrder = new DataModels.PayOrder();
                self.$scope.AcccountId = $cookies.get("cid");
                payOrder.CustomerId = self.$scope.AcccountId;
                payOrder.OrderId = self.$routeParams.orderid;
                payOrder.CardNumber = self.$scope.CardInfo.CardNumber;
                payOrder.CVC = self.$scope.CardInfo.CVC;
                payOrder.Month = self.$scope.CardInfo.Month;
                payOrder.Name = self.$scope.CardInfo.Name;
                payOrder.Year = self.$scope.CardInfo.Year;
                dataSvc.payOrder(payOrder).then(function (res) {
                    self.$location.path("/account");
                });
            };
            self.init();
        }
        OrderPaymentCtrl.prototype.init = function () {
            var self = this;
        };
        return OrderPaymentCtrl;
    }());
    Controllers.OrderPaymentCtrl = OrderPaymentCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var Services;
(function (Services) {
    var PaymentDataSvc = (function () {
        function PaymentDataSvc($http, $q) {
            this.payOrderApiPath = "api/offer/payorder";
            this.httpService = $http;
            this.qService = $q;
        }
        PaymentDataSvc.prototype.payOrder = function (payOrder) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.post(self.payOrderApiPath, payOrder)
                .then(function (result) {
                deferred.resolve(self);
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        PaymentDataSvc.PaymentDataSvcFactory = function ($http, $q) {
            return new PaymentDataSvc($http, $q);
        };
        return PaymentDataSvc;
    }());
    Services.PaymentDataSvc = PaymentDataSvc;
})(Services || (Services = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    var PayOrder = (function () {
        function PayOrder() {
        }
        return PayOrder;
    }());
    DataModels.PayOrder = PayOrder;
})(DataModels || (DataModels = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    var PlaceOrder = (function () {
        function PlaceOrder() {
        }
        return PlaceOrder;
    }());
    DataModels.PlaceOrder = PlaceOrder;
})(DataModels || (DataModels = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    var Order = (function () {
        function Order() {
        }
        return Order;
    }());
    DataModels.Order = Order;
})(DataModels || (DataModels = {}));

/// <reference path="../all.ts" />
var Controllers;
(function (Controllers) {
    var OrderCtrl = (function () {
        function OrderCtrl($scope, $cookies, $routeParams, dataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.init();
            self.$scope.$on('$viewContentLoaded', function (event) {
                console.log("content loaded");
                eval('$(\'.footable\').footable()');
            });
        }
        OrderCtrl.prototype.init = function () {
            var self = this;
            self.dataSvc.getOrderList(self.$scope.AcccountId, self.$scope.CustomerType, 1).then(function (data) {
                self.$scope.Orders = data.OrderList;
            });
        };
        return OrderCtrl;
    }());
    Controllers.OrderCtrl = OrderCtrl;
})(Controllers || (Controllers = {}));

/// <reference path="../all.ts" />
var Services;
(function (Services) {
    var OrderDataSvc = (function () {
        function OrderDataSvc($http, $q) {
            this.getOrderListApiPath = "api/order/getorderlist";
            this.OrderList = new Array();
            this.httpService = $http;
            this.qService = $q;
        }
        OrderDataSvc.prototype.getOrderList = function (accountId, accountType, active) {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.getOrderListApiPath + "/" + accountId + "/" + accountType + "/" + active)
                .then(function (result) {
                self.OrderList = result.data;
                deferred.resolve(self);
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };
        OrderDataSvc.OrderDataSvcFactory = function ($http, $q) {
            return new OrderDataSvc($http, $q);
        };
        return OrderDataSvc;
    }());
    Services.OrderDataSvc = OrderDataSvc;
})(Services || (Services = {}));

/// <reference path="./scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-cookies.d.ts" />
/// <reference path="./scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="./customer/model/Customer.ts" />
/// <reference path="./customer/model/Photographer.ts" />
/// <reference path="./common/IBaseScope.ts" />
/// <reference path="./customer/service/CustomerDataSvc.ts" />
/// <reference path="./customer/controller/ManageAccountCtrl.ts" />
/// <reference path="./customer/controller/AddCustomerCtrl.ts" />
/// <reference path="./main/MainPageCtrl.ts" />
/// <reference path="./main/MainPage.ts" />
/// <reference path="./main/MainPageDataSvc.ts" />
/// <reference path="./customer/controller/SignOnCustomerCtrl.ts" />
/// <reference path="./phototype/PhotoTypeCtrl.ts" />
/// <reference path="./phototype/PhotoTypeDataSvc.ts" />
/// <reference path="./offer/OfferDetailsCtrl.ts" />
/// <reference path="./offer/OfferDetailsDataSvc.ts" />
/// <reference path="./offer/OrderPaymentCtrl.ts" />
/// <reference path="./offer/PaymentDataSvc.ts" />
/// <reference path="./offer/PayOrder.ts" />
/// <reference path="./offer/PlaceOrder.ts" />
/// <reference path="./order/Order.ts" />
/// <reference path="./order/OrderCtrl.ts" />
/// <reference path="./order/OrderDataSvc.ts" /> 

/// <reference path="./all.ts" />
var OneStopCustomerApp;
(function (OneStopCustomerApp) {
    var Config = (function () {
        function Config($routeProvider) {
            $routeProvider
                .when("/", { controller: "IndexPageCtrl" })
                .when("/account", { templateUrl: "customer/view/account.html", controller: "ManageMyAccountCtrl" })
                .when("/orderlist", { templateUrl: "order/orderlist.html", controller: "GetOrderListCtrl" })
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .when("/signin", { templateUrl: "customer/view/signin.html", controller: "CustomerSignOnCtrl" })
                .when("/phototype/:phototypeid", { templateUrl: "phototype/phototype.html", controller: "GetPhotoTypeCtrl" })
                .when("/offerdetails/:offerid", { templateUrl: "offer/details.html", controller: "GetOfferDetailsCtrl" })
                .when("/orderpayment/:orderid", { templateUrl: "offer/orderpayment.html", controller: "ProcessOrderPaymentCtrl" })
                .otherwise({ redirectTo: '/' });
        }
        return Config;
    }());
    OneStopCustomerApp.Config = Config;
    Config.$inject = ['$routeProvider'];
    Controllers.AddCustomerCtrl.$inject = ['$scope', '$cookies', 'customerDataSvc'];
    Controllers.SignOnCustomerCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.ManageAccountCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.MainPageCtrl.$inject = ['$scope', '$cookies', 'mainPageDataSvc'];
    Controllers.PhotoTypeCtrl.$inject = ['$scope', '$routeParams', 'photoTypeDataSvc'];
    Controllers.OfferDetailsCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'offerDetailsDataSvc'];
    Controllers.OrderPaymentCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'paymentDataSvc'];
    Controllers.OrderCtrl.$inject = ['$scope', '$cookies', '$routeParams', 'orderDataSvc'];
    //test
    var app = angular.module("webApp", ['ngRoute', 'ngCookies']);
    app.config(Config);
    app.factory('customerDataSvc', ['$http', '$q', Services.CustomerDataSvc.CustomerDataSvcFactory]);
    app.factory('mainPageDataSvc', ['$http', '$q', Services.MainPageDataSvc.MainPageDataSvcFactory]);
    app.factory('photoTypeDataSvc', ['$http', '$q', Services.PhotoTypeDataSvc.PhotoTypeDataSvcFactory]);
    app.factory('offerDetailsDataSvc', ['$http', '$q', Services.OfferDetailsDataSvc.OfferDetailsDataSvcFactory]);
    app.factory('paymentDataSvc', ['$http', '$q', Services.PaymentDataSvc.PaymentDataSvcFactory]);
    app.factory('orderDataSvc', ['$http', '$q', Services.OrderDataSvc.OrderDataSvcFactory]);
    app.controller('AddNewCustomerCtrl', Controllers.AddCustomerCtrl);
    app.controller('CustomerSignOnCtrl', Controllers.SignOnCustomerCtrl);
    app.controller('IndexPageCtrl', Controllers.MainPageCtrl);
    app.controller('GetPhotoTypeCtrl', Controllers.PhotoTypeCtrl);
    app.controller('GetOfferDetailsCtrl', Controllers.OfferDetailsCtrl);
    app.controller('ProcessOrderPaymentCtrl', Controllers.OrderPaymentCtrl);
    app.controller('ManageMyAccountCtrl', Controllers.ManageAccountCtrl);
    app.controller('GetOrderListCtrl', Controllers.OrderCtrl);
})(OneStopCustomerApp || (OneStopCustomerApp = {}));

/// <reference path="../all.ts" />
var DataModels;
(function (DataModels) {
    (function (OrderStatusValue) {
        OrderStatusValue[OrderStatusValue["OrderPending"] = 0] = "OrderPending";
        OrderStatusValue[OrderStatusValue["OrderConfirmed"] = 1] = "OrderConfirmed";
        OrderStatusValue[OrderStatusValue["RawPhotoUploading"] = 2] = "RawPhotoUploading";
        OrderStatusValue[OrderStatusValue["RawPhotoUploaded"] = 3] = "RawPhotoUploaded";
        OrderStatusValue[OrderStatusValue["PhotoSelecting"] = 4] = "PhotoSelecting";
        OrderStatusValue[OrderStatusValue["PhotoSelected"] = 5] = "PhotoSelected";
        OrderStatusValue[OrderStatusValue["RetouchedPhotoUploading"] = 6] = "RetouchedPhotoUploading";
        OrderStatusValue[OrderStatusValue["RetouchedPhotoUploaded"] = 7] = "RetouchedPhotoUploaded";
        OrderStatusValue[OrderStatusValue["RetouchedPhotoConfirming"] = 8] = "RetouchedPhotoConfirming";
        OrderStatusValue[OrderStatusValue["OrderFinalised"] = 9] = "OrderFinalised";
        OrderStatusValue[OrderStatusValue["OrderRejected"] = 10] = "OrderRejected";
        OrderStatusValue[OrderStatusValue["OrderCancelled"] = 11] = "OrderCancelled"; //Ctm
    })(DataModels.OrderStatusValue || (DataModels.OrderStatusValue = {}));
    var OrderStatusValue = DataModels.OrderStatusValue;
})(DataModels || (DataModels = {}));

// Type definitions for Angular JS 1.4 (ngCookies module)
// Project: http://angularjs.org
// Definitions by: Diego Vilar <http://github.com/diegovilar>, Anthony Ciccarello <http://github.com/aciccarello>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped
/// <reference path="angular.d.ts" />

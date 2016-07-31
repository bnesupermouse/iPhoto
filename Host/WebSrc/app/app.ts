/// <reference path="./all.ts" />
module OneStopCustomerApp {
    export class Config {
        constructor($routeProvider: ng.route.IRouteProvider) {
            $routeProvider
                .when("/", { templateUrl: "main/main.html", controller: "IndexPageCtrl" })
                .when("/account", { templateUrl: "customer/view/account.html", controller: "ManageMyAccountCtrl" })
                .when("/orderlist", { templateUrl: "order/orderlist.html", controller: "GetOrderListCtrl" })
                .when("/signup", { templateUrl: "customer/view/signup.html", controller: "AddNewCustomerCtrl" })
                .when("/signin", { templateUrl: "customer/view/signin.html", controller: "CustomerSignOnCtrl" })
                .when("/phototype/:phototypeid", { templateUrl: "phototype/phototype.html", controller: "GetPhotoTypeCtrl" })
                .when("/offerdetails/:offerid", { templateUrl: "offer/details.html", controller: "GetOfferDetailsCtrl" })
                .when("/orderpayment/:orderid", { templateUrl: "offer/orderpayment.html", controller: "ProcessOrderPaymentCtrl" })
                .when("/orderdetails-0/:orderid", { templateUrl: "order/orderdetails-0.html", controller: "ManageOrderCtrl" })
                .when("/orderdetails-1/:orderid", { templateUrl: "order/orderdetails-1.html", controller: "ManageOrderCtrl" })
                .when("/orderdetails-2/:orderid", { templateUrl: "order/orderdetails-2.html", controller: "ManageOrderCtrl" })
                .when("/orderdetails-3/:orderid", { templateUrl: "order/orderdetails-3.html", controller: "ManageOrderCtrl" })
                .when("/orderdetails-4/:orderid", { templateUrl: "order/orderdetails-4.html", controller: "ManageOrderCtrl" })
                .when("/orderdetails-5/:orderid", { templateUrl: "order/orderdetails-5.html", controller: "ManageOrderCtrl" })
                .when("/addoffer", { templateUrl: "offer/updoffer.html", controller: "GetOfferDetailsCtrl" })
                .when("/offerlist", { templateUrl: "offer/offerlist.html", controller: "GetOfferListCtrl" })
                .when("/updoffer/:offerid", { templateUrl: "offer/updoffer.html", controller: "GetOfferDetailsCtrl" })
                .when("/photographerlist", { templateUrl: "admin/photographerlist.html", controller: "GetPhotographerListCtrl" })
                .when("/updphotographer/:photographerid", { templateUrl: "admin/updphotographer.html", controller: "GetPhotographerDetailsCtrl" })
                .when("/updphotographerinfo/:photographerid", { templateUrl: "customer/view/updphotographerinfo.html", controller: "GetPhotographerInfoDetailsCtrl" })
                .when("/updcustomer/:customerid", { templateUrl: "customer/view/updcustomer.html", controller: "GetCustomerDetailsCtrl" })
                .otherwise({ redirectTo: '/' });
        }
    }
    Config.$inject = ['$routeProvider'];
    Controllers.AddCustomerCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.SignOnCustomerCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.ManageAccountCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.MainPageCtrl.$inject = ['$scope', '$cookies', 'mainPageDataSvc'];
    Controllers.PhotoTypeCtrl.$inject = ['$scope', '$routeParams', 'photoTypeDataSvc'];
    Controllers.OfferDetailsCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', '$filter', 'offerDetailsDataSvc'];
    Controllers.OrderPaymentCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'paymentDataSvc'];
    Controllers.OrderCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'orderDataSvc'];
    Controllers.OrderDetailsCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'orderDataSvc'];
    Controllers.OfferManCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'offerDetailsDataSvc'];
    Controllers.PhotographerManCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'photographerManDataSvc'];
    Controllers.PhotographerDetailsCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'photographerManDataSvc'];
    Controllers.PhotographerInfoCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'customerDataSvc'];
    Controllers.CustomerInfoCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', '$filter', 'customerDataSvc'];
    //test
    var app = angular.module("webApp", ['ngRoute', 'ngCookies', 'infinite-scroll', 'ui.bootstrap.datetimepicker', 'daypilot']);
    app.config(Config);
    app.factory('customerDataSvc', ['$http', '$q', Services.CustomerDataSvc.CustomerDataSvcFactory]);
    app.factory('mainPageDataSvc', ['$http', '$q', Services.MainPageDataSvc.MainPageDataSvcFactory]);
    app.factory('photoTypeDataSvc', ['$http', '$q', Services.PhotoTypeDataSvc.PhotoTypeDataSvcFactory]);
    app.factory('offerDetailsDataSvc', ['$http', '$q', Services.OfferDetailsDataSvc.OfferDetailsDataSvcFactory]);
    app.factory('paymentDataSvc', ['$http', '$q', Services.PaymentDataSvc.PaymentDataSvcFactory]);
    app.factory('orderDataSvc', ['$http', '$q', Services.OrderDataSvc.OrderDataSvcFactory]);
    app.factory('photographerManDataSvc', ['$http', '$q', Services.PhotographerManDataSvc.PhotographerManDataSvcFactory]);

    app.controller('AddNewCustomerCtrl', Controllers.AddCustomerCtrl);
    app.controller('CustomerSignOnCtrl', Controllers.SignOnCustomerCtrl);
    app.controller('IndexPageCtrl', Controllers.MainPageCtrl);
    app.controller('GetPhotoTypeCtrl', Controllers.PhotoTypeCtrl);
    app.controller('GetOfferDetailsCtrl', Controllers.OfferDetailsCtrl);
    app.controller('ProcessOrderPaymentCtrl', Controllers.OrderPaymentCtrl);
    app.controller('ManageMyAccountCtrl', Controllers.ManageAccountCtrl);
    app.controller('GetOrderListCtrl', Controllers.OrderCtrl);
    app.controller('ManageOrderCtrl', Controllers.OrderDetailsCtrl);
    app.controller('GetOfferListCtrl', Controllers.OfferManCtrl);
    app.controller('GetPhotographerListCtrl', Controllers.PhotographerManCtrl);
    app.controller('GetPhotographerDetailsCtrl', Controllers.PhotographerDetailsCtrl);
    app.controller('GetPhotographerInfoDetailsCtrl', Controllers.PhotographerInfoCtrl);
    app.controller('GetCustomerDetailsCtrl', Controllers.CustomerInfoCtrl);
}
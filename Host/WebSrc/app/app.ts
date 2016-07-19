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
                .when("/addoffer", { templateUrl: "offer/updoffer.html", controller: "GetOfferDetailsCtrl" })
                .when("/offerlist", { templateUrl: "offer/offerlist.html", controller: "GetOfferListCtrl" })
                .when("/updoffer/:offerid", { templateUrl: "offer/updoffer.html", controller: "GetOfferDetailsCtrl" })
                .otherwise({ redirectTo: '/' });
        }
    }
    Config.$inject = ['$routeProvider'];
    Controllers.AddCustomerCtrl.$inject = ['$scope', '$cookies', 'customerDataSvc'];
    Controllers.SignOnCustomerCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.ManageAccountCtrl.$inject = ['$scope', '$cookies', '$location', 'customerDataSvc'];
    Controllers.MainPageCtrl.$inject = ['$scope', '$cookies', 'mainPageDataSvc'];
    Controllers.PhotoTypeCtrl.$inject = ['$scope', '$routeParams', 'photoTypeDataSvc'];
    Controllers.OfferDetailsCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'offerDetailsDataSvc'];
    Controllers.OrderPaymentCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'paymentDataSvc'];
    Controllers.OrderCtrl.$inject = ['$scope', '$cookies', '$routeParams', 'orderDataSvc'];
    Controllers.OrderDetailsCtrl.$inject = ['$scope', '$cookies', '$routeParams', '$location', 'orderDataSvc'];
    Controllers.OfferManCtrl.$inject = ['$scope', '$cookies', '$routeParams', 'offerDetailsDataSvc'];
    //test
    var app = angular.module("webApp", ['ngRoute', 'ngCookies', 'infinite-scroll', 'ui.bootstrap.datetimepicker']);
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
    app.controller('ManageOrderCtrl', Controllers.OrderDetailsCtrl);
    app.controller('GetOfferListCtrl', Controllers.OfferManCtrl);
}
/// <reference path="../all.ts" />
module Controllers {
    export interface IOrderPaymentRouteParams extends ng.route.IRouteParamsService {
        orderid: number;
    }
    export class OrderPaymentCtrl {
        $scope: DataModels.IPaymentPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOrderPaymentRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.PaymentDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IPaymentPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOrderPaymentRouteParams, $location: ng.ILocationService, dataSvc: Services.PaymentDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.payOrder = function () {
                let payOrder = new DataModels.PayOrder();
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
            }
            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

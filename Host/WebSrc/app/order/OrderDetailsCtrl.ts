/// <reference path="../all.ts" />
module Controllers {
    export class OrderDetailsCtrl {
        $scope: DataModels.IOrderDetailsPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOrderRouteParams;
        dataSvc: Services.OrderDataSvc;
        Details: DataModels.OrderDetails;

        constructor($scope: DataModels.IOrderDetailsPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOrderRouteParams, dataSvc: Services.OrderDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.$scope.confirmOrder = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.OrderConfirmed).then(function (data) {
                    //self.$scope.Details = data.Details;
                });
            }
            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getOrderDetails(self.$routeParams.orderid).then(function (data) {
                self.$scope.Details = data.Details;
            });
        }
    }
}

module Controllers {
    export interface IOrderRouteParams extends ng.route.IRouteParamsService {
        orderid: number;
    }
    export class OrderCtrl {
        $scope: DataModels.IOrderListPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOrderRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.OrderDataSvc;
        PhotoTypeId: number;
        OrderDetails: DataModels.OrderDetails;

        constructor($scope: DataModels.IOrderListPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOrderRouteParams, $location: ng.ILocationService, dataSvc: Services.OrderDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.$scope.StatusFilter = -1;
            self.$scope.SearchOrders = function () {
                self.dataSvc.getOrderList(self.$scope.AcccountId, self.$scope.CustomerType, self.$scope.StatusFilter).then(function (data) {
                    self.$scope.Orders = data.OrderList;
                });
            }
            if ($cookies.get("cid") == null) {
                self.$location.path("/signin");
                return;
            }
            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getOrderList(self.$scope.AcccountId, self.$scope.CustomerType, self.$scope.StatusFilter).then(function (data) {
                self.$scope.Orders = data.OrderList;
            });
        }
    }
}

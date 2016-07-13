module Controllers {
    export interface IOrderRouteParams extends ng.route.IRouteParamsService {
        orderid: number;
    }
    export class OrderCtrl {
        $scope: DataModels.IOrderListPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOrderRouteParams;
        dataSvc: Services.OrderDataSvc;
        PhotoTypeId: number;
        OrderDetails: DataModels.OrderDetails;

        constructor($scope: DataModels.IOrderListPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOrderRouteParams, dataSvc: Services.OrderDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.init();
            //self.$scope.$on('$viewContentLoaded', function (event) {
            //    console.log("content loaded");
            //    eval('$(\'.footable\').footable()');
            //});
        }

        private init(): void {
            var self = this;
            self.dataSvc.getOrderList(self.$scope.AcccountId, self.$scope.CustomerType, 1).then(function (data) {
                self.$scope.Orders = data.OrderList;
            });
        }
    }
}

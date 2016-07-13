module Controllers {
    export interface IOfferDetailsRouteParams extends ng.route.IRouteParamsService {
        offerid: number;
    }
    export class OfferDetailsCtrl {
        $scope: DataModels.IOfferPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOfferDetailsRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.OfferDetailsDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IOfferPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOfferDetailsRouteParams, $location: ng.ILocationService, dataSvc: Services.OfferDetailsDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.placeOrder = function () {
                let placeOrder = new DataModels.PlaceOrder();
                self.$scope.AcccountId = $cookies.get("cid");
                placeOrder.CustomerId = self.$scope.AcccountId;
                placeOrder.OfferId = self.$scope.OfferDetails.OfferId;
                dataSvc.placeOrder(placeOrder).then(function (res) {
                    let orderId = res;
                    self.$location.path("/orderpayment/" + orderId);
                });
            }
            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getOfferDetails(self.$routeParams.offerid).then(function (data) {
                self.$scope.OfferDetails = data.OfferDetails;
            });
        }
    }
}

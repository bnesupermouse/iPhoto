module Controllers {
    export interface IOfferRouteParams extends ng.route.IRouteParamsService {
        offerid: number;
    }
    export class OfferManCtrl {
        $scope: DataModels.IOfferListPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOfferRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.OfferDetailsDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IOfferListPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOfferRouteParams, $location: ng.ILocationService, dataSvc: Services.OfferDetailsDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.$scope.IsAdmin = 0;
            if ($cookies.get("isadmin") == "true") {
                self.$scope.IsAdmin = 1;
            }
            self.$scope.StatusFilter = -1;
            self.$scope.SearchOffers = function () {
                self.dataSvc.getOfferList(self.$scope.IsAdmin, self.$scope.StatusFilter).then(function (data) {
                    self.$scope.Offers = data.Offers;
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
            self.dataSvc.getOfferList(self.$scope.IsAdmin, self.$scope.StatusFilter).then(function (data) {
                self.$scope.Offers = data.Offers;
            });
        }
    }
}

﻿module Controllers {
    export interface IOfferRouteParams extends ng.route.IRouteParamsService {
        offerid: number;
    }
    export class OfferManCtrl {
        $scope: DataModels.IOfferListPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOfferRouteParams;
        dataSvc: Services.OfferDetailsDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IOfferListPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOfferRouteParams, dataSvc: Services.OfferDetailsDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getOfferList().then(function (data) {
                self.$scope.Offers = data.Offers;
            });
        }
    }
}
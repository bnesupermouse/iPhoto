/// <reference path="../all.ts" />
module Controllers {
    export interface IOfferDetailsRouteParams extends ng.route.IRouteParamsService {
        offerid: number;
    }
    export class OfferDetailsCtrl {
        $scope: DataModels.IOfferPageScope;
        $routeParams: IOfferDetailsRouteParams;
        dataSvc: Services.OfferDetailsDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IOfferPageScope, $routeParams: IOfferDetailsRouteParams, dataSvc: Services.OfferDetailsDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;

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

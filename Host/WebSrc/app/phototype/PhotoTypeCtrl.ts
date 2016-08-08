module Controllers {
    export interface IPhotoTypeRouteParams extends ng.route.IRouteParamsService {
        phototypeid: number;
    }
    export class PhotoTypeCtrl {
        $scope: DataModels.IPhotoTypePageScope;
        $routeParams: IPhotoTypeRouteParams;
        dataSvc: Services.PhotoTypeDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IPhotoTypePageScope, $routeParams: IPhotoTypeRouteParams, dataSvc: Services.PhotoTypeDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.SearchOffer = function () {
                let lower = 0;
                let upper = 0;
                if (self.$scope.PriceFilter == 1) {
                    lower = 1;
                    upper = 500;
                }
                else if (self.$scope.PriceFilter == 2) {
                    lower = 501;
                    upper = 1000;
                }
                else if (self.$scope.PriceFilter == 3) {
                    lower = self.$scope.LowerRange;
                    upper = self.$scope.UpperRange;
                }
                self.dataSvc.getPhotoTypeOffers(self.$routeParams.phototypeid, lower, upper, 0).then(function (data) {
                    self.$scope.Offers = data.OfferList;
                    self.$scope.PhotoTypeId = data.PhotoTypeId;
                    self.$scope.PhotoTypeName = data.PhotoTypeName;
                });
            }


            self.$scope.loadMoreOffers = function () {
                if (self.$scope.LastPhotoTypeOffer) {
                    return;
                }
                self.$scope.busy = true;
                let lastOfferId = 0;
                if (self.$scope.Offers == null) {
                    self.$scope.Offers = new Array<DataModels.Offer>();
                }
                else {
                    if (self.$scope.Offers.length > 0) {
                        lastOfferId = self.$scope.Offers[self.$scope.Offers.length - 1].OfferId;
                    }
                }
                let lower = 0;
                let upper = 0;
                if (self.$scope.PriceFilter == 1) {
                    lower = 1;
                    upper = 500;
                }
                else if (self.$scope.PriceFilter == 2) {
                    lower = 501;
                    upper = 1000;
                }
                else if (self.$scope.PriceFilter == 3) {
                    lower = self.$scope.LowerRange;
                    upper = self.$scope.UpperRange;
                }

                self.dataSvc.getPhotoTypeOffers(self.$routeParams.phototypeid, lower, upper, lastOfferId).then(function (data) {
                    self.$scope.PhotoTypeId = data.PhotoTypeId;
                    self.$scope.PhotoTypeName = data.PhotoTypeName;
                    if (self.$scope.Offers == null) {
                        self.$scope.Offers = new Array<DataModels.Offer>();
                    }
                    if (data.OfferList.length == 0) {
                        self.$scope.LastPhotoTypeOffer = true;
                    }
                    for (var i = 0; i < data.OfferList.length; i++) {
                        self.$scope.Offers.push(data.OfferList[i]);
                    }
                    self.$scope.busy = false;
                });
            }
            self.init();
        }

        private init(): void {
            var self = this;
            
        }
    }
}

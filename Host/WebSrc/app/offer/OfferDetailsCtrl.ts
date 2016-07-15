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
                placeOrder.AppointmentDate = self.$scope.AppointmentDate;
                dataSvc.placeOrder(placeOrder).then(function (res) {
                    let orderId = res;
                    self.$location.path("/orderpayment/" + orderId);
                });
            }


            self.$scope.loadMorePhotoPics = function () {
                self.$scope.busy = true;
                let lastPicId = 0;
                if (self.$scope.OfferDetails == null) {
                    self.$scope.OfferDetails = new DataModels.Offer();
                }
                if (self.$scope.OfferDetails.OfferPics == null) {
                    self.$scope.OfferDetails.OfferPics = new Array<DataModels.PicInfo>();
                }
                else {
                    if (self.$scope.OfferDetails.OfferPics.length > 0) {
                        lastPicId = self.$scope.OfferDetails.OfferPics[self.$scope.OfferDetails.OfferPics.length - 1].PictureId;
                    }
                }
                self.dataSvc.getMoreOfferPics(self.$routeParams.offerid, lastPicId).then(function (data) {
                    if (self.$scope.OfferDetails.OfferPics == null) {
                        self.$scope.OfferDetails.OfferPics = new Array<DataModels.PicInfo>();
                        }
                        for (var i = 0; i < data.Pics.length; i++) {
                            self.$scope.OfferDetails.OfferPics.push(data.Pics[i]);
                        }
                    self.$scope.busy = false;
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

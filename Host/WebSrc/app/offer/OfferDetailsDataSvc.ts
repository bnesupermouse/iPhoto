/// <reference path="../all.ts" />
module Services {
    export class OfferDetailsDataSvc {
        private getOfferDetailsApiPath: string;
        private placeOrderApiPath: string;
        public OfferDetails: DataModels.Offer;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        getOfferDetails(offerId:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOfferDetailsApiPath + "/" + offerId)
                .then(function (result: any) {
                    self.OfferDetails = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }
        placeOrder(placeOrder: DataModels.PlaceOrder): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.placeOrderApiPath, placeOrder)
                .then(function (result: any) {
                    self.OfferDetails = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }
        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.getOfferDetailsApiPath = "api/offer/getofferdetails";
            this.placeOrderApiPath = "api/offer/placeorder";
            this.OfferDetails = new DataModels.Offer();
            this.httpService = $http;
            this.qService = $q;
        }

        public static OfferDetailsDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): OfferDetailsDataSvc {
            return new OfferDetailsDataSvc($http, $q);
        }

    }
}

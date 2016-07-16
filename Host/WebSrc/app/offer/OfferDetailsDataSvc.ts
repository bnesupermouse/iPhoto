module Services {
    export class OfferDetailsDataSvc {
        private getOfferDetailsApiPath: string;
        private placeOrderApiPath: string;
        private getOfferPicApiPath: string;
        private addOfferApiPath: string;
        private getPhotoTypesApiPath: string;
        
        public OfferDetails: DataModels.Offer;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;
        OrderId: number;
        Pics: Array<DataModels.PicInfo>;
        PhotoTypes: Array<DataModels.PhotoType>;

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
                    self.OrderId = result.data.OrderId;
                    deferred.resolve(self.OrderId);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }


        addOffer(offer: DataModels.Offer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.addOfferApiPath, offer)
                .then(function (result: any) {
                    self.OfferDetails.OfferId = result.data.OfferId;
                    deferred.resolve(self.OfferDetails.OfferId);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        getMoreOfferPics(offerId: number, lastPicId: number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOfferPicApiPath + "/" + offerId + "/" + lastPicId)
                .then(function (result: any) {
                    self.Pics = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        getPhotoTypes(): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.getPhotoTypesApiPath)
                .then(function (result: any) {
                    self.PhotoTypes = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.getOfferDetailsApiPath = "api/offer/getofferdetails";
            this.placeOrderApiPath = "api/offer/placeorder";
            this.getOfferPicApiPath = "api/offer/getofferpics";
            this.addOfferApiPath = "api/offer/addnewoffer";
            this.getPhotoTypesApiPath = "api/offer/getphototypes";
            
            this.OrderId = 0;
            this.OfferDetails = new DataModels.Offer();
            this.httpService = $http;
            this.qService = $q;
        }

        public static OfferDetailsDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): OfferDetailsDataSvc {
            return new OfferDetailsDataSvc($http, $q);
        }

    }
}

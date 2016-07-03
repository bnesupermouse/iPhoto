/// <reference path="../all.ts" />
module Services {
    export class PhotoTypeDataSvc {
        private getPhotoTypeOffersApiPath: string;
        public OfferList: Array<DataModels.Offer>;
        public PhotoTypeId: number;
        public PhotoTypeName: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        getPhotoTypeOffers(photoTypeId:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getPhotoTypeOffersApiPath+"/"+photoTypeId)
                .then(function (result: any) {
                    self.PhotoTypeId = result.data.PhotoTypeId;
                    self.PhotoTypeName = result.data.PhotoTypeName;
                    self.OfferList = result.data.OfferList;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }
        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.getPhotoTypeOffersApiPath = "api/phototype/getphototypeoffers";
            this.OfferList = new Array<DataModels.Offer>();
            this.PhotoTypeId = 0;
            this.PhotoTypeName = "";
            this.httpService = $http;
            this.qService = $q;
        }

        public static PhotoTypeDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): PhotoTypeDataSvc {
            return new PhotoTypeDataSvc($http, $q);
        }

    }
}

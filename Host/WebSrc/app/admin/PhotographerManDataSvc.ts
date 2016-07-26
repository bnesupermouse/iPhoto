module Services {
    export class PhotographerManDataSvc {
        private getPhotographerListApiPath: string;
        private getPhotographerDetailsApiPath: string;
        private updatePhotographerApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;
        Photographers: Array<DataModels.Photographer>;
        Details: DataModels.Photographer;
        PhotographerId: number;
        ErrorNo: number;
        ErrorMsg: string;

        updatePhotographer(updPhotographer: DataModels.UpdPhotographer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.updatePhotographerApiPath, updPhotographer)
                .then(function (result: any) {
                    self.ErrorNo = result.data.ErrorNo;
                    self.ErrorMsg = result.data.ErrorMsg;
                    self.PhotographerId = result.data.PhotographerId;
                    self.ErrorNo = result.data.ErrorNo;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        getPhotographerList(statusFilter:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();
            self.httpService.get(self.getPhotographerListApiPath + "/" + statusFilter)
                .then(function (result: any) {
                    self.Photographers = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }
        getPhotographerDetails(photographerId: number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getPhotographerDetailsApiPath + "/" + photographerId)
                .then(function (result: any) {
                    self.Details = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }
        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.getPhotographerListApiPath = "api/admin/getphotographerlist";
            this.updatePhotographerApiPath = "api/admin/updatephotographer";
            this.getPhotographerDetailsApiPath = "api/admin/getphotographer";
            this.httpService = $http;
            this.qService = $q;
        }

        public static PhotographerManDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): PhotographerManDataSvc {
            return new PhotographerManDataSvc($http, $q);
        }

    }
}

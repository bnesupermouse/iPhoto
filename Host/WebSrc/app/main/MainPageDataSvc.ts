/// <reference path="../all.ts" />
module Services {
    export class MainPageDataSvc {
        private indexApiPath: string;
        private OfferList: Array<DataModels.Offer>;
        private NavHeader: DataModels.NavigationHeader;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        getMainPage(): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.indexApiPath)
                .then(function (data) {
                    deferred.resolve();
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }


        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.indexApiPath = "api/mainpage/index";
            this.httpService = $http;
            this.qService = $q;
        }

        public static MainPageDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): MainPageDataSvc {
            return new MainPageDataSvc($http, $q);
        }

    }
}

/// <reference path="../all.ts" />
module Services {
    export class MainPageDataSvc {
        private indexApiPath: string;
        public OfferList: Array<DataModels.Offer>;
        public NavHeader: DataModels.NavigationHeader;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        getMainPage(): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.indexApiPath)
                .then(function (result: any) {
                    self.NavHeader.CustomerName = result.data.CustomerName;
                    self.NavHeader.PhotoTypes = result.data.PhotoTypes;
                    //alert(JSON.stringify(self));
                    deferred.resolve(self.NavHeader);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }


        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.indexApiPath = "api/mainpage/index";
            this.NavHeader = new DataModels.NavigationHeader();
            this.httpService = $http;
            this.qService = $q;
        }

        public static MainPageDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): MainPageDataSvc {
            return new MainPageDataSvc($http, $q);
        }

    }
}

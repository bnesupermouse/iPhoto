module Services {
    export class MainPageDataSvc {
        private indexApiPath: string;
        public OfferList: Array<DataModels.Offer>;
        public MainPageContent: DataModels.MainContent;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        getMainPage(): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.indexApiPath)
                .then(function (result: any) {
                    self.MainPageContent.CustomerName = result.data.CustomerName;
                    self.MainPageContent.PhotoTypes = result.data.PhotoTypes;
                    self.MainPageContent.PhotoTypeOffers = result.data.PhotoTypeOffers;
                    deferred.resolve(self.MainPageContent);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }


        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.indexApiPath = "api/mainpage/index";
            this.MainPageContent = new DataModels.MainContent();
            this.httpService = $http;
            this.qService = $q;
        }

        public static MainPageDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): MainPageDataSvc {
            return new MainPageDataSvc($http, $q);
        }

    }
}

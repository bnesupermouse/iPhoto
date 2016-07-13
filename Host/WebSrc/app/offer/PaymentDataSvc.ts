module Services {
    export class PaymentDataSvc {
        private payOrderApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        payOrder(payOrder: DataModels.PayOrder): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.payOrderApiPath, payOrder)
                .then(function (result: any) {
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }
        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.payOrderApiPath = "api/offer/payorder";
            this.httpService = $http;
            this.qService = $q;
        }

        public static PaymentDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): PaymentDataSvc {
            return new PaymentDataSvc($http, $q);
        }

    }
}

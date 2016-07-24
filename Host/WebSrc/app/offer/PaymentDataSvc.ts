module Services {
    export class PaymentDataSvc {
        private payOrderApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;
        getOrderInfoApi: string;
        OrderInfo: DataModels.Order;

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

        getOrderInfo(orderId:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOrderInfoApi + "/" + orderId)
                .then(function (result: any) {
                    self.OrderInfo = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.payOrderApiPath = "api/offer/payorder";
            this.getOrderInfoApi = "api/offer/getorderinfo";
            this.httpService = $http;
            this.qService = $q;
        }

        public static PaymentDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): PaymentDataSvc {
            return new PaymentDataSvc($http, $q);
        }

    }
}

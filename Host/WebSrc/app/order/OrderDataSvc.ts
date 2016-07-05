/// <reference path="../all.ts" />
module Services {
    export class OrderDataSvc {
        private getOrderListApiPath: string;
        public OrderList: Array<DataModels.Order>;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        getOrderList(accountId:number, accountType:number, active:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOrderListApiPath + "/" + accountId + "/" + accountType+"/"+active)
                .then(function (result: any) {
                    self.OrderList = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }
        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.getOrderListApiPath = "api/order/getorderlist";
            this.OrderList = new Array<DataModels.Order>();
            this.httpService = $http;
            this.qService = $q;
        }

        public static OrderDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): OrderDataSvc {
            return new OrderDataSvc($http, $q);
        }

    }
}

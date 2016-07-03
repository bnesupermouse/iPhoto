﻿/// <reference path="../../all.ts" />
module Services {
    export class CustomerDataSvc {
        private techCtmApiPath: string;
        private signOnApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        addCustomer(customer: DataModels.Customer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.techCtmApiPath, customer)
                .then(function (result) {
                    deferred.resolve();
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        signOnCustomer(customer: DataModels.Customer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.signOnApiPath, customer)
                .then(function (result) {
                    deferred.resolve();
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.techCtmApiPath = "api/customer/newaccount";
            this.signOnApiPath = "api/customer/signon";
            this.httpService = $http;
            this.qService = $q;
        }

        public static CustomerDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): CustomerDataSvc {
            return new CustomerDataSvc($http, $q);
        }

    }
}
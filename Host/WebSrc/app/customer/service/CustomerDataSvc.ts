/// <reference path="../../all.ts" />
module Services {
    export class CustomerDataSvc {
        private techCtmApiPath: string;
        private signOnApiPath: string;
        private addPhApiPath: string;
        private signOnPhApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        private SessionId: number

        cookieInfo: DataModels.CookieInfo;

        addCustomer(customer: DataModels.Customer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.techCtmApiPath, customer)
                .then(function (result) {
                    self.cookieInfo.sid = result.data.SessionId;
                    self.cookieInfo.skey = result.data.SessionKey;
                    self.cookieInfo.cid = result.data.CustomerId;
                    self.cookieInfo.cname = result.data.CustomerName;
                    deferred.resolve(self.cookieInfo);
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
                    self.cookieInfo.sid = result.data.SessionId;
                    self.cookieInfo.skey = result.data.SessionKey;
                    self.cookieInfo.cid = result.data.CustomerId;
                    self.cookieInfo.cname = result.data.CustomerName;
                    deferred.resolve(self.cookieInfo);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        addPhotographer(photographer: DataModels.Photographer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.addPhApiPath, photographer)
                .then(function (result) {
                    self.cookieInfo.sid = result.data.SessionId;
                    self.cookieInfo.skey = result.data.SessionKey;
                    self.cookieInfo.cid = result.data.PhotographerId;
                    self.cookieInfo.cname = result.data.PhotographerName;
                    deferred.resolve(self.cookieInfo);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        signOnPhotographer(photographer: DataModels.Photographer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.signOnPhApiPath, photographer)
                .then(function (result) {
                    self.cookieInfo.sid = result.data.SessionId;
                    self.cookieInfo.skey = result.data.SessionKey;
                    self.cookieInfo.cid = result.data.PhotographerId;
                    self.cookieInfo.cname = result.data.PhotographerName;
                    deferred.resolve(self.cookieInfo);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.techCtmApiPath = "api/customer/newaccount";
            this.signOnApiPath = "api/customer/signon";
            this.addPhApiPath = "api/photographer/newaccount";
            this.signOnPhApiPath = "api/photographer/signon";
            this.httpService = $http;
            this.qService = $q;
            this.cookieInfo = new DataModels.CookieInfo();
        }

        public static CustomerDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): CustomerDataSvc {
            return new CustomerDataSvc($http, $q);
        }

    }
}

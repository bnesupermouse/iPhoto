module Services {
    export class CustomerDataSvc {
        private techCtmApiPath: string;
        private signOnApiPath: string;
        private addPhApiPath: string;
        private signOnPhApiPath: string;
        private updatePhotographerApiPath: string;
        private getPhotographerDetailsApiPath: string;
        private updateCustomerApiPath: string;
        private getCustomerDetailsApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;
        ErrorNo: number;
        ErrorMsg: string;
        IsAdmin: boolean;
        IsVerified: boolean;
        PhotographerId: number;
        CustomerId: number;
        PhotographerDetails: DataModels.Photographer;
        CustomerDetails: DataModels.Customer;

        private SessionId: number

        cookieInfo: DataModels.CookieInfo;

        addCustomer(customer: DataModels.Customer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.techCtmApiPath, customer)
                .then(function (result) {
                    if (result.data.ErrorNo == 0) {
                        self.cookieInfo.sid = result.data.SessionId;
                        self.cookieInfo.skey = result.data.SessionKey;
                        self.cookieInfo.cid = result.data.CustomerId;
                        self.cookieInfo.cname = result.data.CustomerName;
                    }
                    
                    self.ErrorNo = result.data.ErrorNo;
                    self.ErrorMsg = result.data.ErrorMsg;
                    deferred.resolve(self);
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
                    if (result.data.ErrorNo == 0) {
                        self.cookieInfo.sid = result.data.SessionId;
                        self.cookieInfo.skey = result.data.SessionKey;
                        self.cookieInfo.cid = result.data.CustomerId;
                        self.cookieInfo.cname = result.data.CustomerName;
                    }
                    self.ErrorNo = result.data.ErrorNo;
                    self.ErrorMsg = result.data.ErrorMsg;
                    deferred.resolve(self);
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
                    if (result.data.ErrorNo == 0) {
                        self.cookieInfo.sid = result.data.SessionId;
                        self.cookieInfo.skey = result.data.SessionKey;
                        self.cookieInfo.cid = result.data.PhotographerId;
                        self.cookieInfo.cname = result.data.PhotographerName;
                        self.IsAdmin = result.data.IsAdmin;
                        self.IsVerified = result.data.IsVerified;
                    }
                    self.ErrorNo = result.data.ErrorNo;
                    self.ErrorMsg = result.data.ErrorMsg;
                    deferred.resolve(self);
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
                    if (result.data.ErrorNo == 0) {
                        self.cookieInfo.sid = result.data.SessionId;
                        self.cookieInfo.skey = result.data.SessionKey;
                        self.cookieInfo.cid = result.data.PhotographerId;
                        self.cookieInfo.cname = result.data.PhotographerName;
                        self.IsAdmin = result.data.IsAdmin;
                        self.IsVerified = result.data.IsVerified;
                    }
                    self.ErrorNo = result.data.ErrorNo;
                    self.ErrorMsg = result.data.ErrorMsg;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        updatePhotographer(updPhotographer: DataModels.UpdPhotographer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.updatePhotographerApiPath, updPhotographer)
                .then(function (result: any) {
                    self.PhotographerId = result.data.PhotographerId;
                    self.ErrorNo = result.data.ErrorNo;
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
                    self.PhotographerDetails = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        updateCustomer(updCustomer: DataModels.UpdCustomer): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.post(self.updateCustomerApiPath, updCustomer)
                .then(function (result: any) {
                    self.CustomerId = result.data.CustomerId;
                    self.ErrorNo = result.data.ErrorNo;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        getCustomerDetails(customerId: number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getCustomerDetailsApiPath + "/" + customerId)
                .then(function (result: any) {
                    self.CustomerDetails = result.data;
                    deferred.resolve(self);
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
            this.updatePhotographerApiPath = "api/photographer/updatephotographer";
            this.getPhotographerDetailsApiPath = "api/photographer/getphotographer";
            this.updateCustomerApiPath = "api/customer/updatecustomer";
            this.getCustomerDetailsApiPath = "api/customer/getcustomer";
            this.httpService = $http;
            this.qService = $q;
            this.cookieInfo = new DataModels.CookieInfo();
            this.IsAdmin = false;
            this.IsVerified = false;
        }

        public static CustomerDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): CustomerDataSvc {
            return new CustomerDataSvc($http, $q);
        }

    }
}

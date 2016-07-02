/// <reference path="../model/Customer.ts" />
module Services {
    export class CustomerService {
        private customers: Array<DataModels.Customer>;
        private signUpApiPath: string;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;

        addNewCustomer(customer: DataModels.Customer) {
            var self = this;
            self.httpService.post(self.signUpApiPath, customer)
                .then(function (result) {
                    alert(result);
                }, function (error) {
                    alert(error);
                });

        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.signUpApiPath = "api/customer/newaccount";
            this.httpService = $http;
            this.qService = $q;
        }

        public static CustomerServiceFactory($http: ng.IHttpService, $q: ng.IQService): CustomerService {
            return new CustomerService($http, $q);
        }

    }
}

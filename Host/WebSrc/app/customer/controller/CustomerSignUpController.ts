/// <reference path="../service/CustomerService.ts" />
/// <reference path="../model/Customer.ts" />
module Controllers {
    export interface CustomerSignUpScope extends ng.IScope {
        CustomerName: string;
        Email: string;
        Password: string;
        addVideo(): void;
    }
    export class CustomerSignUpController {
        private $scope: CustomerSignUpScope;
        private dataSvc: Services.CustomerService;

        constructor($scope: CustomerSignUpScope, customerService: Services.CustomerService) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = customerService;

            self.$scope.addVideo = function () {
                let customer = new DataModels.Customer();
                customer.CustomerName = $scope.CustomerName;
                customer.Email = $scope.Email;
                customer.Password = $scope.Password;
                self.dataSvc.addNewCustomer(customer);

            };
        }
    }
    CustomerSignUpController.$inject = ['$scope', 'customerService'];
}

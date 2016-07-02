/// <reference path="../../all.ts" />
module Controllers {
    export class SignOnCustomerCtrl {
        $scope: DataModels.ISignOnCustomerScope;
        dataSvc: Services.CustomerDataSvc;

        constructor($scope: DataModels.ISignOnCustomerScope, dataSvc: Services.CustomerDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.dataSvc = dataSvc;

            self.$scope.signOnCustomer = function () {
                let ctm = new DataModels.Customer();
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.signOnCustomer(ctm).then(function () {
                    alert("Successful");
                });

            };

            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

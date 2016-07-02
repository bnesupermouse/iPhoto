/// <reference path="../../all.ts" />
module Controllers {
    export class AddCustomerCtrl {
        $scope: DataModels.IAddCustomerScope;
        dataSvc: Services.CustomerDataSvc;

        constructor($scope: DataModels.IAddCustomerScope, dataSvc: Services.CustomerDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.dataSvc = dataSvc;

            self.$scope.addCustomer = function () {
                let ctm = new DataModels.Customer();
                ctm.CustomerName = self.$scope.CustomerName;
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.addCustomer(ctm).then(function () {
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

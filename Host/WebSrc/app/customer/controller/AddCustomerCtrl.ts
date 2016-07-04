/// <reference path="../../all.ts" />
module Controllers {
    export class AddCustomerCtrl {
        $scope: DataModels.IAddCustomerScope;
        $cookies: ng.cookies.ICookieStoreService;
        dataSvc: Services.CustomerDataSvc;

        constructor($scope: DataModels.IAddCustomerScope, $cookies: ng.cookies.ICookieStoreService, dataSvc: Services.CustomerDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;

            self.$scope.addCustomer = function () {
                let ctm = new DataModels.Customer();
                ctm.CustomerName = self.$scope.CustomerName;
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.addCustomer(ctm).then(function (res) {
                    $cookies.put("sid", String(res.sid));
                    $cookies.put("skey", String(res.skey));
                    $cookies.put("cid", String(res.cid));
                    $cookies.put("cname", String(res.cname));
                    self.$scope.CustomerName = self.$cookies.get("cname");
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

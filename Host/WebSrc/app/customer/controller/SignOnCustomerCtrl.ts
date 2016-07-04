/// <reference path="../../all.ts" />
module Controllers {
    export class SignOnCustomerCtrl {
        $scope: DataModels.ISignOnCustomerScope;
        $cookies: ng.cookies.ICookieStoreService;
        $location: ng.ILocationService;
        dataSvc: Services.CustomerDataSvc;
        
        //static $inject = ['$scope', '$cookies', 'dataSvc'];
        constructor($scope: DataModels.ISignOnCustomerScope, $cookies: ng.cookies.ICookieStoreService, $location: ng.ILocationService, dataSvc: Services.CustomerDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;

            self.$location = $location;

            self.$scope.signOnCustomer = function () {
                let ctm = new DataModels.Customer();
                ctm.Email = self.$scope.Email;
                ctm.Password = self.$scope.Password;
                dataSvc.signOnCustomer(ctm).then(function (res) {
                    $cookies.put("sid", String(res.sid));
                    $cookies.put("skey", String(res.skey));
                    $cookies.put("cid", String(res.cid));
                    $cookies.put("cname", String(res.cname));
                    self.$scope.CustomerName = self.$cookies.get("cname");
                    self.$location.path("/");
                });
            };

            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

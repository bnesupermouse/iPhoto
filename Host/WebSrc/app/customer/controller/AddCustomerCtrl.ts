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
                if (self.$scope.CustomerType == 1) {
                    let ctm = new DataModels.Customer();
                    ctm.CustomerName = self.$scope.NewCustomerName;
                    ctm.Email = self.$scope.Email;
                    ctm.Password = self.$scope.Password;
                    dataSvc.addCustomer(ctm).then(function (res) {
                        $cookies.put("ctype", String(1));
                        $cookies.put("sid", String(res.sid));
                        $cookies.put("skey", String(res.skey));
                        $cookies.put("cid", String(res.cid));
                        $cookies.put("cname", String(res.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        alert("Successful");
                    });
                }
                else {
                    let phg = new DataModels.Photographer();
                    phg.PhotographerName = self.$scope.NewCustomerName;
                    phg.Email = self.$scope.Email;
                    phg.Password = self.$scope.Password;
                    dataSvc.addPhotographer(phg).then(function (res) {
                        $cookies.put("ctype", String(2));
                        $cookies.put("sid", String(res.sid));
                        $cookies.put("skey", String(res.skey));
                        $cookies.put("cid", String(res.cid));
                        $cookies.put("cname", String(res.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        alert("Successful");
                    });
                }

            };

            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

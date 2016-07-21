module Controllers {
    export class AddCustomerCtrl {
        $scope: DataModels.IAddCustomerScope;
        $cookies: ng.cookies.ICookieStoreService;
        $location: ng.ILocationService;
        dataSvc: Services.CustomerDataSvc;

        constructor($scope: DataModels.IAddCustomerScope, $cookies: ng.cookies.ICookieStoreService, $location: ng.ILocationService, dataSvc: Services.CustomerDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;

            self.$scope.addCustomer = function () {
                if (self.$scope.CustomerType != 1 && self.$scope.CustomerType != 2) {
                    self.$scope.ErrorMsg = "Please select Customer or Photographer";
                    return;
                }
                if (self.$scope.CustomerType == 1) {
                    let ctm = new DataModels.Customer();
                    ctm.CustomerName = self.$scope.NewCustomerName;
                    ctm.Email = self.$scope.Email;
                    ctm.Password = self.$scope.Password;
                    dataSvc.addCustomer(ctm).then(function (res) {
                        if (res.ErrorNo != 0) {
                            self.$scope.ErrorMsg = res.ErrorMsg;
                            return;
                        }
                        $cookies.put("ctype", String(1));
                        $cookies.put("sid", String(res.cookieInfo.sid));
                        $cookies.put("skey", String(res.cookieInfo.skey));
                        $cookies.put("cid", String(res.cookieInfo.cid));
                        $cookies.put("cname", String(res.cookieInfo.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        self.$scope.ErrorMsg = "";
                        self.$location.path("/");
                    },
                        function (error) {
                            self.$scope.ErrorMsg = error;
                            return;
                        });
                }
                else {
                    let phg = new DataModels.Photographer();
                    phg.PhotographerName = self.$scope.NewCustomerName;
                    phg.Email = self.$scope.Email;
                    phg.Password = self.$scope.Password;
                    dataSvc.addPhotographer(phg).then(function (res) {
                        if (res.ErrorNo != 0) {
                            self.$scope.ErrorMsg = res.ErrorMsg;
                            return;
                        }
                        $cookies.put("ctype", String(2));
                        $cookies.put("sid", String(res.cookieInfo.sid));
                        $cookies.put("skey", String(res.cookieInfo.skey));
                        $cookies.put("cid", String(res.cookieInfo.cid));
                        $cookies.put("cname", String(res.cookieInfo.cname));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        self.$scope.ErrorMsg = "";
                        self.$location.path("/");
                    }, 
                        function (error) {
                            self.$scope.ErrorMsg = error;
                            return;
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

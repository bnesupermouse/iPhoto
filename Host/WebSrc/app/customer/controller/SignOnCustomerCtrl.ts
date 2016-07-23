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
                if (self.$scope.CustomerType != 1 && self.$scope.CustomerType != 2) {
                    self.$scope.ErrorMsg = "Please select Customer or Photographer";
                    return;
                }
                if (self.$scope.CustomerType == 1) {
                    let ctm = new DataModels.Customer();
                    ctm.Email = self.$scope.Email;
                    ctm.Password = self.$scope.Password;
                    dataSvc.signOnCustomer(ctm).then(function (res) {
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
                        self.$scope.AcccountId = self.$cookies.get("cid");
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
                    phg.Email = self.$scope.Email;
                    phg.Password = self.$scope.Password;
                    dataSvc.signOnPhotographer(phg).then(function (res) {
                        if (res.ErrorNo != 0) {
                            self.$scope.ErrorMsg = res.ErrorMsg;
                            return;
                        }
                        $cookies.put("ctype", String(2));
                        $cookies.put("sid", String(res.cookieInfo.sid));
                        $cookies.put("skey", String(res.cookieInfo.skey));
                        $cookies.put("cid", String(res.cookieInfo.cid));
                        $cookies.put("cname", String(res.cookieInfo.cname));
                        $cookies.put("isadmin", String(res.IsAdmin));
                        $cookies.put("isverified", String(res.IsVerified));
                        self.$scope.CustomerName = self.$cookies.get("cname");
                        self.$scope.IsAdmin = res.IsAdmin;
                        self.$scope.IsVerified = res.IsVerified;
                        self.$scope.ErrorMsg = "";
                        self.$location.path("/");
                    },
                        function (error) {
                            self.$scope.ErrorMsg = error;
                            return;
                        });
                }
            }
            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

module Controllers {
    export class ManageAccountCtrl {
        $scope: DataModels.IBaseScope;
        $cookies: ng.cookies.ICookieStoreService;
        $location: ng.ILocationService;
        dataSvc: Services.CustomerDataSvc;
        
        //static $inject = ['$scope', '$cookies', 'dataSvc'];
        constructor($scope: DataModels.IBaseScope, $cookies: ng.cookies.ICookieStoreService, $location: ng.ILocationService, dataSvc: Services.CustomerDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$scope.IsAdmin = 0;
            self.$scope.IsVerified = 0;

            self.$location = $location;
            let sid = $cookies.get("sid");
            if (sid == null) {
                self.$location.path("/signin");
            }
            else {
                self.$scope.CustomerName = $cookies.get("cname");
                self.$scope.AcccountId = $cookies.get("cid");
                let isadmin = $cookies.get("isadmin");
                let isverified = $cookies.get("isverified");

                if (isadmin == "true") {
                    self.$scope.IsAdmin = 1;
                }
                if (isverified == "true") {
                    self.$scope.IsVerified = 1;
                }
                self.$scope.CustomerType = $cookies.get("ctype");
            }
            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

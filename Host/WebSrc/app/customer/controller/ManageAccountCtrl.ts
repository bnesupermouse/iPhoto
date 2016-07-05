/// <reference path="../../all.ts" />
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

            self.$location = $location;
            let sid = $cookies.get("sid");
            if (sid == null) {
                self.$location.path("/signin");
            }
            else {
                self.$scope.CustomerName = $cookies.get("cname");
                self.$scope.AcccountId = $cookies.get("cid");
                self.$scope.CustomerType = $cookies.get("ctype");
            }
            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}

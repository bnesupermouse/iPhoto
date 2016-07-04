/// <reference path="../all.ts" />
module Controllers {
    export class MainPageCtrl {
        $scope: DataModels.IMainPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        dataSvc: Services.MainPageDataSvc;

        constructor($scope: DataModels.IMainPageScope, $cookies: ng.cookies.ICookieStoreService, dataSvc: Services.MainPageDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;

            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getMainPage().then(function (data) {
                self.$scope.CustomerName = self.$cookies.get("cname");
                self.$scope.PhotoTypes = data.PhotoTypes;
                self.$scope.Offers = data.Offers;
            });
        }
    }
}

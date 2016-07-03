/// <reference path="../all.ts" />
module Controllers {
    export class MainPageCtrl {
        $scope: DataModels.IMainPageScope;
        dataSvc: Services.MainPageDataSvc;

        constructor($scope: DataModels.IMainPageScope, dataSvc: Services.MainPageDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.dataSvc = dataSvc;

            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getMainPage().then(function (data) {
                self.$scope.Header = data;
            });
        }
    }
}

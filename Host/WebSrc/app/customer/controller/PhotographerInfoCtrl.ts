module Controllers {
    export interface IPhotographerInfoRouteParams extends ng.route.IRouteParamsService {
        photographerid: number;
    }
    export class PhotographerInfoCtrl {
        $scope: DataModels.IPhotographerPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IPhotographerInfoRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.CustomerDataSvc;
        PhotographerId: number;

        constructor($scope: DataModels.IPhotographerPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IPhotographerInfoRouteParams, $location: ng.ILocationService, dataSvc: Services.CustomerDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;

            self.$scope.updatePhotographer = function () {
                let updPhotographer = new DataModels.UpdPhotographer();
                updPhotographer.OldPhotographer = self.$scope.OldPhotographer;
                updPhotographer.NewPhotographer = self.$scope.NewPhotographer;
                updPhotographer.PhotographerId = $cookies.get("cid");
                if (updPhotographer.OldPhotographer.Status != 0 && updPhotographer.NewPhotographer.Status == 0) {
                    self.$scope.ErrorMsg = "Cannot change the status value back to New";
                    return;
                }
                if (updPhotographer.OldPhotographer != null && updPhotographer.NewPhotographer != null) {
                    updPhotographer.Action = 2;
                }
                else if (updPhotographer.NewPhotographer != null) {
                    updPhotographer.Action = 1;
                }
                else {
                    updPhotographer.Action = 3;
                }
                dataSvc.updatePhotographer(updPhotographer).then(function (res) {
                    if (res.ErrorNo != 0) {
                        self.$scope.ErrorMsg = res.ErrorMsg;
                        return;
                    }
                    if (res.ErrorNo == 0) {
                        self.$location.path("/account");
                    }
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }
            if ($cookies.get("cid") == null) {
                self.$location.path("/signin");
                return;
            }
            self.init();
        }
        private init(): void {
            var self = this;
            if (self.$routeParams.photographerid != null) {
                self.dataSvc.getPhotographerDetails(self.$routeParams.photographerid).then(function (data) {
                    self.$scope.OldPhotographer = data.PhotographerDetails;
                    self.$scope.NewPhotographer = self.clone(self.$scope.OldPhotographer);
                });
            }
        }
        public clone<T>(obj: T): T {
        var newObj: any = {};
        for (var k in obj) newObj[k] = (<any>obj)[k];
        return newObj;
    }
    }
}

module Controllers {
    export interface IPhotographerManRouteParams extends ng.route.IRouteParamsService {
        photographerid: number;
    }
    export class PhotographerManCtrl {
        $scope: DataModels.IPhotographerListPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IPhotographerManRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.PhotographerManDataSvc;
        PhotographerId: number;

        constructor($scope: DataModels.IPhotographerListPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IPhotographerManRouteParams, $location: ng.ILocationService, dataSvc: Services.PhotographerManDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.$scope.StatusFilter = -1;

            self.$scope.SearchPhotographers = function () {
                self.dataSvc.getPhotographerList(self.$scope.StatusFilter).then(function (data) {
                    self.$scope.Photographers = data.Photographers;
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
            self.dataSvc.getPhotographerList(self.$scope.StatusFilter).then(function (data) {
                self.$scope.Photographers = data.Photographers;
            });
        }
    }
}

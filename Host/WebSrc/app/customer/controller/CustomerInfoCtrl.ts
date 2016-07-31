module Controllers {
    export interface ICustomerDetailsRouteParams extends ng.route.IRouteParamsService {
        customerid: number;
    }
    export class CustomerInfoCtrl {
        $scope: DataModels.ICustomerPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: ICustomerDetailsRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.CustomerDataSvc;
        CustomerId: number;
        $filter: ng.IFilterService;

        constructor($scope: DataModels.ICustomerPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: ICustomerDetailsRouteParams, $location: ng.ILocationService, $filter: ng.IFilterService, dataSvc: Services.CustomerDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$filter = $filter;

            self.$scope.$watch('NewCustomer.OpenDate', function (newDate) {
                $scope.NewCustomer.OpenDate = $filter('date')(newDate, 'dd/MM/yyyy HH:mm:ss');
            });

            self.$scope.$watch('NewCustomer.LastLoginTime', function (newDate) {
                $scope.NewCustomer.LastLoginTime = $filter('date')(newDate, 'dd/MM/yyyy HH:mm:ss');
            });

            self.$scope.updateCustomer = function () {
                let updCustomer = new DataModels.UpdCustomer();
                updCustomer.OldCustomer = self.$scope.OldCustomer;
                updCustomer.NewCustomer = self.$scope.NewCustomer;
                updCustomer.CustomerId = $cookies.get("cid");
                if (updCustomer.OldCustomer.Status != 0 && updCustomer.NewCustomer.Status == 0) {
                    self.$scope.ErrorMsg = "Cannot change the status value back to New";
                    return;
                }
                if (updCustomer.OldCustomer != null && updCustomer.NewCustomer != null) {
                    updCustomer.Action = 2;
                }
                else if (updCustomer.NewCustomer != null) {
                    updCustomer.Action = 1;
                }
                else {
                    updCustomer.Action = 3;
                }
                dataSvc.updateCustomer(updCustomer).then(function (res) {
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
            if (self.$routeParams.customerid != null) {
                self.dataSvc.getCustomerDetails(self.$routeParams.customerid).then(function (data) {
                    self.$scope.OldCustomer = data.CustomerDetails;
                    self.$scope.NewCustomer = self.clone(self.$scope.OldCustomer);
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

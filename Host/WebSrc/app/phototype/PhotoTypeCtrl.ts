/// <reference path="../all.ts" />
module Controllers {
    export interface IPhotoTypeRouteParams extends ng.route.IRouteParamsService {
        phototypeid: number;
    }
    export class PhotoTypeCtrl {
        $scope: DataModels.IPhotoTypePageScope;
        $routeParams: IPhotoTypeRouteParams;
        dataSvc: Services.PhotoTypeDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IPhotoTypePageScope, $routeParams: IPhotoTypeRouteParams, dataSvc: Services.PhotoTypeDataSvc) {
            var self = this;

            self.$scope = $scope;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;

            self.init();
        }

        private init(): void {
            var self = this;
            self.dataSvc.getPhotoTypeOffers(self.$routeParams.phototypeid).then(function (data) {
                self.$scope.Offers = data.OfferList;
                self.$scope.PhotoTypeId = data.PhotoTypeId;
                self.$scope.PhotoTypeName = data.PhotoTypeName;
            });
        }
    }
}

module Services {
    export class OrderDataSvc {
        private getOrderListApiPath: string;
        private getOrderDetailsApiPath: string;
        private updateOrderStatusApiPath: string;
        private getOrderPhotos: string;
        private selectRawPhotosPath: string;
        private selectRetouchedPhotosPath: string;

        public OrderList: Array<DataModels.Order>;
        private httpService: ng.IHttpService;
        private qService: ng.IQService;
        public Details: DataModels.OrderDetails;
        public Photos: DataModels.PhotoInfo;
        OrderId: number;

        getOrderList(accountId:number, accountType:number, active:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOrderListApiPath + "/" + accountId + "/" + accountType+"/"+active)
                .then(function (result: any) {
                    self.OrderList = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        getOrderDetails(orderId: number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOrderDetailsApiPath + "/" + orderId)
                .then(function (result: any) {
                    self.Details = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        getMorePhotos(orderId: number, photoType: number, lastPhotoId: number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();

            self.httpService.get(self.getOrderPhotos + "/" + orderId + "/" + photoType + "/" + lastPhotoId)
                .then(function (result: any) {
                    self.Photos = result.data;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        updateOrderStatus(orderId: number, toStatus:number): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();
            let updateOrder = new DataModels.UpdateOrderStatus;
            updateOrder.OrderId = orderId;
            updateOrder.ToStatus = toStatus;
            self.httpService.post(self.updateOrderStatusApiPath, updateOrder)
                .then(function (result: any) {
                    self.OrderId = result.data.OrderId;
                    self.Details.Status = result.data.Status;
                    self.Details.StatusString = result.data.StatusString;
                    self.Details.LabelString = result.data.LabelString;
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;


        }

        selectRawPhotos(orderId: number, selectedPhotoIds:Array<number>, deSelectedPhotoIds:Array<number>): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();
            let selectPhotos = new DataModels.SelectPhotos();
            selectPhotos.OrderId = orderId;
            selectPhotos.SelectedPhotoIds = selectedPhotoIds;
            selectPhotos.DeselectedPhotoIds = deSelectedPhotoIds;
            self.httpService.post(self.selectRawPhotosPath, selectPhotos)
                .then(function (result: any) {
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        selectRetouchedPhotos(orderId: number, selectedPhotoIds: Array<number>, deSelectedPhotoIds: Array<number>): ng.IPromise<any> {
            var self = this;
            var deferred = self.qService.defer();
            let selectPhotos = new DataModels.SelectPhotos();
            selectPhotos.OrderId = orderId;
            selectPhotos.SelectedPhotoIds = selectedPhotoIds;
            selectPhotos.DeselectedPhotoIds = deSelectedPhotoIds;
            self.httpService.post(self.selectRetouchedPhotosPath, selectPhotos)
                .then(function (result: any) {
                    deferred.resolve(self);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        constructor($http: ng.IHttpService, $q: ng.IQService) {
            this.getOrderListApiPath = "api/order/getorderlist";
            this.getOrderDetailsApiPath = "api/order/getorderdetails";
            this.updateOrderStatusApiPath = "api/order/updateorderstatus";
            this.getOrderPhotos = "api/order/getorderphotos";
            this.selectRawPhotosPath = "api/order/selectrawphotos";
            this.selectRetouchedPhotosPath = "api/order/selectretouchedphotos";
            this.OrderList = new Array<DataModels.Order>();
            this.httpService = $http;
            this.qService = $q;
        }

        public static OrderDataSvcFactory($http: ng.IHttpService, $q: ng.IQService): OrderDataSvc {
            return new OrderDataSvc($http, $q);
        }

    }
}

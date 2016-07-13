module Controllers {
    export class OrderDetailsCtrl {
        $scope: DataModels.IOrderDetailsPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOrderRouteParams;
        dataSvc: Services.OrderDataSvc;

        constructor($scope: DataModels.IOrderDetailsPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOrderRouteParams, dataSvc: Services.OrderDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");

            self.$scope.confirmOrder = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.OrderConfirmed).then(function (data) {
                    //self.$scope.Details = data.Details;
                });
            }

            self.$scope.confirmRawPhotosUploaded = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.RawPhotoUploaded).then(function (data) {
                    //self.$scope.Details = data.Details;
                });
            }
            self.$scope.loadMore = function (photoType) {
                let lastPhotoId = 0;
                if (self.$scope.Details == null) {
                    self.$scope.Details = new DataModels.OrderDetails();
                }
                if (photoType == 1) {
                    if (self.$scope.Details.RawPhotos == null) {
                        self.$scope.Details.RawPhotos = new Array<DataModels.PhotoInfo>();
                    }
                    else {
                        if (self.$scope.Details.RawPhotos.length > 0) {
                            lastPhotoId = self.$scope.Details.RawPhotos[self.$scope.Details.RawPhotos.length - 1].PhotoId;
                        }
                    }
                }
                else {
                    if (self.$scope.Details.RetouchedPhotos == null) {
                        self.$scope.Details.RetouchedPhotos = new Array<DataModels.PhotoInfo>();
                    }
                    else {
                        if (self.$scope.Details.RetouchedPhotos.length > 0) {
                            lastPhotoId = self.$scope.Details.RetouchedPhotos[self.$scope.Details.RetouchedPhotos.length - 1].PhotoId;
                        }
                    }
                }
                self.dataSvc.getMorePhotos(self.$routeParams.orderid, photoType, lastPhotoId).then(function (data) {
                    if (photoType == 1) {
                        if (self.$scope.Details.RawPhotos == null) {
                            self.$scope.Details.RawPhotos = new Array<DataModels.PhotoInfo>();
                        }
                        for (var i = 0; i < data.Photos.length; i++) {
                            self.$scope.Details.RawPhotos.push(data.Photos[i]);
                        }
                        
                    }
                    else {
                        if (self.$scope.Details.RetouchedPhotos == null) {
                            self.$scope.Details.RetouchedPhotos = new Array<DataModels.PhotoInfo>();
                        }
                        for (var i = 0; i < data.Photos.length; i++) {
                            self.$scope.Details.RetouchedPhotos.push(data.Photos[i]);
                        }
                        
                    }
                    
                });
            }
            self.$scope.setFiles = function () {
                self.$scope.PhotoList = new Array<DataModels.Photo>();
                self.$scope.$apply();
                let files = (<HTMLInputElement>document.getElementById("fileupload")).files;
                for (var i = 0; i < files.length; i++) {
                    let photo = new DataModels.Photo();
                    photo.file = files[i];
                    photo.name = files[i].name;
                    photo.size = files[i].size;
                    photo.type = files[i].type;
                    self.$scope.PhotoList.push(photo);
                }
                self.$scope.$apply();
            }


            self.$scope.uploadPhotos = function () {
                for (var i = 0; i < self.$scope.PhotoList.length; i++) {
                    self.uploadIndividualPhoto(self.$scope.PhotoList[i], i, self.$scope.Details.Status);
                }
            }
            self.init();
        }
        private uploadIndividualPhoto(photo: DataModels.Photo, index: number, status: number) {
            var self = this;
            //Create XMLHttpRequest Object
            var reqObj = new XMLHttpRequest();

            //event Handler
            reqObj.upload.addEventListener("progress", uploadProgress, false)
            reqObj.addEventListener("load", uploadComplete, false)
            reqObj.addEventListener("error", uploadFailed, false)
            reqObj.addEventListener("abort", uploadCanceled, false)


            //open the object and set method of call(get/post), url to call, isasynchronous(true/False)
            reqObj.open("POST", "/api/order/UploadPhoto", true);

            //Set Other header like file name,size and type
            reqObj.setRequestHeader('X-File-Name', photo.name);
            reqObj.setRequestHeader('X-File-Type', photo.type);
            reqObj.setRequestHeader('X-File-Size', photo.size.toString());
            reqObj.setRequestHeader('X-Order-Status', status.toString());
            reqObj.setRequestHeader('X-Order-Id', self.$scope.Details.SerialNo.toString());

            // send the file
            reqObj.send(photo.file);
            //self.dataSvc.uploadAPhoto(fdata);
            function uploadProgress(evt) {
                if (evt.lengthComputable) {

                    var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);

                    document.getElementById("P" + index.toString()).innerHTML = uploadProgressCount.toString();

                    if (uploadProgressCount == 100) {
                        document.getElementById('P' + index).innerHTML =
                            '<i class="fa fa-refresh fa-spin" style="color:maroon;"></i>';
                    }

                }
            }

            function uploadComplete(evt) {
                /* This event is raised when the server  back a response */

                document.getElementById('P' + index).innerHTML = 'Saved';
                //$scope.NoOfFileSaved++;
                //$scope.$apply();
            }

            function uploadFailed(evt) {
                document.getElementById('P' + index).innerHTML = 'Upload Failed..';
            }

            function uploadCanceled(evt) {

                document.getElementById('P' + index).innerHTML = 'Canceled....';
            }
        }
        private init(): void {
            var self = this;
            self.dataSvc.getOrderDetails(self.$routeParams.orderid).then(function (data) {
                self.$scope.Details = data.Details;
            });
        }
    }
}

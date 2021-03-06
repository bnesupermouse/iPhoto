﻿module Controllers {
    export class OrderDetailsCtrl {
        $scope: DataModels.IOrderDetailsPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOrderRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.OrderDataSvc;

        constructor($scope: DataModels.IOrderDetailsPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOrderRouteParams, $location: ng.ILocationService, dataSvc: Services.OrderDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.CustomerName = $cookies.get("cname");
            self.$scope.AcccountId = $cookies.get("cid");
            self.$scope.CustomerType = $cookies.get("ctype");
            self.$scope.busy = false;
            
            self.$scope.confirmOrder = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.OrderConfirmed).then(function (res) {
                    if (res.ErrorNo != 0) {
                        self.$scope.ErrorMsg = res.ErrorMsg;
                        return;
                    }
                    self.$location.path("/orderlist");
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.rejectOrder = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.OrderRejected).then(function (res) {
                    if (res.ErrorNo != 0) {
                        self.$scope.ErrorMsg = res.ErrorMsg;
                        return;
                    }
                    self.$location.path("/orderlist");
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.confirmRawPhotosUploaded = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.RawPhotoUploaded).then(function (res) {
                    if (res.ErrorNo != 0) {
                        self.$scope.ErrorMsg = res.ErrorMsg;
                        return;
                    }
                    self.$location.path("/orderlist");
                });
            }

            self.$scope.confirmRetouchedPhotosUploaded = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.RetouchedPhotoUploaded).then(function (res) {
                    if (res.ErrorNo != 0) {
                        self.$scope.ErrorMsg = res.ErrorMsg;
                        return;
                    }
                    self.$location.path("/orderlist");
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.confirmPhotoSelected = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.PhotoSelected).then(function (data) {
                    if (data.ErrorNo != 0) {
                        self.$scope.ErrorMsg = data.ErrorMsg;
                        return;
                    }
                    self.$scope.Details.Status = data.Details.Status;
                    self.$scope.Details.StatusString = data.Details.StatusString;
                    self.$scope.Details.LabelString = data.Details.LabelString;
                    self.$location.path("/orderlist");
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.finaliseOrder = function () {
                self.dataSvc.updateOrderStatus(self.$routeParams.orderid, DataModels.OrderStatusValue.OrderFinalised).then(function (data) {
                    if (data.ErrorNo != 0) {
                        self.$scope.ErrorMsg = data.ErrorMsg;
                        return;
                    }
                    self.$scope.Details.Status = data.Details.Status;
                    self.$scope.Details.StatusString = data.Details.StatusString;
                    self.$scope.Details.LabelString = data.Details.LabelString;
                    self.$location.path("/orderlist");
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.loadMore = function (photoType) {
                self.$scope.busy = true;
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
                    self.$scope.busy = false;
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

            self.$scope.selectRawPhotos = function () {
                let selectedPhotoIds = new Array<number>();
                for (var i = 0; i < self.$scope.Details.RawPhotos.length; i++) {
                    if (!self.$scope.Details.RawPhotos[i].Selected && self.$scope.Details.RawPhotos[i].NewSelected) {
                        selectedPhotoIds.push(self.$scope.Details.RawPhotos[i].PhotoId);
                    }
                    
                }
                let deSelectedPhotoIds = new Array<number>();
                for (var i = 0; i < self.$scope.Details.RawPhotos.length; i++) {
                    if (self.$scope.Details.RawPhotos[i].Selected && !self.$scope.Details.RawPhotos[i].NewSelected) {
                        deSelectedPhotoIds.push(self.$scope.Details.RawPhotos[i].PhotoId);
                    }

                }
                self.dataSvc.selectRawPhotos(self.$routeParams.orderid, selectedPhotoIds, deSelectedPhotoIds).then(function (data) {
                    if (data.ErrorNo != 0) {
                        self.$scope.ErrorMsg = data.ErrorMsg;
                        return;
                    }
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.selectRetouchedPhotos = function () {
                let selectedPhotoIds = new Array<number>();
                for (var i = 0; i < self.$scope.Details.RetouchedPhotos.length; i++) {
                    if (!self.$scope.Details.RetouchedPhotos[i].Confirmed && self.$scope.Details.RetouchedPhotos[i].NewConfirmed) {
                        selectedPhotoIds.push(self.$scope.Details.RetouchedPhotos[i].PhotoId);
                    }

                }
                let deSelectedPhotoIds = new Array<number>();
                for (var i = 0; i < self.$scope.Details.RetouchedPhotos.length; i++) {
                    if (self.$scope.Details.RetouchedPhotos[i].Confirmed && !self.$scope.Details.RetouchedPhotos[i].NewConfirmed) {
                        deSelectedPhotoIds.push(self.$scope.Details.RetouchedPhotos[i].PhotoId);
                    }

                }
                self.dataSvc.selectRetouchedPhotos(self.$routeParams.orderid, selectedPhotoIds, deSelectedPhotoIds).then(function (data) {
                    if (data.ErrorNo != 0) {
                        self.$scope.ErrorMsg = data.ErrorMsg;
                        return;
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
            self.$scope.busy = true;
            self.dataSvc.getOrderDetails(self.$routeParams.orderid).then(function (data) {
                self.$scope.Details = data.Details;
                self.$scope.busy = false;
                self.$scope.config = {
                    startDate: self.$scope.Details.AppointmentTime.toString(),
                    viewType: "Day"
                };
            });
        }
    }
}

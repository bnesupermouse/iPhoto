module Controllers {
    export interface IOfferDetailsRouteParams extends ng.route.IRouteParamsService {
        offerid: number;
    }
    export class OfferDetailsCtrl {
        $scope: DataModels.IOfferPageScope;
        $cookies: ng.cookies.ICookieStoreService;
        $routeParams: IOfferDetailsRouteParams;
        $location: ng.ILocationService;
        dataSvc: Services.OfferDetailsDataSvc;
        PhotoTypeId: number;

        constructor($scope: DataModels.IOfferPageScope, $cookies: ng.cookies.ICookieStoreService, $routeParams: IOfferDetailsRouteParams, $location: ng.ILocationService, dataSvc: Services.OfferDetailsDataSvc) {
            var self = this;
            self.$scope = $scope;
            self.$cookies = $cookies;
            self.$location = $location;
            self.dataSvc = dataSvc;
            self.$routeParams = $routeParams;
            self.$scope.placeOrder = function () {
                let placeOrder = new DataModels.PlaceOrder();
                self.$scope.AcccountId = $cookies.get("cid");
                placeOrder.CustomerId = self.$scope.AcccountId;
                placeOrder.OfferId = self.$scope.OfferDetails.OfferId;
                placeOrder.AppointmentDate = self.$scope.AppointmentDate;
                dataSvc.placeOrder(placeOrder).then(function (res) {
                    let orderId = res;
                    self.$location.path("/orderpayment/" + orderId);
                });
            }

            self.$scope.addOffer = function () {
                let ctype = $cookies.get("ctype");
                if (ctype == 2) {
                    self.$scope.OfferDetails.PhotographerId = $cookies.get("cid");
                    dataSvc.addOffer(self.$scope.OfferDetails).then(function (res) {
                        self.$scope.OfferDetails.OfferId = res;
                        if (self.$scope.OfferDetails.OfferPics.length > 0) {
                            self.$scope.uploadPhotos();
                        }
                    });
                }
            }

            self.$scope.setFiles = function () {
                if (self.$scope.OfferDetails == null) {
                    self.$scope.OfferDetails = new DataModels.Offer;
                }
                self.$scope.OfferDetails.OfferPics = new Array<DataModels.PicInfo>();
                self.$scope.$apply();
                let files = (<HTMLInputElement>document.getElementById("fileupload")).files;
                for (var i = 0; i < files.length; i++) {
                    let photo = new DataModels.PicInfo();
                    photo.file = files[i];
                    photo.PictureName = files[i].name;
                    photo.size = files[i].size;
                    photo.type = files[i].type;
                    self.$scope.OfferDetails.OfferPics.push(photo);
                }
                self.$scope.$apply();
            }


            self.$scope.uploadPhotos = function () {
                for (var i = 0; i < self.$scope.OfferDetails.OfferPics.length; i++) {
                    self.uploadIndividualPhoto(self.$scope.OfferDetails.OfferPics[i], i);
                }
            }

            self.$scope.loadMorePhotoPics = function () {
                self.$scope.busy = true;
                let lastPicId = 0;
                if (self.$scope.OfferDetails == null) {
                    self.$scope.OfferDetails = new DataModels.Offer();
                }
                if (self.$scope.OfferDetails.OfferPics == null) {
                    self.$scope.OfferDetails.OfferPics = new Array<DataModels.PicInfo>();
                }
                else {
                    if (self.$scope.OfferDetails.OfferPics.length > 0) {
                        lastPicId = self.$scope.OfferDetails.OfferPics[self.$scope.OfferDetails.OfferPics.length - 1].PictureId;
                    }
                }
                self.dataSvc.getMoreOfferPics(self.$routeParams.offerid, lastPicId).then(function (data) {
                    if (self.$scope.OfferDetails.OfferPics == null) {
                        self.$scope.OfferDetails.OfferPics = new Array<DataModels.PicInfo>();
                        }
                        for (var i = 0; i < data.Pics.length; i++) {
                            self.$scope.OfferDetails.OfferPics.push(data.Pics[i]);
                        }
                    self.$scope.busy = false;
                });
            }


            self.init();
        }


        private uploadIndividualPhoto(photo: DataModels.PicInfo, index: number) {
            var self = this;
            //Create XMLHttpRequest Object
            var reqObj = new XMLHttpRequest();

            //event Handler
            reqObj.upload.addEventListener("progress", uploadProgress, false)
            reqObj.addEventListener("load", uploadComplete, false)
            reqObj.addEventListener("error", uploadFailed, false)
            reqObj.addEventListener("abort", uploadCanceled, false)


            //open the object and set method of call(get/post), url to call, isasynchronous(true/False)
            reqObj.open("POST", "/api/offer/UploadPhoto", true);

            //Set Other header like file name,size and type
            reqObj.setRequestHeader('X-File-Name', photo.PictureName);
            reqObj.setRequestHeader('X-File-Type', photo.type);
            reqObj.setRequestHeader('X-File-Size', photo.size.toString());
            reqObj.setRequestHeader('X-Order-Id', self.$scope.OfferDetails.OfferId.toString());

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
            if (self.$routeParams.offerid != null) {
                self.dataSvc.getOfferDetails(self.$routeParams.offerid).then(function (data) {
                    self.$scope.OfferDetails = data.OfferDetails;
                });
            }
            else {
                self.dataSvc.getPhotoTypes().then(function (data) {
                    self.$scope.PhotoTypes = data.PhotoTypes;
                });
            }
        }
    }
}

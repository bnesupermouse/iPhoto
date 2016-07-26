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
            self.$scope.IsAdmin = 0;
            if ($cookies.get("isadmin") == "true") {
                self.$scope.IsAdmin = 1;
            }
            self.$scope.placeOrder = function () {
                let placeOrder = new DataModels.PlaceOrder();
                self.$scope.AcccountId = $cookies.get("cid");
                let cType = $cookies.get("ctype");
                if (self.$scope.AcccountId == null) {
                    self.$scope.ErrorMsg = "Please sign in first!";
                    return;
                }
                if (self.$scope.AppointmentDate == null) {
                    self.$scope.ErrorMsg = "Please select the appointment date!";
                    return;
                }
                let now = new Date();
                if (self.$scope.AppointmentDate < new Date(now.setDate(now.getDate() + 1))) {
                    self.$scope.ErrorMsg = "Invalid Appointment Time";
                    return;
                }
                if (cType == 2) {
                    self.$scope.ErrorMsg = "Photographer is not allowed to place order at the moment!";
                    return;
                }
                placeOrder.CustomerId = self.$scope.AcccountId;
                placeOrder.OfferId = self.$scope.OfferDetails.OfferId;
                placeOrder.AppointmentDate = self.$scope.AppointmentDate;
                dataSvc.placeOrder(placeOrder).then(function (res) {
                    if (res.ErrorNo != 0) {
                        self.$scope.ErrorMsg = res.ErrorMsg;
                        return;
                    }
                    let orderId = res.OrderId;
                    self.$location.path("/orderdetails-0/" + orderId);
                },
                    function (error) {
                        self.$scope.ErrorMsg = error;
                        return;
                    });
            }

            self.$scope.updateOffer = function () {
                let ctype = $cookies.get("ctype");
                if (ctype == 2) {
                    let updOffer = new DataModels.UpdOffer();
                    updOffer.OldOffer = self.$scope.OldOffer;
                    updOffer.NewOffer = self.$scope.OfferDetails;
                    if (updOffer.OldOffer != null && updOffer.NewOffer != null) {
                        updOffer.Action = 2;
                    }
                    else if (updOffer.NewOffer != null) {
                        updOffer.Action = 1;
                    }
                    else {
                        updOffer.Action = 3;
                    }
                    updOffer.PhotographerId = $cookies.get("cid");
                    dataSvc.updateOffer(updOffer).then(function (res) {
                        if (res.ErrorNo != 0) {
                            self.$scope.ErrorMsg = res.ErrorMsg;
                            return;
                        }
                        self.$scope.OfferDetails.OfferId = res.OfferDetails.OfferId;
                        if (self.$scope.OfferDetails.PicList != null && self.$scope.OfferDetails.PicList.length > 0) {
                            self.$scope.uploadPhotos();
                        }
                        self.$location.path("/account");
                    },
                        function (error) {
                            self.$scope.ErrorMsg = error;
                            return;
                        });
                }
            }

            self.$scope.setFiles = function () {
                if (self.$scope.OfferDetails == null) {
                    self.$scope.OfferDetails = new DataModels.Offer;
                }
                self.$scope.OfferDetails.PicList = new Array<DataModels.PicInfo>();
                let files = (<HTMLInputElement>document.getElementById("fileupload")).files;
                for (var i = 0; i < files.length; i++) {
                    let photo = new DataModels.PicInfo();
                    photo.file = files[i];
                    photo.PictureName = files[i].name;
                    photo.size = files[i].size;
                    photo.type = files[i].type;
                    self.$scope.OfferDetails.PicList.push(photo);
                }
                self.$scope.$apply();
            }


            self.$scope.uploadPhotos = function () {
                for (var i = 0; i < self.$scope.OfferDetails.PicList.length; i++) {
                    self.uploadIndividualPhoto(self.$scope.OfferDetails.PicList[i], i);
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

            if ($location.path().indexOf("/offerdetails/") < 0) {
                if ($cookies.get("cid") == null) {
                    self.$location.path("/signin");
                }
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
                    self.$scope.OldOffer = self.clone(self.$scope.OfferDetails);
                    
                    self.dataSvc.getPhotoTypes().then(function (data) {
                        self.$scope.PhotoTypes = data.PhotoTypes;
                        for (var i = 0; i < self.$scope.PhotoTypes.length; i++) {
                            if (self.$scope.PhotoTypes[i].PhotoTypeId == self.$scope.OfferDetails.PhotoTypeId) {
                                self.$scope.PhotoTypeName = self.$scope.PhotoTypes[i].PhotoTypeName;
                            }
                        }
                    });
                });
            }
            else {
                self.dataSvc.getPhotoTypes().then(function (data) {
                    self.$scope.PhotoTypes = data.PhotoTypes;
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

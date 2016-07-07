/// <reference path="../all.ts" />
module DataModels {
    export enum OrderStatusValue {
        OrderPending,// -- Ctm
        OrderConfirmed,// --Ph
        RawPhotoUploading,// --Ph
        RawPhotoUploaded,// --Ph
        PhotoSelecting,// --Ctm
        PhotoSelected,// --Ctm
        RetouchedPhotoUploading,// --Ph
        RetouchedPhotoUploaded,// --Ph
        RetouchedPhotoConfirming,// --Ctm
        OrderFinalised,// --Ctm
        OrderRejected,//Ph
        OrderCancelled//Ctm
    }
    export interface IDictionary {
        [index: number]: string;
    }
}
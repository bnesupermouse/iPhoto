module DataModels {
    export enum OrderStatusValue {
        OrderPending,// -- Ctm
        OrderConfirmed,// --Ph
        RawPhotoUploaded,// --Ph
        PhotoSelected,// --Ctm
        RetouchedPhotoUploaded,// --Ph
        OrderFinalised,// --Ctm
        OrderRejected,//Ph
        OrderCancelled//Ctm
    }
    export interface IDictionary {
        [index: number]: string;
    }
}
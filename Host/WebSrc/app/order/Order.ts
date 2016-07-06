/// <reference path="../all.ts" />
module DataModels {
    export class Order {
        SerialNo: number;
        OfferId: number;
        OfferName: string;
        CustomerId: number;
        CustomerName: string;
        PhotographerId: number;
        PhotographerName: string;
        AppointmentTime: string;
        OrderTime: string;
        Amount: number;
        Status: number;
        StatusString: string;
        LabelString: string;
    }
}
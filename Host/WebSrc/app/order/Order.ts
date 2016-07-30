module DataModels {
    export class Order {
        SerialNo: number;
        OfferId: number;
        OfferName: string;
        CustomerId: number;
        CustomerName: string;
        Phone: string;
        PhotographerId: number;
        PhotographerName: string;
        AppointmentTime: string;
        OrderTime: string;
        Amount: number;
        Status: number;
        StatusString: string;
        LabelString: string;
        Paid: number;
        RawArchived: number;
        RawZip: string;
        RetouchedArchived: number;
        RetouchedZip: string;
    }
}
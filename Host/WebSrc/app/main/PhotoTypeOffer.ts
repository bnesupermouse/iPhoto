module DataModels {
    export class PhotoTypeOffer {
        PhotoTypeId: number;
        PhotoTypeName: string;
        Offers: Array<Offer>;
    }
}
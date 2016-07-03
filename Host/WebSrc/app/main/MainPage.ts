/// <reference path="../all.ts" />
module DataModels {
    export class Offer {
        OfferName: string;
        OfferId: number;
        Description: string;
        Price: number;
        OfferPics: Array<string>;
        SortOrder: number;
    }
    export interface IOfferPageScope extends IBaseScope {
        OfferDetails: Offer;
    }
    export interface IMainPageScope extends IBaseScope {
        Offers: Array<Offer>;
    }

    export interface IPhotoTypePageScope extends IMainPageScope {
        PhotoTypeId: number;
        PhotoTypeName: string;
    }
}
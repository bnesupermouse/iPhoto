/// <reference path="../all.ts" />
module DataModels {
    export class Offer {
        OfferName: string;
        SortOrder: number;
    }
    export interface IMainPageScope extends IBaseScope {
        Offers: Array<Offer>;
    }
}
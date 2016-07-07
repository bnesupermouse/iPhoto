/// <reference path="../all.ts" />
module DataModels {
    export class OrderDetails extends Order {
        RawPhotos: Array<string>;
        RetouchedPhotos: Array<string>;
    }
}
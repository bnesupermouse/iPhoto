module DataModels {
    export class OrderDetails extends Order {
        RawPhotos: Array<PhotoInfo>;
        RetouchedPhotos: Array<PhotoInfo>;
    }
}
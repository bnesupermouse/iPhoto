module DataModels {
    export class SelectPhotos {
        OrderId: number;
        SelectedPhotoIds: Array<number>;
        DeselectedPhotoIds: Array<number>;
    }
}
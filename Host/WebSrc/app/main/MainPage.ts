module DataModels {
    export class Offer {
        OfferName: string;
        OfferId: number;
        Description: string;
        Price: number;
        OfferPics: Array<PicInfo>;
        SortOrder: number;
    }
    export interface IBaseScope extends ng.IScope {
        AcccountId: number;
        CustomerName: string;
        CustomerType: number;
    }
    export interface IAddCustomerScope extends IBaseScope {
        NewCustomerName: string;
        Email: string;
        Password: string;
        addCustomer(): void;
    }
    export interface ISignOnCustomerScope extends IBaseScope {
        Email: string;
        Password: string;
        signOnCustomer(): void;
    }
    export interface IMainPageScope extends IBaseScope {
        PhotoTypes: Array<PhotoType>;
        Offers: Array<Offer>;
    }
    export class MainContent{
        CustomerName: string;
        PhotoTypes: Array<PhotoType>;
        Offers: Array<Offer>;
    }
    export interface IPhotoTypePageScope extends IBaseScope {
        PhotoTypeId: number;
        PhotoTypeName: string;
        Offers: Array<Offer>;
    }
    export interface IOfferPageScope extends IBaseScope {
        OfferDetails: Offer;
        AppointmentDate: Date;
        placeOrder(): void;
        loadMorePhotoPics(): void;
        busy: boolean;
    }
    export interface IOrderListPageScope extends IBaseScope {
        Orders: Array<Order>;
    }

    export interface IPaymentPageScope extends IBaseScope {
        CardInfo: PayOrder;
        payOrder(): void;
    }

    export interface IOrderDetailsPageScope extends IBaseScope {
        Selector: string;
        Details: OrderDetails;
        PhotoList: Array<Photo>;
        confirmOrder(): void;
        confirmRawPhotosUploaded(): void;
        setFiles(): void;
        uploadPhotos(): void;
        selectRawPhotos(): void;
        confirmPhotoSelected(): void;
        loadMore(photoType: number): void;
        busy: boolean;
    }
}
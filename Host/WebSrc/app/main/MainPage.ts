module DataModels {
    export class Offer {
        OfferName: string;
        PhotographerId: number;
        OfferId: number;
        PhotoTypeId: number;
        Description: string;
        Price: number;
        AdditionalRetouchPrice: number;
        StartTime: Date;
        EndTime: Date;
        NoServicer: number;
        MaxPeople: number;
        NoRawPhoto: number;
        NoRetouchedPhoto: number;
        NoMakeup: number;
        NoCostume: number;
        NoVenue: number;
        DurationHour: number;
        Comment: string;
        OfferPics: Array<PicInfo>;
        PicList: Array<PicInfo>;
        SortOrder: number;
    }
    export interface IBaseScope extends ng.IScope {
        AcccountId: number;
        CustomerName: string;
        CustomerType: number;
        IsAdmin: number;
        IsVerified: number;
        ErrorMsg: string;
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
        PhotoTypeOffers: Array<PhotoTypeOffer>;
    }
    export class MainContent{
        CustomerName: string;
        PhotoTypes: Array<PhotoType>;
        PhotoTypeOffers: Array<PhotoTypeOffer>;
    }
    export interface IPhotoTypePageScope extends IBaseScope {
        PhotoTypeId: number;
        PhotoTypeName: string;
        Offers: Array<Offer>;
        LowerRange: number;
        UpperRange: number;
        PriceFilter: number;
        SearchOffer(): void;
        loadMoreOffers(): void;
        busy: boolean;
    }
    export interface IOfferPageScope extends IBaseScope {
        OfferDetails: Offer;
        OldOffer: Offer;
        AppointmentDate: Date;
        PhotoTypes: Array<PhotoType>;
        placeOrder(): void;
        loadMorePhotoPics(): void;
        updateOffer(): void;
        setFiles(): void;
        uploadPhotos(): void;
        busy: boolean;
        PhotoTypeName: string;
    }
    export interface IOfferListPageScope extends IBaseScope {
        Offers: Array<Offer>;
        StatusFilter: number;
        SearchOffers(): void;
    }
    export interface IPhotographerListPageScope extends IBaseScope {
        Photographers: Array<Photographer>;
        StatusFilter: number;
        SearchPhotographers(): void;
    }
    export interface IOrderListPageScope extends IBaseScope {
        Orders: Array<Order>;
        StatusFilter: number;
        SearchOrders(): void;
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
        rejectOrder(): void;
        confirmRawPhotosUploaded(): void;
        setFiles(): void;
        uploadPhotos(): void;
        selectRawPhotos(): void;
        selectRetouchedPhotos(): void;
        confirmPhotoSelected(): void;
        loadMore(photoType: number): void;
        confirmRetouchedPhotosUploaded(): void;
        finaliseOrder(): void;
        busy: boolean;
        config: any;
    }
    export interface IPhotographerPageScope extends IBaseScope {
        OldPhotographer: Photographer;
        NewPhotographer: Photographer;
        updatePhotographer(): void;
    }
}
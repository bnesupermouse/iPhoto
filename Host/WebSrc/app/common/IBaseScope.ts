/// <reference path="../all.ts" />
module DataModels {
    export class PhotoType {
        PhotoTypeId: number;
        PhotoTypeName: string;
        SortOrder: number;
    }
    export class NavigationHeader {
        CustomerName: string;
        PhotoTypes: Array<PhotoType>;
    }
    export interface IBaseScope extends ng.IScope {
        Header: NavigationHeader;
    }
}
﻿/// <reference path="../all.ts" />
module DataModels {
    export class PhotoInfo {
        PhotoId: number;
        PhotoName: string;
        Path: string;
        Selected: boolean;
        Retouched: boolean;
        Confirmed: boolean;
    }
}
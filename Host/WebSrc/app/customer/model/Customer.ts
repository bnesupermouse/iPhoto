/// <reference path="../../all.ts" />
module DataModels {
    export class Customer {
        public CustomerId: number;
        public Email: string;
        public CustomerName: string;
        public Password: string;
        public Gender: number;
        public Age: number;
        public Address: string;
        public Phone: string;
        public OpenDate: string;
        public LastLoginTime: string;
        public Status: number;
    }
    export class CookieInfo {
        sid: number;
        skey: string;
        cid: number;
        cname: string;
    }
}
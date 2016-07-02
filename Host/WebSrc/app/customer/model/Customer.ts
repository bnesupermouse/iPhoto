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
    export interface IAddCustomerScope extends ng.IScope {
        CustomerName: string;
        Email: string;
        Password: string;
        addCustomer(): void;
    }
    export interface ISignOnCustomerScope extends ng.IScope {
        Email: string;
        Password: string;
        signOnCustomer(): void;
    }
}
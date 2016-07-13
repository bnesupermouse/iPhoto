module DataModels {
    export class PayOrder {
        public CustomerId: number;
        public OrderId: number;
        CardNumber: string;
        Month: string;
        Year: string;
        CVC: string;
        Name: string;
    }
}
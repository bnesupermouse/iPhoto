﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Host;
using HostDB;
using HostMessage.Responses;
using Host.Common;

namespace Host
{
    public class TxPlaceOrder:Tx
    {
        public TxPlaceOrder()
        {
            TxnId = 1;
        }
        public UpdEntity UpdOrder { get; set; }

        public override Result Validate()
        {
            var curReq = request as PlaceOrder;

            //Construct new Order
            CustomerOrder order = new CustomerOrder();
            order.PhotographerId = curReq.PhotographerId;
            order.CustomerId = curReq.CustomerId;
            order.OfferId = curReq.OfferId;
            order.AppointmentTime = DateTime.Now;
            order.OrderTime = DateTime.Now;
            order.Status = (int)OrderStatus.OrderPending;

            //Check Session
            var res = UpdateCustomerSession(true);
            if(res != Result.Success)
            {
                return res;
            }
            res = ValidateOrderInfo(order);
            if (res != Result.Success)
            {
                return res;
            }
            Photographer ph = new Photographer();
            ph.PhotographerId = order.PhotographerId;
            ph = ph.Fetch() as Photographer;
            //Check Offer
            Offer offer = new Offer();
            offer.OfferId = curReq.OfferId;
            offer = offer.Fetch() as Offer;

            order.Amount = offer.Price * 1;
            order.PhotographerPay = order.Amount * (decimal)((double)ph.PayRate / (double)100.0);

            Data.AddNew(null, order);

            //Update Account
            Account acc = new Account();
            acc.AccountId = 1;
            acc = acc.Fetch() as Account;
            if(acc == null)
            {
                acc = new Account();
                acc.AccountId = 1;
                acc.Balance += order.Amount;
                acc.PhotographerPay = 0;
                acc.PendingPay += order.PhotographerPay.Value;
                acc.Expense = 0;
                Data.AddNew(null, acc);
            }
            else
            {
                var newAcc = acc.Clone() as Account;
                newAcc.Balance += order.Amount;
                newAcc.PendingPay += order.PhotographerPay.Value;
                Data.AddNew(acc, newAcc);
            }

            //Update PhotographerAccount
            PhotographerAccount pa = new PhotographerAccount();
            pa.PhotographerId = order.PhotographerId;
            pa = pa.Fetch() as PhotographerAccount;
            if (pa == null)
            {
                return Result.Failed;
            }
            var newPa = pa.Clone() as PhotographerAccount;
            newPa.TotalBalance += order.PhotographerPay.Value;
            newPa.PendingBalance += order.PhotographerPay.Value;
            Data.AddNew(pa, newPa);
            return Result.Success;
        }
        public override Result Prepare()
        {
            var order = Data.GetEntityListByType<CustomerOrder>().FirstOrDefault();
            if(order != null)
            {
                (order.NewEntity as CustomerOrder).SerialNo = GetNextId<CustomerOrder>();
                var resp = new PlaceOrderResponse();
                resp.OrderId = (order.NewEntity as CustomerOrder).SerialNo;
                response = resp;
            }
            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}

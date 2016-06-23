using System;
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
    public class TxCancelOrder : Tx
    {
        public TxCancelOrder()
        {
            TxnId = 1;
        }
        public UpdEntity UpdOrder { get; set; }

        public override Result Validate()
        {
            var curReq = request as CustomerCancelOrder;
            //Check Session
            var res = UpdateCustomerSession(true);
            if (res != Result.Success)
            {
                return res;
            }
            
            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if(order == null)
            {
                return Result.Failed;
            }
            res = ValidateOrderInfo(order);
            if (res != Result.Success)
            {
                return res;
            }
            //Update Order
            var newOrder = order.Clone() as CustomerOrder;
            newOrder.Status = (int)OrderStatus.OrderCancelled;
            Data.AddNew(order, newOrder);

            //Update Account
            Account acc = new Account();
            acc.AccountId = 1;
            acc = acc.Fetch() as Account;
            if(acc == null)
            {
                return Result.Failed;
            }
            else
            {
                var newAcc = acc.Clone() as Account;
                newAcc.Balance -= order.Amount;
                newAcc.PendingPay -= order.PhotographerPay.Value;
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
            newPa.TotalBalance -= order.PhotographerPay.Value;
            newPa.PendingBalance -= order.PhotographerPay.Value;
            Data.AddNew(pa, newPa);
            return Result.Success;
        }
        public override Result Prepare()
        {
            //DO BANK TRANSFER

            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}

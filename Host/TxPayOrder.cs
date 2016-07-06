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
    public class TxPayOrder:Tx
    {
        public TxPayOrder()
        {
            TxnId = 1;
        }
        public UpdEntity UpdOrder { get; set; }

        public override Result Validate()
        {
            var curReq = request as PayOrder;
            CustomerId = curReq.CustomerId;
            //Check Customer
            Customer ctm = new Customer();
            ctm.CustomerId = CustomerId;
            ctm = ctm.Fetch() as Customer;
            if(ctm == null)
            {
                return Result.Failed;
            }

            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = curReq.OrderId;
            order = order.Fetch() as CustomerOrder;
            if(order == null)
            {
                return Result.Failed;
            }
            if(order.Status != (int)OrderStatus.OrderPending)
            {
                return Result.Failed;
            }
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


            try
            {
                var tokenId = Utility.GetTokenId(curReq);
                if(tokenId == null)
                {
                    return Result.Failed;
                }
                var chargeId = Utility.ChargeCustomer(tokenId.Result, (int)(order.Amount * 100));
            }
            catch (Exception e)
            {
                return Result.Failed;
            }

            var newOrder = order.Clone() as CustomerOrder;
            newOrder.Paid = true;
            Data.AddNew(order, newOrder);

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

            var resp = new PlaceOrderResponse();
            resp.OrderId = order.SerialNo;
            response = resp;
            return Result.Success;
        }
        public override Result Prepare()
        {
            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}

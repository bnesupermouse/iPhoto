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
    public class TxPhotographerWithdraw : Tx
    {
        public TxPhotographerWithdraw()
        {
            TxnId = 1;
        }

        public override Result Validate()
        {
            var curReq = request as PhotographerWithdraw;
            //Check Session
            var res = UpdatePhotographerSession(true);
            if(res != Result.Success)
            {
                return res;
            }

            //Check Photographer
            Photographer ph = new Photographer();
            ph.PhotographerId = PhotographerId;
            ph = ph.Fetch() as Photographer;
            if(ph == null)
            {
                return Result.Failed;
            }

            //Check PhotographerAccount
            PhotographerAccount pa = new PhotographerAccount();
            pa.PhotographerId = PhotographerId;
            pa = pa.Fetch() as PhotographerAccount;
            if(pa == null)
            {
                return Result.Failed;
            }
            //Check Amount
            if(curReq.Amount > pa.Balance)
            {
                return Result.Failed;
            }
            //Update PhotographerAccount
            var newPa = pa.Clone() as PhotographerAccount;
            newPa.Balance -= curReq.Amount;
            newPa.TotalBalance -= curReq.Amount;
            Data.AddNew(pa, newPa);

            //Update Account
            Account acc = new Account();
            acc.AccountId = 1;
            acc = acc.Fetch() as Account;
            if(acc == null)
            {
                return Result.Failed;
            }
            //Check Amount
            if (curReq.Amount > acc.Balance)
            {
                return Result.Failed;
            }
            var newAcc = acc.Clone() as Account;
            newAcc.PhotographerPay += curReq.Amount;
            Data.AddNew(acc, newAcc);
            PhotographerWithdrawResponse resp = new PhotographerWithdrawResponse();
            resp.PhotographerWorkId = PhotographerId;
            response = resp;
            return Result.Success;
        }
        public override Result Prepare()
        {
            //DO BANK WITHDRAW
            return Data.Validate();
        }
        public override Result Update()
        {
            return Data.Update();
        }
    }
}

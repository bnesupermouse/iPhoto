using Host.Common;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Host
{
    public class Utility
    {
        public static Result ValidateEmail(string email)
        {
            string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                              + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                              + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            if (!Regex.IsMatch(email, MatchEmailPattern))
            {
                return Result.Failed;
            }
            else
            {
                return Result.Success;
            }
        }

        public static async Task<string> GetTokenId(PayOrder payment)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var myToken = new StripeTokenCreateOptions();

            // if you need this...
            myToken.Card = new StripeCreditCardOptions()
            {
                // set these properties if passing full card details (do not
                // set these properties if you set TokenId)
                Number = payment.CardNumber,
                ExpirationYear = payment.Year,
                ExpirationMonth = payment.Month,
                Cvc = payment.CVC
            };

            // set this property if using a customer (stripe connect only)
            //myToken.CustomerId = *customerId *;

            var tokenService = new StripeTokenService();
                try
                {
                    StripeToken stripeToken = tokenService.Create(myToken);
                    return stripeToken.Id;
                }
                catch(Exception ex)
                {
                    return null;
                }
            });
        }

        public static async Task<string> ChargeCustomer(string tokenId, int amount)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions();

                // always set these properties
                myCharge.Amount = amount;
                myCharge.Currency = "aud";
                // set this if you want to
                myCharge.Description = "Charge it like it's hot";

                myCharge.SourceTokenOrExistingSourceId = tokenId;

                // set this property if using a customer - this MUST be set if you are using an existing source!
                //myCharge.CustomerId = *customerId *;

                // set this if you have your own application fees (you must have your application configured first within Stripe)
                //myCharge.ApplicationFee = 25;

                // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
                myCharge.Capture = true;

                var chargeService = new StripeChargeService();
                StripeCharge stripeCharge = chargeService.Create(myCharge);

                return stripeCharge.Id;
                
            });
        }
    }
}

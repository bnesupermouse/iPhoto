using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    //Ctm -> Customer
    //Ph -> Photographer
    public enum OrderStatus
    {
        OrderPending,// -- Ctm
        OrderConfirmed,// --Ph
        RawPhotoUploaded,// --Ph
        PhotoSelected,// --Ctm
        RetouchedPhotoUploaded,// --Ph
        OrderFinalised,// --Ctm
        OrderRejected,//Ph
        OrderCancelled//Ctm
    }
}

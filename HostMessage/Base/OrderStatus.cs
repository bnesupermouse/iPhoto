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
        RawPhotoUploading,// --Ph
        RawPhotoUploaded,// --Ph
        PhotoSelecting,// --Ctm
        PhotoSelected,// --Ctm
        RetouchedPhotoUploading,// --Ph
        RetouchedPhotoUploaded,// --Ph
        RetouchedPhotoConfirming,// --Ctm
        OrderFinalised,// --Ctm
        OrderRejected,//Ph
        OrderCancelled//Ctm
    }
}

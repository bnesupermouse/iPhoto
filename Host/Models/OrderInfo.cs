using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Models
{
    public class OrderInfo
    {
        public long SerialNo { get; set; }
        public long OfferId { get; set; }
        public string OfferName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long PhotographerId { get; set; }
        public string PhotographerName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }
    }

    public class StatusValue
    {
        public static string[] Values = new string[20];
        public static string GetStringValue(int status)
        {
            Values[(int)OrderStatus.OrderCancelled] = "Cancelled";
            Values[(int)OrderStatus.OrderConfirmed] = "Confirmed";
            Values[(int)OrderStatus.OrderFinalised] = "Finalised";
            Values[(int)OrderStatus.OrderPending] = "Pending";
            Values[(int)OrderStatus.OrderRejected] = "Rejected";
            Values[(int)OrderStatus.PhotoSelected] = "RawPhotosSelected";
            Values[(int)OrderStatus.PhotoSelecting] = "SelectingRawPhotos";
            Values[(int)OrderStatus.RawPhotoUploaded] = "RawPhotosUploaded";
            Values[(int)OrderStatus.RawPhotoUploading] = "UploadingRawPhotos";
            Values[(int)OrderStatus.RetouchedPhotoConfirming] = "ConfirmingRetouchedPhotos";
            Values[(int)OrderStatus.RetouchedPhotoUploaded] = "RetouchedPhotosUploaded";
            Values[(int)OrderStatus.RetouchedPhotoUploading] = "UploadingRetouchedPhotos";
            return Values[status];
        }
    }
}

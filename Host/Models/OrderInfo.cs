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
        public string LabelString { get; set; }
    }

    public class StatusValue
    {
        public static string[] Values = new string[20];
        public static string[] LabelValues = new string[20];
        public static string GetStatusValue(int status)
        {
            Values[(int)OrderStatus.OrderCancelled] = "Cancelled";
            Values[(int)OrderStatus.OrderConfirmed] = "Confirmed";
            Values[(int)OrderStatus.OrderFinalised] = "Finalised";
            Values[(int)OrderStatus.OrderPending] = "Pending";
            Values[(int)OrderStatus.OrderRejected] = "Rejected";
            Values[(int)OrderStatus.PhotoSelected] = "InProgress";
            Values[(int)OrderStatus.PhotoSelecting] = "InProgress";
            Values[(int)OrderStatus.RawPhotoUploaded] = "InProgress";
            Values[(int)OrderStatus.RawPhotoUploading] = "InProgress";
            Values[(int)OrderStatus.RetouchedPhotoConfirming] = "InProgress";
            Values[(int)OrderStatus.RetouchedPhotoUploaded] = "InProgress";
            Values[(int)OrderStatus.RetouchedPhotoUploading] = "InProgress";
            return Values[status];
        }

        public static string GetLabelValue(int status)
        {
            LabelValues[(int)OrderStatus.OrderCancelled] = "label-default";
            LabelValues[(int)OrderStatus.OrderConfirmed] = "label-info";
            LabelValues[(int)OrderStatus.OrderFinalised] = "label-success";
            LabelValues[(int)OrderStatus.OrderPending] = "label-warning";
            LabelValues[(int)OrderStatus.OrderRejected] = "label-danger";
            LabelValues[(int)OrderStatus.PhotoSelected] = "label-primary";
            LabelValues[(int)OrderStatus.PhotoSelecting] = "label-primary";
            LabelValues[(int)OrderStatus.RawPhotoUploaded] = "label-primary";
            LabelValues[(int)OrderStatus.RawPhotoUploading] = "label-primary";
            LabelValues[(int)OrderStatus.RetouchedPhotoConfirming] = "label-primary";
            LabelValues[(int)OrderStatus.RetouchedPhotoUploaded] = "label-primary";
            LabelValues[(int)OrderStatus.RetouchedPhotoUploading] = "label-primary";

            return LabelValues[status];
        }
    }
}

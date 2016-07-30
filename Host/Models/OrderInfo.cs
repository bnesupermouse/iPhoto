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
        public string Phone { get; set; }
        public long PhotographerId { get; set; }
        public string PhotographerName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }

        public string StatusString { get; set; }
        public string LabelString { get; set; }
        public int Paid { get; set; }
        public bool RawArchived { get; set; }
        public string RawZip { get; set; }
        public bool RetouchedArchived { get; set; }
        public string RetouchedZip { get; set; }
    }

    public class Appointment
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public long id { get; set; }
        public string text { get; set; }
    }
    public class OrderDetails:OrderInfo
    {
        public List<PhotoInfo> RawPhotos { get; set; }
        public List<PhotoInfo> RetouchedPhotos { get; set; }
        public List<Appointment> Events { get; set; }
        public OfferInfo OfferInfo { get; set; }
    }

    public class StatusValue
    {
        public static string[] Values = new string[20];
        public static string[] LabelValues = new string[20];
        public static string GetStatusValue(int status, bool paid)
        {
            Values[(int)OrderStatus.OrderCancelled] = "Cancelled";
            Values[(int)OrderStatus.OrderConfirmed] = "Confirmed";
            Values[(int)OrderStatus.OrderFinalised] = "Finalised";
            Values[(int)OrderStatus.OrderPending] = "Pending";
            Values[(int)OrderStatus.OrderRejected] = "Rejected";
            Values[(int)OrderStatus.PhotoSelected] = "RawSelected";
            Values[(int)OrderStatus.RawPhotoUploaded] = "RawUploaded";
            Values[(int)OrderStatus.RetouchedPhotoUploaded] = "RetouchedUploaded";
            return Values[status];
        }

        public static string GetLabelValue(int status, bool paid)
        {
            if(!paid)
            {
                return "label-danger";
            }
            LabelValues[(int)OrderStatus.OrderCancelled] = "label-default";
            LabelValues[(int)OrderStatus.OrderConfirmed] = "label-info";
            LabelValues[(int)OrderStatus.OrderFinalised] = "label-success";
            LabelValues[(int)OrderStatus.OrderPending] = "label-warning";
            LabelValues[(int)OrderStatus.OrderRejected] = "label-danger";
            LabelValues[(int)OrderStatus.PhotoSelected] = "label-primary";
            LabelValues[(int)OrderStatus.RawPhotoUploaded] = "label-primary";
            LabelValues[(int)OrderStatus.RetouchedPhotoUploaded] = "label-primary";

            return LabelValues[status];
        }
    }
}

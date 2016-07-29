using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Host;
using HostDB;
using HostMessage.Responses;
using Host.Common;
using Host.Models;
using System.IO.Compression;
using Microsoft.Owin.FileSystems;
using System.IO;

namespace Host
{
    public class TxArchivePhoto : Tx
    {
        public TxArchivePhoto()
        {
            TxnId = 1;
        }
        public long OrderId;
        public override Result Validate()
        {
            var curReq = request as UpdateOrderStatus;
            //Check Order
            CustomerOrder order = new CustomerOrder();
            order.SerialNo = OrderId;
            order = order.Fetch() as CustomerOrder;
            if (order == null)
            {
                LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Invalid Request", Log4NetLevel.Error);
                response.ErrorNo = (int)Errors.InvalidRequest;
                response.ErrorMsg = "Invalid Request";
                return Result.Failed;
            }
            if(order.Status == (int)OrderStatus.OrderFinalised)
            {
                var rootPath = new PhysicalFileSystem(@"../../WebSrc/app");
                long OrderId = order.SerialNo;

                string orderPath = @"/images/customer/" + OrderId + @"/";
                string rawPhotoPath = orderPath + @"raw";
                string zipFoler = orderPath + @"zip/";
                string rawZipFile = zipFoler + OrderId + @"_Raw.zip";
                string retouchedPhotoPath = orderPath + @"retouched";
                string retouchedZipFile = zipFoler + OrderId + @"_Retouched.zip";

                string rawZipPath = rawZipFile;
                string retouchedZipPath = retouchedZipFile;

                rawPhotoPath = rootPath.Root + rawPhotoPath;
                retouchedPhotoPath = rootPath.Root + retouchedPhotoPath;
                rawZipFile = rootPath.Root + rawZipFile;
                retouchedZipFile = rootPath.Root + retouchedZipFile;
                try
                {
                    if(File.Exists(rawZipFile))
                    {
                        File.Delete(rawZipFile);
                    }
                    if (File.Exists(retouchedZipFile))
                    {
                        File.Delete(retouchedZipFile);
                    }
                    if (!Directory.Exists(zipFoler))
                    {
                        Directory.CreateDirectory(zipFoler);
                    }
                    ZipFile.CreateFromDirectory(rawPhotoPath, rawZipFile);
                    ZipFile.CreateFromDirectory(retouchedPhotoPath, retouchedZipFile);
                }
                catch(Exception ex)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Failed to Archive photos", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Failed to Archive photos";
                    return Result.Failed;
                }
                var newOrder = order.Clone() as CustomerOrder;
                newOrder.Archived = true;
                newOrder.RawZip = rawZipPath;
                newOrder.RetouchedZip = retouchedZipPath;
                Data.AddNew(order, newOrder);
            }
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

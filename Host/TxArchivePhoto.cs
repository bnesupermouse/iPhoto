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
        public long OrderId { get; set; }
        public bool ArchiveRaw { get; set; }

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
            if (ArchiveRaw)
            {
                var rootPath = new PhysicalFileSystem(@"../../WebSrc/app");
                long OrderId = order.SerialNo;

                string orderPath = @"/images/customer/" + OrderId + @"/";
                string rawPhotoPath = orderPath + @"raw";
                string zipFolder = orderPath + @"zip/";
                string rawZipFile = zipFolder + OrderId + @"_Raw.zip";

                string rawZipPath = rawZipFile;

                rawPhotoPath = rootPath.Root + rawPhotoPath;
                rawZipFile = rootPath.Root + rawZipFile;
                try
                {
                    zipFolder = rootPath.Root + zipFolder;
                    if (File.Exists(rawZipFile))
                    {
                        File.Delete(rawZipFile);
                    }
                    if (!Directory.Exists(zipFolder))
                    {
                        Directory.CreateDirectory(zipFolder);
                    }
                    ZipFile.CreateFromDirectory(rawPhotoPath, rawZipFile);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Failed to Archive Raw photos", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Failed to Archive Raw photos";
                    return Result.Failed;
                }
                var newOrder = order.Clone() as CustomerOrder;
                newOrder.RawArchived = true;
                newOrder.RawZip = rawZipPath;
                Data.AddNew(order, newOrder);
            }
            else
            {
                var rootPath = new PhysicalFileSystem(@"../../WebSrc/app");
                long OrderId = order.SerialNo;

                string orderPath = @"/images/customer/" + OrderId + @"/";
                string zipFolder = orderPath + @"zip/";
                string retouchedPhotoPath = orderPath + @"retouched";
                string retouchedZipFile = zipFolder + OrderId + @"_Retouched.zip";

                string retouchedZipPath = retouchedZipFile;

                retouchedPhotoPath = rootPath.Root + retouchedPhotoPath;
                retouchedZipFile = rootPath.Root + retouchedZipFile;
                try
                {
                    zipFolder = rootPath.Root + zipFolder;
                    if (File.Exists(retouchedZipFile))
                    {
                        File.Delete(retouchedZipFile);
                    }
                    if (!Directory.Exists(zipFolder))
                    {
                        Directory.CreateDirectory(zipFolder);
                    }
                    ZipFile.CreateFromDirectory(retouchedPhotoPath, retouchedZipFile);
                }
                catch(Exception ex)
                {
                    LogHelper.WriteLog(typeof(TxUpdateOrderStatus), "Failed to Archive Retouched photos", Log4NetLevel.Error);
                    response.ErrorNo = (int)Errors.InvalidRequest;
                    response.ErrorMsg = "Failed to Archive Retouched photos";
                    return Result.Failed;
                }
                var newOrder = order.Clone() as CustomerOrder;
                newOrder.RetouchedArchived = true;
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

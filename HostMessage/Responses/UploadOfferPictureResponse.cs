using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class UploadOfferPictureResponse : Response
    {
        public long OfferId { get; set; }
        public List<string> PicturePaths { get; set; }
    }
}

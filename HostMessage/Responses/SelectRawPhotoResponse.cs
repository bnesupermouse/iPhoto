﻿using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostMessage.Responses
{
    public class SelectRawPhotoResponse : Response
    {
        public long OrderId { get; set; }
        public List<long> PhotoIds { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostDB;
namespace Host
{
    public class PhotographerLogin : Request
    {
        public PhotographerLogin()
        {
            TxnId = 3;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Action { get; set; }
    }
}

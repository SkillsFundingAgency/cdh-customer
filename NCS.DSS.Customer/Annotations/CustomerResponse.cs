﻿using System;

namespace NCS.DSS.Customer.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomerResponse : Attribute
    {
        public int HttpStatusCode { get; set; }
        public string Description { get; set; }
        public bool ShowSchema { get; set; }
    }
}

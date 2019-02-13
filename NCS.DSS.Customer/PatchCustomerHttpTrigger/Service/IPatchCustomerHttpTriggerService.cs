﻿using NCS.DSS.Customer.Models;
using System;
using System.Threading.Tasks;

namespace NCS.DSS.Customer.PatchCustomerHttpTrigger.Service
{
    public interface IPatchCustomerHttpTriggerService
    {
        Models.Customer PatchResource(Models.Customer customer, CustomerPatch customerPatch);
        Task<Models.Customer> UpdateCosmosAsync(Models.Customer customer);
        Task<Models.Customer> GetCustomerByIdAsync(Guid customerId);
        Task SendToServiceBusQueueAsync(Models.CustomerPatch customerPatch, Guid customerId, string reqUrl);
    }
}

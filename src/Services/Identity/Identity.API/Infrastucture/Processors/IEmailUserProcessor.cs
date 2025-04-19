﻿using IdentityServer4.Validation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Infrastucture.Processors
{
    public interface IEmailUserProcessor
    {
        Task<GrantValidationResult> ProcessAsync(JObject userInfo, string email, string provider);
    }
}

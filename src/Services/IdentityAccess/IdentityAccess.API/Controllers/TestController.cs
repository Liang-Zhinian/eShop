extern alias MySqlConnectorAlias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using global::IdentityAccess.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.IdentityAccess.API.Application.IntegrationEvents;
using SaaSEqt.eShop.Services.IdentityAccess.API.Infrastructure.Services;

namespace SaaSEqt.eShop.Services.IdentityAccess.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TestController : Controller
    {
        private readonly ITenantService _tenantService;
        private readonly IIdentityAccessIntegrationEventService _identityAccessIntegrationEventService;
        private readonly IHostingEnvironment _env;
        private readonly IdentityAccessSettings _settings;
        private readonly ILogger<TestController> _logger;

        public TestController(
            IIdentityAccessIntegrationEventService identityAccessIntegrationEventService,
            ITenantService tenantService,
              IHostingEnvironment env,
                              IOptionsSnapshot<IdentityAccessSettings> settings,
                              ILogger<TestController> logger
        )
        {
            _identityAccessIntegrationEventService = identityAccessIntegrationEventService;
            _tenantService = tenantService;
            _env = env;
            _settings = settings.Value;
            _logger = logger;
        }

    }
}

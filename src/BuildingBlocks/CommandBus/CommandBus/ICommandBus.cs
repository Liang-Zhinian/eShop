using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eva.BuildingBlocks.CommandBus
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : IntegrationCommand;

    }
}


namespace SaaSEqt.Common.Domain.Model
{
    using System;

    public interface IDomainEvent
    {
        int Version { get; set;  }
        DateTimeOffset TimeStamp { get; set; }
    }
}

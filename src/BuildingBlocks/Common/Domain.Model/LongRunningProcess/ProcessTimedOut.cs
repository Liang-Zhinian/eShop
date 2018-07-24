
namespace SaaSEqt.Common.Domain.Model.LongRunningProcess
{
    using System;
    using SaaSEqt.Common.Domain.Model;

    public class ProcessTimedOut : IDomainEvent
    {
        public ProcessTimedOut(
                string tenantId,
                ProcessId processId,
                int totalRetriesPermitted,
            int retryCount):base()
        {
            this.Id = Guid.NewGuid();
            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
            this.ProcessId = processId;
            this.RetryCount = retryCount;
            this.TenantId = tenantId;
            this.TotalRetriesPermitted = totalRetriesPermitted;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public ProcessId ProcessId { get; private set; }
        public int RetryCount { get; private set; }
        public string TenantId { get; private set; }
        public int TotalRetriesPermitted { get; private set; }
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

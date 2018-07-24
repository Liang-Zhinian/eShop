
namespace SaaSEqt.IdentityAccess.Domain.Identity.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using SaaSEqt.Common.Domain.Model;

    [NotMapped]
    public class UserDescriptor : ValueObject
    {
        public static UserDescriptor NullDescriptorInstance()
        {
            return new UserDescriptor();
        }

        public UserDescriptor(TenantId tenantId, string username, string emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.TenantId = tenantId;
            this.Username = username;
        }

        UserDescriptor() { }

        public string EmailAddress { get; private set; }

        public TenantId TenantId { get; private set; }

        public string Username { get; private set; }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.EmailAddress;
            yield return this.TenantId;
            yield return this.Username;
        }

        public override string ToString()
        {
            return "UserDescriptor [emailAddress=" + EmailAddress
                    + ", tenantId=" + TenantId + ", username=" + Username + "]";
        }
    }
}

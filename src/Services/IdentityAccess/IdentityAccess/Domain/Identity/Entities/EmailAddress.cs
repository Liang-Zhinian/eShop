using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

using SaaSEqt.Common.Domain.Model;

namespace SaaSEqt.IdentityAccess.Domain.Identity.Entities
{
    [NotMapped]
    public class EmailAddress : ValueObject
    {
        public EmailAddress(string address)
        {
            this.Address = address;
        }

        public EmailAddress(EmailAddress emailAddress)
            : this(emailAddress.Address)
        {
        }

        protected EmailAddress() { }

        string address;

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "The email address is required.");
                AssertionConcern.AssertArgumentLength(value, 1, 100, "Email address must be 100 characters or less.");
                AssertionConcern.AssertArgumentMatches(
                        "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",
                        value,
                        "Email address format is invalid.");

                this.address = value;
            }
        }

        public override string ToString()
        {
            return "EmailAddress [address=" + Address + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return address.ToUpper();
        }
    }
}



namespace SaaSEqt.eShop.Services.Sites.API.Model.Security
{
	using System;
	using System.Collections.Generic;
    using System.Linq;

    public partial class Tenant
    {
        #region [ Public Properties ]

        //[Key]
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public bool Active { get; private set; }

        public string Description { get; private set; }

        #endregion
    }
}
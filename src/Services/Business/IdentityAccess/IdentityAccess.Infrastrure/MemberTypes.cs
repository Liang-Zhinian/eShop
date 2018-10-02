namespace SaaSEqt.IdentityAccess.Infrastructure
{
    using System;

    public enum MemberTypes : byte
    {
        /// <summary>
        /// Indicates that the group member is a <see cref="Group"/>.
        /// </summary>
        Group = 0,

        /// <summary>
        /// Indicates that the group member is a <see cref="User"/>.
        /// </summary>
        User = 1
    }
}

using System;

namespace ThomasWoodcock.Service.Application.Accounts.Entities
{
    /// <summary>
    ///     Represents a key that can be used to activate an account.
    /// </summary>
    public sealed class AccountActivationKey
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountActivationKey" /> class.
        /// </summary>
        /// <param name="value">
        ///     The value with which to create the activation key.
        /// </param>
        internal AccountActivationKey(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.Value = value;
        }

        /// <summary>
        ///     Gets the value of the activation key.
        /// </summary>
        internal Guid Value { get; }
    }
}

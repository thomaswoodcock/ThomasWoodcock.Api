using System;

namespace ThomasWoodcock.Service.Application.Accounts.Entities
{
    /// <summary>
    ///     Represents a key that can be used to activate an account.
    /// </summary>
    public sealed class AccountActivationKey
    {
        /// <summary>
        ///     Allows an ORM to initialize the <see cref="AccountActivationKey" /> class.
        /// </summary>
        private AccountActivationKey()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountActivationKey" /> class.
        /// </summary>
        /// <param name="value">
        ///     The value with which to create the activation key.
        /// </param>
        public AccountActivationKey(Guid value)
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
        public Guid Value { get; }
    }
}

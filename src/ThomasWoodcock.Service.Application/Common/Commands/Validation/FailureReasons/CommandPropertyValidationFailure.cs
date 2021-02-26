using System;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="CommandValidationFailure" /> class that represents a failure that occurs when a
    ///     property on a command fails validation.
    /// </summary>
    internal sealed class CommandPropertyValidationFailure : CommandValidationFailure
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandPropertyValidationFailure" /> class.
        /// </summary>
        /// <param name="propertyName">
        ///     The property that failed validation.
        /// </param>
        /// <param name="message">
        ///     The reason that the property failed validation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the given <paramref name="propertyName" /> is null.
        /// </exception>
        public CommandPropertyValidationFailure(string propertyName, string message)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(message);
            }

            this.PropertyName = propertyName;
            this.Message = message;
        }

        /// <summary>
        ///     Gets the property that failed validation.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     Gets the reason that the property failed validation.
        /// </summary>
        public string Message { get; }
    }
}

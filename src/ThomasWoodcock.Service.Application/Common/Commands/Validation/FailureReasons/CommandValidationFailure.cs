using System;
using System.Collections.Generic;
using System.Linq;

using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IFailureReason" /> interface that represents a failure that occurs when a
    ///     command fails validation.
    /// </summary>
    internal class CommandValidationFailure : IFailureReason
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandValidationFailure" /> class without any inner validation
        ///     failures.
        /// </summary>
        protected CommandValidationFailure()
        {
            this.Failures = Enumerable.Empty<IFailureReason>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandValidationFailure" /> class.
        /// </summary>
        /// <param name="failures">
        ///     One or more failures that occurred during validation.
        /// </param>
        public CommandValidationFailure(IEnumerable<IFailureReason> failures)
        {
            this.Failures = failures ?? throw new ArgumentNullException(nameof(failures));
        }

        /// <summary>
        ///     Gets the failures that occurred during validation.
        /// </summary>
        public IEnumerable<IFailureReason> Failures { get; }
    }
}

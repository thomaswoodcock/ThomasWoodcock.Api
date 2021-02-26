using System;
using System.Collections.Generic;
using System.Linq;

using ThomasWoodcock.Service.Application.Common.Commands.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.Rules;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <inheritdoc />
    /// <summary>
    ///     A concrete implementation of the <see cref="ICommandValidator{T}" /> interface.
    /// </summary>
    internal sealed class CommandValidator<T> : ICommandValidator<T>
        where T : class, ICommand
    {
        private readonly IEnumerable<ICommandValidationRule<T>> _rules;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandValidator{T}" /> class.
        /// </summary>
        /// <param name="rules">
        ///     The validation rules that the validator will run against commands.
        /// </param>
        public CommandValidator(IEnumerable<ICommandValidationRule<T>> rules)
        {
            this._rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        /// <inheritdoc />
        public IResult Validate(T command)
        {
            if (command == null)
            {
                return Result.Failure(new InvalidCommandFailure());
            }

            IFailureReason[] failureReasons = this._rules.Select(rule => rule.Check(command))
                .Where(result => result.IsFailed && result.FailureReason != null)
                .Select(result => result.FailureReason)
                .ToArray();

            return failureReasons.Length > 0 ? Result.Failure(new CommandValidationFailure(failureReasons)) : Result.Success();
        }
    }
}

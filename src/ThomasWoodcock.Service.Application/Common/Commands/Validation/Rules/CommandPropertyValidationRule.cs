using System;
using System.Linq.Expressions;

using ThomasWoodcock.Service.Application.Common.Commands.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands.Validation.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation.Rules
{
    /// <summary>
    ///     Creates a failure message for property validation.
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property that failed validation.
    /// </param>
    internal delegate string PropertyValidationFailureMessageBuilder(string propertyName);

    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="ICommandValidationRule{T}" /> interface used to validate a command property.
    /// </summary>
    /// <typeparam name="TCommand">
    ///     The type of command whose property will be validated.
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     The type of property that will be validated.
    /// </typeparam>
    internal sealed class CommandPropertyValidationRule<TCommand, TProperty> : ICommandValidationRule<TCommand>
        where TCommand : class, ICommand
    {
        private readonly string _message;
        private readonly Predicate<TProperty> _predicate;
        private readonly string _propertyName;
        private readonly Func<TCommand, TProperty> _propertySelector;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandPropertyValidationRule{TCommand,TProperty}" /> class.
        /// </summary>
        /// <param name="propertySelector">
        ///     The property that will be validated.
        /// </param>
        /// <param name="predicate">
        ///     The criteria that the property must meet in order to be declared valid.
        /// </param>
        /// <param name="messageBuilder">
        ///     The message that will be returned if the property is deemed invalid.
        /// </param>
        public CommandPropertyValidationRule(Expression<Func<TCommand, TProperty>> propertySelector,
            Predicate<TProperty> predicate, PropertyValidationFailureMessageBuilder messageBuilder)
        {
            this._propertySelector = propertySelector?.Compile() ??
                                     throw new ArgumentNullException(nameof(propertySelector));

            this._predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this._propertyName = ((MemberExpression)propertySelector.Body).Member.Name;

            this._message = messageBuilder?.Invoke(this._propertyName) ??
                            throw new ArgumentNullException(nameof(messageBuilder));
        }

        /// <inheritdoc />
        public IResult Check(TCommand command)
        {
            if (command == null)
            {
                return Result.Failure(new InvalidCommandFailure());
            }

            TProperty propertyValue = this._propertySelector(command);
            bool checkPassed = this._predicate(propertyValue);

            return checkPassed
                ? Result.Success()
                : Result.Failure(new CommandPropertyValidationFailure(this._propertyName, this._message));
        }
    }
}

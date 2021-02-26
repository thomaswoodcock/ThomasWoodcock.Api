using System;
using System.Linq.Expressions;

using ThomasWoodcock.Service.Application.Common.Commands.Validation.Rules;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     A builder for command property validation rules.
    /// </summary>
    /// <typeparam name="TCommand">
    ///     The type of command whose property will be validated.
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     The type of property that will be validated.
    /// </typeparam>
    internal sealed class CommandPropertyValidatorBuilder<TCommand, TProperty>
        where TCommand : class, ICommand
    {
        private readonly CommandValidatorBuilder<TCommand> _builder;
        private readonly Expression<Func<TCommand, TProperty>> _propertySelector;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandPropertyValidatorBuilder{TCommand,TProperty}" /> class.
        /// </summary>
        /// <param name="builder">
        ///     The <see cref="CommandValidatorBuilder{T}" /> to which rules will be added.
        /// </param>
        /// <param name="propertySelector">
        ///     The property that will be validated.
        /// </param>
        public CommandPropertyValidatorBuilder(CommandValidatorBuilder<TCommand> builder,
            Expression<Func<TCommand, TProperty>> propertySelector)
        {
            this._builder = builder ?? throw new ArgumentNullException(nameof(builder));
            this._propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        /// <summary>
        ///     Adds a validation rule for the property.
        /// </summary>
        /// <param name="predicate">
        ///     The criteria that the property must meet in order to be considered valid.
        /// </param>
        /// <param name="messageBuilder">
        ///     The message to return if the property is deemed invalid.
        /// </param>
        /// <returns>
        ///     The <see cref="CommandPropertyValidatorBuilder{TCommand,TProperty}" /> to allow further rules to be added.
        /// </returns>
        public CommandPropertyValidatorBuilder<TCommand, TProperty> AddRule(Predicate<TProperty> predicate,
            PropertyValidationFailureMessageBuilder messageBuilder)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (messageBuilder == null)
            {
                throw new ArgumentNullException(nameof(messageBuilder));
            }

            this._builder.AddRule(
                new CommandPropertyValidationRule<TCommand, TProperty>(this._propertySelector, predicate,
                    messageBuilder));

            return this;
        }
    }
}

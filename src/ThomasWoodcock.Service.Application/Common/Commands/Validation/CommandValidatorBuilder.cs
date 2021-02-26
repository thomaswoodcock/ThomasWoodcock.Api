using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ThomasWoodcock.Service.Application.Common.Commands.Validation.Rules;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     A builder for command validators.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of command that the created validators will validate.
    /// </typeparam>
    internal sealed class CommandValidatorBuilder<T>
        where T : class, ICommand
    {
        private readonly List<ICommandValidationRule<T>> _rules = new();

        /// <summary>
        ///     Builds a validator.
        /// </summary>
        /// <returns>
        ///     A validator.
        /// </returns>
        public CommandValidator<T> Build() => new(this._rules);

        /// <summary>
        ///     Adds a validation rule to the builder.
        /// </summary>
        /// <param name="rule">
        ///     The rule to add to the builder.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the given <paramref name="rule" /> is null.
        /// </exception>
        public void AddRule(ICommandValidationRule<T> rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            this._rules.Add(rule);
        }

        /// <summary>
        ///     Provides validation methods for a property on the command.
        /// </summary>
        /// <param name="propertySelector">
        ///     The property to validate.
        /// </param>
        /// <typeparam name="TProperty">
        ///     The type of property that will be validated.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="CommandPropertyValidatorBuilder{TCommand,TProperty}" /> used to validate the property.
        /// </returns>
        public CommandPropertyValidatorBuilder<T, TProperty> Property<TProperty>(
            Expression<Func<T, TProperty>> propertySelector)
        {
            if (propertySelector == null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            return new CommandPropertyValidatorBuilder<T, TProperty>(this, propertySelector);
        }
    }
}

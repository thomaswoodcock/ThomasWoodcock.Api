using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     Command validation-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers command validation-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddCommandValidation(this IServiceCollection collection)
        {
            collection.AutoRegisterCommandValidators();
        }

        private static void AutoRegisterCommandValidators(this IServiceCollection collection)
        {
            // Get all types that implement configuration interface.
            IEnumerable<Type> configurationTypes = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(ICommandValidatorConfiguration<>)));

            foreach (Type configurationType in configurationTypes)
            {
                Type commandType = configurationType.GetInterface("ICommandValidatorConfiguration`1")
                    ?.GenericTypeArguments.FirstOrDefault();

                if (commandType == null)
                {
                    continue;
                }

                // Create validator builder.
                Type builderType = typeof(CommandValidatorBuilder<>).MakeGenericType(commandType);
                object builder = Activator.CreateInstance(builderType);

                if (builder == null)
                {
                    continue;
                }

                // Configure validator builder.
                MethodInfo configure = configurationType.GetMethod("Configure");

                if (configure == null)
                {
                    continue;
                }

                object configuration = Activator.CreateInstance(configurationType);

                if (configuration == null)
                {
                    continue;
                }

                configure.Invoke(configuration, new[] { builder });

                // Build validator.
                MethodInfo build = builderType.GetMethod("Build");

                if (build == null)
                {
                    continue;
                }

                object validator = build.Invoke(builder, Array.Empty<object>());

                if (validator == null)
                {
                    continue;
                }

                // Register validator.
                Type validatorType = typeof(ICommandValidator<>).MakeGenericType(commandType);
                collection.AddSingleton(validatorType, validator);
            }
        }
    }
}

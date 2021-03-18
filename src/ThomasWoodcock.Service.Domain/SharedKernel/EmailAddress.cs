using System;
using System.Globalization;
using System.Text.RegularExpressions;

using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents an email address.
    /// </summary>
    public sealed partial record EmailAddress
    {
        private readonly string _value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailAddress" /> class.
        /// </summary>
        /// <param name="value">
        ///     The value with which to initialize the <see cref="EmailAddress" />.
        /// </param>
        private EmailAddress(string value)
        {
            this._value = value;
        }

        /// <inheritdoc />
        public override string ToString() => this._value;
    }

    public sealed partial record EmailAddress
    {
        /// <summary>
        ///     Creates an <see cref="EmailAddress" />.
        /// </summary>
        /// <param name="emailAddress">
        ///     The email address with which to create the <see cref="EmailAddress" />.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult{T}" /> of type <see cref="EmailAddress" />.
        /// </returns>
        /// <remarks>
        ///     https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        /// </remarks>
        internal static IResult<EmailAddress> Create(string emailAddress) =>
            !EmailAddress.IsValid(emailAddress)
                ? Result.Failure<EmailAddress>(new InvalidFormatFailure())
                : Result.Success(new EmailAddress(emailAddress));

        /// <summary>
        ///     Determines whether a given <paramref name="emailAddress" /> is in a valid email address format.
        /// </summary>
        /// <param name="emailAddress">
        ///     The email address to validate.
        /// </param>
        /// <returns>
        ///     A value that indicates whether the given <paramref name="emailAddress" /> is  in a valid email address format.
        /// </returns>
        public static bool IsValid(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            try
            {
                // Checks that the email address contains a valid domain name.
                emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", DomainMapper, RegexOptions.None,
                    TimeSpan.FromMilliseconds(200));

                static string DomainMapper(Match match)
                {
                    IdnMapping idn = new();

                    string domainName = idn.GetAscii(match.Groups[2]
                        .Value);

                    return match.Groups[1]
                        .Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                // Checks that the email address is in a valid format.
                return Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

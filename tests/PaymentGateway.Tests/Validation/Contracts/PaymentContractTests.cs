using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using PaymentGateway.Contracts;
using PaymentGateway.Enums;
using Xunit;

namespace PaymentGateway.Tests.Validation.Contracts
{
    public class PaymentContractTests
    {
        const string FieldRequired = "The {0} field is required.";
        const string NotValidCVV = "Not valid card verification value";
        const string NotValidCardNumber = "Not valid card number";

        [Theory]
        [ClassData(typeof(StringFieldsTestData))]
        public void ValidatePaymentRequest_MissingStringFieldFailValidation(string cardNumber, string cvv, string expectedErrorMessage)
        {
            var target = new PaymentContract
            {
                Amount = 0m,
                Currency = Currency.GBP,
                CardNumber = cardNumber,
                CVV = cvv,
                ExpiryMonth = 1,
                ExpiryYear = 2000
            };

            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(target, context, results, true);

            isValid.Should().BeFalse();
            results.Count.Should().Be(1);
            results.Select(r => r.ErrorMessage).Should().Contain(expectedErrorMessage);
        }

        public class StringFieldsTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null, "123", string.Format(FieldRequired, "CardNumber") };
                yield return new object[] { string.Empty, "123", string.Format(FieldRequired, "CardNumber") };
                yield return new object[] { "   ", "123", string.Format(FieldRequired, "CardNumber") };
                yield return new object[] { "1234-5678-9101-1121", "123", NotValidCardNumber };
                yield return new object[] { "4916-5993-6736-4789", string.Empty, string.Format(FieldRequired, "CVV") };
                yield return new object[] { "4916-5993-6736-4789", null, string.Format(FieldRequired, "CVV") };
                yield return new object[] { "4916-5993-6736-4789", "   ", string.Format(FieldRequired, "CVV") };
                yield return new object[] { "4916-5993-6736-4789", "abc", NotValidCVV };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
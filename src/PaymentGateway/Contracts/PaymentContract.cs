using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using PaymentGateway.Contracts.Attributes;
using PaymentGateway.Enums;

namespace PaymentGateway.Contracts
{
    /// <summary>
    /// Represents payment details. <br/> Allows creation, storage and retrival of information regarding payment.
    /// </summary>
    public record PaymentContract
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [JsonIgnoreOnDeserialize]
        public Guid Id { get; init; }

        /// <summary>
        /// Date of payment
        /// </summary>
        [JsonIgnoreOnDeserialize]
        public DateTime Date { get; init; }
        /// <summary>
        /// The amount paid.
        /// </summary>
        [Required, JsonProperty(Required = Required.Always)]
        public decimal Amount { get; init; }

        /// <summary>
        /// Currency code of payment.
        /// </summary>
        [Required, JsonProperty(Required = Required.Always)]
        public Currency Currency { get; init; }

        /// <summary>
        /// Credit card number.<br/>Here we use built-in Credit Card validation attribute which is not perfect, but enough for the purpose of the test.
        /// </summary>
        /// <description></description>
        [Required, CreditCard(ErrorMessage = "Not valid card number")]
        public string CardNumber { get; init; }

        /// <summary>
        /// Card Verification Value number. <br/> This value may be 3-4 digits long. Only 3 digits nubmer is allowed here.
        /// </summary>
        [Required, RegularExpression(@"^\d{3,4}$", ErrorMessage = "Not valid card verification value")]
        public string CVV { get; init; }

        /// <summary>
        /// Number of month when card is expired.
        /// </summary>
        [Required, JsonProperty(Required = Required.Always)]
        public int ExpiryMonth { get; init; }

        /// <summary>
        /// Year when card is expired.<br/> Year number must have 4 digits length.
        /// </summary>
        [Required, JsonProperty(Required = Required.Always)]
        public int ExpiryYear { get; init; }
    }
}
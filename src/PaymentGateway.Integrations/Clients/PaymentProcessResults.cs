using System.Collections.Generic;

namespace PaymentGateway.Integrations.Clients
{
    public class PaymentProcessResults
    {
        List<string> _rejectionReasons = new List<string>();
        public bool Success { get; set; }
        public string TranscactionIdentifier { get; set; }
        public IReadOnlyList<string> RejectionReasons => _rejectionReasons;
        public void AddRejectionReasons(string[] reasons) => _rejectionReasons.AddRange(reasons);
    }
}
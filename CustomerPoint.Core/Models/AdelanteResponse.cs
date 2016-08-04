namespace CustomerPoint.Models
{
    [System.Obsolete("Use CustomerPoint.Payments.Adelante.PaymentResponse", false)]
    public class AdelanteResponse
    {
        public int? ErrorStatus { get; set; }
        public int? AuthStatus { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string IasOrderNo { get; set; }
    }
}

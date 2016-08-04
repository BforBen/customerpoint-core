using CustomerPoint.Models;

namespace CustomerPoint.Payments.Adelante
{
    public class PaymentResponse
    {
        public int? ErrorStatus { get; set; }
        public int? AuthStatus { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string IasOrderNo { get; set; }

        public PaymentRequestStatus? Status
        {
            get
            {
                if (ErrorStatus.HasValue)
                {
                    if (ErrorStatus.Value == 1)
                    {
                        if (AuthStatus.HasValue && AuthStatus.Value == 1)
                        {
                            return PaymentRequestStatus.Successful;
                        }
                        else
                        {
                            return PaymentRequestStatus.Declined;
                        }
                    }
                    else
                    {
                        if (ErrorCode.HasValue && ErrorCode.Value == 51)
                        {
                            return PaymentRequestStatus.NotVerified;
                        }
                        else
                        {
                            return PaymentRequestStatus.Unsuccessful;
                        }
                    }
                }
                return null;
            }
        }
    }
}

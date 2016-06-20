namespace CustomerPoint.Models
{
    public class AdelanteResponse
    {
        public int? ErrorStatus { get; set; }
        public int? AuthStatus { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string IasOrderNo { get; set; }
    }
}

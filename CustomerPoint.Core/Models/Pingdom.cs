using System.Xml.Serialization;

namespace CustomerPoint.Models
{
    [XmlRoot("pingdom_http_custom_check")]
    public class PingdomHttpCustomCheck
    {
        public PingdomHttpCustomCheck()
        {
            Status = "DOWN";
        }
        [XmlElement("status")]
        public string Status { get; set; }
        [XmlElement("response_time")]
        public decimal ResponseTime { get; set; }
    }
}

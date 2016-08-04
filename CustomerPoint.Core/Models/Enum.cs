using System.ComponentModel.DataAnnotations;

namespace CustomerPoint.Models
{
    public enum PaymentRequestStatus
    {
        /// <summary>
        /// The payment was successful
        /// </summary>
        Successful,
        /// <summary>
        /// The payment was reported as declined
        /// </summary>
        [Display(Name = "Not verified", Description = "The payment was reported as declined")]
        Declined,
        /// <summary>
        /// Unable to verify in the payment was successful or not
        /// </summary>
        [Display(Name = "Not verified", Description = "Unable to verify in the payment was successful or not")]
        NotVerified,
        /// <summary>
        /// The payment was unsuccessful
        /// </summary>
        [Display(Name = "Unsuccessful", Description = "")]
        Unsuccessful
    }
}

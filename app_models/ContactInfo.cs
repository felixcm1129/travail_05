namespace BillingManagement.Models
{
    public class ContactInfo
    {
        public string ContactType { get; set; }
        public string Contact { get; set; }

        public string Info => $"{ContactType} : {Contact}";

        public int ContactInfoID { get; set; }
    }
}
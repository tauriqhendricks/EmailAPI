namespace EmailAPI.Models
{
    public class EmailSettings
    {
        public const string Email = "Email";

        public string To { get; set; }
        public string From { get; set; }
        public string Key { get; set; }
    }
}

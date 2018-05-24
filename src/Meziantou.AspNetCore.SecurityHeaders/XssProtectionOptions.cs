namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class XssProtectionOptions
    {
        public bool Enabled { get; set; } = true;
        public bool Block { get; set; }
        public string ReportUri { get; set; }
    }
}

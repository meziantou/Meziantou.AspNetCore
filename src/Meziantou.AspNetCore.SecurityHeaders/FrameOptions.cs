namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class FrameOptions
    {
        public FrameOptionsValue Value { get; set; } = FrameOptionsValue.Deny;
        public string AllowedUri { get; set; }
    }
}

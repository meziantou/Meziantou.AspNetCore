namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ReferrerPolicyOptions
    {
        public ReferrerPolicy Value { get; set; } = ReferrerPolicy.NoReferrerWhenDowngrade;
    }
}

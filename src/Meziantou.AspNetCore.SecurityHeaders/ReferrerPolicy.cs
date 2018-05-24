namespace Meziantou.AspNetCore.SecurityHeaders
{
    public enum ReferrerPolicy
    {
        NoReferrer,
        NoReferrerWhenDowngrade,
        Origin,
        OriginWhenCrossOrigin,
        SameOrigin,
        StrictOrigin,
        StrictOriginWhenCrossOrigin,
        UnsafeUrl
    }
}

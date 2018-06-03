using System;
using System.Linq;
using System.Text;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ContentSecurityPolicyBuilder
    {
        public ContentSecurityPolicyCollectionBuilder DefaultSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ScriptSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ImageSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder StylesheetSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder FontSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder FrameSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ManifestSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ConnectSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ReportUris { get; } = new ContentSecurityPolicyCollectionBuilder();
        public bool UpgradeInsecureRequests { get; set; }
        public bool DisownOpener { get; set; }

        public ContentSecurityPolicyBuilder WithDefaultSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(DefaultSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithScriptSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ScriptSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithImageSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ImageSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithStylesheetSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(StylesheetSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithFontSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(FontSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithFrameSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(FrameSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithManifestSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ManifestSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithConnectSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ConnectSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithReportUris(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ReportUris);
            return this;
        }

        public ContentSecurityPolicyBuilder WithUpgradeInsecureRequests(bool value)
        {
            UpgradeInsecureRequests = value;
            return this;
        }

        public ContentSecurityPolicyBuilder WithDisownOpener(bool value)
        {
            DisownOpener = value;
            return this;
        }

        public string Build()
        {
            var sb = new StringBuilder();
            Append("default-src", DefaultSources);
            Append("script-src", ScriptSources);
            Append("img-src", ImageSources);
            Append("style-src", StylesheetSources);
            Append("font-src", FontSources);
            Append("frame-src", FrameSources);
            Append("connect-src", ConnectSources);
            Append("report-uri", ReportUris);
            Append("manifest-src", ManifestSources);
            if (UpgradeInsecureRequests)
            {
                sb.Append("upgrade-insecure-requests;");
            }

            if (DisownOpener)
            {
                sb.Append("disown-opener");
            }

            return sb.ToString();

            void Append(string directive, ContentSecurityPolicyCollectionBuilder values)
            {
                if (values.Values.Any())
                {
                    sb.Append(directive);
                    foreach (var value in values.Values)
                    {
                        sb.Append(' ').Append(value);
                    }

                    sb.Append(';');
                }
            }
        }
    }
}
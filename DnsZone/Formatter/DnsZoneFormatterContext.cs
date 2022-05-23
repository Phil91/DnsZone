using System;
using System.Net;
using System.Text;
using DnsZone.Records;

namespace DnsZone.Formatter {
    public class DnsZoneFormatterContext {
        private readonly bool _formatTimeInMilliseconds;

        private const string TAB_CHAR = "\t";

        public string Origin { get; set; }

        public TimeSpan? DefaultTtl { get; set; }


        public string PrevName { get; set; }

        public string PrevClass { get; set; }


        public DnsZoneFile Zone { get; }

        public StringBuilder Sb { get; }

        public DnsZoneFormatterContext(DnsZoneFile zone, StringBuilder sb, bool formatTimeInMilliseconds) {
            _formatTimeInMilliseconds = formatTimeInMilliseconds;
            Sb = sb;
            Zone = zone;
        }

        public void WritePreference(ushort val) {
            Sb.Append(val);
            Sb.Append(TAB_CHAR);
        }

        public void WritePreference(uint val) {
            Sb.Append(val);
            Sb.Append(TAB_CHAR);
        }

        public void WriteAndCompressDomainName(string val) {
            WriteValWithTab(CompressDomainName(val));
        }

        public void WriteValWithTab(string val) {
            Sb.Append(val);
            Sb.Append(TAB_CHAR);
        }

        public void WriteIpAddress(IPAddress val) {
            Sb.Append(val);
            Sb.Append(TAB_CHAR);
        }

        public void WriteTimeSpan(TimeSpan val) {
            Sb.Append(DnsZoneUtils.FormatTimeSpan(val, _formatTimeInMilliseconds));
            Sb.Append(TAB_CHAR);
        }

        public void WriteDateTime(DateTime val, string format) {
            Sb.Append(val.ToString(format));
            Sb.Append(TAB_CHAR);
        }

        public void WriteString(string val) {
            val = val
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
            Sb.Append($"\"{val}\"");
            Sb.Append(TAB_CHAR);
        }

        public void WriteOrigin(string origin) {
            Sb.AppendLine($"$ORIGIN {origin}.");
        }

        public void WriteResourceRecordType(ResourceRecordType  val) {
            Sb.Append(DnsZoneUtils.FormatResourceRecordType(val));
            Sb.Append(TAB_CHAR);
        }

        public string CompressDomainName(string val) {
            if (val == Origin) {
                return "@";
            }
            var relativeSuffix = "." + Origin;
            if (Origin != null && val.EndsWith(relativeSuffix)) {
                return val.Substring(0, val.Length - relativeSuffix.Length);
            }
            return val + ".";
        }

    }
}

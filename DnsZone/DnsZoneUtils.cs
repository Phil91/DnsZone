﻿using System;
using System.Globalization;
using System.Text;
using DnsZone.Records;

namespace DnsZone {
    public static class DnsZoneUtils {

        public static ResourceRecordType ParseResourceRecordType(string val) {
            switch (val.ToUpperInvariant()) {
                case "A": return ResourceRecordType.A;
                case "AAAA": return ResourceRecordType.AAAA;
                case "NS": return ResourceRecordType.NS;
                case "MD": return ResourceRecordType.MD;
                case "MF": return ResourceRecordType.MF;
                case "CNAME": return ResourceRecordType.CNAME;
                case "SOA": return ResourceRecordType.SOA;
                case "MB": return ResourceRecordType.MB;
                case "MG": return ResourceRecordType.MG;
                case "MR": return ResourceRecordType.MR;
                case "NULL": return ResourceRecordType.NULL;
                case "WKS": return ResourceRecordType.WKS;
                case "PTR": return ResourceRecordType.PTR;
                case "SRV": return ResourceRecordType.SRV;
                case "SPF": return ResourceRecordType.SPF;
                case "SSHFP": return ResourceRecordType.SSHFP;
                case "HINFO": return ResourceRecordType.HINFO;
                case "MX": return ResourceRecordType.MX;
                case "TXT": return ResourceRecordType.TXT;
                case "CAA": return ResourceRecordType.CAA;
                case "TLSA": return ResourceRecordType.TLSA;
                case "ALIAS": return ResourceRecordType.ALIAS;
                case "DNAME": return ResourceRecordType.DNAME;
                case "DS": return ResourceRecordType.DS;
                case "LUA": return ResourceRecordType.LUA;
                case "NAPTR": return ResourceRecordType.NAPTR;
                case "DNSKEY": return ResourceRecordType.DNSKEY;
                case "NSEC": return ResourceRecordType.NSEC;
                case "NSEC3": return ResourceRecordType.NSEC3;
                case "NSEC3PARAM": return ResourceRecordType.NSEC3PARAM;
                case "RRSIG": return ResourceRecordType.RRSIG;
                default:
                    throw new NotSupportedException($"unsupported resource record type {val}");
            }
        }

        public static string FormatResourceRecordType(ResourceRecordType val) {
            switch (val) {
                case ResourceRecordType.A: return "A";
                case ResourceRecordType.AAAA: return "AAAA";
                case ResourceRecordType.NS: return "NS";
                case ResourceRecordType.MD: return "MD";
                case ResourceRecordType.MF: return "MF";
                case ResourceRecordType.CNAME: return "CNAME";
                case ResourceRecordType.SOA: return "SOA";
                case ResourceRecordType.MB: return "MB";
                case ResourceRecordType.MG: return "MG";
                case ResourceRecordType.MR: return "MR";
                case ResourceRecordType.NULL: return "NULL";
                case ResourceRecordType.WKS: return "WKS";
                case ResourceRecordType.PTR: return "PTR";
                case ResourceRecordType.SRV: return "SRV";
                case ResourceRecordType.SPF: return "SPF";
                case ResourceRecordType.HINFO: return "HINFO";
                case ResourceRecordType.MX: return "MX";
                case ResourceRecordType.TXT: return "TXT";
                case ResourceRecordType.CAA: return "CAA";
                case ResourceRecordType.TLSA: return "TLSA";
                case ResourceRecordType.SSHFP: return "SSHFP";
                case ResourceRecordType.ALIAS: return "ALIAS";
                case ResourceRecordType.DNAME: return "DNAME";
                case ResourceRecordType.DS: return "DS";
                case ResourceRecordType.LUA: return "LUA";
                case ResourceRecordType.NAPTR: return "NAPTR";
                case ResourceRecordType.DNSKEY: return "DNSKEY";
                case ResourceRecordType.NSEC: return "NSEC";
                case ResourceRecordType.NSEC3: return "NSEC3";
                case ResourceRecordType.NSEC3PARAM: return "NSEC3PARAM";
                case ResourceRecordType.RRSIG: return "RRSIG";
                default:
                    throw new NotSupportedException($"unsupported resource record type {val}");
            }
        }


        public static ResourceRecord CreateRecord(ResourceRecordType type) {
            switch (type) {
                case ResourceRecordType.A: return new AResourceRecord();
                case ResourceRecordType.AAAA: return new AaaaResourceRecord();
                case ResourceRecordType.NS: return new NsResourceRecord();
                case ResourceRecordType.CNAME: return new CNameResourceRecord();
                case ResourceRecordType.SOA: return new SoaResourceRecord();
                case ResourceRecordType.PTR: return new PtrResourceRecord();
                case ResourceRecordType.SRV: return new SrvResourceRecord();
                case ResourceRecordType.HINFO: return new HinfoResourceRecord();
                case ResourceRecordType.MX: return new MxResourceRecord();
                case ResourceRecordType.TXT: return new TxtResourceRecord();
                case ResourceRecordType.CAA: return new CaaResourceRecord();
                case ResourceRecordType.TLSA: return new TlsaResourceRecord();
                case ResourceRecordType.SSHFP: return new SshfpResourceRecord();
                case ResourceRecordType.ALIAS: return new AliasResourceRecord();
                case ResourceRecordType.DNAME: return new DnameResourceRecord();
                case ResourceRecordType.DS: return new DsResourceRecord();
                case ResourceRecordType.LUA: return new LuaResourceRecord();
                case ResourceRecordType.NAPTR: return new NaptrResourceRecord();
                case ResourceRecordType.SPF: return new SpfResourceRecord();
                case ResourceRecordType.DNSKEY: return new DnskeyResourceRecord();
                case ResourceRecordType.NSEC: return new NsecResourceRecord();
                case ResourceRecordType.NSEC3: return new Nsec3ResourceRecord();
                case ResourceRecordType.NSEC3PARAM: return new Nsec3ParamResourceRecord();
                case ResourceRecordType.RRSIG: return new RrsigResourceRecord();
                default:
                    throw new NotSupportedException($"unsupported resource record type {type}");
            }
        }

        public static bool TryParseClass(string val, out string @class) {
            @class = null;
            switch (val.ToUpperInvariant()) {
                case "IN":
                    @class = "IN";
                    break;
                case "CS":
                    @class = "CS";
                    break;
                case "CH":
                    @class = "CH";
                    break;
                case "HS":
                    @class = "HS";
                    break;
                default:
                    return false;
            }
            return true;
        }

        public static TimeSpan ParseTimeSpan(string val) {
            var res = TimeSpan.Zero;
            int? part = null;
            foreach (var ch in val) {
                if (ch >= '0' && ch <= '9') {
                    part = (part ?? 0) * 10 + (ch - '0');
                } else {
                    if (part == null) throw new FormatException("timespan value expected");
                    switch (char.ToLowerInvariant(ch)) {
                        case 'w':
                            res += TimeSpan.FromDays(part.Value * 7);
                            break;
                        case 'd':
                            res += TimeSpan.FromDays(part.Value);
                            break;
                        case 'h':
                            res += TimeSpan.FromHours(part.Value);
                            break;
                        case 'm':
                            res += TimeSpan.FromMinutes(part.Value);
                            break;
                        case 's':
                            res += TimeSpan.FromSeconds(part.Value);
                            break;
                        default:
                            throw new NotSupportedException($"timespan unit {ch} is not supported");
                    }
                    part = null;
                }
            }
            if (part != null) {
                res += TimeSpan.FromSeconds(part.Value);
            }
            return res;
        }

        public static DateTime ParseDateTime(string val)
        {
            var provider = CultureInfo.InvariantCulture;
            var result = DateTime.ParseExact(val, "yyyyMMddHHmmss", provider);
            return result;
        }

        public static bool TryParseTimeSpan(string val, out TimeSpan timestamp) {
            timestamp = TimeSpan.Zero;
            int? part = null;
            foreach (var ch in val) {
                if (ch >= '0' && ch <= '9') {
                    part = (part ?? 0) * 10 + (ch - '0');
                } else {
                    if (part == null) return false;
                    switch (char.ToLower(ch)) {
                        case 'w':
                            timestamp += TimeSpan.FromDays(part.Value * 7);
                            break;
                        case 'd':
                            timestamp += TimeSpan.FromDays(part.Value);
                            break;
                        case 'h':
                            timestamp += TimeSpan.FromHours(part.Value);
                            break;
                        case 'm':
                            timestamp += TimeSpan.FromMinutes(part.Value);
                            break;
                        case 's':
                            timestamp += TimeSpan.FromSeconds(part.Value);
                            break;
                        default:
                            return false;
                    }
                    part = null;
                }
            }
            if (part != null) {
                timestamp += TimeSpan.FromSeconds(part.Value);
            }
            return true;
        }

        public static string FormatTimeSpan(TimeSpan val, bool formatTimeInSeconds) {
            var sb = new StringBuilder();

            if (formatTimeInSeconds)
            {
                sb.Append(val.TotalSeconds);
                return sb.ToString();
            }

            var weeks = Math.Floor(val.TotalDays / 7);
            if (weeks > 0) {
                sb.Append($"{weeks}w");
                val = val.Subtract(TimeSpan.FromDays(7 * weeks));
            }

            var days = Math.Floor(val.TotalDays);
            if (days > 0) {
                sb.Append($"{days}d");
                val = val.Subtract(TimeSpan.FromDays(days));
            }

            var hours = Math.Floor(val.TotalHours);
            if (hours > 0) {
                sb.Append($"{hours}h");
                val = val.Subtract(TimeSpan.FromHours(hours));
            }

            var minutes = Math.Floor(val.TotalMinutes);
            if (minutes > 0) {
                sb.Append($"{minutes}m");
                val = val.Subtract(TimeSpan.FromMinutes(minutes));
            }

            var seconds = Math.Floor(val.TotalSeconds);
            if (seconds > 0) {
                sb.Append(sb.Length > 0 ? $"{seconds}s" : $"{seconds}");
            }

            return sb.ToString();
        }
    }
}

﻿using System;
using System.Linq;
using System.Text;
using DnsZone.Records;
using DnsZone.Tokens;

namespace DnsZone.Parser {
    public class ResourceRecordReader : IResourceRecordVisitor<DnsZoneParseContext, ResourceRecord> {

        public ResourceRecord Visit(AResourceRecord record, DnsZoneParseContext context) {
            record.Address = context.ReadIpAddress();
            return record;
        }

        public ResourceRecord Visit(AaaaResourceRecord record, DnsZoneParseContext context) {
            record.Address = context.ReadIpAddress();
            return record;
        }

        public ResourceRecord Visit(AliasResourceRecord record, DnsZoneParseContext context) {
            record.Content = context.ReadString();
            return record;
        }

        public ResourceRecord Visit(CNameResourceRecord record, DnsZoneParseContext context) {
            record.CanonicalName = context.ReadDomainName();
            return record;
        }

        public ResourceRecord Visit(DnameResourceRecord record, DnsZoneParseContext context) {
            record.Content = context.ReadString();
            return record;
        }

        public ResourceRecord Visit(DsResourceRecord record, DnsZoneParseContext context) {
            record.KeyTag = context.ReadSerialNumber();
            record.Algorithm = context.ReadPreference();
            record.HashType = context.ReadPreference();
            record.Hash = context.ReadSerialNumber();
            return record;
        }

        public ResourceRecord Visit(HinfoResourceRecord record, DnsZoneParseContext context) {
            record.Cpu = context.ReadStringValue();
            record.Os = context.ReadStringValue();
            return record;
        }

        public ResourceRecord Visit(LuaResourceRecord record, DnsZoneParseContext context) {
            record.TargetType = context.ReadString();
            record.Script = context.ReadStringValue();
            return record;
        }

        public ResourceRecord Visit(MxResourceRecord record, DnsZoneParseContext context) {
            record.Preference = context.ReadPreference();
            record.Exchange = context.ReadAndResolveDomainName();
            return record;
        }

        public ResourceRecord Visit(NaptrResourceRecord record, DnsZoneParseContext context) {
            record.Order = context.ReadPreference();
            record.Preference = context.ReadPreference();
            record.Flags = context.ReadStringValue();
            record.Services = context.ReadStringValue();
            record.Regexp = context.ReadStringValue();
            record.Replacement = context.ReadAndResolveDomainName();

            return record;
        }

        public ResourceRecord Visit(NsResourceRecord record, DnsZoneParseContext context) {
            record.NameServer = context.ReadAndResolveDomainName();
            return record;
        }

        public ResourceRecord Visit(PtrResourceRecord record, DnsZoneParseContext context) {
            record.HostName = context.ReadAndResolveDomainName();
            return record;
        }

        public ResourceRecord Visit(SoaResourceRecord record, DnsZoneParseContext context) {
            record.NameServer = context.ReadAndResolveDomainName();
            record.ResponsibleEmail = context.ReadEmail();
            record.SerialNumber = context.ReadSerialNumber();
            record.Refresh = context.ReadTimeSpan();
            record.Retry = context.ReadTimeSpan();
            record.Expiry = context.ReadTimeSpan();
            record.Minimum = context.ReadTimeSpan();
            return record;
        }

        public ResourceRecord Visit(SrvResourceRecord record, DnsZoneParseContext context) {
            record.Priority = context.ReadPreference();
            record.Weight = context.ReadPreference();
            record.Port = context.ReadPreference();
            record.Target = context.ReadAndResolveDomainName();
            return record;
        }

        public ResourceRecord Visit(TxtResourceRecord record, DnsZoneParseContext context) {
            var sb = new StringBuilder();
            while (!context.IsEof) {
                var token = context.Tokens.Peek();
                if (token.Type == TokenType.NewLine) break;
                if (token.Type == TokenType.QuotedString || token.Type == TokenType.Literal) {
                    sb.Append(token.StringValue);
                    context.Tokens.Dequeue();
                } else {
                    throw new NotSupportedException($"unexpected token {token.Type}");
                }
            }
            record.Content = sb.ToString();
            return record;
        }

        public ResourceRecord Visit(CaaResourceRecord record, DnsZoneParseContext context) {
            record.Flag = context.ReadPreference();
            record.Tag = context.Tokens.Dequeue().StringValue;
            var sb = new StringBuilder();
            while (!context.IsEof) {
                var token = context.Tokens.Peek();
                if (token.Type == TokenType.NewLine) break;
                if (token.Type == TokenType.QuotedString || token.Type == TokenType.Literal) {
                    sb.Append(token.StringValue);
                    context.Tokens.Dequeue();
                } else {
                    throw new NotSupportedException($"unexpected token {token.Type}");
                }
            }
            record.Value = sb.ToString();

            return record;
        }

        public ResourceRecord Visit(TlsaResourceRecord record, DnsZoneParseContext context) {
            record.CertificateUsage = context.ReadPreference();
            record.Selector = context.ReadPreference();
            record.MatchingType = context.ReadPreference();
            record.CertificateAssociationData = context.ReadString();

            return record;
        }

        public ResourceRecord Visit(SshfpResourceRecord record, DnsZoneParseContext context) {
            record.AlgorithmNumber = context.ReadPreference();
            record.FingerprintType = context.ReadPreference();
            record.Fingerprint = context.ReadString();

            return record;
        }
    }
}

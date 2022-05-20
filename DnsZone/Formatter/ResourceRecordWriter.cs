using DnsZone.Records;

namespace DnsZone.Formatter {
    public class ResourceRecordWriter : IResourceRecordVisitor<DnsZoneFormatterContext, ResourceRecord> {

        public ResourceRecord Visit(AResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteIpAddress(record.Address);
            return record;
        }

        public ResourceRecord Visit(AaaaResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteIpAddress(record.Address);
            return record;
        }

        public ResourceRecord Visit(AliasResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteValWithTab(record.Content);
            return record;
        }

        public ResourceRecord Visit(CNameResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteValWithTab(record.CanonicalName);
            return record;
        }

        public ResourceRecord Visit(DnameResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteValWithTab(record.Content);
            return record;
        }

        public ResourceRecord Visit(DsResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteValWithTab(record.KeyTag);
            context.WritePreference(record.Algorithm);
            context.WritePreference(record.HashType);
            context.WriteValWithTab(record.Hash);
            return record;
        }

        public ResourceRecord Visit(HinfoResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteString(record.Cpu);
            context.WriteString(record.Os);
            return record;
        }

        public ResourceRecord Visit(LuaResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteValWithTab(record.TargetType);
            context.WriteString(record.Script);
            return record;
        }

        public ResourceRecord Visit(MxResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Preference);
            context.WriteAndCompressDomainName(record.Exchange);
            return record;
        }

        public ResourceRecord Visit(NaptrResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Order);
            context.WritePreference(record.Preference);
            context.WriteString(record.Flags);
            context.WriteString(record.Services);
            context.WriteString(record.Regexp);
            context.WriteValWithTab(record.Replacement);
            return record;
        }

        public ResourceRecord Visit(CaaResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Flag);
            context.WriteValWithTab(record.Tag);
            context.WriteString(record.Value);
            return record;
        }

        public ResourceRecord Visit(TlsaResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.CertificateUsage);
            context.WritePreference(record.Selector);
            context.WritePreference(record.MatchingType);
            context.WriteValWithTab(record.CertificateAssociationData);
            return record;
        }

        public ResourceRecord Visit(SshfpResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.AlgorithmNumber);
            context.WritePreference(record.FingerprintType);
            context.WriteValWithTab(record.Fingerprint);
            return record;
        }

        public ResourceRecord Visit(NsResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteAndCompressDomainName(record.NameServer);
            return record;
        }

        public ResourceRecord Visit(PtrResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteAndCompressDomainName(record.HostName);
            return record;
        }

        public ResourceRecord Visit(SoaResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteAndCompressDomainName(record.NameServer);
            context.WriteValWithTab(record.ResponsibleEmail);
            context.WriteValWithTab(record.SerialNumber);
            context.WriteTimeSpan(record.Refresh);
            context.WriteTimeSpan(record.Retry);
            context.WriteTimeSpan(record.Expiry);
            context.WriteTimeSpan(record.Minimum);
            return record;
        }

        public ResourceRecord Visit(SrvResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Priority);
            context.WritePreference(record.Weight);
            context.WritePreference(record.Port);
            context.WriteAndCompressDomainName(record.Target);
            return record;
        }

        public ResourceRecord Visit(TxtResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteString(record.Content);
            return record;
        }

        public ResourceRecord Visit(SpfResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteString(record.Content);
            return record;
        }

        public ResourceRecord Visit(DnskeyResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Flags);
            context.WritePreference(record.Protokoll);
            context.WritePreference(record.HashType);
            context.WriteValWithTab(record.Hash);
            return record;
        }
        
        public ResourceRecord Visit(IsdnResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Adress);
            context.WritePreference(record.Subadress);
            return record;
        }
        
        public ResourceRecord Visit(NsecResourceRecord record, DnsZoneFormatterContext context) {
            context.WriteValWithTab(record.NextDomainName);
            context.WriteValWithTab(record.TypeList);
            return record;
        }
        
        public ResourceRecord Visit(Nsec3ResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Flags);
            context.WritePreference(record.Iteration);
            context.WriteValWithTab(record.Salt);
            context.WriteValWithTab(record.Hash);
            context.WriteValWithTab(record.TypesList);
            return record;
        }
        
        public ResourceRecord Visit(Nsec3ParamResourceRecord record, DnsZoneFormatterContext context) {
            context.WritePreference(record.Flags);
            context.WritePreference(record.Iteration);
            context.WriteValWithTab(record.Salt);
            return record;
        }
    }
}

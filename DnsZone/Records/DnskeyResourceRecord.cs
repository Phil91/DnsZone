namespace DnsZone.Records {
    public class DnskeyResourceRecord : ResourceRecord {
        
        public ushort Flags { get; set; }
        
        public ushort Protokoll { get; set; }
        
        public ushort HashType { get; set; }

        public string Hash { get; set; }

        public override ResourceRecordType Type => ResourceRecordType.DNSKEY;

        public override TResult AcceptVistor<TArg, TResult>(IResourceRecordVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

        public override string ToString() {
            return $"{Flags} {Protokoll} {HashType} {Hash}";
        }
    }
}

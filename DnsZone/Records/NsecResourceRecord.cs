namespace DnsZone.Records {
    public class NsecResourceRecord : ResourceRecord {

        public string NextDomainName { get; set; }

        public string TypeList { get; set; }

        public override ResourceRecordType Type => ResourceRecordType.NSEC;

        public override TResult AcceptVistor<TArg, TResult>(IResourceRecordVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

        public override string ToString()
        {
            return $"{NextDomainName} {TypeList}";
        }
    }
}

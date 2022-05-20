namespace DnsZone.Records 
{
    public class Nsec3ResourceRecord : ResourceRecord {

        public ushort Algorithm { get; set; }

        public ushort Flags { get; set; }

        public ushort Iteration { get; set; }

        public string Salt { get; set; }

        public string Hash { get; set; }

        public string TypesList { get; set; }

        public override ResourceRecordType Type => ResourceRecordType.NSEC3;

        public override TResult AcceptVistor<TArg, TResult>(IResourceRecordVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

        public override string ToString()
        {
            return $"{Algorithm} {Flags} {Iteration} {Salt} {Hash} {TypesList}";
        }
    }
}

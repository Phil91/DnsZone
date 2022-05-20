namespace DnsZone.Records 
{
    public class SpfResourceRecord : ResourceRecord {

        public string Content { get; set; }

        public override ResourceRecordType Type => ResourceRecordType.SPF;

        public override TResult AcceptVistor<TArg, TResult>(IResourceRecordVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

        public override string ToString() {
            return Content;
        }
    }
}

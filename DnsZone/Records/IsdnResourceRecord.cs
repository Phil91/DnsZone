using System;

namespace DnsZone.Records {
    public class IsdnResourceRecord : ResourceRecord {

        public uint Adress { get; set; }

        public uint Subadress { get; set; }

        public override ResourceRecordType Type => ResourceRecordType.ISDN;

        public override TResult AcceptVistor<TArg, TResult>(IResourceRecordVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

        public override string ToString()
        {
            var subadress = string.IsNullOrWhiteSpace(Subadress.ToString()) ? Subadress.ToString() : $" {Subadress}";
            return $"{Adress}{subadress}";
        }
    }
}

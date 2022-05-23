using System;

namespace DnsZone.Records {
    public class RrsigResourceRecord : ResourceRecord {

        public string TypeCovered { get; set; }

        public ushort Algorithm { get; set; }

        public ushort NumberOfLabels { get; set; }

        public TimeSpan OriginalTtl { get; set; }

        public DateTime ExpirationTime { get; set; }

        public DateTime InceptionTime { get; set; }

        public uint KeyTag { get; set; }

        public string SignatureName { get; set; }

        public string Signature { get; set; }

        public override ResourceRecordType Type => ResourceRecordType.RRSIG;

        public override TResult AcceptVistor<TArg, TResult>(IResourceRecordVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

        public override string ToString() {
            return $"{TypeCovered} {Algorithm} {NumberOfLabels} {OriginalTtl.TotalSeconds} {ExpirationTime.ToString("yyyyMMddHHmmss")} {InceptionTime.ToString("yyyyMMddHHmmss")} {KeyTag} {SignatureName} {Signature}";
        }
    }
}

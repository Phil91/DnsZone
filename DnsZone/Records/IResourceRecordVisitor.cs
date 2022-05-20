namespace DnsZone.Records {
    public interface IResourceRecordVisitor<in TArg, out TResult> {

        TResult Visit(AResourceRecord record, TArg arg);

        TResult Visit(AaaaResourceRecord record, TArg arg);

        TResult Visit(AliasResourceRecord record, TArg arg);

        TResult Visit(CNameResourceRecord record, TArg arg);

        TResult Visit(DnameResourceRecord record, TArg arg);

        TResult Visit(DsResourceRecord record, TArg arg);

        TResult Visit(HinfoResourceRecord record, TArg arg);

        TResult Visit(LuaResourceRecord record, TArg arg);

        TResult Visit(MxResourceRecord record, TArg arg);

        TResult Visit(NaptrResourceRecord record, TArg arg);

        TResult Visit(NsResourceRecord record, TArg arg);

        TResult Visit(PtrResourceRecord record, TArg arg);

        TResult Visit(SoaResourceRecord record, TArg arg);

        TResult Visit(SrvResourceRecord record, TArg arg);

        TResult Visit(TxtResourceRecord record, TArg arg);

        TResult Visit(CaaResourceRecord record, TArg arg);

        TResult Visit(TlsaResourceRecord record, TArg arg);

        TResult Visit(SshfpResourceRecord record, TArg arg);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using DnsZone.IO;
using DnsZone.Records;
using DnsZone.Tokens;

namespace DnsZone.Parser {
    public class DnsZoneParseContext {

        public IDnsSource Source { get; }

        protected string FileName { get; set; }


        public string Origin { get; set; }

        public TimeSpan? DefaultTtl { get; set; }


        public string PrevName { get; set; }

        public string PrevClass { get; set; }


        public DnsZoneFile Zone { get; }

        public Queue<Token> Tokens { get; }

        public bool IsEof => Tokens.Count == 0;

        public DnsZoneParseContext(IEnumerable<Token> tokens, IDnsSource source) {
            Source = source;
            Zone = new DnsZoneFile();
            Tokens = new Queue<Token>(tokens);
        }

        public DnsZoneParseContext CreateChildContext(string fileName) {
            var newFile = Source.ResolveFile(fileName, FileName);

            var tokenizer = new Tokenizer();
            var fileSource = new FileSource {
                Content = Source.LoadContent(newFile)
            };
            var tokens = tokenizer.Read(fileSource).ToArray();
            return new DnsZoneParseContext(tokens, Source) {
                FileName = newFile
            };
        }

        public ushort ReadPreference() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("preference expected", token);
            return ushort.Parse(token.StringValue);
        }

        public uint ReadNumber() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("preference expected", token);
            return uint.Parse(token.StringValue);
        }

        public string ReadDomainName() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("domain name expected", token);
            return token.StringValue;
        }

        public string ReadAndResolveDomainName() {
            return ResolveDomainName(ReadDomainName());
        }

        public string ReadEmail() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("email expected", token);
            return token.StringValue;
        }

        public string ReadString()
        {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("string expected", token);
            return token.StringValue;
        }

        public string ReadStringValue()
        {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.QuotedString) throw new TokenException("string expected", token);
            return token.StringValue;
        }

        public string ReadSerialNumber() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("serial number expected", token);
            return token.StringValue;
        }

        public IPAddress ReadIpAddress() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("ipaddress expected", token);
            var ipv4 = new Regex(@"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}");
            var ipv6 = new Regex(@"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))");
            if ((!ipv4.IsMatch(token.StringValue) && !ipv6.IsMatch(token.StringValue)) || !IPAddress.TryParse(token.StringValue, out var ipAddress))
            {
                throw new TokenException("IP-Address is not valid.", token);
            }

            return ipAddress;
        }

        public TimeSpan ReadTimeSpan() {
            var val = Tokens.Dequeue().StringValue;
            return DnsZoneUtils.ParseTimeSpan(val);
        }

        public DateTime ReadDateTime() {
            var val = Tokens.Dequeue().StringValue;
            return DnsZoneUtils.ParseDateTime(val);
        }

        public ResourceRecordType ReadResourceRecordType() {
            var token = Tokens.Dequeue();
            if (token.Type != TokenType.Literal) throw new TokenException("resource record type expected", token);
            return DnsZoneUtils.ParseResourceRecordType(token.StringValue);
        }

        public string ResolveDomainName(string val) {
            if (val == "@") {
                if (string.IsNullOrWhiteSpace(Origin)) {
                    throw new ArgumentException("couldn't resolve @ domain");
                }
                return Origin;
            }

            var ipv4 = new Regex(@"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}");
            var ipv6 = new Regex(@"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))");
            if (!val.EndsWith(".") && ((!ipv4.IsMatch(val) && !ipv6.IsMatch(val)) || !IPAddress.TryParse(val, out var ipAddress))) {
                if (string.IsNullOrWhiteSpace(Origin)) {
                    throw new ArgumentException("couldn't resolve relative domain name");
                }
                val = val + "." + Origin;
            } else {
                val = val.TrimEnd('.');
            }
            return val;
        }

        public TimeSpan GetTimeSpan(TimeSpan? explicitValue) {
            if (explicitValue.HasValue) return explicitValue.Value;
            if (DefaultTtl.HasValue) return DefaultTtl.Value;
            throw new NotSupportedException("unknown ttl value");
        }

        public bool TryParseTtl(out TimeSpan? timestamp) {
            if (TryParseTtl(out TimeSpan val)) {
                timestamp = val;
                return true;
            }
            timestamp = null;
            return false;
        }

        public bool TryParseClass(out string @class) {
            @class = null;
            var token = Tokens.Peek();
            if (token.Type != TokenType.Literal) return false;
            if (DnsZoneUtils.TryParseClass(token.StringValue, out @class)) {
                Tokens.Dequeue();
                return true;
            }
            return false;
        }

        protected void SkipWhiteAndComments() {
            while (!IsEof) {
                var token = Tokens.Peek();
                switch (token.Type) {
                    case TokenType.Whitespace:
                    case TokenType.Comments:
                        Tokens.Dequeue();
                        break;
                    default:
                        return;
                }
            }
        }
        
        private bool TryParseTtl(out TimeSpan val) {
            val = TimeSpan.Zero;
            var token = Tokens.Peek();
            if (token.Type != TokenType.Literal || !DnsZoneUtils.TryParseTimeSpan(token.StringValue, out val)) return false;

            Tokens.Dequeue();
            return true;
        }
    }
}

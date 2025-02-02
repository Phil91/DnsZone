﻿using System.Linq;
using DnsZone.Records;
using NUnit.Framework;

namespace DnsZone.Tests.Records {
    [TestFixture]
    public class AliasResourceRecordTests {

        [Test]
        public void ParseTest() {
            const string str = @"
; zone fragment example.com
; mail servers in the same zone
; will support incoming email with addresses of the format 
; user@example.com
$TTL 2d ; zone default = 2 days or 172800 seconds
$ORIGIN example.com.
test           IN  ALIAS     host.external.org          ; Alias for test.example.com
alias1         IN  ALIAS     new.origin.com             ; Alias for alias1.example.com";
            var zone = DnsZoneFile.Parse(str);
            Assert.AreEqual(2, zone.Records.Count);

            Assert.IsAssignableFrom<AliasResourceRecord>(zone.Records.First());

            var record = (AliasResourceRecord)zone.Records.First();
            Assert.AreEqual("test.example.com", record.Name);
            Assert.AreEqual("IN", record.Class);
            Assert.AreEqual(ResourceRecordType.ALIAS, record.Type);
            Assert.AreEqual("host.external.org", record.Content);
            Assert.AreEqual("host.external.org", record.ToString());
        }
        
        [Test]
        public void OutputTest() {
            var zone = new DnsZoneFile();

            var record = new AliasResourceRecord() {
                Name = "example.com",
                Class = "IN",
                Content = "host.external.org",
            };
            
            zone.Records.Add(record);
            var sOutput = zone.ToString();
            Assert.AreEqual(";ALIAS records\r\nexample.com.\tIN\t\tALIAS\thost.external.org\t\r\n\r\n", sOutput);
        }
    }
}

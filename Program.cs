using System;

namespace csharp_pem_reader {
  class MainClass {
    public static void Main(string[] args) {
      string pubKey = 
@"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAu5ZkhqFQLzl2dOCH2aSf
K6BG/crDnf2d7JCy0rEkkCU7iH1T55wGKAVPjsWYDXrqezWLdwgW7xIQDk8tF9tT
rdDGe3kmUqjXo/bNOYT8y6eAuibkYDrtVT0Sh6XBF5+pxDOhQolHV14hTmsJKo9E
0fI12FA44F0ifwNY+BAQmihCW7X6KqaOOB0nA+bxptcmYVjfUVGPuTdebQjJ83b9
hJ7BRoXYegWRBNaBR9qvXj1JuwpgO/higWYBU/IZJgfqQC06qsqAAymt+X4GBHLI
wnR5bB9G0Pk5ni3sLjgyIL9kpmLfL6CmmNcNA5P5ImMMweueGuw6PNZ75KZw+TGf
FQIDAQAB
-----END PUBLIC KEY-----";

      string result = PemReader.DecodePublicKey(pubKey);
      Console.WriteLine(result);
    }
  }
}

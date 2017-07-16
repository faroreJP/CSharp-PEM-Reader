//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// PEM Reader
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.PEM {
  public class Reader {
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private static readonly string    BeginPublicKeyString  = "-----BEGIN PUBLIC KEY-----";
    private static readonly string    EndPublicKeyString    = "-----END PUBLIC KEY-----";
    private static readonly string[]  StringSeparators      = new string[] {System.Environment.NewLine};

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Decode PEM formatted public key
    public static string DecodePublicKey (string pemString) {
      System.Console.WriteLine("[PemReader] INPUT PEM STRING : " + System.Environment.NewLine + pemString + System.Environment.NewLine);

      string[] splitString = pemString.Split(StringSeparators, System.StringSplitOptions.None);

      //------------------------------------------------------------
      // Validate input string
      if (splitString[0] != BeginPublicKeyString || splitString[splitString.Length - 1] != EndPublicKeyString) {
        throw new System.ArgumentException("[PemReader] Invalid input string! : " + System.Environment.NewLine + pemString);
      }

      //------------------------------------------------------------
      // Join BASE64 string
      var base64StringBuilder = new System.Text.StringBuilder();
      for (int i = 1;i < splitString.Length - 1;i++) {
        base64StringBuilder.Append(splitString[i]);
      }

      //------------------------------------------------------------
      // Decode BASE64 string
      string base64String = base64StringBuilder.ToString();
      byte[] berBytes    = System.Convert.FromBase64String(base64String);

      System.Console.WriteLine("[PemReader] BER BINARY LENGTH : " + berBytes.Length);

      //------------------------------------------------------------
      // Decode ASN.1 binary
      var berObj = FaroreUtil.BER.Decoder.Decode(berBytes);
      var contents = (FaroreUtil.BER.Data[])((FaroreUtil.BER.ValueSequence)berObj.Value).Value;

      //------------------------------------------------------------
      // Decode ASN.1 binary (PUBLIC KEY)
      var key = FaroreUtil.BER.Decoder.Decode( (byte[])contents[1].Value.Value );

      return key.ToString();
    }
  }
}

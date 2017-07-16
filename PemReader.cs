//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// PEM Reader
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil {
  public class PemReader {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private static readonly string    BeginPublicKeyString  = "-----BEGIN PUBLIC KEY-----";
    private static readonly string    EndPublicKeyString    = "-----END PUBLIC KEY-----";
    private static readonly string[]  StringSeparators      = new string[] {System.Environment.NewLine};

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Decode PEM formatted public key
    public static string DecodePublicKey (string pemString) {
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

      //------------------------------------------------------------
      // Decode ASN.1 binary
      var bufferBytes = berBytes;

      // Read tag
      var tagInfo = ReadTag(bufferBytes);
      bufferBytes = ReadOn(bufferBytes, tagInfo.TagLength);

      // Read length
      var lengthInfo = ReadLength(bufferBytes);
      bufferBytes    = ReadOn(bufferBytes, 1); // TODO

      return string.Format("[TAG] {1}{0}", System.Environment.NewLine, tagInfo.ToString());
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Private
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private static byte[] ReadOn (byte[] bytes, int readLength) {
      byte[] readBytes = new byte[bytes.Length - readLength];
      System.Buffer.BlockCopy(bytes, 0, readBytes, 0, readLength);
      return readBytes;
    }

    private static TagInfo ReadTag (byte[] tag) {
      byte                  readCode    = 0;
      TagInfo.eClassType    classType   = TagInfo.eClassType.Universal;
      TagInfo.eContentType  contentType = TagInfo.eContentType.Primitive; 
      int                   tagLength   = 0;
      int                   tagNumber   = 0;

      //------------------------------------------------------------
      // Read class type
      readCode = (byte)((tag[0] & 0xC0) >> 6);
      switch (readCode) {
        case 0x00:  classType = TagInfo.eClassType.Universal;       break;
        case 0x01:  classType = TagInfo.eClassType.Application;     break;
        case 0x02:  classType = TagInfo.eClassType.ContentSpecific; break;
        case 0x03:  classType = TagInfo.eClassType.Private;         break;
      }

      //------------------------------------------------------------
      // Read content type
      contentType = ((tag[0] & 0x20) == 0x00 ? TagInfo.eContentType.Primitive : TagInfo.eContentType.Constructed);

      //------------------------------------------------------------
      // Read length
      readCode = (byte)(tag[0] & 0x1f);

      tagLength = 1;
      if (readCode == 0x1f) {
        throw new System.NotSupportedException("Any length tag is not supported!");
      }
      else {
        tagNumber = (int)readCode;
      }

      //------------------------------------------------------------
      // Create tag info instance
      return new TagInfo(classType, contentType, tagLength, tagNumber);
    }

    private static LengthInfo ReadLength (byte[] berBytes) {
      byte readCode       = berBytes[0];
      bool isMsbEnabled   = ((readCode & 0x80) != 0x00);
      byte bit7to1        = (byte)(readCode & 0x7f);
      bool isLongDefinite = (isMsbEnabled && bit7to1 != 0x00);

      if (isLongDefinite) {
        throw new System.NotSupportedException("Long definite length is not supported!");
      }
      else {
        return new LengthInfo((int)bit7to1);
      }
    }
  }
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Value : Bit String
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class ValueBitString : ValueBase {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ValueBitString (byte[] bytes) : base (Decode(bytes)) {}

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Private
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private static byte[] Decode (byte[] bytes) {
      // Copy bit string
      var decodedBytes = new byte[bytes.Length - 1];
      System.Buffer.BlockCopy(bytes, 1, decodedBytes, 0, decodedBytes.Length);

      // Truncate
      byte bit = (byte)(0xff << bytes[0]);
      decodedBytes[decodedBytes.Length - 1] = (byte)(decodedBytes[decodedBytes.Length - 1] & bit);

      return decodedBytes;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      var bytes   = (byte[])Value;
      var builder = new System.Text.StringBuilder();

      int column = 0;

      builder.Append(bytes[0]);
      for (int i = 1;i < bytes.Length;i++) {
        column++;

        builder.Append(":");

        if (column % 15 == 0) builder.AppendLine();
        builder.Append(bytes[i].ToString("X2"));
      }

      return builder.ToString();
    }
  }
}





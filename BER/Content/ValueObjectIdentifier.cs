//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Value : Object Identifier
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class ValueObjectIdentifier : ValueBase {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ValueObjectIdentifier (byte[] bytes) : base (Decode(bytes)) {}

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Private
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private static byte[] Decode (byte[] bytes) {
      var oidList = new System.Collections.Generic.List<byte>();

      oidList.Add((byte)(bytes[0] / 40));
      oidList.Add((byte)(bytes[0] % 40));

      for (int i = 1;i < bytes.Length;i++) {
        oidList.Add((byte)(bytes[i] & 0x7f));
        if ((bytes[i] & 0x80) == 0) {
          break;
        }
      }

      return oidList.ToArray();
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      var bytes   = (byte[])Value;
      var builder = new System.Text.StringBuilder();

      builder.Append("{");
      builder.Append(bytes[0]);
      for (int i = 1;i < bytes.Length;i++) {
        builder.Append(", ");
        builder.Append(bytes[i]);
      }
      builder.Append("}");

      return builder.ToString();
    }
  }
}





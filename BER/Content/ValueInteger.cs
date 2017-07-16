//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Value : Integer
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class ValueInteger : ValueBase {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ValueInteger (byte[] bytes) : base (bytes) {}

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      var bytes   = (byte[])Value;
      var builder = new System.Text.StringBuilder();

      int column = 0;

      builder.Append(bytes[0].ToString("X2"));
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






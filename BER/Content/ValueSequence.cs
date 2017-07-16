//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Value : Sequence
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class ValueSequence : ValueBase {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ValueSequence (byte[] bytes) : base (Decode(bytes)) {}

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Private
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private static Data[] Decode (byte[] bytes) {
      var buffer   = bytes;
      var dataList = new System.Collections.Generic.List<Data>();

      do {
        var data = Decoder.Decode(buffer);
        dataList.Add(data);

        if (data.ByteSize >= buffer.Length) {
          break;
        }

        buffer = Decoder.ReadOn(buffer, data.ByteSize);
      } while (true);

      return dataList.ToArray();
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      var dataArray = (Data[])Value;
      var builder   = new System.Text.StringBuilder();

      builder.AppendLine();
      builder.Append(dataArray[0].ToString());
      for (int i = 1;i < dataArray.Length;i++) {
        builder.AppendLine();
        builder.Append(dataArray[i].ToString());
      }

      return builder.ToString();
    }
  }
}


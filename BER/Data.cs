//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Object
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class Data {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public TagInfo    Tag     { get; private set; }
    public LengthInfo Length  { get; private set; }
    public ValueBase  Value   { get; private set; }

    public int ByteSize { 
      get {
        return Tag.ByteSize + Length.ByteSize + Length.Length;
      }
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public Data (TagInfo tag, LengthInfo length, ValueBase value) {
      Tag     = tag;
      Length  = length;
      Value   = value;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      return string.Format("{1}{0}{2}{0}{3}", System.Environment.NewLine, Tag.ToString(), Length.ToString(), Value.ToString());
    }
  }
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Value : Octet String
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class ValueNull : ValueBase {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ValueNull () : base (new byte[0]) {}
  }
}



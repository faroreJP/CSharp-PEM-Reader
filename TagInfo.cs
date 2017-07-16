//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ASN.1(BER) Tag Info
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil {
  public class TagInfo {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Enum
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public enum eClassType {
      Universal,
      Application,
      ContentSpecific,
      Private,
    }

    public enum eContentType {
      Primitive,
      Constructed,
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field / Property
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public eClassType   ClassType     { get; private set; }
    public eContentType ContentType   { get; private set; }
    public int          TagNumber     { get; private set; }
    public int          ByteSize      { get; private set; }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public TagInfo (eClassType classType, eContentType contentType, int tagNumber, int byteSize) {
      ClassType   = classType;
      ContentType = contentType;
      TagNumber   = tagNumber;
      ByteSize    = byteSize;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      return string.Format("ClassType : {0}, ContentType : {1}, TagNumber : {2}, ByteSize : {3}", ClassType, ContentType, TagNumber, ByteSize);
    }
  }
}


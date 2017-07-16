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
    public int          TagLength     { get; private set; }
    public int          TagNumber     { get; private set; }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public TagInfo (eClassType classType, eContentType contentType, int tagLength, int tagNumber) {
      ClassType   = classType;
      ContentType = contentType;
      TagLength   = tagLength;
      TagNumber   = tagNumber;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      return string.Format("ClassType : {0}, ContentType : {1}, TagLength : {2}, TagNumber : {3}", ClassType, ContentType, TagLength, TagNumber);
    }
  }
}


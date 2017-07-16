//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ASN.1(BER) Tag Info
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
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

    // Read tag data from buffered bytes
    // @Param :
    //  [in] bytes - buffered byte array
    // @Return :
    //  tag data for current content
    public static TagInfo ReadTag (byte[] bytes) {
      byte          readCode    = 0;
      eClassType    classType   = eClassType.Universal;
      eContentType  contentType = eContentType.Primitive; 
      int           byteSize    = 0;
      int           tagNumber   = 0;

      //------------------------------------------------------------
      // Read class type
      readCode = (byte)((bytes[0] & 0xC0) >> 6);
      switch (readCode) {
        case 0x00:  classType = eClassType.Universal;       break;
        case 0x01:  classType = eClassType.Application;     break;
        case 0x02:  classType = eClassType.ContentSpecific; break;
        case 0x03:  classType = eClassType.Private;         break;
      }

      //------------------------------------------------------------
      // Read content type 
      contentType = ((bytes[0] & 0x20) == 0x00 ? eContentType.Primitive : eContentType.Constructed);

      //------------------------------------------------------------
      // Read length
      readCode = (byte)(bytes[0] & 0x1f);

      byteSize = 1;
      if (readCode == 0x1f) {
        throw new System.NotSupportedException("Any length tag is not supported!");
      }
      else {
        tagNumber = (int)readCode;
      }

      //------------------------------------------------------------
      // Create tag info instance
      return new TagInfo(classType, contentType, tagNumber, byteSize);
    }
  }
}

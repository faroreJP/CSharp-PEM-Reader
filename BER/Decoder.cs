//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Decoder
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class Decoder {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Decode BER binary
    // @Param :
    //  [in] bytes - BER byte array
    public static Data Decode (byte[] bytes) {
      // System.Console.WriteLine("[Decoder] Start BER decoding ... ");
      byte[] buffer = bytes;

      // Read tag
      var tag = TagInfo.ReadTag(buffer);
      buffer  = ReadOn(buffer, tag.ByteSize);
      // System.Console.WriteLine(tag.ToString());

      // Read length
      var length = LengthInfo.ReadLength(buffer);
      buffer     = ReadOn(buffer, length.ByteSize); 
      // System.Console.WriteLine(length.ToString());

      // Read value
      if (length.IsInfinite) {
        throw new System.NotSupportedException("Infinite length value is not supported!");
      }
      else if (length.Length > 0) {
        var value = ReadValue(tag, length, buffer);
        buffer    = ReadOn(buffer, length.Length);
        return new Data(tag, length, value);
      }
      else {
        return new Data(tag, length, new ValueNull());
      }
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Private
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Read on buffer byte array
    // @Param :
    //  [in] bytes      - buffer byte array
    //  [in] readLength - length for read block size
    public static byte[] ReadOn (byte[] bytes, int readLength) {
      byte[] readBytes = new byte[bytes.Length - readLength];
      System.Buffer.BlockCopy(bytes, readLength, readBytes, 0, readBytes.Length);
      return readBytes;
    }

    // Read value data
    // @Param :
    //  [in] tag    - value tag data
    //  [in] length - value length
    //  [in] bytes  - value byte array
    private static ValueBase ReadValue (TagInfo tag, LengthInfo length, byte[] bytes) {
      byte[] bufferBytes = null;

      if (length.IsInfinite) {
        int endOfContentIndex = 0;
        for (int i = 1;i < bytes.Length;i++) {
          if (bytes[i - 1] == 0x00 && bytes[i] == 0x00) {
            endOfContentIndex = i;
            break;
          }
        }

        if (endOfContentIndex == 0) {
          throw new System.ArgumentException("End of Content is not found! : ");
        }

        bufferBytes = new byte[endOfContentIndex - 1];
        System.Buffer.BlockCopy(bytes, 0, bufferBytes, 0, bufferBytes.Length);
      }
      else {
        bufferBytes = new byte[length.Length];
        System.Buffer.BlockCopy(bytes, 0, bufferBytes, 0, bufferBytes.Length);
      }

      switch (tag.TagNumber) {
        case 2:  return new ValueInteger(bufferBytes);
        case 3:  return new ValueBitString(bufferBytes);
        case 5:  return new ValueNull();
        case 6:  return new ValueObjectIdentifier(bufferBytes);
        case 16: return new ValueSequence(bufferBytes);

      }

      return null;
    }
  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageReadCS
{
    public class GrayscaleFloatImage
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public readonly float[] rawdata;

        public GrayscaleFloatImage(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            rawdata = new float[Width * Height];
        }

        public float this[int x, int y]
        {
            get
            {
#if DEBUG
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", x, y, Width, Height));
#endif
                return rawdata[y * Width + x];
            }
            set
            {
#if DEBUG
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", x, y, Width, Height));
#endif
                rawdata[y * Width + x] = value;
            }
        }

        public GrayscaleByteImage ToGrayscaleByteImage()
        {
            GrayscaleByteImage res = new GrayscaleByteImage(Width, Height);
            for (int i = 0; i < res.rawdata.Length; i++)
                res.rawdata[i] = rawdata[i] < 0.0f ? (byte)0 : rawdata[i] > 255.0f ? (byte)255 : (byte)rawdata[i];
            return res;
        }

        public ColorFloatImage ToColorFloatImage()
        {
            ColorFloatImage res = new ColorFloatImage(Width, Height);
            for (int i = 0; i < res.rawdata.Length; i++)
                res.rawdata[i] = new ColorFloatPixel() { b = rawdata[i], g = rawdata[i], r = rawdata[i], a = 0.0f };
            return res;
        }

        public ColorByteImage ToColorByteImage()
        {
            ColorByteImage res = new ColorByteImage(Width, Height);
            for (int i = 0; i < res.rawdata.Length; i++)
            {
                byte c = rawdata[i] < 0.0f ? (byte)0 : rawdata[i] > 255.0f ? (byte)255 : (byte)rawdata[i];
                res.rawdata[i] = new ColorBytePixel() { b = c, g = c, r = c, a = 0 };
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperTest.NFIQ2
{
    public class PGMImage
    {
        public byte[] Pixels { get; }
        public uint Height { get; }
        public uint Width { get; }
        public int MaxVal { get; }

        public PGMImage(byte[] pixels, uint width, uint height, int maxVal)
        {
            Pixels = pixels;
            Height = height;
            Width = width;
            MaxVal = maxVal;
        }

        // Source: https://jamesmccaffrey.wordpress.com/2014/10/21/a-pgm-image-viewer-using-c/
        public static PGMImage Load(string filePath)
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
            {
                // Parse Magic Number
                string magic = NextNonCommentLine(reader);
                if (magic != "P5")
                {
                    throw new Exception($"Unknown magic number: {magic}");
                }

                // Parse Width and Height
                var widthHeight = NextNonCommentLine(reader);
                var tokens = widthHeight.Split(' ');
                var width = uint.Parse(tokens[0]);
                var height = uint.Parse(tokens[1]);

                // Parse maximum pixel value
                var maxVal = int.Parse(NextNonCommentLine(reader));

                // Read bytes
                var pixels = new byte[height * width];
                for (int i = 0; i < pixels.Length; ++i)
                {
                    pixels[i] = reader.ReadByte();
                }

                return new PGMImage(pixels, width, height, maxVal);
            }
        }

        private static string NextAnyLine(BinaryReader br)
        {
            string s = "";
            byte b = 0; // dummy
            while (b != 10) // newline
            {
                b = br.ReadByte();
                char c = (char)b;
                s += c;
            }
            return s.Trim();
        }

        private static string NextNonCommentLine(BinaryReader br)
        {
            string s = NextAnyLine(br);
            while (s.StartsWith("#") || s == "")
                s = NextAnyLine(br);
            return s;
        }
    }
}

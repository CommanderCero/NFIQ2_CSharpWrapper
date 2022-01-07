using System;
using System.IO;
using WrapperTest.NFIQ2;

namespace WrapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing NFIQ2...");
            NFIQ2Wrapper.Initialize();

            Console.WriteLine("Processing Images...");
            foreach (string s in Directory.EnumerateFiles("../../../../test_images"))
            {
                PGMImage image = PGMImage.Load(s);
                var quality = NFIQ2Wrapper.ComputeQualityScore(image.Pixels, image.Width, image.Height, 0, 500);
                Console.WriteLine($"Quality Score for {s} is '{quality}'");
            }
            
            //NFIQ2Wrapper.Initialize();
        }
    }
}

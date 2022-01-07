using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.ComponentModel;
using WrapperTest.NFIQ2;

namespace WrapperTest.NFIQ2
{
    public class NFIQ2Wrapper
    {
        [DllImport(@"./NFIQ2/NFIQ2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void initializeNFIQ2([MarshalAs(UnmanagedType.LPStr)] string modelFile);
        [DllImport(@"./NFIQ2/NFIQ2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern uint computeQualityScore(byte[] data, uint width, uint height, uint fingerCode, ushort ppi);
        [DllImport(@"./NFIQ2/NFIQ2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void uninitializeNFIQ2();
        [DllImport(@"./NFIQ2/NFIQ2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern bool getLastError([MarshalAs(UnmanagedType.LPStr)] out string errorMessage);

        public static void Initialize()
        {
            initializeNFIQ2("./NFIQ2/nist_plain_tir-ink.txt");
            CheckErrors();
        }

        public static uint ComputeQualityScore(byte[] data, uint width, uint height, uint fingerCode, ushort ppi)
        {
            uint returnValue = computeQualityScore(data, width, height, fingerCode, ppi);
            CheckErrors();
            return returnValue;
        }

        void Uninitialize()
        {
            uninitializeNFIQ2();
        }

        private static void CheckErrors()
        {
            if (getLastError(out string errorMessage))
            {
                throw new NFIQ2Exception(errorMessage);
            }
        }
    }
}

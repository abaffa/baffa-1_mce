using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public static class Utils
    {

        public static byte LSN(byte w)
        {
            return (byte)(w & 0x0f);
        }


        public static byte MSN(byte w)
        {
            return (byte)((w >> 4) & 0x0f);
        }

        public static ushort WORD(byte l, byte h)
        {
            return (ushort)(((h) << 8) | (l));
        }

        public static byte LSB(ushort w)
        {
            return (byte)(w & 0xff);
        }

        public static byte MSB(ushort w)
        {
            return (byte)((w >> 8) & 0xff);
        }

        public static ushort SET_LSB(ushort Rg, byte l)
        {
            Rg = (ushort)((Rg & 0xFF00) | (l & 0xff));
            return Rg;
        }

        public static ushort SET_MSB(ushort Rg, byte h)
        {
            Rg = (ushort)((h << 8) | (Rg & 0x00FF));
            return Rg;
        }


        public static ushort SET_WORD(ushort Rg, byte l, byte h)
        {
            Rg = WORD(l, h);
            return Rg;
        }


        public static string INV_BYTE_TO_BINARY(byte b)
        {
            string str = "";
            str += (b & 0x01) > 0 ? '1' : '0';
            str += (b & 0x02) > 0 ? '1' : '0';
            str += (b & 0x04) > 0 ? '1' : '0';
            str += (b & 0x08) > 0 ? '1' : '0';
            str += (b & 0x10) > 0 ? '1' : '0';
            str += (b & 0x20) > 0 ? '1' : '0';
            str += (b & 0x40) > 0 ? '1' : '0';
            str += (b & 0x80) > 0 ? '1' : '0';

            return str;
        }

        public static string BYTE_TO_BINARY(byte b)
        {
            string str = "";

            str += (b & 0x80) > 0 ? '1' : '0';
            str += (b & 0x40) > 0 ? '1' : '0';
            str += (b & 0x20) > 0 ? '1' : '0';
            str += (b & 0x10) > 0 ? '1' : '0';
            str += (b & 0x08) > 0 ? '1' : '0';
            str += (b & 0x04) > 0 ? '1' : '0';
            str += (b & 0x02) > 0 ? '1' : '0';
            str += (b & 0x01) > 0 ? '1' : '0';

            return str;
        }

        public static string NIBBLE_TO_BINARY(byte b)
        {
            string str = "";

            str += (b & 0x08) > 0 ? '1' : '0';
            str += (b & 0x04) > 0 ? '1' : '0';
            str += (b & 0x02) > 0 ? '1' : '0';
            str += (b & 0x01) > 0 ? '1' : '0';
            return str;
        }


        public static string print_nibble_bin(byte b)
        {
            String s = "";
            s += String.Format("{0} ", b.ToString("X1"));
            s += NIBBLE_TO_BINARY(b);
            return s;
        }


        public static string print_byte_bin(byte b)
        {
            String s = "";
            s += String.Format("{0} ", b.ToString("X2"));
            s += BYTE_TO_BINARY(b);
            return s;
        }

        public static string print_word_bin(ushort n)
        {
            byte h = Utils.MSB(n);
            byte l = Utils.LSB(n);

            string s = "";
            s += String.Format("{0} ", h.ToString("X2"));
            s += BYTE_TO_BINARY(h);
            s += " ";
            s += String.Format("{0} ", l.ToString("X2"));
            s += BYTE_TO_BINARY(l);

            return s;
        }

        public static string print_word_bin_nibbles(ushort n)
        {
            byte bh = Utils.MSB(n);
            byte bl = Utils.LSB(n);

            byte bhnh = Utils.MSN(bh);
            byte bhnl = Utils.LSN(bh);

            byte blnh = Utils.MSN(bl);
            byte blnl = Utils.LSN(bl);


            string s = "";
            s += String.Format("{0} ", bhnh.ToString("X1"));
            s += NIBBLE_TO_BINARY(bhnh);
            s += " ";

            s += String.Format("{0} ", bhnl.ToString("X1"));
            s += NIBBLE_TO_BINARY(bhnl);
            s += " ";

            s += String.Format("{0} ", blnh.ToString("X1"));
            s += NIBBLE_TO_BINARY(blnh);
            s += " ";

            s += String.Format("{0} ", blnl.ToString("X1"));
            s += NIBBLE_TO_BINARY(blnl);

            return s;
        }


        public static int Convert_hexstr_to_value(string value)
        {
            int address = 0;
            int NumberChars = value.Length;
            byte[] bytes = new byte[NumberChars / 2];
            try
            {
                for (int i = 0; i < NumberChars; i += 2) {
                    address <<= 8;
                    address += Convert.ToByte(value.Substring(i, 2), 16);
                }
            }
            catch { }

            return address;
        }


        public static ushort GetWordBit(ushort v, int bit)
        {
            if (bit == 0)
                return (ushort)(v & 0b0000000000000001);

            return (ushort)((v & (0b0000000000000001 << bit)) >> bit);
        }

        public static ushort SetWordBit(ushort v, int bit)
        {
            if (bit == 0)
                return (ushort)(v & 0b0000000000000001);

            return (ushort)((v & 0b0000000000000001) << bit);
        }

        public static byte GetByteBit(byte v, int bit)
        {
            if (bit == 0)
                return (byte)(v & 0b00000001);

            return (byte)((v & (0b00000001 << bit)) >> bit);
        }

        public static byte SetByteBit(byte v, int bit)
        {
            if (bit == 0)
                return (byte)(v & 0b00000001);

            return (byte)((v & 0b00000001) << bit);
        }

        public static bool CheckByteBit(byte v, int bit)
        {
            if (bit == 0)
                return (v & 0b00000001) != 0x00;

            return (v & (0b00000001 << bit)) != 0x00;
        }
        


        public static void SaveToLog(string s, TextWriter fa, string str)
        {

            //FILE *fa = fopen("File1.txt", "a");
            if (fa == null)
            {
                if (s == "")
                    Console.WriteLine("can not open target file\n");
                else
                    s += "can not open target file\n";

            }

            fa.Write(str);
            fa.Flush();
        }



        public static void Reg8BitPrint(TextWriter fa, string dir, string reg, byte value)
        {
            string line = String.Format("         \t\t\t\t{0} REG\t {1} \t= {2}\n", dir, reg, value.ToString("X2").ToLower());
            SaveToLog("", fa, line);
        }

        public static uint StrLen(byte[] array, int index)
        {
            uint len = 0;
            while (array[index] != 0x00)
            {
                len++;
                index++;
            }

            return len;
        }

        public static string GetStr(byte[] array, int index)
        {
            String str = "";
            while (array[index] != 0x00)
            {
                str += (char)array[index];
                index++;
            }

            return str;
        }




        public static byte[] Loadfile(out string s, string filename, out long size)
        {
            byte[] buf = null;

            s = String.Format("The filename to load is: {0}", filename);

            size = 0;
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                    size = stream.Length;
                }

                try
                {
                    using (FileStream fs = new FileStream(filename,
               FileMode.Open))
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        buf = new byte[size];
                        br.Read(buf, 0, (int)size);

                        s += " | OK.\n";
                    }
                }
                catch
                {
                    s += " | Failed to read from file.\n";
                    return null;
                }

            }
            catch
            {
                s += " | Failed to open the file.\n";
                return null;
            }

            return buf;
        }
    }
}

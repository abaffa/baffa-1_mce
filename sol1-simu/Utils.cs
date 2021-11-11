using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol1_simu
{
    public static class Utils
    {

        public static int BoolToInt(bool b)
        {
            if (b == true) return 1;
            else return 0;
        }

        public static string FormatMemoName(string tmp_instr_name)
        {
            tmp_instr_name = tmp_instr_name.Trim();
            tmp_instr_name = tmp_instr_name.Replace(", ", ",");
            tmp_instr_name = tmp_instr_name.Replace(" + ", "+");
            tmp_instr_name = tmp_instr_name.Replace(" - ", "-");
            tmp_instr_name = tmp_instr_name.Replace(" * ", "*");
            tmp_instr_name = tmp_instr_name.Replace(" ^ ", "^");
            tmp_instr_name = tmp_instr_name.Replace(" / ", "/");

            tmp_instr_name = tmp_instr_name.Replace(" \\ ", "\\");
            tmp_instr_name = tmp_instr_name.Replace(" | ", "|");

            tmp_instr_name = tmp_instr_name.Replace("\t", " ");
            tmp_instr_name = tmp_instr_name.Replace("  ", " ");

            //tmp_instr_name = tmp_instr_name.Replace("/", "\\");
            //tmp_instr_name = tmp_instr_name.Replace("-", "|");

            tmp_instr_name = tmp_instr_name.Replace(",", ", ");
            tmp_instr_name = tmp_instr_name.Replace("+", " + ");
            tmp_instr_name = tmp_instr_name.Replace("-", " - ");
            tmp_instr_name = tmp_instr_name.Replace("*", " * ");
            tmp_instr_name = tmp_instr_name.Replace("/", " / ");
            tmp_instr_name = tmp_instr_name.Replace("^", " ^ ");
            tmp_instr_name = tmp_instr_name.Replace("|", " | ");
            return tmp_instr_name;
        }


        public static string StrBinToHex(string strBinary)
        {
            string strHex = Convert.ToInt32(strBinary, 2).ToString("X");
            return strHex;
        }

        public static int StrToInt(String s)
        {
            int ret = 0;
            try
            {
                ret = int.Parse(s);
            }
            catch { }
            return ret;
        }

        public static String IntToStrBin(int n)
        {
            String ret = "";
            if ((n & 0x80) != 0) ret += "1"; else ret += "0";
            if ((n & 0x40) != 0) ret += "1"; else ret += "0";
            if ((n & 0x20) != 0) ret += "1"; else ret += "0";
            if ((n & 0x10) != 0) ret += "1"; else ret += "0";
            if ((n & 0x08) != 0) ret += "1"; else ret += "0";
            if ((n & 0x04) != 0) ret += "1"; else ret += "0";
            if ((n & 0x02) != 0) ret += "1"; else ret += "0";
            if ((n & 0x01) != 0) ret += "1"; else ret += "0";

            return ret;
        }


        public static String GetStringFromByteArray(byte[] fileBytes2, int start, int max)
        {
            String ret = "";
            for (int i = 0; i < max && fileBytes2[start + i] != 0x00; i++)
                ret += Convert.ToChar(fileBytes2[start + i]);

            return ret.Trim('\0');
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sol1_simu
{
    public class RomManager
    {
        public const int NBR_ROMS = 15;
        public const int TOTAL_CONTROL_BITS = NBR_ROMS * 8;
        public const int CYCLES_PER_INSTR = 64;
        public const int NBR_INSTRUCTIONS = 256;
        public const int TOTAL_CYCLES = CYCLES_PER_INSTR * NBR_INSTRUCTIONS;

        public const int STRING_LEN = 256;
        public const int INFO_LEN = 256;

        public byte[][] ROMS = new byte[NBR_ROMS][];
        public string[] instr_names = new string[NBR_INSTRUCTIONS];
        public string[] info = new string[NBR_INSTRUCTIONS * CYCLES_PER_INSTR];

        public String current_filename = "";
        public String working_folder = "";

        public void Clear()
        {
            for (int i = 0; i < NBR_ROMS; i++)
            {
                ROMS[i] = new byte[NBR_INSTRUCTIONS * CYCLES_PER_INSTR];
            }
        }
        public void New()
        {
            for (int i = 0; i < NBR_INSTRUCTIONS * CYCLES_PER_INSTR; i++)
            {
                ResetLists(i);
            }

            for (int i = 0; i < RomManager.NBR_INSTRUCTIONS; i++)
            {
                instr_names[i] = "";
            }
        }



        public void ResetLists(int cycle)
        {
            info[cycle] = "";
            ROMS[0][cycle] = 0;
            ROMS[1][cycle] = 0;
            ROMS[2][cycle] = 0xC0;
            ROMS[3][cycle] = 0;
            ROMS[4][cycle] = 0;
            ROMS[5][cycle] = 0;
            ROMS[6][cycle] = 0;
            ROMS[7][cycle] = 0xF0;
            ROMS[8][cycle] = 0x8F;
            ROMS[9][cycle] = 0xFF;
            ROMS[10][cycle] = 0xFF;
            ROMS[11][cycle] = 0x47;
            ROMS[12][cycle] = 0xC0;
            ROMS[13][cycle] = 0;
            ROMS[14][cycle] = 0;

        }

        public bool UsedCycle(int instr_nbr, int index)
        {
            return info[(instr_nbr * CYCLES_PER_INSTR) + index].Trim() != "";
        }




        public bool Read(string filename)
        {

            if (File.Exists(filename))
            {
                string name = "";
                int i, j, k;

                current_filename = filename;
                if (current_filename.LastIndexOf("\\") > -1)
                    working_folder = current_filename.Substring(0, current_filename.LastIndexOf("\\"));
                else
                    working_folder = System.Environment.CurrentDirectory;


                byte[] fileBytes2 = File.ReadAllBytes(filename);

                j = 0;
                i = 0;

                for (i = 0; i < NBR_INSTRUCTIONS * CYCLES_PER_INSTR * INFO_LEN; i = i + INFO_LEN)
                {
                    info[j++] = Utils.GetStringFromByteArray(fileBytes2, i, INFO_LEN);
                }
                j = 0;
                k = 0;
                for (k = 0; k < NBR_INSTRUCTIONS * STRING_LEN; k = k + STRING_LEN)
                {
                    instr_names[j++] = Utils.GetStringFromByteArray(fileBytes2, i + k, STRING_LEN);
                }

                for (i = 0; i < NBR_ROMS; i++)
                {
                    name = filename;
                    name = name + i.ToString();

                    byte[] fileBytes = File.ReadAllBytes(name);
                    j = 0;
                    foreach (byte b in fileBytes)
                    {
                        ROMS[i][j] = b;
                        j++;
                    }


                }

                return true;
            }
            return false;
        }

        public bool Write(string filename)
        {

            string name = "";

            name = filename;
            current_filename = filename;
            if (current_filename.LastIndexOf("\\") > -1)
                working_folder = current_filename.Substring(0, current_filename.LastIndexOf("\\"));
            else
                working_folder = System.Environment.CurrentDirectory;

            ASCIIEncoding _asiiencode = new ASCIIEncoding();

            using (BinaryWriter binWriter = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                for (int i = 0; i < NBR_INSTRUCTIONS * CYCLES_PER_INSTR; i++)  //NBR_INSTRUCTIONS * CYCLES_PER_INSTR * STRING_LEN
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(info[i].PadRight(STRING_LEN, '\0'));
                    binWriter.Write(bytes);
                }

                for (int i = 0; i < NBR_INSTRUCTIONS; i++) //NBR_INSTRUCTIONS * STRING_LEN
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(instr_names[i].PadRight(STRING_LEN, '\0'));
                    binWriter.Write(bytes);
                }
            }

            for (int j = 0; j < NBR_ROMS; j++)
            {
                name = filename;
                name = name + j.ToString();

                using (BinaryWriter binWriter = new BinaryWriter(File.Open(name, FileMode.Create)))
                {
                    for (int i = 0; i < NBR_INSTRUCTIONS * CYCLES_PER_INSTR; i++)

                        binWriter.Write(ROMS[j][i]);
                }

            }


            using (StreamWriter sw = new StreamWriter(working_folder.Trim('\\') + "\\opcode_list.txt", false))
            {
                foreach (InstructionItem i in InstructionList().OrderBy(p => p.OpCode.Length))
                {
                    sw.WriteLine(i.InstructionCode());
                }
            }

            if (File.Exists(filename))
                return true;

            return false;
        }




        public List<InstructionItem> InstructionList()
        {
            List<InstructionItem> lst = new List<InstructionItem>();

            for (int i = 0; i < RomManager.NBR_INSTRUCTIONS; i++)
            {
                if (instr_names[i].IndexOf("|") == -1)
                {
                    String inst = instr_names[i].Trim();
                    string _opcode = i.ToString("X2");

                    if (i > 0 && inst.IndexOf('\\') > -1)
                    {
                        foreach (String t in getInstrVariations(inst))
                        {
                            lst.Add(new InstructionItem() { Text = t, OpCode = _opcode, Value = i.ToString("X2") + ": " + instr_names[i] });
                        }
                    }
                    else
                        lst.Add(new InstructionItem() { Text = inst, OpCode = _opcode, Value = i.ToString("X2") + ": " + instr_names[i] });


                }
                else
                {
                    String[] n = instr_names[i].Split('|');
                    for (int j = 0; j < n.Length; j++)
                    {
                        String inst = n[j].Trim();
                        string _opcode = (j == 0 ? "" : "FD") + i.ToString("X2");

                        if (i > 0 && n[j].IndexOf('\\') > -1)
                        {
                            foreach (String t in getInstrVariations(inst))
                            {
                                lst.Add(new InstructionItem() { Text = t, OpCode = _opcode, Value = i.ToString("X2") + ": " + instr_names[i] });
                            }
                        }
                        else
                            lst.Add(new InstructionItem() { Text = inst, OpCode = _opcode, Value = i.ToString("X2") + ": " + instr_names[i] });
                    }
                }
            }

            return lst;
        }




        public List<String> NamesList()
        {
            List<String> lst = new List<String>();

            for (int i = 0; i < RomManager.NBR_INSTRUCTIONS; i++)
            {
                lst.Add(i.ToString("X2") + ": " + instr_names[i]);
            }

            return lst;
        }

        private List<string> getInstrVariations(String inst)
        {

            List<string> varlst = new List<string>();

            if (inst.IndexOf('\\') > -1)
            {
                String[] tokens = inst.Split('\\');
                String inst_params = inst.IndexOf(' ') > -1 ? inst.Substring(inst.IndexOf(' ')) : "";
                foreach (String t in tokens)
                {
                    string i = "";
                    if (t.IndexOf(' ') > -1)
                        i = t.Substring(0, t.IndexOf(' '));
                    else
                        i = t;

                    varlst.Add(i + inst_params);
                }
            }
            else
            {
                varlst.Add(inst);
            }

            return varlst;
        }


        private string create_tasm_instruction(int icode, int escape, string instr_name, bool tasm_byte = false)
        {
            String inst = instr_name;
            int len = escape > 0 ? 2 : 1;

            len += Regex.Matches(instr_name, "i8").Count;
            len += Regex.Matches(instr_name, "u8").Count;
            len += (Regex.Matches(instr_name, "i16").Count * 2);
            len += (Regex.Matches(instr_name, "u16").Count * 2);

            if (!tasm_byte)
            {
                inst = inst.Replace("i8", "@");
                inst = inst.Replace("u8", "@");
                inst = inst.Replace("i16", "@");
                inst = inst.Replace("u16", "@");
            }

            inst = inst.Replace(", ", ",");
            inst = inst.Replace(" + ", "+");
            inst = inst.Replace(" - ", "-");
            inst = inst.Replace(" * ", "*");
            inst = inst.Replace(" ^ ", "^");


            string _params = icode.ToString("X2");

            if (escape == 1) _params += "FD";

            _params += "\t\t" + len.ToString();
            _params += "\t\t NOP \t\t 1";

            string line = "";
            if (inst.IndexOf('\\') > -1)
            {
                foreach (String t in getInstrVariations(inst))
                {
                    line += t.ToUpper();
                    line += "\t\t\t";
                    line += _params + "\r\n";
                }


                line = line.Trim(new char[] { '\r', '\n' });
            }
            else
            {
                line = inst.ToUpper();
                line += "\t\t\t";
                line += _params;
            }

            return line;
        }
        public void GenerateTasmTable(string filename, bool tasm_byte = false)
        {
            StreamWriter sw = new StreamWriter(filename, false);

            sw.WriteLine("\"TASMSOL\"");
            sw.WriteLine(".ALTWILD");
            sw.WriteLine("");

            for (int i = 1; i < NBR_INSTRUCTIONS; i++)
            {
                if (i % 0x10 == 0) sw.WriteLine("");

                if (instr_names[i].IndexOf('|') > -1)
                {
                    String[] instrs = instr_names[i].Split('|');
                    for (int j = instrs.Length - 1; j >= 0; j--)
                        sw.WriteLine(create_tasm_instruction(i, j, instrs[j].Trim(), tasm_byte));
                }
                else
                    sw.WriteLine(create_tasm_instruction(i, 0, instr_names[i].Trim(), tasm_byte));
            }
            sw.Close();
        }


        public string CalculateAVgCyclesPerInstruction()
        {
            string msg = "";
            double count = 0.0;
            double average;

            for (int i = 0; i < 256; i++)
            {
                for (int j = 63; j >= 0; j--)
                {
                    if ((ROMS[0][i * 64 + j] & 0x03) == 0x02)
                    {
                        count = count + (float)(j + 1);
                        msg += i.ToString("X2") + ": " + (j + 1).ToString() + "\r\n";
                        break;
                    }
                }
            }

            average = (count + 2) / 255.0; // the +2 accounts for fetch

            msg += "Average: " + average.ToString("N2") + "\r\n";

            return msg;
        }
    }
}

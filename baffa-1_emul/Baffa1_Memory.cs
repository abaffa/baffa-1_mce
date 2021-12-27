using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Memory
    {
        public byte[] mem_bios { get; set; }

        public byte[] mem_page_table0 { get; set; }
        public byte[] mem_page_table1 { get; set; }

        public byte[] low_memory { get; set; }

        public byte[] high_memory0 { get; set; }
        public byte[] high_memory1 { get; set; }
        public byte[] high_memory2 { get; set; }
        public byte[] high_memory3 { get; set; }
        public byte[] high_memory4 { get; set; }
        public byte[] high_memory5 { get; set; }
        public byte[] high_memory6 { get; set; }
        public byte[] high_memory7 { get; set; }

        public int debug_mem_offset { get; set; }
        public bool debug_manual_offset { get; set; }



        public Baffa1_Memory()
        {

            this.mem_bios = new byte[Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE];

            this.mem_page_table0 = new byte[Baffa1_Config.BAFFA1_PAGING_MEMORY_SIZE];
            this.mem_page_table1 = new byte[Baffa1_Config.BAFFA1_PAGING_MEMORY_SIZE];

            this.low_memory = new byte[Baffa1_Config.BAFFA1_LOW_MEMORY_SIZE];

            this.high_memory0 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory1 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory2 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory3 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory4 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory5 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory6 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];
            this.high_memory7 = new byte[Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE];

            this.debug_mem_offset = 0;
            this.debug_manual_offset = false;

            this.Reset();
        }

        public void Reset()
        {
            uint address = 0;

            for (address = 0; address < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE; address++)
            {
                this.mem_bios[address] = 0x00;
                this.low_memory[address] = 0x00;
            }
            for (address = 0; address < Baffa1_Config.BAFFA1_PAGING_MEMORY_SIZE; address++)
            {
                this.mem_page_table0[address] = 0x00;
                this.mem_page_table1[address] = 0x00;
            }
            for (address = 0; address < Baffa1_Config.BAFFA1_MAINPAGE_MEMORY_SIZE; address++)
            {
                this.high_memory0[address] = 0x00;
                this.high_memory1[address] = 0x00;
                this.high_memory2[address] = 0x00;
                this.high_memory3[address] = 0x00;
                this.high_memory4[address] = 0x00;
                this.high_memory5[address] = 0x00;
                this.high_memory6[address] = 0x00;
                this.high_memory7[address] = 0x00;
            }

        }


        public bool Load_bios(HW_TTY hw_tty)
        {
            String str_out = "";
            int i;
            long size = 0;
            byte[] buf = Utils.Loadfile(out str_out, Baffa1_Config.WORKSPACE + "bios.obj", out size);
            hw_tty.Print(str_out);

            if (buf == null)
                return false;

            for (i = 0; i < size; i++)
            {
                this.mem_bios[i] = buf[i];
            }

            return true;
        }

        public void DisplayBiosMemory(Baffa1_Registers registers, HW_TTY hw_tty)
        {
            ushort MAR = Baffa1_Registers.Value(registers.MARl, registers.MARh);

            if (!this.debug_manual_offset)
            {
                if (0 + this.debug_mem_offset > MAR || MAR >= 256 + this.debug_mem_offset)
                    this.debug_mem_offset = (int)((MAR / 0x10) * 0x10);
            }

            List<byte> mem_bytes = new List<byte>();
            for (int i = 0 + this.debug_mem_offset; i < 256 + this.debug_mem_offset; i++)
            {
                mem_bytes.Add(this.mem_bios[i]);
            }

            displayMemory(mem_bytes, debug_mem_offset, MAR, hw_tty);
        }




        public void DisplayLowMemory(Baffa1_Registers registers, HW_TTY hw_tty)
        {
            ushort MAR = Baffa1_Registers.Value(registers.MARl, registers.MARh);

            if (!this.debug_manual_offset)
            {
                if (0 + this.debug_mem_offset > MAR || MAR >= 256 + this.debug_mem_offset)
                    this.debug_mem_offset = (int)((MAR / 0x10) * 0x10);
            }

            List<byte> mem_bytes = new List<byte>();
            for (int i = 0 + this.debug_mem_offset; i < 256 + this.debug_mem_offset; i++)
            {
                mem_bytes.Add(this.low_memory[i]);
            }

            displayMemory(mem_bytes, debug_mem_offset, MAR, hw_tty);
        }

        public void DisplayMainMemory(Baffa1_Registers registers, HW_TTY hw_tty)
        {
            ushort MAR = Baffa1_Registers.Value(registers.MARl, registers.MARh);

            if (!this.debug_manual_offset)
            {
                if (0 + this.debug_mem_offset > MAR || MAR >= 256 + this.debug_mem_offset)
                    this.debug_mem_offset = (int)((MAR / 0x10) * 0x10);
            }

            List<byte> mem_bytes = new List<byte>();
            for (int i = 0 + this.debug_mem_offset; i < 256 + this.debug_mem_offset; i++)
            {
                if (i > 0x7FFF)
                    mem_bytes.Add(this.low_memory[i]);
                else
                    mem_bytes.Add(this.mem_bios[i]);
            }

            displayMemory(mem_bytes, debug_mem_offset, MAR, hw_tty);
        }


        public string GetMainMemory(Baffa1_Registers registers)
        {
            ushort MAR = Baffa1_Registers.Value(registers.MARl, registers.MARh);

            if (!this.debug_manual_offset)
            {
                if (0 + this.debug_mem_offset > MAR || MAR >= 256 + this.debug_mem_offset)
                    this.debug_mem_offset = (int)((MAR / 0x10) * 0x10);
            }

            List<byte> mem_bytes = new List<byte>();
            for (int i = 0 + this.debug_mem_offset; i < 256 + this.debug_mem_offset; i++)
            {
                if (i > 0x7FFF)
                    mem_bytes.Add(this.low_memory[i]);
                else
                    mem_bytes.Add(this.mem_bios[i]);
            }

            return getMemory(mem_bytes, debug_mem_offset, MAR);
        }


        public string GetMainMemoryDump(Baffa1_Registers registers)
        {
            ushort MAR = Baffa1_Registers.Value(registers.MARl, registers.MARh);

            List<byte> mem_bytes = new List<byte>();
            for (int i = 0; i < Baffa1_Config.BAFFA1_LOW_MEMORY_SIZE; i++)
            {
                if (i > 0x7FFF)
                    mem_bytes.Add(this.low_memory[i]);
                else
                    mem_bytes.Add(this.mem_bios[i]);
            }

            return getMemory(mem_bytes, debug_mem_offset, MAR);
        }



        private void displayMemory(List<byte> mem_bytes, int offset, ushort MAR, HW_TTY hw_tty)
        {

            hw_tty.Print("\n        ");

            for (uint i = 0; i < 16; i++)
            {
                hw_tty.Print(String.Format("{0} ", i.ToString("X2")));
            }

            hw_tty.Print(String.Format("\n\n {0} ", offset.ToString("X4")));

            for (int i = 0; i < mem_bytes.Count; i++)
            {
                if (i % 16 == 0)
                    if (MAR == i + offset)
                        hw_tty.Print(" *");
                    else
                        hw_tty.Print("  ");
                if (MAR == i + offset || MAR - 1 == i + offset)
                {
                    hw_tty.Print(String.Format("{0}*", mem_bytes[i].ToString("X2")));
                }
                else
                {
                    hw_tty.Print(String.Format("{0} ", mem_bytes[i].ToString("X2")));
                }

                if ((i + 1) % 16 == 0 && i < mem_bytes.Count)
                {
                    hw_tty.Print("  |");
                    for (int j = (i + 1) - 16; j < (i + 1); j++)
                    {
                        if (mem_bytes[j] < 0x20)
                            hw_tty.Print(".");
                        else
                        {
                            hw_tty.Print(((char)mem_bytes[j]).ToString());
                        }
                    }
                    hw_tty.Print("|");

                    if (i < mem_bytes.Count - 1)
                    {
                        hw_tty.Print(String.Format("\n {0} ", (i + offset + 1).ToString("X4")));
                    }
                    else
                        hw_tty.Print("\n");
                }
            }
        }

        private string getMemory(List<byte> mem_bytes, int offset, ushort MAR)
        {

            string ret = "\n        ";

            for (uint i = 0; i < 16; i++)
            {
                ret += String.Format("{0} ", i.ToString("X2"));
            }

            ret += String.Format("\r\n\r\n {0} ", offset.ToString("X4"));

            int total = mem_bytes.Count;

            for (int i = 0; i < total; i++)
            {
                if (i % 16 == 0)
                    if (MAR == i + offset)
                        ret += " *";
                    else
                        ret += "  ";

                if (MAR == i + offset || MAR - 1 == i + offset)
                {
                    ret += String.Format("{0}*", mem_bytes[i].ToString("X2"));
                }
                else
                {
                    ret += String.Format("{0} ", mem_bytes[i].ToString("X2"));
                }

                
                if ((i + 1) % 16 == 0 && i < total)
                {
                    /*
                    ret += "  |";
                    for (int j = (i + 1) - 16; j < (i + 1); j++)
                    {
                        if (mem_bytes[j] < 0x20)
                            ret += ".";
                        else
                        {
                            ret += ((char)mem_bytes[j]).ToString();
                        }
                    }
                    ret += "|";
                    */
                    if (i < total - 1)
                    {
                        ret += String.Format("\r\n {0} ", (i + offset + 1).ToString("X4"));
                    }
                    else
                        ret += "\r\n";
                }
                
            }

            return ret;
        }


        public void DisplayHighMemory(Baffa1_Registers registers, HW_TTY hw_tty)
        {
            ushort MAR = Baffa1_Registers.Value(registers.MARl, registers.MARh);

            if (!this.debug_manual_offset)
            {
                if (0 + this.debug_mem_offset > MAR || MAR >= 256 + this.debug_mem_offset)
                    this.debug_mem_offset = (int)((MAR / 0x10) * 0x10);
            }

            hw_tty.Print("\n        ");

            for (uint i = 0; i < 16; i++)
            {
                hw_tty.Print(String.Format("{0} ", i.ToString("X2")));
            }

            hw_tty.Print(String.Format("\n\n {0} ", this.debug_mem_offset.ToString("X4")));

            for (int i = 0 + this.debug_mem_offset; i < 256 + this.debug_mem_offset; i++)
            {
                if (i % 16 == 0)
                    if (MAR == i)
                        hw_tty.Print(" *");
                    else
                        hw_tty.Print("  ");
                if (MAR == i || MAR - 1 == i)
                {
                    hw_tty.Print(String.Format("{0}*", this.low_memory[i].ToString("X2")));
                }
                else
                {
                    hw_tty.Print(String.Format("{0} ", this.low_memory[i].ToString("X2")));
                }

                if ((i + 1) % 16 == 0 && i <= 255 + this.debug_mem_offset)
                {
                    hw_tty.Print("  |");
                    for (int j = (i + 1) - 16; j < (i + 1); j++)
                    {
                        if (this.low_memory[j] < 0x20)
                            hw_tty.Print(".");
                        else
                        {
                            hw_tty.Print(((char)this.low_memory[j]).ToString());
                        }
                    }
                    hw_tty.Print("|");

                    if (i < 255 + this.debug_mem_offset)
                    {
                        hw_tty.Print(String.Format("\n {0} ", (i + 1).ToString("X4")));
                    }
                    else
                        hw_tty.Print("\n");

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public static class DebugMenu_Main
    {

        public static void debugmenu_main_menu(HW_TTY hw_tty)
        {
            hw_tty.Print("\n");
            hw_tty.Print("BAFFA-1 Debug Monitor | 30 May 2021\n");
            hw_tty.Print("\n");
            hw_tty.Print("  D - Display Memory\n");
            hw_tty.Print("  I - Disassemble Memory\n");
            hw_tty.Print("  E - Edit Memory\n");
            hw_tty.Print("  F - Fill Memory\n");
            hw_tty.Print("  L - Load Memory\n");
            hw_tty.Print("\n");
            hw_tty.Print("  R - Display Registers\n");
            hw_tty.Print("  A - Edit Register\n");
            hw_tty.Print("  B - Edit Breakpoint\n");
            hw_tty.Print("  P - Edit Program Counter\n");
            hw_tty.Print("\n");
            hw_tty.Print("  G - Go(Run)\n");
            hw_tty.Print("  T - Debug Trace\n");
            hw_tty.Print("  Z - Reset CPU\n");
            hw_tty.Print("  V - Reload Bios\n");
            hw_tty.Print("\n");
            hw_tty.Print("  X - Debug Roms\n");
            hw_tty.Print("\n");
            hw_tty.Print("  ? - Display Menu\n");
            hw_tty.Print("  Q - Quit\n");
            hw_tty.Print("\n");
        }


        public static void debugmenu_main_disassemble_mem(HW_TTY hw_tty)
        {
        }


        public static void debugmenu_main_edit_mem(Baffa1_Memory memory, HW_TTY hw_tty)
        {

            hw_tty.Print("Edit Memory | Address? ");
            String input = hw_tty.Gets(4);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            ushort address = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));
            hw_tty.Print(String.Format("\n  {0}={1} ? ", address.ToString("X4"), (address > 0x7FFF ? memory.low_memory[address] : memory.mem_bios[address]).ToString("X2")));

            input = hw_tty.Gets(2);

            while (input.Length > 0)
            {
                byte b = (byte)Utils.Convert_hexstr_to_value(input.PadLeft(2, '0'));

                if (address > 0x7FFF)
                    memory.low_memory[address] = b;
                else
                    memory.mem_bios[address] = b;

                address++;
                if (address == 0) { hw_tty.Print("\n"); return; }

                hw_tty.Print(String.Format("\n  {0}={1} ? ", address.ToString("X4"), (address > 0x7FFF ? memory.low_memory[address] : memory.mem_bios[address]).ToString("X2")));
                input = hw_tty.Gets(2);
            }

            hw_tty.Print("\n");
        }



        public static void debugmenu_main_fill_mem(Baffa1_Memory memory, HW_TTY hw_tty)
        {


            ulong address, start, end;
            byte data;

            hw_tty.Print("Fill Memory | Start Address? ");
            string input = hw_tty.Gets(4);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            start = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));

            hw_tty.Print(" | End Address? ");
            input = hw_tty.Gets(4);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            end = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));

            hw_tty.Print(" | Data? ");
            input = hw_tty.Gets(2);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            data = (byte)Utils.Convert_hexstr_to_value(input.PadLeft(2, '0'));


            for (address = start; address <= end && address < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE; address++)
                if (address > 0x7FFF)
                    memory.low_memory[address] = data;
                else
                    memory.mem_bios[address] = data;

            hw_tty.Print("\n");
        }




        public static void debugmenu_main_load_mem(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {
            ushort start;

            hw_tty.Print("Load Memory | Start Address? ");
            String input = hw_tty.Gets(4);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            start = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));


            hw_tty.Print("\n  Code (max 128 bytes) ] ");
            input = hw_tty.Gets(256);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            int numdigits = input.Length / 2;
            for (int i = 0; i != numdigits; ++i)
            {
                byte output = Convert.ToByte(input.Substring(i*2, 2), 16); ;
                if (i + start > 0x7FFF)
                    baffa1_cpu.memory.low_memory[i + start] = output;
                else
                    baffa1_cpu.memory.mem_bios[i + start] = output;
            }

            Baffa1_Registers.Set(baffa1_cpu.registers.PCl, baffa1_cpu.registers.PCh, start);

            hw_tty.Print("\n");
        }



        public static void debugmenu_main_display_registers(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {

            hw_tty.Print("Display Registers\n");
            hw_tty.Print("\n");
            baffa1_cpu.Display_registers(hw_tty);
        }

        public static void debugmenu_main_edit_register(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {

            //unsigned ushort address; // , end
            //	unsigned char data;

            hw_tty.Print("Edit Register ? ");
            string input = hw_tty.Gets(3);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            if (input == "A")
            {
                hw_tty.Print(String.Format(" | A={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.Al, baffa1_cpu.registers.Ah).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.Al, baffa1_cpu.registers.Ah, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "B")
            {
                hw_tty.Print(String.Format(" | B={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.Bl, baffa1_cpu.registers.Bh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.Bl, baffa1_cpu.registers.Bh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "C")
            {
                hw_tty.Print(String.Format(" | C={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.Cl, baffa1_cpu.registers.Ch).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.Cl, baffa1_cpu.registers.Ch, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "D")
            {
                hw_tty.Print(String.Format(" | D={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.Dl, baffa1_cpu.registers.Dh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.Dl, baffa1_cpu.registers.Dh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "G")
            {
                hw_tty.Print(String.Format(" | G={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.Gl, baffa1_cpu.registers.Gh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.Gl, baffa1_cpu.registers.Gh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }


            else if (input == "BP")
            {
                hw_tty.Print(String.Format(" | BP={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.BPl, baffa1_cpu.registers.BPh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.BPl, baffa1_cpu.registers.BPh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "SP")
            {
                hw_tty.Print(String.Format(" | SP={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.SPl, baffa1_cpu.registers.SPh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.SPl, baffa1_cpu.registers.SPh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }

            else if (input == "SI")
            {
                hw_tty.Print(String.Format(" | SI={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.SIl, baffa1_cpu.registers.SIh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.SIl, baffa1_cpu.registers.SIh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "DI")
            {
                hw_tty.Print(String.Format(" | DI={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.DIl, baffa1_cpu.registers.DIh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.DIl, baffa1_cpu.registers.DIh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "PC")
            {
                hw_tty.Print(String.Format(" | PC={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.PCl, baffa1_cpu.registers.PCh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.PCl, baffa1_cpu.registers.PCh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }


            else if (input == "TDR")
            {
                hw_tty.Print(String.Format(" | TDR={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.TDRl, baffa1_cpu.registers.TDRh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.TDRl, baffa1_cpu.registers.TDRh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }
            else if (input == "SSP")
            {
                hw_tty.Print(String.Format(" | SSP={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.SSPl, baffa1_cpu.registers.SSPh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.SSPl, baffa1_cpu.registers.SSPh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));

            }
            else if (input == "PTB")
            {
                hw_tty.Print(String.Format(" | PTB={0} | Data? ", baffa1_cpu.registers.PTB.Value().ToString("X2")));
                input = hw_tty.Gets(2);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                baffa1_cpu.registers.PTB.Set((byte)Utils.Convert_hexstr_to_value(input.PadLeft(2, '0')));
            }
            else if (input == "MSW")
            {
                hw_tty.Print(String.Format(" | MSW={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.MSWl, baffa1_cpu.registers.MSWh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                Baffa1_Registers.Set(baffa1_cpu.registers.MSWl, baffa1_cpu.registers.MSWh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            }

            else if (input == "MAR")
            {
                hw_tty.Print(String.Format(" | MAR={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.MARl, baffa1_cpu.registers.MARh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                ushort index = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));
                Baffa1_Registers.Set(baffa1_cpu.registers.MARl, baffa1_cpu.registers.MARh, index);
                Baffa1_Registers.Set(baffa1_cpu.registers.MDRl, baffa1_cpu.registers.MDRh, baffa1_cpu.memory.mem_bios[index]);
            }

            else if (input == "MDR")
            {
                hw_tty.Print(String.Format(" | MDR={0} | Data? ", Baffa1_Registers.Value(baffa1_cpu.registers.MDRl, baffa1_cpu.registers.MDRh).ToString("X4")));
                input = hw_tty.Gets(4);

                if (input.Length == 0) { hw_tty.Print("\n"); return; }

                ushort index = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));
                Baffa1_Registers.Set(baffa1_cpu.registers.MDRl, baffa1_cpu.registers.MDRh, index);
            }
            hw_tty.Print("\n");
        }

        public static void debugmenu_main_edit_breakpoint(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {
            hw_tty.Print(String.Format("Edit Breakpoint | BKPT=%04x | Address (FFFF=disable) ? ", baffa1_cpu.BKPT.ToString("X4")));
            String input = hw_tty.Gets(4);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            baffa1_cpu.BKPT = (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0'));

            hw_tty.Print("\n");
        }

        public static void debugmenu_main_edit_programcounter(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {

            hw_tty.Print(String.Format("Edit Program Counter | PC={0} | Address ? ", Baffa1_Registers.Value(baffa1_cpu.registers.PCl, baffa1_cpu.registers.PCh).ToString("X4")));
            String input = hw_tty.Gets(4);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            Baffa1_Registers.Set(baffa1_cpu.registers.PCl, baffa1_cpu.registers.PCh, (ushort)Utils.Convert_hexstr_to_value(input.PadLeft(4, '0')));
            hw_tty.Print("\n");
        }

        public static void debugmenu_main_reset_cpu(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {

            hw_tty.Print("Reset CPU\n");
            hw_tty.Print("\n");
            baffa1_cpu.microcode.Init(hw_tty);
            baffa1_cpu.Reset();
            baffa1_cpu.Display_registers(hw_tty);
        }

        public static void debugmenu_main_reload_bios(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {

            hw_tty.Print("Reloading BIOS\n");
            baffa1_cpu.memory.Load_bios(hw_tty);
        }


        //debugmenu_main_menu();
        public static uint debugmenu_main(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {

            while (true)
            {

                hw_tty.Print("> ");
                uint key = hw_tty.GetChar();

                ////////

                if (key == (uint)'d' || key == (uint)'D')
                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);

                //else if (key == (uint)'i' || key == (uint)'I')

                else if (key == (uint)'e' || key == (uint)'E')
                    debugmenu_main_edit_mem(baffa1_cpu.memory, hw_tty);

                else if (key == (uint)'f' || key == (uint)'F')
                    debugmenu_main_fill_mem(baffa1_cpu.memory, hw_tty);

                else if (key == (uint)'l' || key == (uint)'L')
                    debugmenu_main_load_mem(baffa1_cpu, hw_tty);

                ////////

                else if (key == (uint)'r' || key == (uint)'R')
                    debugmenu_main_display_registers(baffa1_cpu, hw_tty);

                else if (key == (uint)'a' || key == (uint)'A')
                    debugmenu_main_edit_register(baffa1_cpu, hw_tty);

                else if (key == (uint)'b' || key == (uint)'B')
                    debugmenu_main_edit_breakpoint(baffa1_cpu, hw_tty);

                else if (key == (uint)'p' || key == (uint)'P')
                    debugmenu_main_edit_programcounter(baffa1_cpu, hw_tty);

                ////////

                else if (key == (uint)'g' || key == (uint)'G')
                {
                    return 1;
                }

                else if (key == (uint)'t' || key == (uint)'T')
                {
                    return 2;
                }

                else if (key == (uint)'z' || key == (uint)'Z')
                    debugmenu_main_reset_cpu(baffa1_cpu, hw_tty);

                else if (key == (uint)'v' || key == (uint)'V')
                    debugmenu_main_reload_bios(baffa1_cpu, hw_tty);

                ////////

                else if (key == (uint)'x' || key == (uint)'X')
                    DebugMenu_Roms.debugmenu_roms(baffa1_cpu, hw_tty);

                ////////

                else if (key == (uint)'?')
                    debugmenu_main_menu(hw_tty);

                else if (key == (uint)'q' || key == (uint)'Q')
                    return 0;

                //----------------------
                else if (key == (uint)'m' || key == (uint)'M')
                {
                    baffa1_cpu.memory.debug_manual_offset = !baffa1_cpu.memory.debug_manual_offset;

                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                    else
                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);
                }

                else if (key == (uint)'2')
                {
                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                    {
                        if (baffa1_cpu.memory.debug_mem_offset + 0x10 < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE)
                            baffa1_cpu.memory.debug_mem_offset += 0x10;
                        baffa1_cpu.memory.debug_manual_offset = true;

                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);

                    }
                    else
                    {
                        if (baffa1_cpu.memory.debug_mem_offset + 0x10 < Baffa1_Config.BAFFA1_MAIN_MEMORY_SIZE)
                            baffa1_cpu.memory.debug_mem_offset += 0x10;
                        baffa1_cpu.memory.debug_manual_offset = true;

                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);
                    }
                }
                if (key == (uint)'1')
                {
                    if (baffa1_cpu.memory.debug_mem_offset - 0x10 >= 0)
                        baffa1_cpu.memory.debug_mem_offset -= 0x10;
                    else
                        baffa1_cpu.memory.debug_mem_offset = 0;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                    else
                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'4')
                {
                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                    {
                        if (baffa1_cpu.memory.debug_mem_offset + 0x100 < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE)
                            baffa1_cpu.memory.debug_mem_offset += 0x100;
                        baffa1_cpu.memory.debug_manual_offset = true;

                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                    }
                    else
                    {
                        if (baffa1_cpu.memory.debug_mem_offset + 0x100 < Baffa1_Config.BAFFA1_MAIN_MEMORY_SIZE)
                            baffa1_cpu.memory.debug_mem_offset += 0x100;
                        baffa1_cpu.memory.debug_manual_offset = true;


                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);
                    }
                }
                if (key == (uint)'3')
                {
                    if (baffa1_cpu.memory.debug_mem_offset - 0x100 >= 0)
                        baffa1_cpu.memory.debug_mem_offset -= 0x100;
                    else
                        baffa1_cpu.memory.debug_mem_offset = 0;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                    else
                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'6')
                {
                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                    {
                        if (baffa1_cpu.memory.debug_mem_offset + 0x1000 < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE)
                            baffa1_cpu.memory.debug_mem_offset += 0x1000;
                        baffa1_cpu.memory.debug_manual_offset = true;

                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                    }
                    else
                    {
                        if (baffa1_cpu.memory.debug_mem_offset + 0x1000 < Baffa1_Config.BAFFA1_MAIN_MEMORY_SIZE)
                            baffa1_cpu.memory.debug_mem_offset += 0x1000;
                        baffa1_cpu.memory.debug_manual_offset = true;
                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);
                    }
                }
                if (key == (uint)'5')
                {
                    if (baffa1_cpu.memory.debug_mem_offset - 0x1000 >= 0)
                        baffa1_cpu.memory.debug_mem_offset -= 0x1000;
                    else
                        baffa1_cpu.memory.debug_mem_offset = 0;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    if (!Utils.CheckByteBit(baffa1_cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                        baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                    else
                        baffa1_cpu.memory.DisplayHighMemory(baffa1_cpu.registers, hw_tty);

                }



                else if (key == (uint)'@')
                {
                    if (baffa1_cpu.memory.debug_mem_offset + 0x10 < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE)
                        baffa1_cpu.memory.debug_mem_offset += 0x10;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'!')
                {
                    if (baffa1_cpu.memory.debug_mem_offset - 0x10 >= 0)
                        baffa1_cpu.memory.debug_mem_offset -= 0x10;
                    else
                        baffa1_cpu.memory.debug_mem_offset = 0;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'$')
                {
                    if (baffa1_cpu.memory.debug_mem_offset + 0x100 < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE)
                        baffa1_cpu.memory.debug_mem_offset += 0x100;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'#')
                {
                    if (baffa1_cpu.memory.debug_mem_offset - 0x100 >= 0)
                        baffa1_cpu.memory.debug_mem_offset -= 0x100;
                    else
                        baffa1_cpu.memory.debug_mem_offset = 0;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'^')
                {
                    if (baffa1_cpu.memory.debug_mem_offset + 0x1000 < Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE)
                        baffa1_cpu.memory.debug_mem_offset += 0x1000;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);
                }
                if (key == (uint)'%')
                {
                    if (baffa1_cpu.memory.debug_mem_offset - 0x1000 >= 0)
                        baffa1_cpu.memory.debug_mem_offset -= 0x1000;
                    else
                        baffa1_cpu.memory.debug_mem_offset = 0;
                    baffa1_cpu.memory.debug_manual_offset = true;

                    baffa1_cpu.memory.DisplayMainMemory(baffa1_cpu.registers, hw_tty);

                }

                //----------------------
                ////////

                else
                    hw_tty.Print("\n");

                ////////
            }

            //return 0;
        }


    }
}

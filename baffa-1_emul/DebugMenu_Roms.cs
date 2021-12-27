using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public static class DebugMenu_Roms
    {

        public static void debugmenu_roms_menu(HW_TTY hw_tty)
        {
            hw_tty.Print("\n");
            hw_tty.Print("Baffa-1 Debug Monitor > Roms\n");
            hw_tty.Print("\n");
            hw_tty.Print("  C - Display Rom Microcode Cycles\n");
            hw_tty.Print("  B - Edit Breakpoint\n");
            hw_tty.Print("  ? - Display Menu\n");
            hw_tty.Print("  Q - Back to Debug Monitor\n");
            hw_tty.Print("\n");
        }

        public static void debugmenu_roms_edit_breakpoint(Baffa1_Rom baffa1_rom, HW_TTY hw_tty)
        {

            hw_tty.Print(String.Format("Edit Breakpoint (Opcode/Cycle:{0}{1} | 0000=disable) | Opcode ? ", baffa1_rom.bkpt_opcode.ToString("X2"), baffa1_rom.bkpt_cycle.ToString("X2")));
            String input = hw_tty.Gets(2);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }

            baffa1_rom.bkpt_opcode = (byte)Utils.Convert_hexstr_to_value(input.PadLeft(2,'0'));

            hw_tty.Print(" | Cycle ? ");
            input = hw_tty.Gets(2);

            if (input.Length == 0) { hw_tty.Print("\n"); return; }
            
            baffa1_rom.bkpt_cycle = (byte)Utils.Convert_hexstr_to_value(input.PadLeft(2, '0'));


            hw_tty.Print("\n");
        }


        public static void debugmenu_roms(Baffa1_CPU baffa1_cpu, HW_TTY hw_tty)
        {
            debugmenu_roms_menu(hw_tty);

            while (true)
            {

                hw_tty.Print("roms> ");
                uint key = hw_tty.GetChar();

                ////////

                if (key == (uint)'c' || key == (uint)'C')
                {
                    baffa1_cpu.microcode.rom.debug_cycles(hw_tty);
                }

                else if (key == (uint)'b' || key == (uint)'B')
                    debugmenu_roms_edit_breakpoint(baffa1_cpu.microcode.rom, hw_tty);
                
                else if (key == (uint)'?')
                    debugmenu_roms_menu(hw_tty);

                else if (key == (uint)'q' || key == (uint)'Q')
                    break;

                ////////

                else
                    hw_tty.Print("\n");

                ////////
            }

            hw_tty.Print("\n");
        }
    }
}

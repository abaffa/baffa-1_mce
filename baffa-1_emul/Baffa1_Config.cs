using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public static class Baffa1_Config
    {

        public static string WORKSPACE = @"C:\Backup\Develop\emudev\sol-1_emulator\sol-1_vc\sol-1_emul\sol-1_emul\";

        public const int SERVER_PORT = 20248;

        public const bool PANEL_ENABLED = false;

        public const bool INI_SERVER = false;
        public const bool INI_WEB_SERVER = false;

        //public const bool INI_DEBUG_OPCODE = false;

        public const bool INI_DEBUG_MICROCODE = false;
        public const bool INI_DEBUG_UADDRESSER = false;
        public const bool INI_DEBUG_UADDER = false;
        public const bool INI_DEBUG_UFLAGS = false;


        public const bool INI_DEBUG_LITE = false;
        public const bool INI_DEBUG_LITE_CYCLES = false;

        public const bool INI_DEBUG_BUSES = false;
        public const bool INI_DEBUG_ALU = false;

        public const bool INI_DEBUG_TRACE_RDREG = false;
        public const bool INI_DEBUG_TRACE_WRREG = false;
        public const bool INI_DEBUG_REGISTERS = false;

        public const bool INI_DEBUG_RDMEM = false;
        public const bool INI_DEBUG_TRACE_WRMEM = false;
        public const bool INI_DEBUG_MEMORY = false;

        public const bool INI_DEBUG_UART = false;
        public const bool INI_DEBUG_IDE = false;
        public const bool INI_DEBUG_RTC = false;
        public const bool INI_DEBUG_TIMER = false;

        public const bool INI_DEBUG_LOG_OPCODE = false;


        public const int BAFFA1_BIOS_MEMORY_SIZE = (0xFFFF + 1);
        public const int BAFFA1_LOW_MEMORY_SIZE = (0xFFFF + 1);
        public const int BAFFA1_PAGING_MEMORY_SIZE = (0x7FFF + 1);
        public const int BAFFA1_MAINPAGE_MEMORY_SIZE = (0x7FFF8 + 1);
        public const int BAFFA1_MAIN_MEMORY_SIZE = (0x3FFFFF + 1);


        public const int BAFFA1_IDE_MEMORY_SIZE = (0x3FFFFF + 1);



        public const int BAFFA1_ROM_SIZE = 0x0FFFF + 1;
        // ROMS
        public const int BAFFA1_ROM_NBR_ROMS = 15;
        public const int BAFFA1_ROM_TOTAL_CONTROL_BITS = BAFFA1_ROM_NBR_ROMS * 8;
        public const int BAFFA1_ROM_CYCLES_PER_INSTR = 64;
        public const int BAFFA1_ROM_NBR_INSTRUCTIONS = 256;
        public const int BAFFA1_ROM_DESC = 0x410000;

        public static bool DEBUG_MICROCODE { get; set; }
        public static bool DEBUG_UADDRESSER { get; set; }
        public static bool DEBUG_UADDER { get; set; }
        public static bool DEBUG_UFLAGS { get; set; }

        public static bool DEBUG_BUSES { get; set; }
        public static bool DEBUG_ALU { get; set; }

        public static bool DEBUG_TRACE_RDREG { get; set; }
        public static bool DEBUG_TRACE_WRREG { get; set; }
        public static bool DEBUG_REGISTERS { get; set; }


        public static bool DEBUG_TRACE_RDMEM { get; set; }
        public static bool DEBUG_TRACE_WRMEM { get; set; }
        public static bool DEBUG_MEMORY { get; set; }

        public static bool DEBUG_UART { get; set; }
        public static bool DEBUG_IDE { get; set; }
        public static bool DEBUG_RTC { get; set; }
        public static bool DEBUG_TIMER { get; set; }

        public static bool DEBUG_LOG_OPCODE { get; set; }
        public static bool DEBUG_LITE { get; set; }
        public static bool DEBUG_LITE_CYCLES { get; set; }
        public static bool SERVER { get; set; }
        public static bool WEB_SERVER { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_CPU
    {

        public Baffa1_Memory memory  = new Baffa1_Memory();
        public Baffa1_Registers registers = new Baffa1_Registers();
        public Baffa1_Microcode microcode = new Baffa1_Microcode();

        public Baffa1_Alu alu = new Baffa1_Alu();

        public string last_op_desc { get; set; }

        public ushort BKPT { get; set; } //breakpoint (FFFF = disable)

        public uint display_reg_load { get; set; }


        public void Init(HW_TTY hw_tty, bool debug = false)
        {
            this.alu.Init();

            this.microcode.Init(hw_tty,debug);

            Reset();

            this.display_reg_load = 0;
            Baffa1_Config.DEBUG_MICROCODE = Baffa1_Config.INI_DEBUG_MICROCODE;
            Baffa1_Config.DEBUG_UADDRESSER = Baffa1_Config.INI_DEBUG_UADDRESSER;
            Baffa1_Config.DEBUG_UADDER = Baffa1_Config.INI_DEBUG_UADDER;
            Baffa1_Config.DEBUG_UFLAGS = Baffa1_Config.INI_DEBUG_UFLAGS;


            Baffa1_Config.DEBUG_BUSES = Baffa1_Config.INI_DEBUG_BUSES;
            Baffa1_Config.DEBUG_ALU = Baffa1_Config.INI_DEBUG_ALU;

            Baffa1_Config.DEBUG_TRACE_RDREG = Baffa1_Config.INI_DEBUG_TRACE_RDREG;
            Baffa1_Config.DEBUG_TRACE_WRREG = Baffa1_Config.INI_DEBUG_TRACE_WRREG;
            Baffa1_Config.DEBUG_REGISTERS = Baffa1_Config.INI_DEBUG_REGISTERS;

            Baffa1_Config.DEBUG_TRACE_RDMEM = Baffa1_Config.INI_DEBUG_RDMEM;
            Baffa1_Config.DEBUG_TRACE_WRMEM = Baffa1_Config.INI_DEBUG_TRACE_WRMEM;
            Baffa1_Config.DEBUG_MEMORY = Baffa1_Config.INI_DEBUG_MEMORY;

            Baffa1_Config.DEBUG_LITE = Baffa1_Config.INI_DEBUG_LITE;
            Baffa1_Config.DEBUG_LITE_CYCLES = Baffa1_Config.INI_DEBUG_LITE_CYCLES;

            Baffa1_Config.DEBUG_UART = Baffa1_Config.INI_DEBUG_UART;
            Baffa1_Config.DEBUG_IDE = Baffa1_Config.INI_DEBUG_IDE;
            Baffa1_Config.DEBUG_RTC = Baffa1_Config.INI_DEBUG_RTC;
            Baffa1_Config.DEBUG_TIMER = Baffa1_Config.INI_DEBUG_TIMER;

            Baffa1_Config.SERVER = Baffa1_Config.INI_SERVER;
            Baffa1_Config.WEB_SERVER = Baffa1_Config.INI_WEB_SERVER;
            Baffa1_Config.DEBUG_LOG_OPCODE = Baffa1_Config.INI_DEBUG_LOG_OPCODE;
        }

        public void Reset()
        {

            //DATA REGISTERS
            Baffa1_Registers.Reset(this.registers.Al, this.registers.Ah); // AX (16bit) Accumulator	(Ah/Al)
            Baffa1_Registers.Reset(this.registers.Bl, this.registers.Bh); // BX (16bit) Base		(Bh/Bl)
            Baffa1_Registers.Reset(this.registers.Cl, this.registers.Ch); // CX (16bit) Counter		(Ch/Cl)
            Baffa1_Registers.Reset(this.registers.Dl, this.registers.Dh); // DX (16bit) Data		(Dh/Dl)
            Baffa1_Registers.Reset(this.registers.Gl, this.registers.Gh); // GX (16bit)	Gh/Gl	General Register(For scratch)

            //Pointer Registers
            Baffa1_Registers.Reset(this.registers.BPl, this.registers.BPh); // BP (16bit) Base Pointer  (Used to manage stack frames)
            Baffa1_Registers.Reset(this.registers.SPl, this.registers.SPh); // SP (16bit) Stack Pointer
            Baffa1_Registers.Reset(this.registers.SSPl, this.registers.SSPh); // SSP (16bit) Supervisor Stack Pointer

            //Index Registers
            Baffa1_Registers.Reset(this.registers.SIl, this.registers.SIh); // SI (16bit) Source index (Source address for string operations)
            Baffa1_Registers.Reset(this.registers.DIl, this.registers.DIh); // DI (16bit) Destination Index (Destination address for string operations)
            Baffa1_Registers.Reset(this.registers.PCl, this.registers.PCh); // PC (16bit) Program Counter

            Baffa1_Registers.Reset(this.registers.TDRl, this.registers.TDRh); // TDR (16bit) Temporary Data Register
            this.registers.PTB.Reset();  // PTB (8bit) = Page table base

            Baffa1_Registers.Reset(this.registers.MSWl, this.registers.MSWh); // 

            Baffa1_Registers.Reset(this.registers.MARl, this.registers.MARh); // 
            Baffa1_Registers.Reset(this.registers.MDRl, this.registers.MDRh); // 

            //JumpZ80(z80, z80->registers.PC);

            unchecked
            {
                this.BKPT = (ushort)0xFFFF; // Breakpoint
            }
        }

        public void Display_registers(HW_TTY hw_tty)
        {

            ushort memADDR = Baffa1_Registers.Value(this.registers.PCl, this.registers.PCh);
            byte opcode = Get_current_memory()[memADDR];


            hw_tty.Print(" DATA REGISTERS                                        | POINTER REGISTERS\n");
            hw_tty.Print(String.Format(" *A={0}", Baffa1_Registers.Value(this.registers.Al, this.registers.Ah).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("B={0}", Baffa1_Registers.Value(this.registers.Bl, this.registers.Bh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("C={0}", Baffa1_Registers.Value(this.registers.Cl, this.registers.Ch).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("D={0}", Baffa1_Registers.Value(this.registers.Dl, this.registers.Dh).ToString("X4")));
            hw_tty.Print(" | ");

            hw_tty.Print(String.Format("G={0}", Baffa1_Registers.Value(this.registers.Gl, this.registers.Gh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("BP={0}", Baffa1_Registers.Value(this.registers.BPl, this.registers.BPh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("SP={0}", Baffa1_Registers.Value(this.registers.SPl, this.registers.SPh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("SSP={0}", Baffa1_Registers.Value(this.registers.SSPl, this.registers.SSPh).ToString("X4")));
            hw_tty.Print("\n\n");

            hw_tty.Print(" MEMORY REGISTERS       INDEX REGISTERS\n");
            hw_tty.Print(String.Format(" *MAR={0}", Baffa1_Registers.Value(this.registers.MARl, this.registers.MARh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("MDR={0}", Baffa1_Registers.Value(this.registers.MDRl, this.registers.MDRh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("SI={0}", Baffa1_Registers.Value(this.registers.SIl, this.registers.SIh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("DI={0}", Baffa1_Registers.Value(this.registers.DIl, this.registers.DIh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("PC={0}", Baffa1_Registers.Value(this.registers.PCl, this.registers.PCh).ToString("X4")));
            hw_tty.Print(" | ");


            hw_tty.Print(String.Format("@PC={0}:{1}", opcode.ToString("X2"), Utils.GetStr(this.microcode.rom.rom_desc,0x400000 + (opcode * 256))));

            if (memADDR < Get_current_memory_size() - 3)
            {
                hw_tty.Print(String.Format(" [{0}{1}{2}]", Get_current_memory()[memADDR].ToString("X2"), Get_current_memory()[memADDR + 1].ToString("X2"), Get_current_memory()[memADDR + 2].ToString("X2")));
            }
            else if (memADDR < Get_current_memory_size() - 2)
            {
                hw_tty.Print(String.Format(" [{0}{1}[%02x%02x]", Get_current_memory()[memADDR].ToString("X2"),
                    Get_current_memory()[memADDR + 1].ToString("X2")));
            }
            else if (memADDR < Get_current_memory_size() - 1)
            {
                hw_tty.Print(String.Format(" [{0}]", Get_current_memory()[memADDR].ToString("X2")));
            }

            hw_tty.Print("\n\n");

            hw_tty.Print("                              SPECIAL REGISTERS\n");
            hw_tty.Print(" *FLAGS="); hw_tty.Print(String.Format("{0}:",Utils.print_byte_bin(this.registers.MSWh.Value()))); this.registers.mswh_flags_desc(hw_tty);
            hw_tty.Print(String.Format(" | IR={0}", this.microcode.IR.Value().ToString("X2")));
            hw_tty.Print(String.Format(" | TDR={0}", Baffa1_Registers.Value(this.registers.TDRl, this.registers.TDRh).ToString("X4")));
            hw_tty.Print(String.Format(" | PTB={0}", this.registers.PTB.Value().ToString("X2")));
            hw_tty.Print("\n");
            hw_tty.Print(" *STATS="); hw_tty.Print(String.Format("{0}:       ", Utils.print_byte_bin(this.registers.MSWl.Value())));
            this.registers.mswl_status_desc(hw_tty);
            hw_tty.Print("\n\n");
        }

        public void Display_registers_lite(HW_TTY hw_tty)
        {

            
            hw_tty.Print(String.Format("*   A={0}", Baffa1_Registers.Value(this.registers.Al, this.registers.Ah).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("  B={0}", Baffa1_Registers.Value(this.registers.Bl, this.registers.Bh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("  C={0}", Baffa1_Registers.Value(this.registers.Cl, this.registers.Ch).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("  D={0}", Baffa1_Registers.Value(this.registers.Dl, this.registers.Dh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("  G={0}", Baffa1_Registers.Value(this.registers.Gl, this.registers.Gh).ToString("X4")));
            hw_tty.Print("\n");

            hw_tty.Print(String.Format("*  SI={0}", Baffa1_Registers.Value(this.registers.SIl, this.registers.SIh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format(" DI={0}", Baffa1_Registers.Value(this.registers.DIl, this.registers.DIh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format(" PC={0}", Baffa1_Registers.Value(this.registers.PCl, this.registers.PCh).ToString("X4")));
            hw_tty.Print(" | ");


            ushort memADDR = Baffa1_Registers.Value(this.registers.PCl, this.registers.PCh);
            byte opcode = Get_current_memory()[memADDR];

            hw_tty.Print(String.Format("@PC={0}:{1}", opcode.ToString("X2"), Utils.GetStr(this.microcode.rom.rom_desc, 0x400000 + (opcode * 256))));

            if (memADDR < Get_current_memory_size() - 3)
            {
                hw_tty.Print(String.Format(" [{0}{1}{2}]", Get_current_memory()[memADDR].ToString("X2"), Get_current_memory()[memADDR + 1].ToString("X2"), Get_current_memory()[memADDR + 2].ToString("X2")));
            }
            else if (memADDR < Get_current_memory_size() - 2)
            {
                hw_tty.Print(String.Format(" [{0}{1}]", Get_current_memory()[memADDR].ToString("X2"),
                    Get_current_memory()[memADDR + 1].ToString("X2")
                ));
            }
            else if (memADDR < Get_current_memory_size() - 1)
            {
                hw_tty.Print(String.Format(" [{0}]", Get_current_memory()[memADDR].ToString("X2")));
            }

            hw_tty.Print("\n");



            hw_tty.Print(String.Format("* MAR={0}", Baffa1_Registers.Value(this.registers.MARl, this.registers.MARh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("MDR={0}", Baffa1_Registers.Value(this.registers.MDRl, this.registers.MDRh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("TDR={0}", Baffa1_Registers.Value(this.registers.TDRl, this.registers.TDRh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("PTB={0}", this.registers.PTB.Value().ToString("X2")));
            hw_tty.Print("\n");

            hw_tty.Print(String.Format("*  BP={0}", Baffa1_Registers.Value(this.registers.BPl, this.registers.BPh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format(" SP={0}", Baffa1_Registers.Value(this.registers.SPl, this.registers.SPh).ToString("X4")));
            hw_tty.Print(" | ");
            hw_tty.Print(String.Format("SSP={0}", Baffa1_Registers.Value(this.registers.SSPl, this.registers.SSPh).ToString("X4")));
            hw_tty.Print("\n");

            hw_tty.Print(String.Format("* FLAGS={0}", Utils.print_byte_bin(this.registers.MSWh.Value())));
            hw_tty.Print("   | ");
            hw_tty.Print(String.Format("STATUS={0}", Utils.print_byte_bin(this.registers.MSWl.Value())));
            hw_tty.Print("\n");
            hw_tty.Print(String.Format("* IR: {0}\n", this.microcode.IR.Value().ToString("X2")));
            hw_tty.Print("Flags: "); this.registers.mswh_flags_desc(hw_tty); hw_tty.Print("\n");
            hw_tty.Print("Status: "); this.registers.mswl_status_desc(hw_tty); hw_tty.Print("\n");
        }




        public void Memory_display(HW_TTY hw_tty)
        {
            
            int i = 0, j = 0;
            ushort PC = Baffa1_Registers.Value(this.registers.PCl, this.registers.PCh);

            if (!this.memory.debug_manual_offset)
            {
                if (0 + this.memory.debug_mem_offset > PC || PC >= 256 + this.memory.debug_mem_offset)
                    this.memory.debug_mem_offset = ((PC / 0x10) * 0x10);
            }

            hw_tty.Print("\n        ");

            for (i = 0; i < 16; i++)
            {
                hw_tty.Print(String.Format("{0} ", i.ToString("X2")));
            }

            hw_tty.Print(String.Format("\n\n {0} ", memory.debug_mem_offset.ToString("X4")));

            for (i = 0 + this.memory.debug_mem_offset; i < 256 + this.memory.debug_mem_offset; i++)
            {
                if (i % 16 == 0)
                    if (PC == i)
                        hw_tty.Print(" *");
                    else
                        hw_tty.Print("  ");
                if (PC == i || PC - 1 == i)
                {
                    hw_tty.Print(String.Format("{0}*", memory.mem_bios[i].ToString("X2")));
                }
                else
                {
                    hw_tty.Print(String.Format("{0} ", memory.mem_bios[i].ToString("X2")));
                }

                if ((i + 1) % 16 == 0 && i <= 255 + this.memory.debug_mem_offset)
                {
                    hw_tty.Print("  |");
                    for (j = (i + 1) - 16; j < (i + 1); j++)
                    {
                        if (this.memory.mem_bios[j] < 0x20)
                        {
                            hw_tty.Print(".");
                        }
                        else
                        {
                            hw_tty.Print(((char)this.memory.mem_bios[j]).ToString());
                        }
                    }
                    hw_tty.Print("|");

                    if (i < 255 + this.memory.debug_mem_offset)
                    {
                        hw_tty.Print(String.Format("\n {0} ", (i+1).ToString("X4")));
                    }
                    else
                    {
                        hw_tty.Print("\n");
                    }

                }
            }
        }



        public byte[] Get_current_memory()
        {
            byte[] memory;
            if (!Utils.CheckByteBit(this.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                memory = this.memory.mem_bios;

            else
                memory = this.memory.low_memory;

            return memory;
        }



        public uint Get_current_memory_size()
        {

            if (!Utils.CheckByteBit(this.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                return Baffa1_Config.BAFFA1_BIOS_MEMORY_SIZE;

            else
                return Baffa1_Config.BAFFA1_MAIN_MEMORY_SIZE;
        }



    }
}

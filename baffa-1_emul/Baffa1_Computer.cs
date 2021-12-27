using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Computer
    {

        public Baffa1_CPU cpu = new Baffa1_CPU();
        public Baffa1_Bus bus = new Baffa1_Bus();

        //public struct hw_rtc hw_rtc { get; set; }
        public HW_UART hw_uart = new HW_UART();
        public HW_IDE hw_ide = new HW_IDE();
        //public struct hw_timer hw_timer { get; set; }

        public HW_TTY hw_tty = new HW_TTY();
        //public HW_WEB hw_web { get; set; }

        public Dictionary<string, Tasm_Opcode> ht_opcodes = new Dictionary<string, Tasm_Opcode>();



        private byte[] data_pio = new byte[4];

        private ushort oldPC = 0xFFFF;
        private byte oldOP = 0xFF;

        private ushort last_opcode = 0xFF;

        private TextWriter fa;


        public byte get_current_opcode()
        {
            return (byte)(this.cpu.microcode.u_ad_bus / 64);
        }

        public byte get_current_opcode_cycle()
        {
            return (byte)(this.cpu.microcode.u_ad_bus % 64);
        }


        byte opcode_esc = 0x00;

        public void disassembly_current_opcode()
        {
            ushort current_opcode = get_current_opcode();
            byte current_cycle = get_current_opcode_cycle();

            if (last_opcode != current_opcode)
            {
                String temp = "";
                String line = "";

                if (current_opcode == 0xFD)
                {
                    opcode_esc = 0x01;
                }
                else if (current_opcode != 0xFD)
                {

                    if (opcode_esc == 0x01)
                    {
                        opcode_esc = 0x02;
                        temp = String.Format("{0}fd", current_opcode.ToString("X2")).ToLower();
                        current_opcode = (ushort)((0x00FD << 8) | current_opcode);
                    }
                    else
                    {
                        temp = String.Format("{0}", current_opcode.ToString("X2")).ToLower();

                        if (opcode_esc == 0x02)
                            opcode_esc = 0x03;
                    }

                    if (opcode_esc == 0x03)
                    {
                        opcode_esc = 0x00;
                    }
                    else if (this.ht_opcodes.ContainsKey(temp))
                    {
                        Tasm_Opcode tt = this.ht_opcodes[temp];
                        ushort memADDR = Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh);

                        line = String.Format("    {0}]\t{1}:{2}", ((uint)(memADDR - (temp.Length / 2))).ToString("X4").ToLower(), current_opcode.ToString("X2").ToLower(), tt.desc);

                        if (tt.size > 1)
                        {


                            int opcodesize = (temp.Length / 2) - 1;
                            int param_size = tt.size - opcodesize;
                            line += " (";

                            for (int i = param_size - 2; i >= 0; i--)
                            {
                                if (i != param_size - 2)
                                    line += String.Format(" {0}", this.read_memory((ushort)(memADDR + i)).ToString("X2").ToLower());
                                else
                                    line += String.Format("{0}", this.read_memory((ushort)(memADDR + i)).ToString("X2").ToLower());
                            }
                            line += ")";
                        }

                        line += "\n";

                        String str_out = "";
                        Utils.SaveToLog(str_out, fa, line);
                        //this.hw_tty.Print(str_out);
                    }
                    last_opcode = current_opcode;
                }
            }
        }

        //////////////////////////
        //////////////////////////
        //////////////////////////
        //////////////////////////
        //////////////////////////

        //Refresh Interruptions
        public void refresh_int()
        {

            byte not_clear_all_ints = Utils.GetByteBit((byte)~this.cpu.microcode.controller_bus.clear_all_ints, 0x00);

            byte int_vector_0 = 0x01; // 74ls138 disable = 1
            byte int_vector_1 = 0x01;
            byte int_vector_2 = 0x01;
            byte int_vector_3 = 0x01;
            byte int_vector_4 = 0x01;
            byte int_vector_5 = 0x01;
            byte int_vector_6 = 0x01;
            byte int_vector_7 = 0x01;

            byte int_status_0 = 0x00;
            byte int_status_1 = 0x00;
            byte int_status_2 = 0x00;
            byte int_status_3 = 0x00;
            byte int_status_4 = 0x00;
            byte int_status_5 = 0x00;
            byte int_status_6 = 0x00;
            byte int_status_7 = 0x00;


            if (this.cpu.microcode.controller_bus.int_ack == 0x01)
            {

                byte out_vector = (byte)((Utils.GetByteBit(this.cpu.microcode.controller_bus.int_vector, 3) << 2) |
                    (Utils.GetByteBit(this.cpu.microcode.controller_bus.int_vector, 2) << 1) |
                    (Utils.GetByteBit(this.cpu.microcode.controller_bus.int_vector, 1) << 0));

                if (out_vector == 0)
                    int_vector_0 = 0;
                else if (out_vector == 1)
                    int_vector_1 = 0;
                else if (out_vector == 2)
                    int_vector_2 = 0;
                else if (out_vector == 3)
                    int_vector_3 = 0;
                else if (out_vector == 4)
                    int_vector_4 = 0;
                else if (out_vector == 5)
                    int_vector_5 = 0;
                else if (out_vector == 6)
                    int_vector_6 = 0;
                else if (out_vector == 7)
                    int_vector_7 = 0;
            }

            byte int_clr_0 = (byte)(int_vector_0 & not_clear_all_ints);
            byte int_clr_1 = (byte)(int_vector_1 & not_clear_all_ints);
            byte int_clr_2 = (byte)(int_vector_2 & not_clear_all_ints);
            byte int_clr_3 = (byte)(int_vector_3 & not_clear_all_ints);
            byte int_clr_4 = (byte)(int_vector_4 & not_clear_all_ints);
            byte int_clr_5 = (byte)(int_vector_5 & not_clear_all_ints);
            byte int_clr_6 = (byte)(int_vector_6 & not_clear_all_ints);
            byte int_clr_7 = (byte)(int_vector_7 & not_clear_all_ints);


            if (int_clr_0 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 0))
                int_status_0 = 1;// Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 0), 0);
            else if (int_clr_0 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b11111110);

            if (int_clr_1 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 1))
                int_status_1 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 1), 0);
            else if (int_clr_1 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b11111101);

            if (int_clr_2 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 2))
                int_status_2 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 2), 0);
            else if (int_clr_2 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b11111011);

            if (int_clr_3 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 3))
                int_status_3 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 3), 0);
            else if (int_clr_3 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b11110111);

            if (int_clr_4 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 4))
                int_status_4 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 4), 0);
            else if (int_clr_4 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b11101111);

            if (int_clr_5 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 5))
                int_status_5 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 5), 0);
            else if (int_clr_5 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b11011111);

            if (int_clr_6 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 6))
                int_status_6 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 6), 0);
            else if (int_clr_6 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b10111111);

            if (int_clr_7 == 0x01 && Utils.CheckByteBit(this.cpu.microcode.controller_bus.int_req, 7))
                int_status_7 = 1;//Utils.GetByteBit(~Utils.GetByteBit(this.cpu.microcode.controller_bus.int_status, 7), 0);
            else if (int_clr_7 == 0x00)
                this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req & 0b01111111);


            this.cpu.microcode.controller_bus.int_status = (byte)((int_status_7 << 7) | (int_status_6 << 6) | (int_status_5 << 5) | (int_status_4 << 4) | (int_status_3 << 3) | (int_status_2 << 2) | (int_status_1 << 1) | (int_status_0 << 0));

            byte masked_status = (byte)((~(this.cpu.microcode.controller_bus.int_status & this.cpu.registers.INT_MASKS.Value())) & 0b11111111);

            this.cpu.microcode.controller_bus.int_request = (masked_status != 0b11111111) ? (byte)0x1 : (byte)0x0;

            if (this.cpu.microcode.controller_bus.int_vector_wrt == 0x00)
            {

                byte int_out = 0;

                if (Utils.GetByteBit(masked_status, 0) == 0)
                    int_out = 7;
                else if (Utils.GetByteBit(masked_status, 1) == 0)
                    int_out = 6;
                else if (Utils.GetByteBit(masked_status, 2) == 0)
                    int_out = 5;
                else if (Utils.GetByteBit(masked_status, 3) == 0)
                    int_out = 4;
                else if (Utils.GetByteBit(masked_status, 4) == 0)
                    int_out = 3;
                else if (Utils.GetByteBit(masked_status, 5) == 0)
                    int_out = 2;
                else if (Utils.GetByteBit(masked_status, 6) == 0)
                    int_out = 1;
                else if (Utils.GetByteBit(masked_status, 7) == 0)
                    int_out = 0;

                if (this.cpu.microcode.controller_bus.int_request != 0x00)
                    this.cpu.microcode.controller_bus.int_vector = (byte)(((~int_out) & 0b111) << 1);
            }
        }



        public byte buffer_rd()
        {
            // BUFFER_RD | BUFFER_RD_MEMORY -> BUS_RD
            return this.bus.bus_rd(this.cpu.registers, this.cpu.microcode.controller_bus.rd, this.cpu.microcode.controller_bus.panel_rd);
        }

        public byte buffer_wr()
        {
            // BUFFER_WR | BUFFER_WR_MEMORY -> BUS_WR
            return this.bus.bus_wr(this.cpu.registers, this.cpu.microcode.controller_bus.wr, this.cpu.microcode.controller_bus.panel_wr);
        }

        public byte buffer_mem_io()
        {
            // BUFFER_MEM_IO -> BUS_MEM_IO

            return this.bus.bus_mem_io(this.cpu.registers, this.cpu.microcode.controller_bus.memory_io, this.cpu.microcode.controller_bus.panel_mem_io);
        }



        public byte read_memory(ushort addr)
        {
            byte memory = 0;

            byte BUFFER_MEM_IO = this.buffer_mem_io();
            byte BUFFER_RD = this.buffer_rd();
            byte BUFFER_WR = this.buffer_wr();

            byte not_IO_ADDRESSING = Utils.GetByteBit((byte)(~(Utils.GetWordBit(addr, 7) & Utils.GetWordBit(addr, 8) & Utils.GetWordBit(addr, 9) & Utils.GetWordBit(addr, 10) & Utils.GetWordBit(addr, 11)
                & Utils.GetWordBit(addr, 12) & Utils.GetWordBit(addr, 13) & Utils.GetWordBit(addr, 14))), 0);


            byte not_IO_ADDRESSING2 = Utils.GetByteBit((byte)(
                Utils.GetWordBit((byte)(~(Utils.GetWordBit(addr, 16) & Utils.GetWordBit(addr, 17) & Utils.GetWordBit(addr, 18))), 0) &
                Utils.GetWordBit((byte)(~(Utils.GetWordBit(addr, 19) & Utils.GetWordBit(addr, 20) & Utils.GetWordBit(addr, 21))), 0)
                ), 0);

            byte enable_bios = Utils.GetByteBit((byte)(~(BUFFER_MEM_IO & (~Utils.GetWordBit(addr, 15)) & not_IO_ADDRESSING2)), 0); // not CE

            byte enable_main_mem = Utils.GetByteBit((byte)(~(BUFFER_MEM_IO & not_IO_ADDRESSING2 & not_IO_ADDRESSING & (Utils.GetWordBit(addr, 15)))), 0); // not CE

            byte mem_sel = (byte)(Utils.GetWordBit(addr, 19) | (Utils.GetWordBit(addr, 20) << 1) | (Utils.GetWordBit(addr, 21) << 1));

            if (BUFFER_RD == 0x00)
            {
                if (enable_bios == 0x00)
                    memory = this.cpu.memory.mem_bios[addr];

                else if (enable_main_mem == 0x00)
                    memory = this.cpu.memory.low_memory[addr];
                else
                {
                    switch (mem_sel)
                    {
                        case 0:
                            memory = this.cpu.memory.high_memory0[addr];
                            break;
                        case 1:
                            memory = this.cpu.memory.high_memory1[addr];
                            break;
                        case 2:
                            memory = this.cpu.memory.high_memory2[addr];
                            break;
                        case 3:
                            memory = this.cpu.memory.high_memory3[addr];
                            break;
                        case 4:
                            memory = this.cpu.memory.high_memory4[addr];
                            break;
                        case 5:
                            memory = this.cpu.memory.high_memory5[addr];
                            break;
                        case 6:
                            memory = this.cpu.memory.high_memory6[addr];
                            break;
                        case 7:
                            memory = this.cpu.memory.high_memory7[addr];
                            break;
                    }

                }
            }

            return memory;
        }


        public void write_memory(ushort addr, byte value)
        {
            byte BUFFER_MEM_IO = this.buffer_mem_io();
            byte BUFFER_RD = this.buffer_rd();
            byte BUFFER_WR = this.buffer_wr();

            byte not_IO_ADDRESSING = Utils.GetByteBit((byte)(~(Utils.GetWordBit(addr, 7) & Utils.GetWordBit(addr, 8) & Utils.GetWordBit(addr, 9) & Utils.GetWordBit(addr, 10) & Utils.GetWordBit(addr, 11)
                & Utils.GetWordBit(addr, 12) & Utils.GetWordBit(addr, 13) & Utils.GetWordBit(addr, 14))), 0);


            byte not_IO_ADDRESSING2 = Utils.GetByteBit((byte)(
                Utils.GetWordBit((byte)(~(Utils.GetWordBit(addr, 16) & Utils.GetWordBit(addr, 17) & Utils.GetWordBit(addr, 18))), 0)
                & Utils.GetWordBit((byte)(~(Utils.GetWordBit(addr, 19) & Utils.GetWordBit(addr, 20) & Utils.GetWordBit(addr, 21))), 0)
                ), 0);

            byte enable_bios = Utils.GetByteBit((byte)(~(BUFFER_MEM_IO & (~Utils.GetWordBit(addr, 15)) & not_IO_ADDRESSING2)), 0); // not CE

            byte enable_main_mem = Utils.GetByteBit((byte)(~(BUFFER_MEM_IO & not_IO_ADDRESSING2 & not_IO_ADDRESSING & (Utils.GetWordBit(addr, 15)))), 0); // not CE

            byte mem_sel = (byte)(Utils.GetWordBit(addr, 19) | (Utils.GetWordBit(addr, 20) << 1) | (Utils.GetWordBit(addr, 21) << 2));

            if (BUFFER_WR == 0x00 && enable_main_mem == 0x00)
            {
                this.cpu.memory.low_memory[addr] = value;
            }
            else if (BUFFER_WR == 0x00)
            {
                switch (mem_sel)
                {
                    case 0:
                        this.cpu.memory.high_memory0[addr] = value;
                        break;
                    case 1:
                        this.cpu.memory.high_memory1[addr] = value;
                        break;
                    case 2:
                        this.cpu.memory.high_memory2[addr] = value;
                        break;
                    case 3:
                        this.cpu.memory.high_memory3[addr] = value;
                        break;
                    case 4:
                        this.cpu.memory.high_memory4[addr] = value;
                        break;
                    case 5:
                        this.cpu.memory.high_memory5[addr] = value;
                        break;
                    case 6:
                        this.cpu.memory.high_memory6[addr] = value;
                        break;
                    case 7:
                        this.cpu.memory.high_memory7[addr] = value;
                        break;
                }
            }
        }


        public ulong read_address_bus()
        {
            ulong address_bus = 0x00;

            if (this.bus.bus_tristate(this.cpu.registers) == 0x00)
            {

                if (!Utils.CheckByteBit(this.cpu.registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN))
                {
                    address_bus = (ulong)((this.cpu.registers.MARh.Value() << 8) | this.cpu.registers.MARl.Value());

                    this.cpu.microcode.controller_bus.memory_io = 1;
                    this.cpu.microcode.controller_bus.page_present = 1;
                    this.cpu.microcode.controller_bus.page_writable = 1;
                }
                else
                {
                    ushort ptb_mem_addr = (ushort)((this.cpu.registers.MARh.Value() >> 3) & 0b00011111);

                    if (this.cpu.microcode.Page_table_addr_src(this.cpu.registers.MSWl.Value()) == 0x01)
                        ptb_mem_addr = (ushort)(ptb_mem_addr | (this.cpu.registers.PTB.Value() << 5));

                    if (this.cpu.microcode.controller_bus.mdr_to_pagtbl_en == 0x0)
                        address_bus = (ulong)(((this.cpu.memory.mem_page_table1[ptb_mem_addr] & 0b00000011) << 8) | this.cpu.memory.mem_page_table0[ptb_mem_addr]);
                    else
                        address_bus = (ulong)((this.cpu.registers.MDRh.Value() << 8) | this.cpu.registers.MDRl.Value());

                    address_bus = (ulong)((address_bus << 11) | (ulong)((this.cpu.registers.MARh.Value() & (ulong)0b00000111) << 8) | this.cpu.registers.MARl.Value());

                    this.cpu.microcode.controller_bus.memory_io = Utils.GetByteBit(this.cpu.memory.mem_page_table1[ptb_mem_addr], 3);
                    this.cpu.microcode.controller_bus.page_present = Utils.GetByteBit(this.cpu.memory.mem_page_table1[ptb_mem_addr], 3);
                    this.cpu.microcode.controller_bus.page_writable = Utils.GetByteBit(this.cpu.memory.mem_page_table1[ptb_mem_addr], 4);
                }
            }
            else
            {
                address_bus = this.cpu.microcode.controller_bus.panel_address;
            }
            /////MEMORIA AQUI
            // ~oe = output enabled
            // ~we = write enabled
            // ~ce = chip enabled

            return address_bus;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////

        public void mem_rd(byte peripherical_sel)
        {
            String str_out = "";

            if (buffer_rd() == 0x00)
            {
                ulong mem_addr = read_address_bus();

                switch (peripherical_sel)
                {

                    case 0://UART SERVICES INTERRUPT = FF80 to FF85

                        if (mem_addr - 0xFF80 == 0)
                        {
                            this.hw_uart.Read();
                            this.bus.data_bus = this.hw_uart.data[0];
                        }
                        else if (mem_addr - 0xFF80 == 5)
                        {
                            this.hw_uart.GetLsr();
                            this.bus.data_bus = this.hw_uart.data[5];
                        }
                        else
                        {
                            this.bus.data_bus = this.hw_uart.data[mem_addr - 0xFF80];
                        }

                        if ((Baffa1_Config.DEBUG_UART) && (get_current_opcode() > 0))
                        {

                            String log_uart = this.hw_uart.Print("READ", (int)(mem_addr - 0xFF80));

                            Utils.SaveToLog(str_out, fa, log_uart);
                        }
                        break;
                    /*
                case 1:
                    //Printf("** UART_1 ** \n");
                    break;
                case 2://RTC I/O bank = FFA0 to FFAF

                    //Printf("** RTC ** \n");
                    this.bus.data_bus = this.hw_rtc.data[mem_addr - 0xFFA0];

                    if ((Baffa1_Config.DEBUG_RTC) > 0)
                    {

                        char log_rtc[255];
                        hw_rtc_Print(&this.hw_rtc, (char*)"READ ", (mem_addr - 0xFFA0), log_rtc);

                        Utils.SaveToLog(str_out, fa, log_rtc);
                        //this.hw_tty.Print(str_out);

                    }
                    break;
                case 3:
                    //Printf("** PIO_0 ** \n");
                    //Printf("READ  PIO_0: %02x|%02x|%02x\n", data_pio[0], data_pio[1], data_pio[2], data_pio[3]);
                    break;
                case 4:
                    //Printf("** PIO_1 ** \n");
                    break;
                    */
                    case 5: //IDE = FFD0 to FFD7
                            //Printf("** IDE ** \n");
                        if (mem_addr - 0xFFD0 == 0)
                            hw_ide.Read();

                        this.bus.data_bus = this.hw_ide.data[mem_addr - 0xFFD0];


                        if (Baffa1_Config.DEBUG_IDE && get_current_opcode() > 0)
                        {

                            String log_ide = hw_ide.Print("READ ", (int)(mem_addr - 0xFFD0));

                            Utils.SaveToLog(str_out, fa, log_ide);
                            //this.hw_tty.Print(str_out);

                        }
                        break;
                    /*
                case 6: //TIMER = FFE0 - FFE3
                        //Printf("** TIMER ** \n");
                    this.bus.data_bus = this.hw_timer.data[mem_addr - 0xFFE0];

                    if ((Baffa1_Config.DEBUG_TIMER) > 0)
                    {

                        char log_timer[255];
                        hw_timer_Print(&this.hw_timer, (char*)"READ ", (mem_addr - 0xFFE0), log_timer);

                        Utils.SaveToLog(str_out, fa, log_timer);
                        //this.hw_tty.Print(str_out);
                    }
                    break;
                case 7:
                    //Printf("** BIOS_CONFIG ** \n");
                    break;
                    */
                    default:
                        //this.bus.data_bus = this.cpu.get_current_memory()[mem_addr];
                        this.bus.data_bus = this.read_memory((ushort)mem_addr);

                        if (Baffa1_Config.DEBUG_TRACE_RDMEM && get_current_opcode() > 0)
                        {
                            String log = "";
                            if (this.bus.data_bus == 0x00)
                                log += String.Format("         \t\t\t\tREAD  RAM [{0}]\t= {1} \'\\0\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else if (this.bus.data_bus == 0x09)
                                log += String.Format("         \t\t\t\tREAD  RAM [{0}]\t= {1} \'\\t\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else if (this.bus.data_bus == 0x0a)
                                log += String.Format("         \t\t\t\tREAD  RAM [{0}]\t= {1} \'\\r\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else if (this.bus.data_bus == 0x0d)
                                log += String.Format("         \t\t\t\tREAD  RAM [{0}]\t= {1} \'\\n\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else
                                log += String.Format("         \t\t\t\tREAD  RAM [{0}]\t= {1} \'{2}\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower(), ((char)this.bus.data_bus).ToString());

                            Utils.SaveToLog(str_out, fa, log);
                            //this.hw_tty.Print(str_out);
                        }
                        break;
                }
            }
        }

        public void mem_wr(byte peripherical_sel)
        {

            string str_out = "";

            if (buffer_wr() == 0x00)
            {
                ulong mem_addr = read_address_bus();

                switch (peripherical_sel)
                {

                    case 0://UART SERVICES INTERRUPT = FF80 to FF85

                        if (mem_addr - 0xFF80 == 0)
                        {
                            this.hw_uart.Send(this.bus.data_bus);


                            this.hw_tty.Send(this.bus.data_bus);

                            /*
                            if (Baffa1_Config.WEB_SERVER)
                                this.hw_web.new_char(this.bus.data_bus);
                                */

                        }

                        else
                            this.hw_uart.data[mem_addr - 0xFF80] = this.bus.data_bus;


                        if (Baffa1_Config.DEBUG_UART && get_current_opcode() > 0)
                        {

                            String log_uart = this.hw_uart.Print("WRITE", (int)(mem_addr - 0xFF80));

                            Utils.SaveToLog(str_out, fa, log_uart);

                        }
                        break;
                    /*
                case 1:
                    //Printf("** UART_1 ** \n");
                    break;
                case 2://RTC I/O bank = FFA0 to FFAF
                       //Printf("** RTC ** \n");
                    this.hw_rtc.data[mem_addr - 0xFFA0] = this.bus.data_bus;


                    if ((Baffa1_Config.DEBUG_RTC) > 0)
                    {
                        char log_rtc[255];
                        hw_rtc_Print(&this.hw_rtc, (char*)"WRITE", (mem_addr - 0xFFA0), log_rtc);

                        Utils.SaveToLog(str_out, fa, log_rtc);
                        //this.hw_tty.Print(str_out);
                    }
                    break;
                case 3:
                    //Printf("** PIO_0 ** \n");

                    data_pio[mem_addr - 0xFFB0] = this.bus.data_bus;
                    if (mem_addr - 0xFFB0 == 3 && this.bus.data_bus == 0x80)
                    {

                        //Printf("WRITE PIO_0: %02x|%02x|%02x\n", data_pio[0], data_pio[1], data_pio[2], data_pio[3]);
                    }
                    break;
                case 4:
                    //Printf("** PIO_1 ** \n");
                    break;
                    */
                    case 5: //IDE = FFD0 to FFD7
                            //Printf("** IDE ** \n");
                        this.hw_ide.data[mem_addr - 0xFFD0] = this.bus.data_bus;

                        if (Baffa1_Config.DEBUG_IDE && get_current_opcode() > 0)
                        {
                            string log_ide = hw_ide.Print("WRITE", (int)(mem_addr - 0xFFD0));

                            Utils.SaveToLog(str_out, fa, log_ide);
                            //this.hw_tty.Print(str_out);
                        }

                        if (mem_addr - 0xFFD0 == 0)
                        {
                            hw_ide.Write();
                        }

                        // SET HD NEW STATUS AFTER LOG
                        if (this.hw_ide.data[7] == 0x04)
                        { // RESET IDE
                            this.hw_ide.data[7] = 0x0; // 0x80 ==busy// is ready again
                        }

                        else if (this.hw_ide.data[7] == 0xEF)
                        { // SET FEATURE COMMAND
                            this.hw_ide.data[7] = 0x00; // is ready again
                        }

                        else if (this.hw_ide.data[7] == 0xE6)
                        { // SLEEP
                            this.hw_ide.data[7] = 0x00;// zerar 
                                                       //hw_ide.data[7] = 0x80; // is ready again
                        }

                        else if (this.hw_ide.data[7] == 0x20)
                        { // read sector cmd
                            this.hw_ide.data[7] = 0b00001000;
                            hw_ide.Reset();
                        }
                        else if (this.hw_ide.data[7] == 0x30)
                        { // write sector cmd
                            this.hw_ide.data[7] = 0b00001000;
                            hw_ide.Reset();
                        }
                        break;
                    /*
                case 6: //TIMER = FFE0 - FFE3
                        //Printf("** TIMER ** \n");
                    if (mem_addr - 0xFFE0 == 0x00)
                        hw_timer_set_c0(&this.hw_timer, this.bus.data_bus);

                    else if (mem_addr - 0xFFE0 == 0x01)
                        hw_timer_set_c1(&this.hw_timer, this.bus.data_bus);

                    else
                        this.hw_timer.data[mem_addr - 0xFFE0] = this.bus.data_bus;

                    if ((Baffa1_Config.DEBUG_TIMER) > 0)
                    {

                        char log_timer[255];
                        hw_timer_Print(&this.hw_timer, (char*)"WRITE", (mem_addr - 0xFFE0), log_timer);

                        Utils.SaveToLog(str_out, fa, log_timer);
                        //this.hw_tty.Print(str_out);
                    }
                case 7:
                    //Printf("** BIOS_CONFIG ** \n");
                    break;
                    */
                    default:
                        if (Baffa1_Config.DEBUG_TRACE_WRMEM && get_current_opcode() > 0)
                        {
                            String log = "";
                            if (this.bus.data_bus == 0x00)
                                log += String.Format("         \t\t\t\tWRITE RAM [{0}]\t= {1} \'\\0'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else if (this.bus.data_bus == 0x09)
                                log += String.Format("         \t\t\t\tWRITE RAM [{0}]\t= {1} \'\\t'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else if (this.bus.data_bus == 0x0a)
                                log += String.Format("         \t\t\t\tWRITE RAM [{0}]\t= {1} \'\\r\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else if (this.bus.data_bus == 0x0d)
                                log += String.Format("         \t\t\t\tWRITE RAM [{0}]\t= {1} \'\\n\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower());
                            else
                                log += String.Format("         \t\t\t\tWRITE RAM [{0}]\t= {1} \'{2}\'\n", mem_addr.ToString("X4").ToLower(), this.bus.data_bus.ToString("X2").ToLower(), ((char)this.bus.data_bus).ToString());

                            Utils.SaveToLog(str_out, fa, log);
                        }

                        this.write_memory((ushort)mem_addr, this.bus.data_bus);
                        break;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////



        public byte peripheral_selection()
        {
            ulong addr = read_address_bus();

            byte BUFFER_MEM_IO = this.buffer_mem_io();

            byte not_IO_ADDRESSING = Utils.GetByteBit((byte)(~(Utils.GetWordBit((ushort)addr, 7) & Utils.GetWordBit((ushort)addr, 8) & Utils.GetWordBit((ushort)addr, 9) & Utils.GetWordBit((ushort)addr, 10) & Utils.GetWordBit((ushort)addr, 11)
                & Utils.GetWordBit((ushort)addr, 12) & Utils.GetWordBit((ushort)addr, 13) & Utils.GetWordBit((ushort)addr, 14))), 0);

            byte not_IO_ADDRESSING2 = Utils.GetByteBit((byte)(
                Utils.GetWordBit((byte)(~(Utils.GetWordBit((ushort)addr, 16) & Utils.GetWordBit((ushort)addr, 17) & Utils.GetWordBit((ushort)addr, 18))), 0)
                & Utils.GetWordBit((byte)(~(Utils.GetWordBit((ushort)addr, 19) & Utils.GetWordBit((ushort)addr, 20) & Utils.GetWordBit((ushort)addr, 21))), 0)
                ), 0);


            byte peripheral_sel = 0xFF;

            if ((Utils.GetWordBit((ushort)addr, 15) == 0x01 && not_IO_ADDRESSING == 0x00 && not_IO_ADDRESSING2 == 0x01) && BUFFER_MEM_IO == 0x01)
            {
                // adicionar controlle corretamente aos periféricos
                peripheral_sel = (byte)(Utils.GetWordBit((ushort)addr, 4) | (Utils.GetWordBit((ushort)addr, 5) << 1) | (Utils.GetWordBit((ushort)addr, 6) << 2));
            }

            return peripheral_sel;
        }

        public void bus_update()
        {
            // W = X
            this.bus.w_bus = this.bus.w_bus_refresh(this.cpu.registers, this.cpu.microcode.controller_bus.panel_regsel,
                this.cpu.microcode.controller_bus.alu_a_src, (this.cpu.microcode.controller_bus.display_reg_load > 0 && this.cpu.display_reg_load > 0 ? (byte)1 : (byte)0),
                this.cpu.microcode.controller_bus.int_vector, this.cpu.registers.INT_MASKS.Value(), this.cpu.microcode.controller_bus.int_status, fa, Baffa1_Config.DEBUG_TRACE_RDREG, this.hw_tty);

            ////////////////////////////////////////////////////////////////////////////
            // K = Y
            this.bus.k_bus = this.bus.k_bus_refresh(this.cpu.registers, this.cpu.microcode.controller_bus.alu_b_src);
            ////////////////////////////////////////////////////////////////////////////

            this.bus.alu_bus.x_bus = this.bus.x_bus_refresh(this.cpu.registers, this.cpu.microcode.controller_bus.alu_a_src, this.bus.w_bus);
            this.bus.alu_bus.y_bus = (this.cpu.microcode.controller_bus.alu_b_src == 0x00) ? this.cpu.microcode.controller_bus.imm : this.bus.k_bus;
        }

        public void alu_update()
        {
            this.cpu.alu.ALU_EXEC(this.cpu.microcode.controller_bus, this.bus.alu_bus, this.cpu.alu.U_cf, Utils.GetByteBit(this.cpu.registers.MSWh.Value(), Baffa1_Registers.MSWh_CF), this.hw_tty);
            this.cpu.alu.u_flags_refresh(this.cpu.microcode.controller_bus, this.cpu.registers.MSWl.Value(), this.cpu.registers.MSWh.Value(), this.bus.alu_bus, this.hw_tty);
        }




        public void refresh_pagetable_mem()
        {
            if (this.cpu.microcode.controller_bus.pagtbl_ram_we == 0x01)
            {
                ushort ptb_mem_addr = (ushort)((this.cpu.registers.MARh.Value() >> 3) & 0b00011111);
                if (this.cpu.microcode.Page_table_addr_src(this.cpu.registers.MSWl.Value()) == 0x01)
                    ptb_mem_addr = (ushort)(ptb_mem_addr | (ushort)this.cpu.registers.PTB.Value() << 5);

                if (this.cpu.microcode.controller_bus.mdr_to_pagtbl_en == 0x1)
                {
                    this.cpu.memory.mem_page_table0[ptb_mem_addr] = this.cpu.registers.MDRl.Value();
                    this.cpu.memory.mem_page_table1[ptb_mem_addr] = this.cpu.registers.MDRh.Value();
                }

                else
                {
                    this.cpu.memory.mem_page_table0[ptb_mem_addr] = 0;
                    this.cpu.memory.mem_page_table1[ptb_mem_addr] = 0;
                }
            }
        }

        public void mdr_enable()
        {
            if ((this.cpu.microcode.controller_bus.mdr_out_en & 0b00000001) == 0x01)
            {
                switch (this.cpu.microcode.controller_bus.mdr_out_src & 0b00000001)
                {
                    case 0x00:
                        this.bus.data_bus = this.cpu.registers.MDRl.Value();
                        break;
                    case 0x01:
                        this.bus.data_bus = this.cpu.registers.MDRh.Value();
                        break;
                }
            }
        }


        public void clock_cycle(ulong runtime_counter)
        {

            this.cpu.microcode.controller_bus.clk = this.cpu.microcode.controller_bus.clk == 0 ? (byte)1 : (byte)0;

            if (this.cpu.microcode.controller_bus.clk == 1)
            {
                //CYCLE
                ////////////////////////////////////////////////////////////////////////////
                // MICROCODE
                this.cpu.microcode.U_adder_refresh(this.cpu.microcode.controller_bus.next, this.cpu.microcode.controller_bus.final_condition, this.hw_tty);

                this.cpu.microcode.Sequencer_update(this.cpu.registers.MSWl.Value(), this.hw_tty);     // Sets Microcode

                ////////////////////////////////////////////////////////////////////////////		

                refresh_int();  //Refresh Interruptions

                this.refresh_pagetable_mem();  // PAGETABLE 

                ////////////////////////////////////////////////////////////////////////////
                // MEMORY / PERIPHERAL READ/WRITE
                byte peripheral_sel = this.peripheral_selection();
                mem_rd(peripheral_sel);
                this.mdr_enable();     // MDR READ - MDR ORDER IS CRITICAL - must be before MEM WR 	
                mem_wr(peripheral_sel);

                ////////////////////////////////////////////////////////////////////////////

                this.bus_update();     // BUSES K, W, X, Y		

                this.alu_update();     // ALU
            }
            else
            {
                ////////////////////////////////////////////////////////////////////////////
                // REGISTERS
                // IR 
                if (this.cpu.microcode.controller_bus.ir_wrt == 0x00) this.cpu.microcode.IR.Set(this.bus.data_bus);
                // GENERAL
                this.cpu.registers.Refresh(this.cpu.microcode.controller_bus, this.bus.alu_bus, this.bus.data_bus, this.cpu.alu.U_sf, fa);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }


            //CLOCK HIGH
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////
            // DEBUG
            byte current_opcode = get_current_opcode();
            byte current_opcode_cycle = get_current_opcode_cycle();


            //if ((get_current_opcode() != 0 || (get_current_opcode() == 0 && (get_current_opcode_cycle(this.cpu) < 10 | get_current_opcode_cycle(this.cpu) > 14))))
            if ((Baffa1_Config.DEBUG_LOG_OPCODE) && (current_opcode > 0)) disassembly_current_opcode();

            if (!(this.cpu.microcode.rom.bkpt_opcode == 0 && this.cpu.microcode.rom.bkpt_cycle == 0) &&
                (current_opcode == this.cpu.microcode.rom.bkpt_opcode && current_opcode_cycle == this.cpu.microcode.rom.bkpt_cycle))
            {

                this.hw_tty.Print(String.Format(" Microcode Breakpoint | Starting Opcode/Cycle:{0}{1}.\n", this.cpu.microcode.rom.bkpt_opcode.ToString("X2"), this.cpu.microcode.rom.bkpt_cycle.ToString("X2")));
                Baffa1_Config.DEBUG_MICROCODE = true;
                Baffa1_Config.DEBUG_REGISTERS = true;
                Baffa1_Config.DEBUG_ALU = true;
                this.cpu.microcode.controller_bus.panel_step = 1;
                if (Baffa1_Config.DEBUG_MICROCODE)
                {
                    this.hw_tty.Print("***** MICROCODE\n");
                    this.hw_tty.Print(String.Format("* U_FLAGS={0}\n", Utils.print_byte_bin(this.cpu.alu.U_FLAGS.Value())));
                    this.cpu.microcode.rom.display_current_cycles_desc(current_opcode, current_opcode_cycle, this.hw_tty);
                    this.hw_tty.Print("\n");
                }
                DebugMenu_Main.debugmenu_main(this.cpu, this.hw_tty);
            }

            if (Baffa1_Config.DEBUG_BUSES)
            {
                this.hw_tty.Print("***** BUS\n");
                this.hw_tty.Print(String.Format("* u_ad_bus    : {0}\n", Utils.print_word_bin(this.cpu.microcode.u_ad_bus)));
                this.hw_tty.Print(String.Format("* address bus : {0} {1}\n", Utils.print_byte_bin((byte)(read_address_bus() >> 16)), Utils.print_word_bin((ushort)read_address_bus())));
                this.hw_tty.Print("*  data_bus   |");
                this.hw_tty.Print("    k_bus    |");
                this.hw_tty.Print("    w_bus    |");
                this.hw_tty.Print("    x_bus    |");
                this.hw_tty.Print("    y_bus    |");
                this.hw_tty.Print("    z_bus    ");
                this.hw_tty.Print("\n");
                this.hw_tty.Print(String.Format("  {0}", Utils.print_byte_bin(this.bus.data_bus)));
                this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.k_bus)));
                this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.w_bus)));
                this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.alu_bus.x_bus)));
                this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.alu_bus.y_bus)));
                this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.alu_bus.z_bus)));
                this.hw_tty.Print("\n");
                this.hw_tty.Print("\n");
            }

            if (Baffa1_Config.DEBUG_MEMORY)
            {
                //this.hw_tty.Print("***** MEMORY\n"); 
                this.cpu.memory.DisplayBiosMemory(this.cpu.registers, this.hw_tty);
            }

            if (Baffa1_Config.DEBUG_REGISTERS)
            {
                this.hw_tty.Print("***** REGISTERS\n");
                this.cpu.Display_registers(this.hw_tty);

            }

            if (!(this.cpu.microcode.rom.bkpt_opcode == 0 && this.cpu.microcode.rom.bkpt_cycle == 0) &&
                (current_opcode == this.cpu.microcode.rom.bkpt_opcode &&
                    current_opcode_cycle == this.cpu.microcode.rom.bkpt_cycle))
            {
                this.hw_tty.Print(String.Format(" Microcode Breakpoint | Opcode/Cycle:{0}{1} Finished.\n", this.cpu.microcode.rom.bkpt_opcode.ToString("X2"), this.cpu.microcode.rom.bkpt_cycle.ToString("X2")));
                DebugMenu_Main.debugmenu_main(this.cpu, this.hw_tty);
            }

            if ((this.cpu.BKPT != (ushort)0xFFFF) && (Baffa1_Registers.Value(this.cpu.registers.MARl, this.cpu.registers.MARh) == this.cpu.BKPT
                || Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh) == this.cpu.BKPT))
            {
                Baffa1_Config.DEBUG_MICROCODE = true;
                Baffa1_Config.DEBUG_REGISTERS = true;
                Baffa1_Config.DEBUG_ALU = true;
                this.cpu.microcode.controller_bus.panel_step = 1;
                if (Baffa1_Config.DEBUG_MICROCODE)
                {
                    this.hw_tty.Print("\n***** MICROCODE\n");
                    //sPrintf(str_out, "U-ADDRESS: ");  Print_word_bin(str_out + strlen(str_out), this.cpu.microcode.u_ad_bus); sPrintf(str_out + strlen(str_out), "\n"); this.hw_tty.Print(str_out);
                    //sPrintf(str_out, "OPCODE: %02x (cycle %02x)\n", current_opcode, current_opcode_cycle); this.hw_tty.Print(str_out);
                    //sPrintf(str_out, "microcode: \n"); this.hw_tty.Print(str_out);
                    this.hw_tty.Print(String.Format("* U_FLAGS={0}\n", Utils.print_byte_bin(this.cpu.alu.U_FLAGS.Value())));
                    this.cpu.microcode.rom.display_current_cycles_desc(current_opcode, current_opcode_cycle, this.hw_tty);
                    this.hw_tty.Print("\n");
                }

                this.hw_tty.Print(String.Format(" Memory Breakpoint | Starting Address:{0}.\n", this.cpu.BKPT.ToString("X4")));
                DebugMenu_Main.debugmenu_main(this.cpu, this.hw_tty);
            }


            //hw_timer_tick_c0(&this.hw_timer);

            //this.bus.reset();
        }


        public void RunCPU(ulong runtime_counter)
        {

            while (true)
            {
                clock_cycle(runtime_counter);

                byte current_opcode = get_current_opcode();
                byte current_opcode_cycle = get_current_opcode_cycle();

                if (this.cpu.microcode.MUX(this.cpu.registers.MSWl.Value()) == 0x02)
                {
                    if (!Baffa1_Config.SERVER)
                    {

                        if (Console.KeyAvailable)
                        {
                            char dddd = this.hw_tty.GetChar();


                            if (dddd == 0x04)
                            {
                                this.hw_tty.Debug_call = true;
                            }
                            else
                            {

                                this.hw_uart.Receive((byte)dddd);
                                //this.cpu.microcode.controller_bus.int_request = 0x01;

                            }
                        }
                    }
                }


                if (this.hw_tty.Debug_call == true)
                {
                    this.hw_tty.Debug_call = false;
                    this.hw_tty.Send((byte)'\r');
                    this.hw_tty.Send((byte)'\n');
                    return;
                }


                ////////////////////////////////////////////////////////////////////////////
                ulong addr = read_address_bus();

                byte BUFFER_MEM_IO = this.buffer_mem_io();

                byte not_IO_ADDRESSING = Utils.GetByteBit((byte)(~(Utils.GetWordBit((ushort)addr, 7) & Utils.GetWordBit((ushort)addr, 8) & Utils.GetWordBit((ushort)addr, 9) & Utils.GetWordBit((ushort)addr, 10) & Utils.GetWordBit((ushort)addr, 11)
                    & Utils.GetWordBit((ushort)addr, 12) & Utils.GetWordBit((ushort)addr, 13) & Utils.GetWordBit((ushort)addr, 14))), 0);

                byte not_IO_ADDRESSING2 = Utils.GetByteBit((byte)(
                    Utils.GetWordBit((ushort)(~(Utils.GetWordBit((ushort)addr, 16) & Utils.GetWordBit((ushort)addr, 17) & Utils.GetWordBit((ushort)addr, 18))), 0)
                    & Utils.GetWordBit((ushort)(~(Utils.GetWordBit((ushort)addr, 19) & Utils.GetWordBit((ushort)addr, 20) & Utils.GetWordBit((ushort)addr, 21))), 0)
                    ), 0);

                byte peripherical_sel = 0xFF;

                if ((Utils.GetWordBit((ushort)addr, 15) == 0x01 && not_IO_ADDRESSING == 0x00 && not_IO_ADDRESSING2 == 0x01) && BUFFER_MEM_IO == 0x01)
                {
                    // adicionar controlle corretamente aos periféricos
                    peripherical_sel = (byte)(Utils.GetWordBit((ushort)addr, 4) | (Utils.GetWordBit((ushort)addr, 5) << 1) | (Utils.GetWordBit((ushort)addr, 6) << 2));
                }

                if (buffer_wr() == 0x00)
                {

                    if (this.hw_uart.Write())
                    {
                        if (this.hw_uart.uart_out.Count > 0)
                        {
                            this.cpu.microcode.controller_bus.int_req = (byte)(this.cpu.microcode.controller_bus.int_req | 0b10000000);
                        }
                    }
                    this.hw_uart.GetLsr();
                }


                ////////////////////////////////////////////////////////////////////////////


                if ((Baffa1_Config.DEBUG_LITE && (current_opcode != oldOP || Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh) != oldPC)) || Baffa1_Config.DEBUG_LITE_CYCLES)
                {

                    byte cur_opcode = current_opcode;
                    byte cur_cycle = current_opcode_cycle;

                    byte pc_opcode = this.read_memory(Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh));

                    if (!(cur_opcode == 0x00 && cur_cycle == 0x10))
                    {
                        string str = "";
                        string log_reg_lite = "";

                        string temp = "";


                        temp = current_opcode.ToString("X2");

                        ushort memADDR = Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh);

                        str = String.Format(" Opcode={0}:{1}", cur_opcode.ToString("X2").ToLower(), Utils.GetStr(this.cpu.microcode.rom.rom_desc, 0x400000 + (cur_opcode * 256)));
                        String opcode_desc = str.Length > 40 ? str.Substring(0, 40) : str.PadRight(40);
                        log_reg_lite += String.Format("{0}] {1}", (memADDR - (temp.Length / 2)).ToString("X4").ToLower(), opcode_desc);

                        log_reg_lite += String.Format(" | Cycle: {0}", cur_cycle.ToString("X2").ToLower());
                        log_reg_lite += String.Format(" | PC: {0}", Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh).ToString("X4").ToLower());

                        log_reg_lite += String.Format(" | MEM: {0}{1}{2}{3}\n",

                            this.read_memory(Baffa1_Registers.Value(this.cpu.registers.MARl, this.cpu.registers.MARh)).ToString("X2").ToLower(),
                            this.read_memory((ushort)(Baffa1_Registers.Value(this.cpu.registers.MARl, this.cpu.registers.MARh) + 1)).ToString("X2").ToLower(),
                            this.read_memory((ushort)(Baffa1_Registers.Value(this.cpu.registers.MARl, this.cpu.registers.MARh) + 2)).ToString("X2").ToLower(),
                            this.read_memory((ushort)(Baffa1_Registers.Value(this.cpu.registers.MARl, this.cpu.registers.MARh) + 3)).ToString("X2").ToLower()
                        );

                        Utils.SaveToLog("", fa, log_reg_lite);


                    }
                    oldOP = cur_opcode;
                    oldPC = Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh);
                }

                if (this.cpu.microcode.controller_bus.panel_step == 1 && this.cpu.microcode.MUX(this.cpu.registers.MSWl.Value()) == 0x02)
                {
                    this.hw_tty.Print("###########################################\n");
                    this.hw_tty.Print(String.Format("## End OpStep on Opcode/Cycle:{0}{1}. #######\n", current_opcode.ToString("X2"), current_opcode_cycle.ToString("X2")));
                    this.hw_tty.Print("###########################################\n");
                    return;
                }
                else if (this.cpu.microcode.controller_bus.panel_microcodestep == 1 || (this.cpu.microcode.controller_bus.panel_microcodestep == 0 && this.cpu.microcode.controller_bus.panel_run == 0))
                {
                    this.hw_tty.Print("###########################################\n");
                    this.hw_tty.Print(String.Format("## End MicroStep on Opcode/Cycle:{0}{1}. ####\n", current_opcode.ToString("X2"), current_opcode_cycle.ToString("X2")));
                    this.hw_tty.Print("###########################################\n");
                    return;
                }
                else if (this.cpu.microcode.controller_bus.reset == 1 || this.cpu.microcode.controller_bus.restart == 1)
                {

                    return;
                }

            }

            //this.cpu.microcode.controller_bus.next = 0x00;
        }






        //////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////



        public uint Init()
        {

            TextReader fp = new StreamReader(Baffa1_Config.WORKSPACE + "tasm1.tab");

            if (fp == null)
            {
                this.hw_tty.Print("Could not found tasm1.tab file!");
                System.Environment.Exit(1);
            }

            string line = fp.ReadLine();
            while (line != null)
            {


                string _desc = "";
                string _opcode = "";
                int _size = 0;
                while (line.IndexOf("\t\t") > -1)
                    line = line.Replace("\t\t", "\t");

                while (line.IndexOf("\t\"") > -1)
                    line = line.Replace("\t\"", " \"");

                string[] token = line.Split('\t');

                for (int col = 0; col < token.Length; col++)
                {

                    if (col == 0)
                        _desc = token[col];

                    else if (col == 1)
                        _opcode = token[col];

                    else if (col == 2)
                    {

                        int.TryParse(token[col], out _size);
                    }
                }

                if (_size >= 1)
                {
                    this.ht_opcodes[_opcode.ToLower()] = new Tasm_Opcode(_opcode, _desc, _size);
                }
                else
                    Console.WriteLine(line);

                line = fp.ReadLine();
            }

            this.cpu.Init(this.hw_tty);

            fa = File.CreateText("debug_trace.log");

            if (fa == null)
            {
                this.hw_tty.Print("can not open debug file\n");
                System.Environment.Exit(1);
            }

            this.hw_uart.Init(this.cpu);
            /*
            hw_rtc_init(&this.hw_rtc);
            hw_rtc_start_clock(&this.hw_rtc);
            hw_timer_init(&this.hw_timer);
            */
            hw_ide.Init();

            this.cpu.memory.Load_bios(hw_tty);

            hw_ide.Load_disk();

            //init bus
            this.bus.Init();
            /*
            if (Baffa1_Config.SERVER)
            {
                this.hw_tty.start_server(&this.hw_uart);
            }
            
            if (Baffa1_Config.WEB_SERVER)
            {
                this.hw_web.start_server(this.cpu, &this.hw_uart);
            }
            */

            return 1;
        }







        private void trace_menu()
        {
            this.hw_tty.Print("BAFFA-1 Debug Monitor > Trace\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  S - Opcode Step\n");
            this.hw_tty.Print("  M - Microcode Step\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  I - Back one microcode step\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  R - Display Registers\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  O - Display Memory\n");
            this.hw_tty.Print("  P - Reset Memory\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  G - Go(Run)\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  ? - Display Menu\n");
            this.hw_tty.Print("  Q - Back to Debug Monitor\n");
            this.hw_tty.Print("\n");
            this.hw_tty.Print("  ... or a sequence of opcodes to load into memory");
            this.hw_tty.Print("\n");
        }

        public void Run()
        {
            ulong runtime_counter = 0;

            //cpu_Print(&z80);

            bool debug = false;

            bool start = true;
            while (true)
            {


                if (!Baffa1_Config.PANEL_ENABLED)
                {
                    string input = "";

                    if (start)
                    {

                        input = "q";
                        start = false;
                    }
                    else
                    {
                        this.hw_tty.Print("TRACE> ");
                        input = this.hw_tty.GetLine();
                    }

                    if (input != "q" && input != "Q" &&
                        input != "m" && input != "M" &&
                        input != "r" && input != "R" &&
                        input != "p" && input != "P" &&
                        input != "s" && input != "S" &&
                        input != "o" && input != "O" &&
                        input != "i" && input != "I" &&
                        input != "g" && input != "G" &&
                        input != "?")
                    {
                        int numdigits = input.Length / 2;


                        uint begin = Baffa1_Registers.Value(this.cpu.registers.PCl, this.cpu.registers.PCh);
                        for (int i = 0; i != numdigits; ++i)
                        {
                            byte output = (byte)(16 * (int)input[2 * i] + (int)input[2 * i + 1]);
                            this.cpu.memory.mem_bios[begin + i] = output;
                            //sPrintf("%x\n", output[i]);
                        }

                        debug = true;
                    }
                    else if (input == "q" || input == "Q")
                    {


                        DebugMenu_Main.debugmenu_main_menu(this.hw_tty);
                        uint debug_status = DebugMenu_Main.debugmenu_main(this.cpu, this.hw_tty);
                        if (debug_status == 1)
                        { //SAME as R next IF
                            Baffa1_Config.DEBUG_MICROCODE = false;
                            Baffa1_Config.DEBUG_REGISTERS = false;
                            Baffa1_Config.DEBUG_ALU = false;
                            ///
                            this.cpu.microcode.controller_bus.panel_run = 1;
                            this.cpu.microcode.controller_bus.panel_step = 0;
                            this.cpu.microcode.controller_bus.panel_microcodestep = 0;
                            this.hw_tty.Print("\n\n");
                            this.hw_tty.Print("###########################################\n");
                            this.hw_tty.Print("## Running Instructions ###################\n");
                            this.hw_tty.Print("###########################################\n");
                        }
                        else if (debug_status == 2)
                        { //SAME as S next IF

                            Baffa1_Config.DEBUG_MICROCODE = true;
                            Baffa1_Config.DEBUG_REGISTERS = true;
                            Baffa1_Config.DEBUG_ALU = true;

                            debug = true;
                            this.cpu.microcode.controller_bus.panel_run = 0;
                            this.cpu.microcode.controller_bus.panel_step = 0;
                            this.cpu.microcode.controller_bus.panel_microcodestep = 0;
                            this.hw_tty.Print("\n");

                        }
                        else if (debug_status == 0)
                        {
                            return;
                        }
                    }
                    else if (input == "g" || input == "G")
                    {

                        ///
                        this.cpu.microcode.controller_bus.panel_run = 1;
                        this.cpu.microcode.controller_bus.panel_step = 0;
                        this.cpu.microcode.controller_bus.panel_microcodestep = 0;
                        this.hw_tty.Print("\n\n");
                        this.hw_tty.Print("###########################################\n");
                        this.hw_tty.Print("## Running Instructions ###################\n");
                        this.hw_tty.Print("###########################################\n");
                    }
                    else if (input == "s" || input == "S")
                    {

                        Baffa1_Config.DEBUG_MICROCODE = true;
                        Baffa1_Config.DEBUG_REGISTERS = true;
                        Baffa1_Config.DEBUG_ALU = true;

                        this.cpu.microcode.controller_bus.panel_run = 0;
                        this.cpu.microcode.controller_bus.panel_step = 1;
                        this.cpu.microcode.controller_bus.panel_microcodestep = 0;
                        this.hw_tty.Print("\n\n");
                        this.hw_tty.Print("###########################################\n");
                        this.hw_tty.Print("## OpCode Step ############################\n");
                        this.hw_tty.Print("###########################################\n");
                    }
                    else if (input == "m" || input == "M")
                    {
                        this.cpu.microcode.controller_bus.panel_run = 0;
                        this.cpu.microcode.controller_bus.panel_step = 0;
                        this.cpu.microcode.controller_bus.panel_microcodestep = 1;
                        this.hw_tty.Print("\n\n");
                        this.hw_tty.Print("###########################################\n");
                        this.hw_tty.Print("# Microcode Step ##########################\n");
                        this.hw_tty.Print("###########################################\n");
                        this.hw_tty.Print("***** REGISTERS\n");
                        this.cpu.Display_registers_lite(this.hw_tty);
                        this.hw_tty.Print(String.Format("* IR: {0}\n", this.cpu.microcode.IR.Value().ToString("X2")));
                        this.hw_tty.Print("\n");
                    }
                    else if (input == "i" || input == "I")
                    {
                        this.cpu.microcode.controller_bus.panel_run = 0;
                        this.cpu.microcode.controller_bus.panel_step = 1;
                        this.cpu.microcode.controller_bus.panel_microcodestep = 0;

                        this.cpu.microcode.u_adder = this.cpu.microcode.old_u_ad_bus;
                    }
                    else if (input == "p" || input == "P")
                    {
                        DebugMenu_Main.debugmenu_main_reset_cpu(this.cpu, this.hw_tty);
                        hw_ide.Load_disk();
                        this.cpu.memory.Reset();
                        debug = true;
                    }

                    else if (input == "o" || input == "O")
                    {
                        this.cpu.memory.DisplayBiosMemory(this.cpu.registers, this.hw_tty);
                        debug = true;
                    }
                    else if (input == "r" || input == "R")
                    {
                        DebugMenu_Main.debugmenu_main_display_registers(this.cpu, this.hw_tty);
                        debug = true;
                    }
                    else if (input == "?")
                    {
                        trace_menu();
                        debug = true;
                    }

                    input = "";


                    if ((!debug && !Baffa1_Config.PANEL_ENABLED) || Baffa1_Config.PANEL_ENABLED)
                        RunCPU(runtime_counter);
                    else
                        debug = false;

                    oldPC = 0xFF;
                    oldOP = 0xFF;

                    if (this.cpu.microcode.controller_bus.panel_run == 0 && this.cpu.microcode.controller_bus.panel_step == 0 && this.cpu.microcode.controller_bus.panel_microcodestep == 0)
                        this.cpu.microcode.u_adder = 0x10;

                }
                else
                {

                    if (this.cpu.microcode.controller_bus.reset == 1)
                    {
                    }
                    else if (this.cpu.microcode.controller_bus.restart == 1)
                    {

                        DebugMenu_Main.debugmenu_main_reset_cpu(this.cpu, this.hw_tty);
                        this.cpu.memory.Reset();
                        this.cpu.memory.Load_bios(hw_tty);
                        hw_ide.Load_disk();

                        this.cpu.microcode.controller_bus.restart = 0;
                    }

                    if (this.cpu.microcode.controller_bus.panel_run == 1 || this.cpu.microcode.controller_bus.panel_step == 1 || this.cpu.microcode.controller_bus.panel_microcodestep == 1)
                    {
                        RunCPU(runtime_counter);

                        if (this.cpu.microcode.controller_bus.panel_step == 1)
                            this.cpu.microcode.controller_bus.panel_step = 0;
                        else if (this.cpu.microcode.controller_bus.panel_microcodestep == 1)
                            this.cpu.microcode.controller_bus.panel_microcodestep = 0;
                    }
                }
            }
        }
    }
}

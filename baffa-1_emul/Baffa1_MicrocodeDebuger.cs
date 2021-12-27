using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baffa_1_emul
{
    public class Baffa1_MicrocodeDebuger
    {

        public Baffa1_CPU cpu = new Baffa1_CPU();
        public Baffa1_Bus bus = new Baffa1_Bus();

        public HW_TTY hw_tty = new HW_TTY();


        private byte[] data_pio = new byte[4];

        private ushort oldPC = 0xFFFF;
        private byte oldOP = 0xFF;

        public byte get_current_opcode()
        {
            return (byte)(this.cpu.microcode.u_ad_bus / 64);
        }

        public byte get_current_opcode_cycle()
        {
            return (byte)(this.cpu.microcode.u_ad_bus % 64);
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

            if (buffer_rd() == 0x00)
            {
                ulong mem_addr = read_address_bus();

                switch (peripherical_sel)
                {

                    default:
                        this.bus.data_bus = this.read_memory((ushort)mem_addr);
                        break;
                }
            }
        }

        public void mem_wr(byte peripherical_sel)
        {
            if (buffer_wr() == 0x00)
            {
                ulong mem_addr = read_address_bus();

                switch (peripherical_sel)
                {


                    default:
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
                this.cpu.microcode.controller_bus.int_vector, this.cpu.registers.INT_MASKS.Value(), this.cpu.microcode.controller_bus.int_status, null, Baffa1_Config.DEBUG_TRACE_RDREG, this.hw_tty);

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


        public void clock_cycle()
        {

            this.hw_tty.Print("**************************************************************\n");

            //this.cpu.microcode.controller_bus.clk = this.cpu.microcode.controller_bus.clk == 0 ? (byte)1 : (byte)0;

            //if (this.cpu.microcode.controller_bus.clk == 1)
            //{
            //CYCLE
            ////////////////////////////////////////////////////////////////////////////
            // MICROCODE
            this.cpu.microcode.U_adder_refresh(this.cpu.microcode.controller_bus.next, this.cpu.microcode.controller_bus.final_condition, this.hw_tty);

            this.cpu.microcode.Sequencer_update(this.cpu.registers.MSWl.Value(), this.hw_tty);     // Sets Microcode

            hw_tty.Print("***** MICROCODE\n");
            this.cpu.microcode.rom.display_current_cycles_desc((byte)(this.cpu.microcode.u_ad_bus / 64), (byte)(this.cpu.microcode.u_ad_bus % 64), hw_tty);
            hw_tty.Print("\n");

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
                                   //}
                                   //else
                                   //{
                                   ////////////////////////////////////////////////////////////////////////////
                                   // REGISTERS
                                   // IR 
            if (this.cpu.microcode.controller_bus.ir_wrt == 0x00) this.cpu.microcode.IR.Set(this.bus.data_bus);
            // GENERAL
            this.cpu.registers.Refresh(this.cpu.microcode.controller_bus, this.bus.alu_bus, this.bus.data_bus, this.cpu.alu.U_sf, null);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //}


            //CLOCK HIGH
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////
            // DEBUG
            byte current_opcode = get_current_opcode();
            byte current_opcode_cycle = get_current_opcode_cycle();

            //this.hw_tty.Print("***** BUS\n");
            //this.hw_tty.Print(String.Format("* u_ad_bus    : {0}\n", Utils.print_word_bin(this.cpu.microcode.u_ad_bus)));
            //this.hw_tty.Print(String.Format("* address bus : {0} {1}\n", Utils.print_byte_bin((byte)(read_address_bus() >> 16)), Utils.print_word_bin((ushort)read_address_bus())));
            //this.hw_tty.Print("*  data_bus   |");
            //this.hw_tty.Print("    k_bus    |");
            //this.hw_tty.Print("    w_bus    |");
            //this.hw_tty.Print("    x_bus    |");
            //this.hw_tty.Print("    y_bus    |");
            //this.hw_tty.Print("    z_bus    ");
            //this.hw_tty.Print("\n");
            //this.hw_tty.Print(String.Format("  {0}", Utils.print_byte_bin(this.bus.data_bus)));
            //this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.k_bus)));
            //this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.w_bus)));
            //this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.alu_bus.x_bus)));
            //this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.alu_bus.y_bus)));
            //this.hw_tty.Print(String.Format(" | {0}", Utils.print_byte_bin(this.bus.alu_bus.z_bus)));
            //this.hw_tty.Print("\n");
            //this.hw_tty.Print("\n");

            //this.hw_tty.Print("***** MEMORY\n"); 
            //this.cpu.memory.DisplayBiosMemory(this.cpu.registers, this.hw_tty);

            this.hw_tty.Print("***** REGISTERS\n");
            this.cpu.Display_registers(this.hw_tty);
        }


        public void RunCPU()
        {

            while (true)
            {
                clock_cycle();

                byte current_opcode = get_current_opcode();
                byte current_opcode_cycle = get_current_opcode_cycle();



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

                ////////////////////////////////////////////////////////////////////////////

                if (this.cpu.microcode.controller_bus.panel_step == 1 && this.cpu.microcode.MUX(this.cpu.registers.MSWl.Value()) == 0x02)
                {
                    this.hw_tty.Print(String.Format("## End OpStep on Opcode/Cycle:{0}{1}. #######\n", current_opcode.ToString("X2"), current_opcode_cycle.ToString("X2")));
                    return;
                }
                else if (this.cpu.microcode.controller_bus.panel_microcodestep == 1)
                {
                    this.hw_tty.Print(String.Format("## End MicroStep on Opcode/Cycle:{0}{1}. ####\n", current_opcode.ToString("X2"), current_opcode_cycle.ToString("X2")));
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



        public uint Init(System.Windows.Forms.TextBox textOutput)
        {
            Baffa1_Config.DEBUG_MICROCODE = false;
            Baffa1_Config.DEBUG_UADDRESSER = false;
            Baffa1_Config.DEBUG_UADDER = false;
            Baffa1_Config.DEBUG_UFLAGS = false;


            Baffa1_Config.DEBUG_BUSES = false;
            Baffa1_Config.DEBUG_ALU = false;

            Baffa1_Config.DEBUG_TRACE_RDREG = false;
            Baffa1_Config.DEBUG_TRACE_WRREG = false;
            Baffa1_Config.DEBUG_REGISTERS = false;

            Baffa1_Config.DEBUG_TRACE_RDMEM = false;
            Baffa1_Config.DEBUG_TRACE_WRMEM = false;
            Baffa1_Config.DEBUG_MEMORY = false;

            Baffa1_Config.DEBUG_LITE = false;
            Baffa1_Config.DEBUG_LITE_CYCLES = false;

            Baffa1_Config.DEBUG_UART = false;
            Baffa1_Config.DEBUG_IDE = false;
            Baffa1_Config.DEBUG_RTC = false;
            Baffa1_Config.DEBUG_TIMER = false;

            Baffa1_Config.SERVER = false;
            Baffa1_Config.WEB_SERVER = false;
            Baffa1_Config.DEBUG_LOG_OPCODE = false;


            this.hw_tty.TextOutput = textOutput;

            this.cpu.Init(this.hw_tty, true);

            //init bus
            this.bus.Init();

            this.cpu.memory.Reset();
            return 1;
        }


        public void RunMicroStep()
        {
            this.cpu.microcode.controller_bus.panel_run = 0;
            this.cpu.microcode.controller_bus.panel_step = 0;
            this.cpu.microcode.controller_bus.panel_microcodestep = 1;
            this.hw_tty.Print("## Start MicroStep ####\n");

            //this.hw_tty.Print("***** REGISTERS\n");
            //this.cpu.Display_registers_lite(this.hw_tty);
            //this.hw_tty.Print(String.Format("* IR: {0}\n", this.cpu.microcode.IR.Value().ToString("X2")));
            RunCPU();
        }

        public void RunOpcode()
        {
            this.cpu.microcode.controller_bus.panel_run = 0;
            this.cpu.microcode.controller_bus.panel_step = 1;
            this.cpu.microcode.controller_bus.panel_microcodestep = 0;
            this.hw_tty.Print("## Start OpStep ####\n");
            RunCPU();
        }
    }
}

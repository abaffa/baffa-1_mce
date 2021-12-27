using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Microcode
    {

        public ushort u_adder_b { get; set; }
        public ushort u_ad_bus { get; set; }
        public ushort old_u_ad_bus { get; set; }
        public ushort u_adder { get; set; }
        /////

        public Baffa1_Controller_Bus controller_bus = new Baffa1_Controller_Bus();

        public Baffa1_Register_8Bit IR = new Baffa1_Register_8Bit();

        public Baffa1_Register_8Bit U_ADDRESSl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit U_ADDRESSh = new Baffa1_Register_8Bit();

        public Baffa1_Rom rom = new Baffa1_Rom();


        public void Init(HW_TTY hw_tty, bool debug = false)
        {

            controller_bus.u_escape_0 = 0x00;
            controller_bus.u_escape_1 = 0x00;

            controller_bus.u_esc_in_src = 0x00;

            controller_bus.u_offset = 0b00000000;
            controller_bus.uzf_in_src = 0x00;
            controller_bus.ucf_in_src = 0x00;
            controller_bus.usf_in_src = 0x00;
            controller_bus.uof_in_src = 0x00;

            this.u_adder_b = 0x00;
            this.u_ad_bus = 0x00;
            this.u_adder = 0x00;

            this.IR.Reset();

            this.U_ADDRESSl.Reset();
            this.U_ADDRESSh.Reset();

            ///////////////////////////////////////
            controller_bus.next = 0x00;

            controller_bus.cond_inv = 0x00;
            controller_bus.cond_flags_src = 0x00;
            controller_bus.cond_sel = 0x00;

            controller_bus.ir_wrt = 0x00;

            controller_bus.shift_src = 0x00;
            controller_bus.zbus_out_src = 0x00;

            controller_bus.zf_in_src = 0x00;
            controller_bus.cf_in_src = 0x00;
            controller_bus.sf_in_src = 0x00;
            controller_bus.of_in_src = 0x00;

            controller_bus.rd = 0x00;
            controller_bus.wr = 0x00;


            controller_bus.display_reg_load = 0x00;
            controller_bus.dl_wrt = 0x00;
            controller_bus.dh_wrt = 0x00;
            controller_bus.cl_wrt = 0x00;
            controller_bus.ch_wrt = 0x00;
            controller_bus.bl_wrt = 0x00;
            controller_bus.bh_wrt = 0x00;
            controller_bus.al_wrt = 0x00;
            controller_bus.ah_wrt = 0x00;
            controller_bus.mdr_in_src = 0x00;
            controller_bus.mdr_out_src = 0x00;
            controller_bus.mdr_out_en = 0x00;
            controller_bus.mdrl_wrt = 0x00;
            controller_bus.mdrh_wrt = 0x00;
            controller_bus.tdrl_wrt = 0x00;
            controller_bus.tdrh_wrt = 0x00;
            controller_bus.dil_wrt = 0x00;
            controller_bus.dih_wrt = 0x00;
            controller_bus.sil_wrt = 0x00;
            controller_bus.sih_wrt = 0x00;
            controller_bus.marl_wrt = 0x00;
            controller_bus.marh_wrt = 0x00;
            controller_bus.bpl_wrt = 0x00;
            controller_bus.bph_wrt = 0x00;
            controller_bus.pcl_wrt = 0x00;
            controller_bus.pch_wrt = 0x00;
            controller_bus.spl_wrt = 0x00;
            controller_bus.sph_wrt = 0x00;

            controller_bus.mar_in_src = 0x00;

            controller_bus.ptb_wrt = 0x00;
            controller_bus.pagtbl_ram_we = 0x00;
            controller_bus.mdr_to_pagtbl_en = 0x00;
            controller_bus.force_user_ptb = 0x00;

            controller_bus.gl_wrt = 0x00;
            controller_bus.gh_wrt = 0x00;
            controller_bus.imm = 0x00;

            //
            controller_bus.memory_io = 0x00; // bus_mem_io 
            controller_bus.page_present = 0x00;
            controller_bus.page_writable = 0x00;

            controller_bus.status_wrt = 0x00; //mswl_wrt // status (flags de controle)

            //
            controller_bus.clear_all_ints = 0x00;
            controller_bus.int_vector = 0x00;
            controller_bus.mask_flags_wrt = 0x00;
            controller_bus.int_status = 0x00;
            controller_bus.int_vector_wrt = 0x00;
            controller_bus.int_ack = 0x00;
            controller_bus.int_request = 0x00;
            controller_bus.int_req = 0x00;
            //

            controller_bus.dma_req = 0x00;
            controller_bus.wait = 0x00;
            controller_bus.ext_input = 0x00;

            controller_bus.final_condition = 0x00;

            controller_bus.panel_regsel = 0x00;

            controller_bus.panel_rd = 0x01;
            controller_bus.panel_wr = 0x00;
            controller_bus.panel_mem_io = 0x00;

            controller_bus.panel_address = 0x00;
            controller_bus.panel_data = 0x00;
            controller_bus.panel_req = 0x00;

            controller_bus.panel_run = 0x00;
            controller_bus.panel_step = 0x000;
            controller_bus.panel_microcodestep = 0x00;

            controller_bus.clk = 0x00;


            controller_bus.reset = 0x00;
            controller_bus.restart = 0x00;

            if (!debug)
                this.rom.Init(hw_tty);
        }




        public byte Int_pending(byte reg_status_value)
        {
            return (byte)(Utils.GetByteBit(controller_bus.int_request, 0) & Utils.GetByteBit(reg_status_value, Baffa1_Registers.MSWl_INTERRUPT_ENABLE));
        }
        public byte Any_interruption(byte reg_status_value)
        {
            return (byte)(Utils.GetByteBit((byte)Int_pending(reg_status_value), 0) | Utils.GetByteBit(controller_bus.dma_req, 0));
        }
        public byte Page_table_addr_src(byte reg_status_value)
        {
            return (byte)(Utils.GetByteBit(controller_bus.force_user_ptb, 0) ^ Utils.GetByteBit(reg_status_value, Baffa1_Registers.MSWl_CPU_MODE));
        }



        public byte MUX(byte reg_status_value)
        {

            byte mux_A = ((controller_bus.next == (byte)0b00000011) || (controller_bus.next == (byte)0b00000010 && Any_interruption(reg_status_value) == (byte)0x01)) ? (byte)0x1 : (byte)0x0;
            byte mux_B = controller_bus.next == (byte)0b00000010 ? (byte)0x1 : (byte)0x0;

            byte mux = (byte)(Utils.SetByteBit(mux_B, 1) | Utils.SetByteBit(mux_A, 0));

            return mux;
        }

        private void display_u_adder(byte typ, byte final_condition, HW_TTY hw_tty)
        {

            hw_tty.Print("* next(typ): "); hw_tty.Print(Utils.print_nibble_bin(typ));
            hw_tty.Print(" | ");
            hw_tty.Print(" u_offset: "); hw_tty.Print(Utils.print_nibble_bin(controller_bus.u_offset));
            hw_tty.Print(" | ");
            hw_tty.Print("Final Condition : "); hw_tty.Print(Utils.print_nibble_bin(final_condition));
            hw_tty.Print("\n");

            hw_tty.Print("* A(u_ad): "); hw_tty.Print(Utils.print_word_bin_nibbles(this.u_ad_bus));
            hw_tty.Print("\n");
            hw_tty.Print("* B: "); hw_tty.Print(Utils.print_word_bin_nibbles(this.u_adder_b));
            hw_tty.Print("\n");
            hw_tty.Print("* u_adder: "); hw_tty.Print(Utils.print_word_bin(this.u_adder));
            hw_tty.Print("\n");
        }


        public ushort U_adder_refresh(byte typ, byte final_condition, HW_TTY hw_tty)
        {

            //if type = branch, and condition = false, then next = +1

            //Typ1 Typ0		Desc
            //0    0		offset
            //0    1		branch
            //1    0		pre-fetch
            //1    1		post_fetch

            byte u_offset6 = Utils.GetByteBit(controller_bus.u_offset, 6);

            if (typ == 0b00000001 && !Utils.CheckByteBit(final_condition, 0))
                this.u_adder_b = 0x01;
            else
                this.u_adder_b = (ushort)(controller_bus.u_offset |
                Utils.SetWordBit(u_offset6, 7) |
                Utils.SetWordBit(u_offset6, 8) |
                Utils.SetWordBit(u_offset6, 9) |
                Utils.SetWordBit(u_offset6, 10) |
                Utils.SetWordBit(u_offset6, 11) |
                Utils.SetWordBit(u_offset6, 12) |
                Utils.SetWordBit(u_offset6, 13));

            this.u_adder = (ushort)((this.u_ad_bus & 0b0011111111111111) + this.u_adder_b);

            if (Baffa1_Config.DEBUG_UADDER)
            {
                hw_tty.Print("***** U_ADDER\n");
                display_u_adder(controller_bus.next, controller_bus.final_condition, hw_tty);
                hw_tty.Print("\n");
            }

            return this.u_adder;
        }


        private void addresser(byte reg_status_value, HW_TTY hw_tty)
        {

            ////////////////////////////////////////////////////////////////////////////
            //IC15 //IC59 //IC153 //IC167 //IC61 //IC12 //IC341 //IC175

            //update_final_condition(baffa1_cpu);


            //if type = branch, and condition = false, then next = +1
            /*
            em 2 campos chamados next_0 e next_1.
                Paulo enviou 7 de maio às 02:37
                next = 00 : proximo ciclo definido pelo valor OFFSET
                next = 01 : usado para branches
                next = 10 : proximo ciclo eh instruction fetch
                next = 11 : proximo ciclo definido por IR
                */
            //Typ1 Typ0		Desc
            //0    0		offset
            //0    1		branch
            //1    0		pre-fetch
            //1    1		post_fetch

            //IC45A //IC94B //IC119A //IC43D //IC45A //IC43D //IC34 //IC35 //IC36 //IC37 //IC38 //IC40 //IC73


            //MICROCODE 0 TO 63
            //IS RESERVED FOR CPU STARTUP.
            //U_FETCH AT 0x10
            //U_TRAP AT 0x20
            ushort u_fetch = 0x10;
            ushort u_trap = 0x20;

            byte mux = MUX(reg_status_value);


            ushort u_address_mux_l = 0x00;
            ushort u_address_mux_h = 0x00;

            if (mux == 0x00)
            {
                u_address_mux_l = (ushort)(this.u_adder & 0b00000011111111);
                u_address_mux_h = (ushort)((this.u_adder & 0b11111100000000) >> 8);
            }
            else if (mux == 0x01)
            {
                u_address_mux_l = (ushort)((Utils.GetByteBit(controller_bus.u_escape_0, 0) << 4) | (Utils.GetByteBit(controller_bus.u_escape_1, 0) << 5) | ((this.IR.Value() & 0b00000011) << 6));
                u_address_mux_h = (ushort)((this.IR.Value() & 0b11111100) >> 2);
            }
            else if (mux == 0x02)
            {
                u_address_mux_l = (ushort)(u_fetch & 0b00000011111111);
                u_address_mux_h = (ushort)((u_fetch & 0b11111100000000) >> 8);
            }
            else if (mux == 0x03)
            {
                u_address_mux_l = (ushort)(u_trap & 0b00000011111111);
                u_address_mux_h = (ushort)((u_trap & 0b11111100000000) >> 8);
            }

            ushort u_address = (ushort)((u_address_mux_h << 8) | u_address_mux_l);
            Baffa1_Registers.Set(this.U_ADDRESSl, this.U_ADDRESSh, u_address);

            this.old_u_ad_bus = this.u_ad_bus;

            if (controller_bus.reset == 0x00)
                this.u_ad_bus = u_address;
            else
                this.u_ad_bus = 0;

            if (Baffa1_Config.DEBUG_UADDRESSER)
            {
                hw_tty.Print("***** U_ADDRESSER\n");
                hw_tty.Print("* Next(typ): "); hw_tty.Print(Utils.print_nibble_bin(controller_bus.next));
                hw_tty.Print(" | Any Interruption: "); hw_tty.Print(Utils.print_nibble_bin(Any_interruption(reg_status_value)));
                hw_tty.Print("\n");

                hw_tty.Print(String.Format("* U_ADDRESS={0}", Baffa1_Registers.Value(this.U_ADDRESSl, this.U_ADDRESSh).ToString("X4")));
                hw_tty.Print(" | ");
                hw_tty.Print(String.Format("U_AD={0}", this.u_ad_bus.ToString("X4")));
                hw_tty.Print(" | ");
                hw_tty.Print("Mux: "); hw_tty.Print(Utils.print_nibble_bin(mux));
                hw_tty.Print("\n");

                hw_tty.Print("\n");
            }
        }


        public void Sequencer_update(byte reg_status_value, HW_TTY hw_tty)
        {

            //CLOCK LOW
            this.addresser(reg_status_value, hw_tty); // Sets u-address

            ////////////////////////////////////////////////////////////////////////////

            this.load_microcode_from_rom(); // Sets Microcode from Rom

            if (Baffa1_Config.DEBUG_MICROCODE)
            {

                hw_tty.Print("***** MICROCODE\n");
                this.rom.display_current_cycles_desc((byte)(this.u_ad_bus / 64), (byte)(this.u_ad_bus % 64), hw_tty);
                hw_tty.Print("\n");
            }
        }
        private void load_microcode_from_rom()
        {

            if (controller_bus.reset == 0x00)
            {

                // ROM 0
                controller_bus.next = (byte)(this.rom.roms[0][this.u_ad_bus] & 0b00000011);///////////////////////

                // ROM 1
                controller_bus.u_offset = (byte)(((this.rom.roms[1][this.u_ad_bus] & 0b00000001) << 6) | ((this.rom.roms[0][this.u_ad_bus] >> 2) & 0b00111111));////////////////////
                controller_bus.cond_inv = (byte)((this.rom.roms[1][this.u_ad_bus] >> 1) & 0b00000001);
                controller_bus.cond_flags_src = (byte)((this.rom.roms[1][this.u_ad_bus] >> 2) & 0b00000001);
                controller_bus.cond_sel = (byte)((this.rom.roms[1][this.u_ad_bus] >> 3) & 0b00001111);
                controller_bus.u_escape_0 = (byte)(this.rom.roms[1][this.u_ad_bus] >> 7 & 0b00000001); //ESCAPE

                // ROM 2
                controller_bus.uzf_in_src = (byte)(this.rom.roms[2][this.u_ad_bus] & 0b00000011);/////////////////
                controller_bus.ucf_in_src = (byte)((this.rom.roms[2][this.u_ad_bus] >> 2) & 0b00000011);//////////
                controller_bus.usf_in_src = (byte)((this.rom.roms[2][this.u_ad_bus] >> 4) & 0b00000001);//////////
                controller_bus.uof_in_src = (byte)((this.rom.roms[2][this.u_ad_bus] >> 5) & 0b00000001);//////////
                controller_bus.ir_wrt = (byte)((this.rom.roms[2][this.u_ad_bus] >> 6) & 0b00000001);/////////////////
                controller_bus.status_wrt = (byte)((this.rom.roms[2][this.u_ad_bus] >> 7) & 0b00000001);/////////////

                // ROM 3
                controller_bus.shift_src = (byte)(this.rom.roms[3][this.u_ad_bus] & 0b00000111);/////////////////////
                controller_bus.zbus_out_src = (byte)((this.rom.roms[3][this.u_ad_bus] >> 3) & 0b00000011);///////////
                controller_bus.alu_a_src = (byte)(((this.rom.roms[3][this.u_ad_bus] >> 5) & 0b00000111) | ((this.rom.roms[4][this.u_ad_bus] & 0b00000111) << 3));/////////////////////

                // ROM 4
                controller_bus.alu_op = (byte)((this.rom.roms[4][this.u_ad_bus] >> 3) & 0b00001111);//////////////
                controller_bus.alu_mode = (byte)((this.rom.roms[4][this.u_ad_bus] >> 7) & 0b00000001);////////////

                // ROM 5
                controller_bus.alu_cf_in_src = (byte)((this.rom.roms[5][this.u_ad_bus]) & 0b00000011);////////////
                controller_bus.alu_cf_in_inv = (byte)((this.rom.roms[5][this.u_ad_bus] >> 2) & 0b00000001);///////
                controller_bus.zf_in_src = (byte)((this.rom.roms[5][this.u_ad_bus] >> 3) & 0b00000011);///////////////
                controller_bus.alu_cf_out_inv = (byte)((this.rom.roms[5][this.u_ad_bus] >> 5) & 0b00000001);//////
                controller_bus.cf_in_src = (byte)(((this.rom.roms[5][this.u_ad_bus] >> 6) & 0b00000011) | ((this.rom.roms[6][this.u_ad_bus] & 0b00000001) << 2));/////////////////////

                // ROM 6
                controller_bus.sf_in_src = (byte)((this.rom.roms[6][this.u_ad_bus] >> 1) & 0b00000011);///////////////
                controller_bus.of_in_src = (byte)((this.rom.roms[6][this.u_ad_bus] >> 3) & 0b00000111);///////////////
                controller_bus.rd = (byte)((this.rom.roms[6][this.u_ad_bus] >> 6) & 0b00000001);//////////////////////
                controller_bus.wr = (byte)((this.rom.roms[6][this.u_ad_bus] >> 7) & 0b00000001);//////////////////////

                // ROM 7
                controller_bus.alu_b_src = (byte)(this.rom.roms[7][this.u_ad_bus] & 0b00000111);//////////////////
                controller_bus.display_reg_load = (byte)((this.rom.roms[7][this.u_ad_bus] >> 3) & 0b00000001);////
                controller_bus.dl_wrt = (byte)((this.rom.roms[7][this.u_ad_bus] >> 4) & 0b00000001);//////////////
                controller_bus.dh_wrt = (byte)((this.rom.roms[7][this.u_ad_bus] >> 5) & 0b00000001);//////////////
                controller_bus.cl_wrt = (byte)((this.rom.roms[7][this.u_ad_bus] >> 6) & 0b00000001);//////////////
                controller_bus.ch_wrt = (byte)((this.rom.roms[7][this.u_ad_bus] >> 7) & 0b00000001);//////////////

                // ROM 8
                controller_bus.bl_wrt = (byte)((this.rom.roms[8][this.u_ad_bus] >> 0) & 0b00000001);//////////////
                controller_bus.bh_wrt = (byte)((this.rom.roms[8][this.u_ad_bus] >> 1) & 0b00000001);//////////////
                controller_bus.al_wrt = (byte)((this.rom.roms[8][this.u_ad_bus] >> 2) & 0b00000001);//////////////
                controller_bus.ah_wrt = (byte)((this.rom.roms[8][this.u_ad_bus] >> 3) & 0b00000001);//////////////
                controller_bus.mdr_in_src = (byte)((this.rom.roms[8][this.u_ad_bus] >> 4) & 0b00000001);//////////
                controller_bus.mdr_out_src = (byte)((this.rom.roms[8][this.u_ad_bus] >> 5) & 0b00000001);/////////
                controller_bus.mdr_out_en = (byte)((this.rom.roms[8][this.u_ad_bus] >> 6) & 0b00000001);//////////
                controller_bus.mdrl_wrt = (byte)((this.rom.roms[8][this.u_ad_bus] >> 7) & 0b00000001);////////////

                // ROM 9
                controller_bus.mdrh_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 0) & 0b00000001);////////////
                controller_bus.tdrl_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 1) & 0b00000001);////////////
                controller_bus.tdrh_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 2) & 0b00000001);////////////
                controller_bus.dil_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 3) & 0b00000001);/////////////
                controller_bus.dih_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 4) & 0b00000001);/////////////
                controller_bus.sil_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 5) & 0b00000001);/////////////
                controller_bus.sih_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 6) & 0b00000001);/////////////
                controller_bus.marl_wrt = (byte)((this.rom.roms[9][this.u_ad_bus] >> 7) & 0b00000001);////////////

                // ROM 10
                controller_bus.marh_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 0) & 0b00000001);///////////
                controller_bus.bpl_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 1) & 0b00000001);////////////
                controller_bus.bph_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 2) & 0b00000001);////////////
                controller_bus.pcl_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 3) & 0b00000001);////////////
                controller_bus.pch_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 4) & 0b00000001);////////////
                controller_bus.spl_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 5) & 0b00000001);////////////
                controller_bus.sph_wrt = (byte)((this.rom.roms[10][this.u_ad_bus] >> 6) & 0b00000001);////////////
                controller_bus.u_escape_1 = (byte)((this.rom.roms[10][this.u_ad_bus] >> 7) & 0b00000001);////////////

                // ROM 11
                controller_bus.u_esc_in_src = (byte)((this.rom.roms[11][this.u_ad_bus] >> 0) & 0b00000001);/////
                controller_bus.int_vector_wrt = (byte)((this.rom.roms[11][this.u_ad_bus] >> 1) & 0b00000001);/////
                controller_bus.mask_flags_wrt = (byte)((this.rom.roms[11][this.u_ad_bus] >> 2) & 0b00000001);/////
                controller_bus.mar_in_src = (byte)((this.rom.roms[11][this.u_ad_bus] >> 3) & 0b00000001);/////////
                controller_bus.int_ack = (byte)((this.rom.roms[11][this.u_ad_bus] >> 4) & 0b00000001);
                controller_bus.clear_all_ints = (byte)((this.rom.roms[11][this.u_ad_bus] >> 5) & 0b00000001);
                controller_bus.ptb_wrt = (byte)((this.rom.roms[11][this.u_ad_bus] >> 6) & 0b00000001);////////////
                controller_bus.pagtbl_ram_we = (byte)((this.rom.roms[11][this.u_ad_bus] >> 7) & 0b00000001);//////

                // ROM 12
                controller_bus.mdr_to_pagtbl_en = (byte)((this.rom.roms[12][this.u_ad_bus] >> 0) & 0b00000001);///
                controller_bus.force_user_ptb = (byte)((this.rom.roms[12][this.u_ad_bus] >> 1) & 0b00000001);
                // empty bit 3
                // empty bit 4
                // empty bit 5
                // empty bit 6
                controller_bus.gl_wrt = (byte)((this.rom.roms[12][this.u_ad_bus] >> 6) & 0b00000001);/////////////
                controller_bus.gh_wrt = (byte)((this.rom.roms[12][this.u_ad_bus] >> 7) & 0b00000001);/////////////

                // ROM 13
                controller_bus.imm = this.rom.roms[13][this.u_ad_bus]; ///////////////////////////////////

                // ROM 14
                // empty bit 0
                // empty bit 1
                // empty bit 2
                // empty bit 3
                // empty bit 4
                // empty bit 5
                // empty bit 6
                // empty bit 7


            }
            else
            {
                // ROM 0
                controller_bus.next = 0;

                // ROM 1
                controller_bus.u_offset = 0;
                controller_bus.cond_inv = 0;
                controller_bus.cond_flags_src = 0;
                controller_bus.cond_sel = 0;
                controller_bus.u_escape_0 = 0;

                // ROM 2
                controller_bus.uzf_in_src = 0;
                controller_bus.ucf_in_src = 0;
                controller_bus.usf_in_src = 0;
                controller_bus.uof_in_src = 0;
                controller_bus.ir_wrt = 0;
                controller_bus.status_wrt = 0;

                // ROM 3
                controller_bus.shift_src = 0;
                controller_bus.zbus_out_src = 0;
                controller_bus.alu_a_src = 0;

                // ROM 4
                controller_bus.alu_op = 0;
                controller_bus.alu_mode = 0;

                // ROM 5
                controller_bus.alu_cf_in_src = 0;
                controller_bus.alu_cf_in_inv = 0;
                controller_bus.zf_in_src = 0;
                controller_bus.alu_cf_out_inv = 0;
                controller_bus.cf_in_src = 0;

                // ROM 6
                controller_bus.sf_in_src = 0;
                controller_bus.of_in_src = 0;
                controller_bus.rd = 0;
                controller_bus.wr = 0;

                // ROM 7
                controller_bus.alu_b_src = 0;
                controller_bus.display_reg_load = 0;
                controller_bus.dl_wrt = 0;
                controller_bus.dh_wrt = 0;
                controller_bus.cl_wrt = 0;
                controller_bus.ch_wrt = 0;

                // ROM 8
                controller_bus.bl_wrt = 0;
                controller_bus.bh_wrt = 0;
                controller_bus.al_wrt = 0;
                controller_bus.ah_wrt = 0;
                controller_bus.mdr_in_src = 0;
                controller_bus.mdr_out_src = 0;
                controller_bus.mdr_out_en = 0;
                controller_bus.mdrl_wrt = 0;

                // ROM 9
                controller_bus.mdrh_wrt = 0;
                controller_bus.tdrl_wrt = 0;
                controller_bus.tdrh_wrt = 0;
                controller_bus.dil_wrt = 0;
                controller_bus.dih_wrt = 0;
                controller_bus.sil_wrt = 0;
                controller_bus.sih_wrt = 0;
                controller_bus.marl_wrt = 0;

                // ROM 10
                controller_bus.marh_wrt = 0;
                controller_bus.bpl_wrt = 0;
                controller_bus.bph_wrt = 0;
                controller_bus.pcl_wrt = 0;
                controller_bus.pch_wrt = 0;
                controller_bus.spl_wrt = 0;
                controller_bus.sph_wrt = 0;
                controller_bus.u_escape_1 = 0;

                // ROM 11
                controller_bus.u_esc_in_src = 0;
                controller_bus.int_vector_wrt = 0;
                controller_bus.mask_flags_wrt = 0;
                controller_bus.mar_in_src = 0;
                controller_bus.int_ack = 0;
                controller_bus.clear_all_ints = 0;
                controller_bus.ptb_wrt = 0;
                controller_bus.pagtbl_ram_we = 0;

                // ROM 12
                controller_bus.mdr_to_pagtbl_en = 0;
                controller_bus.force_user_ptb = 0;
                // empty bit 3
                // empty bit 4
                // empty bit 5
                // empty bit 6
                controller_bus.gl_wrt = 0;
                controller_bus.gh_wrt = 0;

                // ROM 13
                controller_bus.imm = 0;

                // ROM 14
                // empty bit 0
                // empty bit 1
                // empty bit 2
                // empty bit 3
                // empty bit 4
                // empty bit 5
                // empty bit 6
                // empty bit 7


            }
        }

    }
}

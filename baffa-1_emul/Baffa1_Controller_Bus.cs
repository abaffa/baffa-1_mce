using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Controller_Bus
    {

        //ROM 01
        public byte next {get;set;}     //TYP
        public byte u_offset {get;set;}

        //ROM 02
        public byte cond_inv {get;set;}
        public byte cond_flags_src {get;set;}
        public byte cond_sel {get;set;}
        public byte u_escape_0 {get;set;}

        //ROM 03
        public byte uzf_in_src {get;set;}
        public byte ucf_in_src {get;set;}
        public byte usf_in_src {get;set;}
        public byte uof_in_src {get;set;}
        public byte ir_wrt {get;set;}
        public byte status_wrt {get;set;} //mswl_wrt // status (flags de controle)

        //ROM 04
        public byte shift_src {get;set;}
        public byte zbus_out_src {get;set;}
        public byte alu_a_src {get;set;}

        //ROM 05
        public byte alu_op {get;set;}
        public byte alu_mode {get;set;}

        //ROM 06
        public byte alu_cf_in_src {get;set;}
        public byte alu_cf_in_inv {get;set;}
        public byte zf_in_src {get;set;} // ZeroFlag
        public byte alu_cf_out_inv {get;set;}
        public byte cf_in_src {get;set;} // Carry Flag

        //ROM 07
        public byte sf_in_src {get;set;} // Sign Flag
        public byte of_in_src {get;set;} // Overflow Flag
        public byte rd {get;set;}
        public byte wr {get;set;}

        //ROM 08
        public byte alu_b_src {get;set;}
        public byte display_reg_load {get;set;}
        public byte dl_wrt {get;set;}
        public byte dh_wrt {get;set;}
        public byte cl_wrt {get;set;}
        public byte ch_wrt {get;set;}

        //ROM 09
        public byte bl_wrt {get;set;}
        public byte bh_wrt {get;set;}
        public byte al_wrt {get;set;}
        public byte ah_wrt {get;set;}
        public byte mdr_in_src {get;set;}
        public byte mdr_out_src {get;set;}
        public byte mdr_out_en {get;set;}
        public byte mdrl_wrt {get;set;}

        //ROM 10
        public byte mdrh_wrt {get;set;}
        public byte tdrl_wrt {get;set;}
        public byte tdrh_wrt {get;set;}
        public byte dil_wrt {get;set;}
        public byte dih_wrt {get;set;}
        public byte sil_wrt {get;set;}
        public byte sih_wrt {get;set;}
        public byte marl_wrt {get;set;}

        //ROM 11
        public byte marh_wrt {get;set;}
        public byte bpl_wrt {get;set;}
        public byte bph_wrt {get;set;}
        public byte pcl_wrt {get;set;}
        public byte pch_wrt {get;set;}
        public byte spl_wrt {get;set;}
        public byte sph_wrt {get;set;}
        public byte u_escape_1 {get;set;}

        //ROM 12
        public byte u_esc_in_src {get;set;}
        public byte int_vector_wrt {get;set;}
        public byte mask_flags_wrt {get;set;}
        public byte mar_in_src {get;set;}
        public byte int_ack {get;set;}
        public byte clear_all_ints {get;set;}
        public byte ptb_wrt {get;set;}
        public byte pagtbl_ram_we {get;set;}

        //ROM 13
        public byte mdr_to_pagtbl_en {get;set;}
        public byte force_user_ptb {get;set;}
        //public byte IC6_3 {get;set;}
        //public byte IC6_4 {get;set;}
        //public byte IC6_5 {get;set;}
        //public byte IC6_6 {get;set;}
        public byte gl_wrt {get;set;}
        public byte gh_wrt {get;set;}

        //ROM 14
        public byte imm {get;set;}

        //ROM 15
        //public byte IC8_1 {get;set;}
        //public byte IC8_2 {get;set;}
        //public byte IC8_3 {get;set;}
        //public byte IC8_4 {get;set;}
        //public byte IC8_5 {get;set;}
        //public byte IC8_6 {get;set;}
        //public byte IC8_7 {get;set;}
        //public byte IC8_8 {get;set;}

        ///////////////////////////////////////////////////////////

        public byte memory_io {get;set;}    // bus_mem_io
        public byte page_present {get;set;} 
        public byte page_writable {get;set;}

        //

        public byte int_vector {get;set;}

        public byte int_status {get;set;}


        public byte int_request {get;set;}
        public byte int_req {get;set;}
        //

        public byte dma_req {get;set;}
        public byte wait {get;set;} 
        public byte ext_input {get;set;} 

        public byte final_condition {get;set;}

        public byte panel_regsel {get;set;}
        public byte panel_rd {get;set;}
        public byte panel_wr {get;set;}
        public byte panel_mem_io {get;set;}

        public ulong panel_address {get;set;}
        public byte panel_data {get;set;}
        public byte panel_req {get;set;}

        public byte panel_run = 0;
        public byte panel_step = 0;
        public byte panel_microcodestep = 0;

        public byte clk = 0;


        public byte reset = 0;
        public byte restart = 0;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Registers
    {

        // FLAG - msw-h - bits
        public const byte MSWh_ZF = 0x00; // ZeroFlag
        public const byte MSWh_CF = 0x01; // Carry Flag
        public const byte MSWh_SF = 0x02; // Overflow Flag
        public const byte MSWh_OF = 0x03; // Sign Flag
        public const byte MSWh_12 = 0x04;
        public const byte MSWh_13 = 0x05;
        public const byte MSWh_14 = 0x06;
        public const byte MSWh_15 = 0x07;

        // STATUS FLAGS - msw-l bits
        public const byte MSWl_DMA_ACK = 0x00;
        public const byte MSWl_INTERRUPT_ENABLE = 0x01;
        public const byte MSWl_CPU_MODE = 0x02;
        public const byte MSWl_PAGING_EN = 0x03;
        public const byte MSWl_HALT = 0x04;
        public const byte MSWl_DISPLAY_REG_LOAD = 0x05;
        public const byte MSWl_14 = 0x06;
        public const byte MSWl_DIR = 0x07;


        //General Purpose Registers
        //DATA REGISTERS
        public Baffa1_Register_8Bit Ah = new Baffa1_Register_8Bit(); // AX (16bit) Accumulator	(Ah/Al)
        public Baffa1_Register_8Bit Al = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit Bh = new Baffa1_Register_8Bit(); // BX (16bit) Base		(Bh/Bl)
        public Baffa1_Register_8Bit Bl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit Ch = new Baffa1_Register_8Bit(); // CX (16bit) Counter		(Ch/Cl)
        public Baffa1_Register_8Bit Cl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit Dh = new Baffa1_Register_8Bit(); // DX (16bit) Data		(Dh/Dl)
        public Baffa1_Register_8Bit Dl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit Gh = new Baffa1_Register_8Bit(); // GX (16bit)	Gh/Gl	General Register(For scratch)
        public Baffa1_Register_8Bit Gl = new Baffa1_Register_8Bit();

        //Pointer Registers
        public Baffa1_Register_8Bit BPh = new Baffa1_Register_8Bit(); // BP (16bit) Base Pointer  (Used to manage stack frames)
        public Baffa1_Register_8Bit BPl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit SPh = new Baffa1_Register_8Bit(); // SP (16bit) Stack Pointer
        public Baffa1_Register_8Bit SPl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit SSPh = new Baffa1_Register_8Bit(); // SSP (16bit) Supervisor Stack Pointer
        public Baffa1_Register_8Bit SSPl = new Baffa1_Register_8Bit();

        //Index Registers
        public Baffa1_Register_8Bit SIh = new Baffa1_Register_8Bit(); // SI (16bit) Source index (Source address for string operations)
        public Baffa1_Register_8Bit SIl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit DIh = new Baffa1_Register_8Bit(); // DI (16bit) Destination Index (Destination address for string operations)
        public Baffa1_Register_8Bit DIl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit PCh = new Baffa1_Register_8Bit(); // PC (16bit) Program Counter
        public Baffa1_Register_8Bit PCl = new Baffa1_Register_8Bit();

        public Baffa1_Register_8Bit TDRh = new Baffa1_Register_8Bit(); // TDR (16bit) Temporary Data Register
        public Baffa1_Register_8Bit TDRl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit PTB = new Baffa1_Register_8Bit();  // PTB (8bit) = Page table base



        public Baffa1_Register_8Bit MSWh = new Baffa1_Register_8Bit(); // MSW (16bit) FLAGS
        public Baffa1_Register_8Bit MSWl = new Baffa1_Register_8Bit(); // STATUS - flags de controle


        public Baffa1_Register_8Bit INT_MASKS = new Baffa1_Register_8Bit(); // INT FLAGS



        public Baffa1_Register_8Bit MARh = new Baffa1_Register_8Bit(); // memory address register
        public Baffa1_Register_8Bit MARl = new Baffa1_Register_8Bit();
        public Baffa1_Register_8Bit MDRh = new Baffa1_Register_8Bit(); // memory data register
        public Baffa1_Register_8Bit MDRl = new Baffa1_Register_8Bit();

        //Control Registers (FLAGS)
        //ZF // Zero Flag
        //CF // Carry Flag
        //SF // Sign Flag
        //OF // Overflow Flag

        //////////////////////////////////////////////////
        //unsigned char S;       /*flag S     Result negative         */
        //unsigned char Z;       /*flag Z     Result is zero          */
        //unsigned char H;       /*flag H     Halfcarry/Halfborrow    */
        //unsigned char P;       /*flag P     Result is even          */
        //unsigned char V;       /*flag V     Overflow occured        */
        //unsigned char N;       /*flag N     Subtraction occured     */
        //unsigned char C;       /*flag C     Carry/Borrow occured    */





        public static ushort Value(Baffa1_Register_8Bit l, Baffa1_Register_8Bit h)
        {

            return (ushort)(l.Value() | (((ushort)h.Value()) << 8));
        }

        public static void Set(Baffa1_Register_8Bit l, Baffa1_Register_8Bit h, ushort v)
        {
            l.Set((byte)(v & 0b11111111));
            h.Set((byte)((v >> 8) & 0b11111111));
        }


        public static void Reset(Baffa1_Register_8Bit l, Baffa1_Register_8Bit h)
        {
            l.Reset();
            h.Reset();
        }


        public void mswh_flags_desc(HW_TTY hw_tty)
        {

            byte b = this.MSWh.Value();
            hw_tty.Print(" [");
            if (Utils.GetByteBit(b, MSWh_ZF) != 0x00)
                hw_tty.Print("Z");
            else hw_tty.Print(" ");
            if (Utils.GetByteBit(b, MSWh_CF) != 0x00)
                hw_tty.Print("C");
            else hw_tty.Print(" ");
            if (Utils.GetByteBit(b, MSWh_SF) != 0x00)
                hw_tty.Print("S");
            else hw_tty.Print(" ");
            if (Utils.GetByteBit(b, MSWh_OF) != 0x00)
                hw_tty.Print("O");
            else hw_tty.Print(" ");
            hw_tty.Print("]");
        }

        public void mswl_status_desc(HW_TTY hw_tty)
        {

            byte b = this.MSWl.Value();
            if (Utils.GetByteBit(b, MSWl_DMA_ACK) != 0x00)
                hw_tty.Print(" | dma_ack ");
            if (Utils.GetByteBit(b, MSWl_INTERRUPT_ENABLE) != 0x00)
                hw_tty.Print(" | interrupt_enable ");
            if (Utils.GetByteBit(b, MSWl_CPU_MODE) != 0x00)
                hw_tty.Print(" | cpu_mode ");
            if (Utils.GetByteBit(b, MSWl_PAGING_EN) != 0x00)
                hw_tty.Print(" | paging_en ");
            if (Utils.GetByteBit(b, MSWl_HALT) != 0x00)
                hw_tty.Print(" | halt ");
            if (Utils.GetByteBit(b, MSWl_DISPLAY_REG_LOAD) != 0x00)
                hw_tty.Print(" | display_reg_load ");
            if (Utils.GetByteBit(b, MSWl_DIR) != 0x00)
                hw_tty.Print(" | dir ");
        }


        private byte refresh_MSWh_ZF(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus)
        {

            byte inMSWh_ZF = 0x00;

            switch (controller_bus.zf_in_src & 0b00000011)
            {
                case 0x00:
                    inMSWh_ZF = Utils.GetByteBit(this.MSWh.Value(), MSWh_ZF);
                    break;

                case 0x01:
                    inMSWh_ZF = Utils.GetByteBit(alu_bus.alu_zf, 0);
                    break;
                case 0x02:
                    inMSWh_ZF = Utils.GetByteBit((byte)(alu_bus.alu_zf & Utils.GetByteBit(this.MSWh.Value(), MSWh_ZF)), 0);
                    break;

                case 0x03:
                    inMSWh_ZF = Utils.GetByteBit(alu_bus.z_bus, 0);
                    break;
            }

            return inMSWh_ZF;
        }

        private byte refresh_MSWh_CF(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus)
        {

            byte inMSWh_CF = 0x00;

            switch (controller_bus.cf_in_src & 0b00000111)
            {
                case 0x00:
                    inMSWh_CF = Utils.GetByteBit(this.MSWh.Value(), MSWh_CF);
                    break;

                case 0x01:
                    inMSWh_CF = Utils.GetByteBit(alu_bus.alu_final_cf, 0);
                    break;

                case 0x02:
                    inMSWh_CF = Utils.GetByteBit(alu_bus.alu_output, 0);
                    break;

                case 0x03:
                    inMSWh_CF = Utils.GetByteBit(alu_bus.z_bus, 1);
                    break;

                case 0x04:
                    inMSWh_CF = Utils.GetByteBit(alu_bus.alu_output, 7);
                    break;
            }

            return inMSWh_CF;
        }
        private byte refresh_MSWh_SF(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus)
        {

            byte inMSWh_SF = 0x00;

            switch (controller_bus.sf_in_src & 0b00000011)
            {
                case 0x00:

                    inMSWh_SF = Utils.GetByteBit(this.MSWh.Value(), MSWh_SF);
                    break;

                case 0x01:
                    inMSWh_SF = Utils.GetByteBit(alu_bus.z_bus, 7);
                    break;

                case 0x02:
                    inMSWh_SF = 0x00;
                    break;

                case 0x03:
                    inMSWh_SF = Utils.GetByteBit(alu_bus.z_bus, 2);
                    break;
            }

            return inMSWh_SF;
        }
        private byte refresh_MSWh_OF(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus, byte u_sf)
        {


            byte inMSWh_OF = 0x00;

            switch (controller_bus.of_in_src & 0b00000111)
            {
                case 0x00:
                    inMSWh_OF = Utils.GetByteBit(this.MSWh.Value(), MSWh_OF);
                    break;

                case 0x01:
                    inMSWh_OF = Utils.GetByteBit(alu_bus.alu_of, 0);
                    break;

                case 0x02:
                    inMSWh_OF = Utils.GetByteBit(alu_bus.z_bus, 7);
                    break;

                case 0x03:
                    inMSWh_OF = Utils.GetByteBit(alu_bus.z_bus, 3);
                    break;

                case 0x04:
                    inMSWh_OF = Utils.GetByteBit(u_sf, 0) != Utils.GetByteBit(alu_bus.z_bus, 7) ? (byte)0x1 : (byte)0x0;
                    break;
            }

            return inMSWh_OF;
        }

        public void Refresh(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus, byte data_bus, byte u_sf, TextWriter fa)
        {
            //#######################
            //IC86B //IC58B //IC86C //IC241 //IC14 //IC255 //IC23

            //
            byte inMSWh_ZF = refresh_MSWh_ZF(controller_bus, alu_bus);
            byte inMSWh_CF = refresh_MSWh_CF(controller_bus, alu_bus);
            byte inMSWh_SF = refresh_MSWh_SF(controller_bus, alu_bus);
            byte inMSWh_OF = refresh_MSWh_OF(controller_bus, alu_bus, u_sf);

            //

            if (true) //0x01
            { // ~RST
              //IC206
                byte inMSW_H = (byte)(Utils.SetByteBit(inMSWh_ZF, 0) | Utils.SetByteBit(inMSWh_CF, 1) | Utils.SetByteBit(inMSWh_SF, 2) | Utils.SetByteBit(inMSWh_OF, 3));

                this.MSWh.Set(inMSW_H);

            }

            if (controller_bus.status_wrt == 0x00)
            {
                //byte oldStatus = this.MSWl.Value();
                this.MSWl.Set(alu_bus.z_bus);
            }



            ///////////////////////////////////////////////////////////////////////////
            // READ DATA
            //DATA REGISTERS
            if (controller_bus.ah_wrt == 0x00) { this.Ah.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Ah", alu_bus.z_bus); } }
            if (controller_bus.al_wrt == 0x00) { this.Al.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Al", alu_bus.z_bus); } }

            if (controller_bus.bh_wrt == 0x00) { this.Bh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Bh", alu_bus.z_bus); } }
            if (controller_bus.bl_wrt == 0x00) { this.Bl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Bl", alu_bus.z_bus); } }

            if (controller_bus.ch_wrt == 0x00) { this.Ch.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Ch", alu_bus.z_bus); } }
            if (controller_bus.cl_wrt == 0x00) { this.Cl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Cl", alu_bus.z_bus); } }

            if (controller_bus.dh_wrt == 0x00) { this.Dh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Dh", alu_bus.z_bus); } }
            if (controller_bus.dl_wrt == 0x00) { this.Dl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Dl", alu_bus.z_bus); } }

            if (controller_bus.gh_wrt == 0x00) { this.Gh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Gh", alu_bus.z_bus); } }
            if (controller_bus.gl_wrt == 0x00) { this.Gl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "Gl", alu_bus.z_bus); } }

            //Pointer Registers
            if (controller_bus.bph_wrt == 0x00) { this.BPh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "BPh", alu_bus.z_bus); } }
            if (controller_bus.bpl_wrt == 0x00) { this.BPl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "BPl", alu_bus.z_bus); } }



            if (!Utils.CheckByteBit(this.MSWl.Value(), MSWl_CPU_MODE))
            {

                if (controller_bus.sph_wrt == 0x00)
                {
                    this.SSPh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "SSPh", alu_bus.z_bus); }
                }
                if (controller_bus.spl_wrt == 0x00)
                {
                    this.SSPl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "SSPl", alu_bus.z_bus); }
                }
            }
            if (controller_bus.sph_wrt == 0x00) { this.SPh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "SPh", alu_bus.z_bus); } }
            if (controller_bus.spl_wrt == 0x00) { this.SPl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "SPl", alu_bus.z_bus); } }


            //Index Registers
            if (controller_bus.sih_wrt == 0x00) { this.SIh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "SIh", alu_bus.z_bus); } }
            if (controller_bus.sil_wrt == 0x00) { this.SIl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "SIl", alu_bus.z_bus); } }

            if (controller_bus.dih_wrt == 0x00) { this.DIh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "DIh", alu_bus.z_bus); } }
            if (controller_bus.dil_wrt == 0x00) { this.DIl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "DIl", alu_bus.z_bus); } }

            if (controller_bus.pch_wrt == 0x00) { this.PCh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "PCh", alu_bus.z_bus); } }
            if (controller_bus.pcl_wrt == 0x00) { this.PCl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "PCl", alu_bus.z_bus); } }

            if (controller_bus.tdrh_wrt == 0x00) { this.TDRh.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "TDRh", alu_bus.z_bus); } }
            if (controller_bus.tdrl_wrt == 0x00) { this.TDRl.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "TDRl", alu_bus.z_bus); } }

            if (controller_bus.ptb_wrt == 0x00) { this.PTB.Set(alu_bus.z_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "PTB", alu_bus.z_bus); } }

            /////////////////////////////////////////////////////////////////////////////
            if (controller_bus.mask_flags_wrt == 0x00) this.INT_MASKS.Set(alu_bus.z_bus);
            /////////////////////////////////////////////////////////////////////////////
            // SET MDR
            //IC7 //IC24 //IC19 //IC183

            if (controller_bus.mdrl_wrt == 0x00) { this.MDRl.Set(controller_bus.mdr_in_src == 0x00 ? alu_bus.z_bus : data_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "MDRl", (controller_bus.mdr_in_src == 0x00 ? alu_bus.z_bus : data_bus)); } }
            if (controller_bus.mdrh_wrt == 0x00) { this.MDRh.Set(controller_bus.mdr_in_src == 0x00 ? alu_bus.z_bus : data_bus); if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "MDRh", (controller_bus.mdr_in_src == 0x00 ? alu_bus.z_bus : data_bus)); } }

            ////////////////////////////////////////////////////////////////////////////
            //MEMORY SET MAR

            if (controller_bus.marl_wrt == 0x00)
            {
                //IC131 //IC128			
                if (controller_bus.mar_in_src == 0x00)
                {
                    this.MARl.Set(alu_bus.z_bus);
                    if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "MARl", alu_bus.z_bus); }
                }
                else
                {
                    this.MARl.Set(this.PCl.Value());
                    if (Baffa1_Config.DEBUG_TRACE_WRREG && fa != null) { Utils.Reg8BitPrint(fa, "WRITE", "MARl", this.PCl.Value()); }
                }
            }

            if (controller_bus.marh_wrt == 0x00)
            {
                //IC129 //IC132			
                if (controller_bus.mar_in_src == 0x00)
                {
                    this.MARh.Set(alu_bus.z_bus);
                    if (Baffa1_Config.DEBUG_TRACE_WRREG) { Utils.Reg8BitPrint(fa, "WRITE", "MARh", alu_bus.z_bus); }
                }
                else
                {
                    this.MARh.Set(this.PCh.Value());
                    if (Baffa1_Config.DEBUG_TRACE_WRREG) { Utils.Reg8BitPrint(fa, "WRITE", "MARh", this.PCh.Value()); }
                }
            }
        }

        public void Refresh_reg_flags(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus, byte u_sf)
        {
            this.refresh_MSWh_ZF(controller_bus, alu_bus);
            this.refresh_MSWh_CF(controller_bus, alu_bus);
            this.refresh_MSWh_SF(controller_bus, alu_bus);
            this.refresh_MSWh_OF(controller_bus, alu_bus, u_sf);
        }

    }
}

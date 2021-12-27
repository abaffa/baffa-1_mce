using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Alu
    {

        public byte _A { get; set; }
        public byte _B { get; set; }
        public byte _C { get; set; }

        public byte A { get; set; }
        public byte B { get; set; }

        public byte C { get; set; }

        public byte CIN { get; set; } //carry in

        public byte COUT { get; set; } //carry out

        public byte EQ { get; set; } //EQ
        public byte F { get; set; } //Larger, equal, zero, carry out

        public byte U_zf { get; set; }
        public byte U_cf { get; set; }
        public byte U_sf { get; set; }
        public byte U_of { get; set; }
        public byte U_esc { get; set; }
        public Baffa1_Register_8Bit U_FLAGS = new Baffa1_Register_8Bit();


        public void Init()
        {


            this.Reset();

            this.U_zf = 0x00;
            this.U_cf = 0x00;
            this.U_sf = 0x00;
            this.U_of = 0x00;
            this.U_esc = 0x00;
            this.U_FLAGS.Reset();
        }


        private void Reset()
        {

            this._A = 0x00;
            this._B = 0x00;
            this._C = 0x00;

            this.A = 0x00;
            this.B = 0x00;

            this.C = 0x00;

            this.CIN = 0x00; //carry in

            this.COUT = 0x00; //carry in

            this.EQ = 0x00; //carry in
            this.F = 0x00; //Larger, equal, zero, carry out

        }

        public void Display_registers(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus, HW_TTY hw_tty)
        {

            hw_tty.Print(String.Format("* A:{0}  | ", Utils.print_byte_bin(this._A)));
            hw_tty.Print(String.Format("B:{0}    | ", Utils.print_byte_bin(this._B)));
            hw_tty.Print(String.Format("C:{0}  | ", Utils.print_byte_bin(this._C)));
            hw_tty.Print(String.Format("Cin:{0} | ", Utils.print_nibble_bin(this.CIN)));
            hw_tty.Print(String.Format("Cout:{0}", Utils.print_nibble_bin(this.COUT)));
            hw_tty.Print("\n");

            hw_tty.Print(String.Format("* alu_op: {0}", Utils.print_nibble_bin(controller_bus.alu_op)));
            hw_tty.Print(String.Format(" | alu_mode: {0}", Utils.print_nibble_bin(controller_bus.alu_mode)));
            hw_tty.Print(String.Format(" | alu_output={0}", Utils.print_byte_bin(alu_bus.alu_output))); hw_tty.Print("\n");

            //hw_tty.Print("* EQ="); Utils.print_nibble_bin(this.EQ); hw_tty.Print(str_out);  hw_tty.Print(" | ");	
            //hw_tty.Print("F="); Utils.print_byte_bin(this.F); hw_tty.Print(str_out); hw_tty.Print("\n");

            hw_tty.Print("* Flags: [");

            if (alu_bus.alu_zf != 0x00) hw_tty.Print("Z"); else hw_tty.Print(" ");
            if (alu_bus.alu_cf != 0x00) hw_tty.Print("C"); else hw_tty.Print(" ");
            if (alu_bus.alu_of != 0x00) hw_tty.Print("O"); else hw_tty.Print(" ");

            hw_tty.Print("] ");
            hw_tty.Print(String.Format(" | alu_a_src={0}", controller_bus.alu_a_src.ToString("X2")));
            hw_tty.Print(String.Format(" | alu_b_src={0}", controller_bus.alu_b_src.ToString("X2")));
            hw_tty.Print(String.Format(" | alu_cf_in_src={0}", controller_bus.alu_cf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | alu_cf_in_inv={0}", controller_bus.alu_cf_in_inv != 0x00 ? "01" : "0"));
            hw_tty.Print(String.Format(" | alu_cf_out_inv={0}", controller_bus.alu_cf_out_inv.ToString("X2")));
            hw_tty.Print(String.Format(" | alu_final_cf={0}", alu_bus.alu_final_cf.ToString("X2")));
            hw_tty.Print("\n");
        }

        public byte ALU_EXEC(Baffa1_Controller_Bus controller_bus, Baffa1_Alu_Bus alu_bus,
        byte u_cf, byte msw_cf, HW_TTY hw_tty)
        {

            byte alu_cin = 0x00;

            if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x00)
                alu_cin = 1;
            else
                if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x01)
                alu_cin = msw_cf;

            else if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x02)
                alu_cin = u_cf;


            else if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x03)
                alu_cin = 0;


            alu_cin = (byte)((alu_cin ^ controller_bus.alu_cf_in_inv) & 0b00000001);


            Baffa1_Alu_4Bit alu41 = new Baffa1_Alu_4Bit();
            Baffa1_Alu_4Bit alu42 = new Baffa1_Alu_4Bit();


            alu41.Op((byte)(alu_bus.x_bus & 0b00001111), (byte)(alu_bus.y_bus & 0b00001111), (byte)((alu_cin) & 0b00000001), controller_bus.alu_op, controller_bus.alu_mode);

            alu42.Op((byte)((alu_bus.x_bus & 0b11110000) >> 4), (byte)((alu_bus.y_bus & 0b11110000) >> 4), (byte)((alu41.COUT) & 0b00000001), controller_bus.alu_op, controller_bus.alu_mode);

            this.A = alu_bus.x_bus; this._A = this.A;
            this.B = alu_bus.y_bus; this._B = this.B;
            this.C = (byte)((alu41.alu_output & 0b00001111) | ((alu42.alu_output & 0b00001111) << 4)); this._C = this.C;
            this.CIN = (byte)(alu_cin & 0b00000001);
            alu_bus.alu_output = this.C;
            this.COUT = alu42.COUT;

            alu_bus.alu_cf = this.COUT;
            alu_bus.alu_final_cf = (byte)((alu_bus.alu_cf ^ (controller_bus.alu_cf_out_inv)) & 0b00000001);


            /////////////////////////////////////////////////////////////////
            // SHIFT
            alu_bus.z_bus = 0x00;


            byte inIC16 = 0x00;

            if (controller_bus.shift_src == 0x00)
                inIC16 = 0x00;

            else if (controller_bus.shift_src == 0x01)
                inIC16 = u_cf;

            else if (controller_bus.shift_src == 0x02)
                inIC16 = msw_cf;

            else if (controller_bus.shift_src == 0x03)
                inIC16 = Utils.GetByteBit(alu_bus.alu_output, 0);

            else if (controller_bus.shift_src == 0x04)
                inIC16 = Utils.GetByteBit(alu_bus.alu_output, 7);

            else if (controller_bus.shift_src == 0x05 ||
                controller_bus.shift_src == 0x06 ||
                controller_bus.shift_src == 0x07)
                inIC16 = 0x01;


            if ((controller_bus.zbus_out_src & 0b00000011) == 0x00)
                alu_bus.z_bus = alu_bus.alu_output;

            else if ((controller_bus.zbus_out_src & 0b00000011) == 0x01)
                alu_bus.z_bus = (byte)((alu_bus.alu_output >> 1) | (inIC16 << 7));

            else if ((controller_bus.zbus_out_src & 0b00000011) == 0x02)
                alu_bus.z_bus = (byte)((alu_bus.alu_output << 1) | inIC16);

            else if ((controller_bus.zbus_out_src & 0b00000011) == 0x03)
                alu_bus.z_bus = Utils.GetByteBit(alu_bus.alu_output, 7) != 0x00 ? (byte)0b11111111 : (byte)0b00000000;

            /*
            SHL: MSB goes into CF.LSB replaced by 0
            SLA : same as SHL
            ROL : LSB becomes MSB.
            RLC : MSB goes into CF.LSB becomes CF

            SHR : LSB goes into CF.MSB replaced by 0
            SRA : LSB goes into CF.MSB is whatever the previous one was
            ROR : MSB becomes LSB
            RRC : MSB becomes CF.LSB goes into CF*/
            /////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////
            // test OF (OVERFLOW)

            bool zNEQx = Utils.GetByteBit(alu_bus.z_bus, 7) != Utils.GetByteBit(alu_bus.x_bus, 7);
            bool xNEQy = Utils.GetByteBit(alu_bus.x_bus, 7) != Utils.GetByteBit(alu_bus.y_bus, 7);

            alu_bus.alu_of = zNEQx && ((controller_bus.alu_op != 0b1001) == xNEQy) ? (byte)1 : (byte)0;
            //
            /////////////////////////////////////////////////////////////////
            //
            alu_bus.alu_zf = (alu_bus.z_bus == 0x00 ? (byte)1 : (byte)0);

            if (Baffa1_Config.DEBUG_ALU)
            {

                hw_tty.Print("***** ALU\n");
                this.Display_registers(controller_bus, alu_bus, hw_tty);

                if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x00)
                    hw_tty.Print("* alu_cin = 0\n");

                else if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x01)
                {
                    hw_tty.Print(String.Format("* alu_cin = msw_cf:{0}\n", Utils.print_byte_bin(msw_cf)));
                }
                else if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x02)
                {
                    hw_tty.Print(String.Format("* alu_cin = u_cf:{0}\n", Utils.print_byte_bin(u_cf)));
                }
                else if ((controller_bus.alu_cf_in_src & 0b00000011) == 0x03)
                    hw_tty.Print("* alu_cin = 1\n");

                hw_tty.Print(String.Format("* z_bus={0}\n", Utils.print_byte_bin(alu_bus.z_bus)));
                hw_tty.Print("\n");
                hw_tty.Print("\n");
            }


            return alu_bus.z_bus;
        }

        public void u_flags_refresh(Baffa1_Controller_Bus controller_bus, byte reg_status_value, byte reg_flags_value, Baffa1_Alu_Bus alu_bus, HW_TTY hw_tty)
        {

            byte inZF = 0x00;
            byte inCF = 0x00;
            byte inSF = 0x00;
            byte inOF = 0x00;

            switch (controller_bus.uzf_in_src)
            {
                case 0x00:
                    inZF = Utils.GetByteBit(this.U_zf, 0);
                    break;
                case 0x01:
                    inZF = Utils.GetByteBit(alu_bus.alu_zf, 0);
                    break;
                case 0x02:
                    inZF = (byte)(Utils.GetByteBit(this.U_zf, 0) & Utils.GetByteBit(alu_bus.alu_zf, 0));
                    break;
            }

            switch (controller_bus.ucf_in_src)
            {
                case 0x00:
                    inCF = Utils.GetByteBit(this.U_cf, 0);
                    break;
                case 0x01:
                    inCF = Utils.GetByteBit(alu_bus.alu_final_cf, 0);
                    break;
                case 0x02:
                    inCF = Utils.GetByteBit(alu_bus.alu_output, 0);
                    break;
                case 0x03:
                    inCF = Utils.GetByteBit(alu_bus.alu_output, 7);
                    break;
            }

            if (controller_bus.usf_in_src == 0x00)
                inSF = Utils.GetByteBit(this.U_sf, 0);
            else
                inSF = Utils.GetByteBit(alu_bus.z_bus, 7);

            if (controller_bus.uof_in_src == 0x00)
                inOF = Utils.GetByteBit(this.U_of, 0);
            else
                inOF = Utils.GetByteBit(alu_bus.alu_of, 0);


            this.U_zf = inZF;
            this.U_cf = inCF;
            this.U_sf = inSF;
            this.U_of = inOF;

            if (controller_bus.u_esc_in_src != 0x00)
                this.U_esc = (byte)(controller_bus.imm & 0b00000011);

            if (Baffa1_Config.DEBUG_UFLAGS)
            {
                hw_tty.Print("***** U_FLAGS\n");
                display_u_flags(controller_bus, hw_tty);
            }

            this.update_final_condition(controller_bus, reg_status_value, reg_flags_value);
        }


        private void display_u_flags(Baffa1_Controller_Bus controller_bus, HW_TTY hw_tty)
        {


            hw_tty.Print(String.Format("* FLAGS: {0}", Utils.print_byte_bin(this.U_FLAGS.Value())));
            hw_tty.Print(" [");

            if (this.U_zf != 0x00) hw_tty.Print("Z"); else hw_tty.Print(" ");
            if (this.U_cf != 0x00) hw_tty.Print("C"); else hw_tty.Print(" ");
            if (this.U_sf != 0x00) hw_tty.Print("S"); else hw_tty.Print(" ");
            if (this.U_of != 0x00) hw_tty.Print("O"); else hw_tty.Print(" ");
            hw_tty.Print("]");

            hw_tty.Print(String.Format(" | u_zf_in_src:{0}", controller_bus.uzf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | u_cf_in_src:{0}", controller_bus.ucf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | u_sf_in_src:{0}", controller_bus.usf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | u_of_in_src:{0}", controller_bus.uof_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" |  u_esc_in_src:{0}", controller_bus.u_esc_in_src.ToString("X2")));
            hw_tty.Print("\n\n");
        }

        private void display_u_flags_lite(Baffa1_Controller_Bus controller_bus, HW_TTY hw_tty)
        {
            hw_tty.Print("* U_FLAGS: ");
            hw_tty.Print(" [");

            if (this.U_zf != 0x00) hw_tty.Print("Z"); else hw_tty.Print(" ");
            if (this.U_cf != 0x00) hw_tty.Print("C"); else hw_tty.Print(" ");
            if (this.U_sf != 0x00) hw_tty.Print("S"); else hw_tty.Print(" ");
            if (this.U_of != 0x00) hw_tty.Print("O"); else hw_tty.Print(" ");
            hw_tty.Print("]");

            hw_tty.Print(String.Format(" | u_zf_in_src:{0}", controller_bus.uzf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | u_cf_in_src:{0}", controller_bus.ucf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | u_sf_in_src:{0}", controller_bus.usf_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" | u_of_in_src:{0}", controller_bus.uof_in_src.ToString("X2")));
            hw_tty.Print(String.Format(" |  u_esc_in_src:{0}", controller_bus.u_esc_in_src.ToString("X2")));

            hw_tty.Print("\n");
        }

        private void update_final_condition(Baffa1_Controller_Bus controller_bus, byte reg_status_value, byte reg_flags_value)
        {
            if (!Utils.CheckByteBit(controller_bus.cond_sel, 3))
            {
                byte inZF = 0x00;
                byte inCF = 0x00;
                byte inSF = 0x00;
                byte inOF = 0x00;

                if (controller_bus.cond_flags_src == 0x00)
                {
                    inZF = Utils.GetByteBit(reg_flags_value, Baffa1_Registers.MSWh_ZF);
                    inCF = Utils.GetByteBit(reg_flags_value, Baffa1_Registers.MSWh_CF);
                    inSF = Utils.GetByteBit(reg_flags_value, Baffa1_Registers.MSWh_SF);
                    inOF = Utils.GetByteBit(reg_flags_value, Baffa1_Registers.MSWh_OF);
                }
                else
                {
                    inZF = Utils.GetByteBit(this.U_zf, 0);
                    inCF = Utils.GetByteBit(this.U_cf, 0);
                    inSF = Utils.GetByteBit(this.U_sf, 0);
                    inOF = Utils.GetByteBit(this.U_of, 0);
                }

                byte SFneqOF = (byte)(inSF ^ inOF); //XOR
                byte ZForCF = (byte)(inZF | inCF);
                byte ZForSFneqOF = (byte)(inZF | SFneqOF);

                switch (controller_bus.cond_sel & 0b00000111)
                {
                    case 0x00:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(inZF, 0));
                        break;

                    case 0x01:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(inCF, 0));
                        break;

                    case 0x02:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(inSF, 0));
                        break;

                    case 0x03:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(inOF, 0));
                        break;

                    case 0x04:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(SFneqOF, 0));
                        break;

                    case 0x05:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(ZForSFneqOF, 0));
                        break;

                    case 0x06:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(ZForCF, 0));
                        break;

                    case 0x07:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(controller_bus.dma_req, 0));
                        break;
                }
            }
            else
            {
                switch (controller_bus.cond_sel & 0b00000111)
                {
                    case 0x00:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(reg_status_value, Baffa1_Registers.MSWl_CPU_MODE));
                        break;

                    case 0x01:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(controller_bus.wait, 0));
                        break;

                    case 0x02:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ int_pending(controller_bus, reg_status_value));
                        break;

                    case 0x03:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(controller_bus.ext_input, 0));
                        break;

                    case 0x04:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(reg_status_value, Baffa1_Registers.MSWl_DIR));
                        break;

                    case 0x05:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ Utils.GetByteBit(reg_status_value, Baffa1_Registers.MSWl_DISPLAY_REG_LOAD));
                        break;

                    case 0x06:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ 0);
                        break;

                    case 0x07:
                        controller_bus.final_condition = (byte)(Utils.GetByteBit(controller_bus.cond_inv, 0) ^ 0);
                        break;

                }

            }
        }


        private byte int_pending(Baffa1_Controller_Bus controller_bus, byte reg_status_value)
        {
            return (byte)(Utils.GetByteBit(controller_bus.int_request, 0) & Utils.GetByteBit(reg_status_value, Baffa1_Registers.MSWl_INTERRUPT_ENABLE));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Rom
    {

        string[] BAFFA1_ROM_CONTROL_LIST =  {
    "next_0", "next_1", "offset_0", "offset_1", "offset_2", "offset_3", "offset_4", "offset_5", "offset_6", "cond_inv", "cond_flags_src", "cond_sel_0",
    "cond_sel_1", "cond_sel_2", "cond_sel_3", "escape_0", "uzf_in_src_0", "uzf_in_src_1", "ucf_in_src_0", "ucf_in_src_1", "usf_in_src", "uof_in_src", "IR_wrt", "status_wrt",
    "shift_src_0", "shift_src_1", "shift_src_2", "zbus_out_src_0", "zbus_out_src_1", "alu_a_src_0", "alu_a_src_1", "alu_a_src_2", "alu_a_src_3", "alu_a_src_4", "alu_a_src_5",
    "alu_op_0", "alu_op_1", "alu_op_2", "alu_op_3", "alu_mode", "alu_cf_in_src_0", "alu_cf_in_src_1", "alu_cf_in_inv", "zf_in_src_0", "zf_in_src_1", "alu_cf_out_inv",
    "cf_in_src_0", "cf_in_src_1", "cf_in_src_2", "sf_in_src_0", "sf_in_src_1", "of_in_src_0", "of_in_src_1", "of_in_src_2", "rd", "wr", "alu_b_src_0", "alu_b_src_1",
    "alu_b_src_2", "display_reg_load", "dl_wrt", "dh_wrt", "cl_wrt", "ch_wrt", "bl_wrt", "bh_wrt", "al_wrt", "ah_wrt", "mdr_in_src", "mdr_out_src", "mdr_out_en",
    "mdrl_wrt", "mdrh_wrt", "tdrl_wrt", "tdrh_wrt", "dil_wrt", "dih_wrt", "sil_wrt", "sih_wrt", "marl_wrt", "marh_wrt", "bpl_wrt", "bph_wrt", "pcl_wrt", "pch_wrt",
    "spl_wrt", "sph_wrt", "escape_1", "esc_in_src", "int_vector_wrt", "mask_flags_wrt", "mar_in_src", "int_ack", "clear_all_ints", "ptb_wrt", "pagtbl_ram_we", "mdr_to_pagtbl_en",
    "force_user_ptb", "-", "-", "-", "-", "gl_wrt", "gh_wrt", "imm_0", "imm_1", "imm_2", "imm_3", "imm_4", "imm_5", "imm_6", "imm_7", "-", "-", "-", "-", "-", "-", "-", "-"
};



        public byte[] rom_desc;
        public byte[][] roms;

        public ushort MAR; // memory address register

        public uint debug_mem_offset;

        public byte bkpt_opcode;
        public byte bkpt_cycle;

        public void Init(HW_TTY hw_tty) //reset
        {


            this.rom_desc = new byte[Baffa1_Config.BAFFA1_ROM_DESC];

            this.roms = new byte[Baffa1_Config.BAFFA1_ROM_DESC][];

            load_rom(Baffa1_Config.WORKSPACE + "rom", this.rom_desc, hw_tty);


            for (uint _roms = 0; _roms < Baffa1_Config.BAFFA1_ROM_NBR_ROMS; _roms++)
            {
                string filename = Baffa1_Config.WORKSPACE + "rom" + _roms.ToString();

                this.roms[_roms] = new byte[8 * Baffa1_Config.BAFFA1_ROM_NBR_ROMS * Baffa1_Config.BAFFA1_ROM_NBR_INSTRUCTIONS];
                load_rom(filename, this.roms[_roms], hw_tty);
            }

            this.MAR = 0x0;
            this.debug_mem_offset = 0;

            this.bkpt_opcode = 0x00;
            this.bkpt_cycle = 0x00;
        }

        public void Display_current_cycles(byte opcode, byte cycle, byte debug_desc_type, HW_TTY hw_tty)
        {
            string str_out = "";
            
            int p = opcode * Baffa1_Config.BAFFA1_ROM_CYCLES_PER_INSTR + cycle;

            for (int j = 0; j < Baffa1_Config.BAFFA1_ROM_NBR_ROMS; j++)
            {

                if (j % 8 == 0)
                {
                    hw_tty.Print(" ");
                    for (int i = j; i < Baffa1_Config.BAFFA1_ROM_NBR_ROMS && i < (j + 8); i++)
                    {
                        str_out = String.Format(" Rom {0:00}  ", i);
                        hw_tty.Print(str_out);
                    }
                    hw_tty.Print("\n");
                    hw_tty.Print(" ");
                }

                hw_tty.Print(" ");

                str_out = Utils.INV_BYTE_TO_BINARY(this.roms[j][p]);
                hw_tty.Print(str_out);

                if ((j + 1) % 8 == 0 && j < Baffa1_Config.BAFFA1_ROM_NBR_ROMS - 1)
                    hw_tty.Print("\n\n");
            }

            hw_tty.Print("\n\n");

            if (debug_desc_type == 1)
            {
                hw_tty.Print("---------\n");
                if (Utils.StrLen(this.rom_desc, (256 * 64 * opcode) + (256 * cycle)) > 0)
                {
                    str_out = Utils.GetStr(this.rom_desc, (256 * 64 * opcode) + (256 * cycle)) + "\n";
                    hw_tty.Print(str_out);
                    hw_tty.Print("---------\n");
                }
            }
            else
            {
                for (int j = 0; j < 24; j++)
                {
                    hw_tty.Print(" ");
                    for (int k = 0; k < 4; k++)
                    {

                        int c_rom = (j + 24 * k) / 8;
                        int c_p = opcode * Baffa1_Config.BAFFA1_ROM_CYCLES_PER_INSTR + cycle;
                        byte c_bit = (byte)Math.Pow(2, (j + 24 * k) % 8);
                        byte c_byte = this.roms[c_rom][c_p];

                        String tmp = "";

                        if ((c_byte & c_bit) >> ((j + 24 * k) % 8) == 1)
                            tmp = "*" + BAFFA1_ROM_CONTROL_LIST[j + 24 * k] + "*";
                        else
                            tmp = " " + BAFFA1_ROM_CONTROL_LIST[j + 24 * k] + " ";

                        str_out = tmp.PadRight(18);
                        hw_tty.Print(str_out);

                    }
                    hw_tty.Print("\n");
                }
            }
            hw_tty.Print("\n");

            str_out = String.Format(" Inst.: {0} | ", opcode.ToString("X2")); hw_tty.Print(str_out);
            str_out = String.Format("Cycle: {0} | ", cycle.ToString("X2")); hw_tty.Print(str_out);
            str_out = Utils.GetStr(this.rom_desc, 0x400000 + (opcode * 256)) + "\n"; hw_tty.Print(str_out);

            hw_tty.Print("\n");
        }

        public void display_current_cycles_desc(byte opcode, byte cycle, HW_TTY hw_tty)
        {

            int p = opcode * Baffa1_Config.BAFFA1_ROM_CYCLES_PER_INSTR + cycle;
            string str_out = "";

            str_out = String.Format(" *Inst.: {0} | ", opcode.ToString("X2")); hw_tty.Print(str_out);
            str_out = String.Format("Cycle: {0} | ", cycle.ToString("X2")); hw_tty.Print(str_out);
            str_out = Utils.GetStr(this.rom_desc, 0x400000 + (opcode * 256)) + "\n"; hw_tty.Print(str_out);

            hw_tty.Print("---------\n");

            if (Utils.StrLen(this.rom_desc, (256 * 64 * opcode) + (256 * cycle)) > 0)
            {
                str_out = Utils.GetStr(this.rom_desc, (256 * 64 * opcode) + (256 * cycle)) + "\n";
                hw_tty.Print(str_out);
                hw_tty.Print("---------\n");
            }

        }

        public void debug_cycles(HW_TTY hw_tty)
        {

            hw_tty.Print("Display Rom Microcode Cycles\n");

            byte opcode = 0;
            byte cycle = 0;

            byte debug_desc_type = 0;

            while (true)
            {


                hw_tty.Print("roms/microcode cycles> ");
                uint key = hw_tty.GetChar();


                if (key == 'n' || key == 'N')
                {
                    if (cycle < Baffa1_Config.BAFFA1_ROM_CYCLES_PER_INSTR - 1)
                        cycle++;
                    hw_tty.Print("\n\n");
                    Display_current_cycles(opcode, cycle, debug_desc_type, hw_tty);
                }
                else if (key == 'P' || key == 'P')
                {
                    if (cycle > 0)
                        cycle--;
                    hw_tty.Print("\n\n");
                    Display_current_cycles(opcode, cycle, debug_desc_type, hw_tty);
                }
                else if (key == 't' || key == 'T')
                {
                    debug_desc_type = (debug_desc_type == 0) ? (byte)0x1 : (byte)0x0;

                    if (debug_desc_type == 0)
                        hw_tty.Print("Showing Microcode Settings\n");
                    else
                        hw_tty.Print("Showing Microcode Description \n");

                }
                else if (key == 'q' || key == 'Q')
                {
                    hw_tty.Print("\n");
                    return;
                }
                else if (key == '?')
                {
                    menu(debug_desc_type, hw_tty);
                }
                else if (key == 's' || key == 'S')
                {
                    hw_tty.Print("Opcode ? ");

                    String input = hw_tty.Gets(2);
                    String value = input.PadLeft(2);

                    opcode = (byte)Utils.Convert_hexstr_to_value(value);
                    cycle = 0;
                    hw_tty.Print("\n\n");
                    Display_current_cycles(opcode, cycle, debug_desc_type, hw_tty);

                }
                else if (key == 'd' || key == 'D')
                {
                    hw_tty.Print("\n\n");
                    Display_current_cycles(opcode, cycle, debug_desc_type, hw_tty);
                }
                else
                    hw_tty.Print("\n");

            }
        }

        private void is_rom_in_bounds(uint index)
        {
            if (index >= 0 && index < Baffa1_Config.BAFFA1_ROM_SIZE)
            {
                throw new Exception("Rom out of bounds");
            }
        }
        private uint load_rom(string filename, byte[] rom, HW_TTY hw_tty)
        {

            String str_out = String.Format("The filename to load is: {0}", filename);
            hw_tty.Print(str_out);


            long size = 0;
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                    size = stream.Length;
                }

                try
                {
                    using (FileStream fs = new FileStream(filename,
               FileMode.Open))
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {


                        byte[] buf = new byte[size];

                        br.Read(buf,0,(int)size);

                        for (uint i = 0; i < size; i++) {
                            rom[i] = buf[i];
                        }

                        hw_tty.Print(" | OK.\n");
                    }
                }
                catch//(Exception ex)
                {
                    hw_tty.Print(" | Failed to read from file.\n");
                    return 0;
                }

            }
            catch// (Exception ex)
            {
                hw_tty.Print(" | Failed to open the file.\n");
                return 0;
            }
            return 1;
        }

        private void menu(byte debug_desc_type, HW_TTY hw_tty)
        {
            hw_tty.Print("\n");
            hw_tty.Print("BAFFA-1 Debug Monitor > Roms > Microcode Cycles\n");
            hw_tty.Print("\n");

            hw_tty.Print("  S - Set Opcode\n");
            hw_tty.Print("  D - Display current Cycle\n");
            hw_tty.Print("  N - Next Cycle\n");
            hw_tty.Print("  P - Previous Cycle\n");

            hw_tty.Print("\n");

            if (debug_desc_type == 0)
                hw_tty.Print("  T - Show Microcode Description \n");
            else
                hw_tty.Print("  T - Show Microcode Settings \n");

            hw_tty.Print("\n");

            hw_tty.Print("  ? - Display Menu\n");
            hw_tty.Print("  Q - Back to Rom Microcode Cycles\n");
            hw_tty.Print("\n");
        }


    }
}

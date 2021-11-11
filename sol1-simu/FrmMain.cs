using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sol1_simu
{
    public partial class FrmMain : Form
    {
        int cycle_nbr = 0;
        int instr_nbr = 0;

        int[] microcode_enable;

        bool updatingDisplay = false;
        bool disabledCmb = false;
        bool disabledLst = false;

        RomManager rommgr;
        RomClipboard clipboard;

        String sys_fileManager = ""; //explorer.exe
        String sys_hexEditor = ""; //"C:\\Program Files\\HxD\\HxD64.exe"
        String sys_calculator = ""; //calc.exe
        String sys_terminal = ""; //cmd.exe
        String sys_notepad = ""; // notepad.exe

        public FrmMain()
        {
            InitializeComponent();

            rommgr = new RomManager();
            rommgr.Clear();

            clipboard = new RomClipboard();
            clipboard.Clear();


            for (int i = 0; i < RomManager.CYCLES_PER_INSTR; i++)
                list_cycle.Items.Add(i.ToString("X2"));

            list_names.MultiColumn = true;
            list_names.ColumnWidth = 310;

            lstInstructions.MultiColumn = true;
            lstInstructions.ColumnWidth = 200;

            String[] microcode = {
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

            int[] microcode_enable2 = {
                2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2,
                2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 0, 0,
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2,
                2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 2, 0, 0, 2, 1, 1, 0, 1, 1,
                1, 3, 3, 3, 3, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3
            };
            microcode_enable = microcode_enable2;

            control_list.MultiColumn = true;
            control_list.ColumnWidth = 110;


            foreach (String s in microcode)
            {
                control_list.Items.Add(s);
            }
            cmbAluOp.SelectedItem = cmbAluOp.Items[0];
            /*
            cmb_next_inst.SelectedItem = cmb_next_inst.Items[0];
            cmb_cond_sel.SelectedItem = cmb_cond_sel.Items[0];
            cmb_flags_src.SelectedItem = cmb_flags_src.Items[0];
            cmb_mar_in_src.SelectedItem = cmb_mar_in_src.Items[0];
            cmb_mdr_in_src.SelectedItem = cmb_mdr_in_src.Items[0];
            cmb_mdr_out_src.SelectedItem = cmb_mdr_out_src.Items[0];
            cmb_alu_b_mux.SelectedItem = cmb_alu_b_mux.Items[0];
            cmb_alu_a_mux.SelectedItem = cmb_alu_a_mux.Items[0];
            cmb_alu_cf_in.SelectedItem = cmb_alu_cf_in.Items[0];
            cmbAluOp.SelectedItem = cmbAluOp.Items[0];

            cmb_alu_cf_out_inv.SelectedItem = cmb_alu_cf_out_inv.Items[0];
            cmb_zbus.SelectedItem = cmb_zbus.Items[0];
            cmb_shift_src.SelectedItem = cmb_shift_src.Items[0];
            cmb_usf.SelectedItem = cmb_usf.Items[0];
            cmb_ucf.SelectedItem = cmb_ucf.Items[0];
            cmb_uzf.SelectedItem = cmb_uzf.Items[0];
            cmb_uof.SelectedItem = cmb_uof.Items[0];
            cmb_of_in.SelectedItem = cmb_of_in.Items[0];
            cmb_sf_in.SelectedItem = cmb_sf_in.Items[0];
            cmb_cf_in.SelectedItem = cmb_cf_in.Items[0];
            cmb_zf_in.SelectedItem = cmb_zf_in.Items[0];
            */
        }


        void write_cycle()
        {

            byte val = 0;
            int index = 0;

            if (mnu_readonly.Checked == true) return;

            rommgr.info[cycle_nbr] = memo_info.Text;

            string tmp_instr_name = Utils.FormatMemoName(memo_name.Text);

            if (tmp_instr_name != rommgr.instr_names[cycle_nbr / RomManager.CYCLES_PER_INSTR])
                rommgr.instr_names[cycle_nbr / RomManager.CYCLES_PER_INSTR] = tmp_instr_name;

            disable_lst_refresh();
            for (int i = 0; i < RomManager.TOTAL_CONTROL_BITS; i++)
            {
                index = i % 8;
                if (index == 0)
                    val = 0;

                if (control_list.CheckedIndices.Contains(i))
                {
                    byte v = Convert.ToByte((int)Math.Pow(2, index));
                    val = Convert.ToByte(val + v);
                }

                if (!control_list.CheckedIndices.Contains(index))
                    control_list.SetItemChecked(index, false);

                rommgr.ROMS[i / 8][cycle_nbr] = val;
            }
            enable_lst_refresh();

            update_display();

        }




        private void cmbAluOp_update()
        {
            int index = 0;
            disable_cmb_refresh();

            if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
            !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
            !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
            control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
            !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("plus");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }
            else if (!control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("minus");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }
            else if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("and");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;

            }
            else if (!control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("or");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;

            }
            else if (!control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("xor");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }
            else if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("A");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;

            }
            else if (!control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("B");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }

            else if (!control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("not A");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;

            }
            else if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("not B");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }

            else if (!control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("nand");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;

            }
            else if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("nor");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }

            else if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_0")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_1")) &&
                    !control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_2")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_op_3")) &&
                    control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_mode")))
            {
                index = cmbAluOp.Items.IndexOf("nxor");
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }
            else
            {
                index = 0;
                if (list_names.SelectedIndex != index)
                    cmbAluOp.SelectedIndex = index;
            }

            enable_cmb_refresh();

        }

        private void update_display()
        {

            if (!updatingDisplay)
            {
                updatingDisplay = true;
                disable_cmb_refresh();
                disable_lst_refresh();

                int i, j;
                int index;

                list_names.Items.Clear();
                for (i = 0; i < RomManager.NBR_INSTRUCTIONS; i++)
                {
                    if (rommgr.instr_names[i] == null)
                        rommgr.instr_names[i] = "";

                    /*
                    if (instr_names[i].IndexOf("-") > -1)
                    {
                        string tmp_instr_name = format_memo_name(instr_names[i].ToUpper());
                        instr_names[i] = tmp_instr_name;
                    }
                    */
                    /*
                    string tmp_instr_name = format_memo_name(instr_names[i].ToLower());
                    instr_names[i] = tmp_instr_name;
                    */
                    list_names.Items.Add(i.ToString("X2") + ": " + rommgr.instr_names[i]);
                }

                for (i = 0; i < RomManager.NBR_ROMS; i++)
                {
                    for (j = 0; j < 8; j++)
                    {

                        index = i * 8 + j;
                        bool ischecked = ((int)rommgr.ROMS[i][cycle_nbr] & (int)Math.Pow(2, j)) != 0;
                        if (control_list.CheckedIndices.Contains(index) != ischecked)
                            control_list.SetItemChecked(index, ischecked);
                    }
                }

                if (memo_info.Text != rommgr.info[cycle_nbr])
                    memo_info.Text = rommgr.info[cycle_nbr];

                if (memo_name.Text != rommgr.instr_names[cycle_nbr / RomManager.CYCLES_PER_INSTR])
                    memo_name.Text = rommgr.instr_names[cycle_nbr / RomManager.CYCLES_PER_INSTR];

                for (i = 0; i < RomManager.CYCLES_PER_INSTR; i++)
                {
                    if (rommgr.instr_names[i / RomManager.CYCLES_PER_INSTR].Trim() != "")
                    {
                        //list_cycle.Items[i]
                    }
                }


                index = (int)(cycle_nbr / RomManager.CYCLES_PER_INSTR);
                if (list_names.SelectedIndex != index)
                    list_names.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(0)) | (Utils.BoolToInt(control_list.CheckedIndices.Contains(1)) << 1);
                if (cmb_next_inst.SelectedIndex != index)
                    cmb_next_inst.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cond_flags_src")));
                if (cmb_flags_src.SelectedIndex != index)
                    cmb_flags_src.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("mdr_in_src")));
                if (cmb_mdr_in_src.SelectedIndex != index)
                    cmb_mdr_in_src.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("mdr_out_src")));
                if (cmb_mdr_out_src.SelectedIndex != index)
                    cmb_mdr_out_src.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("mar_in_src")));
                if (cmb_mar_in_src.SelectedIndex != index)
                    cmb_mar_in_src.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_cf_out_inv")));
                if (cmb_alu_cf_out_inv.SelectedIndex != index)
                    cmb_alu_cf_out_inv.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_cf_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_cf_in_src_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_cf_in_inv"))) << 2;
                if (cmb_alu_cf_in.SelectedIndex != index)
                    cmb_alu_cf_in.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("shift_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("shift_src_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("shift_src_2"))) << 2;
                if (cmb_shift_src.SelectedIndex != index)
                    cmb_shift_src.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_a_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_a_src_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_a_src_2"))) << 2
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_a_src_3"))) << 3
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_a_src_4"))) << 4
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_a_src_5"))) << 5;
                if (cmb_alu_a_mux.SelectedIndex != index)
                    cmb_alu_a_mux.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_b_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_b_src_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("alu_b_src_2"))) << 2;
                if (cmb_alu_b_mux.SelectedIndex != index)
                    cmb_alu_b_mux.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("zbus_out_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("zbus_out_src_1"))) << 1;
                if (cmb_zbus.SelectedIndex != index)
                    cmb_zbus.SelectedIndex = index;

                int offset = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_2"))) << 2
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_3"))) << 3
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_4"))) << 4
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_5"))) << 5;

                if (control_list.CheckedIndices.Contains(control_list.Items.IndexOf("offset_6")))
                    offset = offset | (0xFF << 6);

                //offset = offset | ((offset << 1) & 0x80);
                if (txtInteger.Text != offset.ToString())
                    txtInteger.Text = offset.ToString();


                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cond_sel_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cond_sel_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cond_sel_2"))) << 2
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cond_sel_3"))) << 3
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cond_inv"))) << 4;
                if (cmb_cond_sel.SelectedIndex != index)
                    cmb_cond_sel.SelectedIndex = index;


                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("zf_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("zf_in_src_1"))) << 1;
                if (cmb_zf_in.SelectedIndex != index)
                    cmb_zf_in.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cf_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cf_in_src_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("cf_in_src_2"))) << 2;
                if (cmb_cf_in.SelectedIndex != index)
                    cmb_cf_in.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("sf_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("sf_in_src_1"))) << 1;
                if (cmb_sf_in.SelectedIndex != index)
                    cmb_sf_in.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("of_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("of_in_src_1"))) << 1
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("of_in_src_2"))) << 2;
                if (cmb_of_in.SelectedIndex != index)
                    cmb_of_in.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("uzf_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("uzf_in_src_1"))) << 1;
                cmb_uzf.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("ucf_in_src_0")))
                  | Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("ucf_in_src_1"))) << 1;
                if (cmb_ucf.SelectedIndex != index)
                    cmb_ucf.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("usf_in_src")));
                if (cmb_usf.SelectedIndex != index)
                    cmb_usf.SelectedIndex = index;

                index = Utils.BoolToInt(control_list.CheckedIndices.Contains(control_list.Items.IndexOf("uof_in_src")));
                if (cmb_uof.SelectedIndex != index)
                    cmb_uof.SelectedIndex = index;

                cmbAluOp_update();

                updatingDisplay = false;

                enable_cmb_refresh();
                enable_lst_refresh();

                control_info_refresh();
            }

        }



        private string write_esp_info(string last_single_code, string last_single_code_settings)
        {
            string extra_desc = "";

            if (last_single_code == "next" && cmb_next_inst.SelectedIndex > -1)
                extra_desc = "(" + cmb_next_inst.SelectedItem.ToString() + ")";

            else if (last_single_code == "cond_sel" && cmb_cond_sel.SelectedIndex > -1)
                extra_desc = "(" + cmb_cond_sel.SelectedItem.ToString() + ")";

            else if (last_single_code == "alu_a_src" && cmb_alu_a_mux.SelectedIndex > -1)
                extra_desc = "(" + cmb_alu_a_mux.SelectedItem.ToString() + ")";

            else if (last_single_code == "alu_b_src" && cmb_alu_b_mux.SelectedIndex > -1)
                extra_desc = "(" + cmb_alu_b_mux.SelectedItem.ToString() + ")";

            else if (last_single_code == "alu_op" && cmbAluOp.SelectedIndex > -1)
                extra_desc = "(ALU OP: " + cmbAluOp.SelectedItem.ToString() + ")";

            else if (last_single_code == "alu_cf_in_src" && cmb_alu_cf_in.SelectedIndex > -1)
                extra_desc = "(" + cmb_alu_cf_in.SelectedItem.ToString() + ")";

            else if (last_single_code == "zbus_out_src" && cmb_zbus.SelectedIndex > -1)
                extra_desc = "(" + cmb_zbus.SelectedItem.ToString() + ")";

            else if (last_single_code == "shift_src" && cmb_shift_src.SelectedIndex > -1)
                extra_desc = "(" + cmb_shift_src.SelectedItem.ToString() + ")";

            else if (last_single_code == "uzf_in_src" && cmb_uzf.SelectedIndex > -1)
                extra_desc = "(" + cmb_uzf.SelectedItem.ToString() + ")";

            else if (last_single_code == "ucf_in_src" && cmb_ucf.SelectedIndex > -1)
                extra_desc = "(" + cmb_ucf.SelectedItem.ToString() + ")";

            else if (last_single_code == "zf_in_src" && cmb_zf_in.SelectedIndex > -1)
                extra_desc = "(" + cmb_zf_in.SelectedItem.ToString() + ")";

            else if (last_single_code == "cf_in_src" && cmb_cf_in.SelectedIndex > -1)
                extra_desc = "(" + cmb_cf_in.SelectedItem.ToString() + ")";

            else if (last_single_code == "sf_in_src" && cmb_sf_in.SelectedIndex > -1)
                extra_desc = "(" + cmb_sf_in.SelectedItem.ToString() + ")";

            else if (last_single_code == "of_in_src" && cmb_of_in.SelectedIndex > -1)
                extra_desc = "(" + cmb_of_in.SelectedItem.ToString() + ")";

            else if (last_single_code == "offset")
            {
                if (last_single_code_settings[0] == '1')
                    extra_desc = "(0x" + Utils.StrBinToHex("1111111" + last_single_code_settings) + ")";
                else
                    extra_desc = "(0x" + Utils.StrBinToHex(last_single_code_settings) + ")";
            }

            else if (last_single_code == "imm")
                extra_desc = "(0x" + Utils.StrBinToHex(last_single_code_settings) + ")";

            return last_single_code.PadRight(15) + " = " + last_single_code_settings.PadRight(9) + extra_desc + "\n";
        }
        private void control_info_refresh()
        {

            control_info.Text = "";

            string last_single_code = "";
            string last_single_code_settings = "";


            String mc_settings = "";
            String mc_enabled = "";

            for (int i = 0; i < control_list.Items.Count; i++)
            {
                string current_code = control_list.Items[i].ToString();
                int last_underscore = current_code.LastIndexOf('_');

                string last_token = last_underscore > -1 ? current_code.Substring(last_underscore + 1) : current_code;

                int num = -1;
                if (last_underscore > -1)
                    if (!int.TryParse(last_token, out num))
                        num = -1;

                string single_code = num == -1 ? current_code : current_code.Substring(0, last_underscore);


                if (last_single_code != "" && last_single_code_settings != "" && single_code != last_single_code)
                {
                    mc_settings += write_esp_info(last_single_code, last_single_code_settings);
                    last_single_code_settings = "";
                }


                if (microcode_enable[i] == 0 && !control_list.CheckedIndices.Contains(i))
                {
                    mc_enabled += " * " + current_code + "\n";
                }
                else if (microcode_enable[i] == 1 && control_list.CheckedIndices.Contains(i))
                {
                    mc_enabled += " * " + current_code + "\n";
                }
                else if (microcode_enable[i] == 2)
                {
                    int value = control_list.CheckedIndices.Contains(i) ? 1 : 0;

                    if (num != -1)
                    {
                        last_single_code_settings = value.ToString() + last_single_code_settings;
                    }
                    else
                    {
                        string extra_desc = "";

                        if (control_list.Items[i].ToString() == "cond_flags_src" && cmb_flags_src.SelectedIndex > -1)
                            extra_desc = "(" + cmb_flags_src.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "mar_in_src" && cmb_mar_in_src.SelectedIndex > -1)
                            extra_desc = "(" + cmb_mar_in_src.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "mdr_in_src" && cmb_mdr_in_src.SelectedIndex > -1)
                            extra_desc = "(" + cmb_mdr_in_src.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "mdr_out_src" && cmb_mdr_out_src.SelectedIndex > -1)
                            extra_desc = "(" + cmb_mdr_out_src.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "alu_cf_out_inv" && cmb_alu_cf_out_inv.SelectedIndex > -1)
                            extra_desc = "(" + cmb_alu_cf_out_inv.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "usf_in_src" && cmb_usf.SelectedIndex > -1)
                            extra_desc = "(" + cmb_usf.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "uof_in_src" && cmb_uof.SelectedIndex > -1)
                            extra_desc = "(" + cmb_uof.SelectedItem.ToString() + ")";

                        else if (control_list.Items[i].ToString() == "alu_cf_in_inv" && value == 0)
                            extra_desc = "(Carry-in not inverted)";
                        else if (control_list.Items[i].ToString() == "alu_cf_in_inv" && value == 1)
                            extra_desc = "(Carry-in inverted)";

                        else if (control_list.Items[i].ToString() == "alu_mode" && cmbAluOp.SelectedIndex > -1)
                            extra_desc = "(ALU OP: " + cmbAluOp.SelectedItem.ToString() + ")";

                        if (control_list.Items[i].ToString() != "esc_in_src")
                            mc_settings += control_list.Items[i].ToString().PadRight(15) + " = " + value.ToString().PadRight(9) + extra_desc + "\n";
                    }
                }

                last_single_code = single_code;
            }

            if (last_single_code != "" && last_single_code_settings != "")
            {
                mc_settings += write_esp_info(last_single_code, last_single_code_settings);
                last_single_code_settings = "";
            }

            mc_enabled = String.Join("\r\n", mc_enabled.Split('\n').OrderBy(c => c).ToArray()).Trim(new Char[] { '\r', '\n' });
            mc_settings = String.Join("\r\n", mc_settings.Split('\n').OrderBy(c => c).ToArray()).Trim(new Char[] { '\r', '\n' });
            control_info.Text = mc_settings + (mc_enabled == "" ? "" : "\r\n----------------------\r\n" + mc_enabled);
        }

        //////////////////////////
        //Next Micro-Instruction
        //[0]
        private void cmb_next_inst_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_next_inst.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("next_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("next_1"), (index & 0x02) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[1]

        private void cmb_cond_sel_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_cond_sel.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("cond_sel_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("cond_sel_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("cond_sel_2"), (index & 0x04) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("cond_sel_3"), (index & 0x08) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("cond_inv"), (index & 0x10) != 0);
            enable_lst_refresh();
            write_cycle();
        }

        //[2]
        private void cmb_flags_src_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_flags_src.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("cond_flags_src"), index == 1);
            enable_lst_refresh();
            write_cycle();
        }
        //////////////////////////
        //MAR IN SRC
        private void cmb_mar_in_src_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_mar_in_src.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("mar_in_src"), index == 1);
            enable_lst_refresh();
            write_cycle();
        }

        //////////////////////////
        //MDR IN/OUT SRC
        //[0]
        private void cmb_mdr_in_src_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_mdr_in_src.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("mdr_in_src"), index == 1);
            enable_lst_refresh();
            write_cycle();
        }

        //[1]
        private void cmb_mdr_out_src_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_mdr_out_src.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("mdr_out_src"), index == 1);
            enable_lst_refresh();
            write_cycle();
        }


        //////////////////////////
        //ALU INPUTS A/B
        //[0]
        private void cmb_alu_a_mux_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_alu_a_mux.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_a_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_a_src_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_a_src_2"), (index & 0x04) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_a_src_3"), (index & 0x08) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_a_src_4"), (index & 0x10) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_a_src_5"), (index & 0x20) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[1]

        private void cmb_alu_b_mux_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_alu_b_mux.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_b_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_b_src_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_b_src_2"), (index & 0x04) != 0);
            enable_lst_refresh();
            write_cycle();
        }

        //////////////////////////
        //ALU OPERATIONS IN 
        //[0]
        private void cmbAluOp_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            if (cmbAluOp.Text == "plus")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), false);
            }
            else if (cmbAluOp.Text == "minus")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), false);
            }
            else if (cmbAluOp.Text == "and")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "or")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "xor")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "A")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "B")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "not A")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "not B")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "nand")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "nor")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else if (cmbAluOp.Text == "nxor")
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), true);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), true);
            }
            else
            {
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_0"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_1"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_2"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_op_3"), false);
                control_list.SetItemChecked(control_list.Items.IndexOf("alu_mode"), false);
            }
            enable_lst_refresh();
            write_cycle();
        }
        //[1]
        private void cmb_alu_cf_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_alu_cf_in.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_cf_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_cf_in_src_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("alu_cf_in_inv"), (index & 0x04) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[2]
        private void cmb_alu_cf_out_inv_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_alu_cf_out_inv.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("alu_cf_out_inv"), index == 1);
            enable_lst_refresh();
            write_cycle();
        }
        //////////////////////////
        //ALU to ZBUS
        private void cmb_zbus_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_zbus.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("zbus_out_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("zbus_out_src_1"), (index & 0x02) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //////////////////////////
        //SHIFT SRC IN
        private void cmb_shift_src_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_shift_src.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("shift_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("shift_src_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("shift_src_2"), (index & 0x04) != 0);
            enable_lst_refresh();
            write_cycle();
        }

        //////////////////////////
        /// MICROFLAGS IN
        //[0]
        private void cmb_uzf_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_uzf.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("uzf_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("uzf_in_src_1"), (index & 0x02) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[1]
        private void cmb_ucf_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_ucf.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("ucf_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("ucf_in_src_1"), (index & 0x02) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[2]
        private void cmb_usf_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_usf.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("usf_in_src"), index == 1);
            enable_lst_refresh();
            write_cycle();
        }
        //[3]
        private void cmb_uof_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_uof.SelectedIndex;

            control_list.SetItemChecked(control_list.Items.IndexOf("uof_in_src"), index == 1);
            enable_lst_refresh();
            write_cycle();

        }
        //////////////////////////
        /// Arithmetic Flags In
        //[0]
        private void cmb_zf_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_zf_in.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("zf_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("zf_in_src_1"), (index & 0x02) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[1]
        private void cmb_cf_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_cf_in.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("cf_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("cf_in_src_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("cf_in_src_2"), (index & 0x04) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[2]
        private void cmb_sf_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_sf_in.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("sf_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("sf_in_src_1"), (index & 0x02) != 0);
            enable_lst_refresh();
            write_cycle();
        }
        //[3]
        private void cmb_of_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            disable_lst_refresh();
            int index = cmb_of_in.SelectedIndex;
            control_list.SetItemChecked(control_list.Items.IndexOf("of_in_src_0"), (index & 0x01) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("of_in_src_1"), (index & 0x02) != 0);
            control_list.SetItemChecked(control_list.Items.IndexOf("of_in_src_2"), (index & 0x04) != 0);
            enable_lst_refresh();
            write_cycle();
        }

        private void mnu_readonly_Click(object sender, EventArgs e)
        {
            set_readonly(!mnu_readonly.Checked);
        }

        private void mnu_new_Click(object sender, EventArgs e)
        {
            NewFile();

            memo_info.Clear();
        }

        private void NewFile()
        {
            rommgr.New();

            foreach (String n in rommgr.NamesList())
            {
                list_names.Items.Add(n);
            }

            set_readonly(false);
            update_display();
        }

        private void btn_imm_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (txtInteger.Text == "") i = 0;
            else i = Utils.StrToInt(txtInteger.Text);

            if ((i & 0x01) != 0) control_list.SetItemChecked(13 * 8, true);
            else control_list.SetItemChecked(13 * 8, false);
            if ((i & 0x02) != 0) control_list.SetItemChecked(13 * 8 + 1, true);
            else control_list.SetItemChecked(13 * 8 + 1, false);
            if ((i & 0x04) != 0) control_list.SetItemChecked(13 * 8 + 2, true);
            else control_list.SetItemChecked(13 * 8 + 2, false);
            if ((i & 0x08) != 0) control_list.SetItemChecked(13 * 8 + 3, true);
            else control_list.SetItemChecked(13 * 8 + 3, false);
            if ((i & 0x10) != 0) control_list.SetItemChecked(13 * 8 + 4, true);
            else control_list.SetItemChecked(13 * 8 + 4, false);
            if ((i & 0x20) != 0) control_list.SetItemChecked(13 * 8 + 5, true);
            else control_list.SetItemChecked(13 * 8 + 5, false);
            if ((i & 0x40) != 0) control_list.SetItemChecked(13 * 8 + 6, true);
            else control_list.SetItemChecked(13 * 8 + 6, false);
            if ((i & 0x80) != 0) control_list.SetItemChecked(13 * 8 + 7, true);
            else control_list.SetItemChecked(13 * 8 + 7, false);

            write_cycle();
        }

        private void btn_offset_Click(object sender, EventArgs e)
        {
            int i;
            if (txtInteger.Text == "") i = 0;
            else i = Utils.StrToInt(txtInteger.Text);

            if ((i & 0x01) != 0) control_list.SetItemChecked(2, true);
            else control_list.SetItemChecked(2, false);
            if ((i & 0x02) != 0) control_list.SetItemChecked(3, true);
            else control_list.SetItemChecked(3, false);
            if ((i & 0x04) != 0) control_list.SetItemChecked(4, true);
            else control_list.SetItemChecked(4, false);
            if ((i & 0x08) != 0) control_list.SetItemChecked(5, true);
            else control_list.SetItemChecked(5, false);
            if ((i & 0x10) != 0) control_list.SetItemChecked(6, true);
            else control_list.SetItemChecked(6, false);
            if ((i & 0x20) != 0) control_list.SetItemChecked(7, true);
            else control_list.SetItemChecked(7, false);
            if ((i & 0x40) != 0) control_list.SetItemChecked(8, true);
            else control_list.SetItemChecked(8, false);

            write_cycle();
        }




        private void lstCycles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_cycle.SelectedIndex > -1)
            {
                cycle_nbr = instr_nbr * RomManager.CYCLES_PER_INSTR + list_cycle.SelectedIndex;
                update_display();
            }
        }

        private void control_list_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue != e.NewValue)
            {
                disable_lst_refresh();
                control_list.SetItemCheckState(e.Index, e.NewValue);
                enable_lst_refresh();
                control_info_refresh();
            }
            write_cycle();
        }

        private void memo_info_KeyUp(object sender, KeyEventArgs e)
        {
            write_cycle();
        }

        private void memo_info_KeyDown(object sender, KeyEventArgs e)
        {
            write_cycle();
        }

        private void btnShiftLeft_Click(object sender, EventArgs e)
        {
            clipboard.ShiftLeft(cycle_nbr, rommgr);
            cycle_nbr--;
            update_display();
        }


        private void btnNextCycle_Click(object sender, EventArgs e)
        {
            if (cycle_nbr < RomManager.NBR_INSTRUCTIONS * RomManager.CYCLES_PER_INSTR - 1) cycle_nbr++;

            update_display();

            //list_cycle.ClearSelected();
            list_cycle.SetSelected(cycle_nbr % RomManager.CYCLES_PER_INSTR, true);

            //list_names.ClearSelected();
            //list_names.SetSelected(cycle_nbr / CYCLES_PER_INSTR, true);

            list_cycle.Focus();
        }

        private void btnPrevCycle_Click(object sender, EventArgs e)
        {
            if (cycle_nbr > 0) cycle_nbr--;

            update_display();

            //list_cycle.ClearSelected();
            list_cycle.SetSelected(cycle_nbr % RomManager.CYCLES_PER_INSTR, true);

            //list_names.ClearSelected();
            //list_names.SetSelected(cycle_nbr / CYCLES_PER_INSTR, true);

            list_cycle.Focus();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            do_reset();
        }

        private void cmdPaste_Click(object sender, EventArgs e)
        {
            do_paste();
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            do_copy();
        }

        private void cmdShiftRight_Click(object sender, EventArgs e)
        {
            clipboard.ShiftRight(cycle_nbr, rommgr);
            cycle_nbr++;
            update_display();
        }

        private void btnBitLeft_Click(object sender, EventArgs e)
        {
            cycle_nbr = cycle_nbr / RomManager.CYCLES_PER_INSTR;
            cycle_nbr--;
            cycle_nbr = cycle_nbr * RomManager.CYCLES_PER_INSTR;

            update_display();

            //list_cycle.ClearSelected();
            //list_cycle.SetSelected(cycle_nbr % CYCLES_PER_INSTR, true);

            //list_names.ClearSelected();
            list_names.SetSelected(cycle_nbr / RomManager.CYCLES_PER_INSTR, true);

            list_cycle.Focus();
        }

        private void cmdBitRight_Click(object sender, EventArgs e)
        {
            cycle_nbr = cycle_nbr / RomManager.CYCLES_PER_INSTR;
            cycle_nbr++;
            cycle_nbr = cycle_nbr * RomManager.CYCLES_PER_INSTR;


            update_display();

            //list_cycle.ClearSelected();
            //list_cycle.SetSelected(cycle_nbr % CYCLES_PER_INSTR, true);

            //list_names.ClearSelected();
            list_names.SetSelected(cycle_nbr / RomManager.CYCLES_PER_INSTR, true);

            list_cycle.Focus();
        }

        private void list_names_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_names.SelectedIndex > -1)
            {
                cycle_nbr = list_names.SelectedIndex * RomManager.CYCLES_PER_INSTR;
                instr_nbr = list_names.SelectedIndex;
                update_display();

                //list_cycle.ClearSelected();
                list_cycle.SetSelected(0, true);
                //list_names.SetSelected(cycle_nbr / CYCLES_PER_INSTR, true);

                list_cycle.Refresh();
            }
        }

        private void memo_name_TextChanged(object sender, EventArgs e)
        {
            //write_cycle();
        }

        private void list_cycle_KeyPress(object sender, KeyPressEventArgs e)
        {

            switch (e.KeyChar)
            {
                case 'R':
                case 'r':
                    do_reset();
                    break;

                case 'C':
                case 'c':
                    do_copy();
                    break;

                case 'V':
                case 'v':
                    do_paste();
                    break;
            }
        }


        private void do_copy()
        {
            clipboard.CopyCycle(instr_nbr, list_cycle.SelectedIndex, rommgr);

            update_display();
            send_msg("Last action: Cycles copied.");
        }

        private void do_reset()
        {
            clipboard.Reset(instr_nbr, list_cycle.SelectedIndex, rommgr);

            update_display();

            send_msg("Last action: Cycles reset.");
        }



        private void cmdWorkingFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(sys_fileManager, rommgr.working_folder);
            }
            catch { }
        }

        private void btnHexEditor_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(sys_hexEditor);
            }
            catch { }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(sys_calculator);
            }
            catch { }
        }

        private void btnCmd_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(sys_terminal);
            }
            catch { }
        }

        private void btnNotepad_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(sys_notepad);
            }
            catch { }
        }

        private void calculateAvgCyclesPerInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control_info.Text = rommgr.CalculateAVgCyclesPerInstruction();

        }


        private void pasteInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0) return;
            if (list_names.SelectedIndex > -1)
            {
                clipboard.PasteInstruction(list_names.SelectedIndex, rommgr);
                update_display();
            }
        }



        private void copyInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0) return;

            if (list_names.SelectedIndex > -1)
            {
                clipboard.CopyInstruction(list_names.SelectedIndex, rommgr);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IniFile ini = new IniFile(System.Environment.CurrentDirectory + "\\" + "config.ini");
            ini.IniWriteValue("main", "main_tab_index", tabControl1.SelectedIndex.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int main_tab_index;

            IniFile ini = new IniFile(System.Environment.CurrentDirectory + "\\" + "config.ini");
            try
            {
                main_tab_index = int.Parse(ini.IniReadValue("main", "main_tab_index"));
                tabControl1.SelectedIndex = main_tab_index;
            }
            catch { }


            try
            {
                String current_filename = ini.IniReadValue("general", "last_microcode_open");
                if (current_filename.LastIndexOf("\\") > -1)
                    rommgr.working_folder = current_filename.Substring(0, current_filename.LastIndexOf("\\"));
                else
                    rommgr.working_folder = System.Environment.CurrentDirectory;

            }
            catch { }


            try
            {
                sys_fileManager = ini.IniReadValue("programs", "file_manager");
            }
            catch { }
            try
            {
                sys_hexEditor = ini.IniReadValue("programs", "hex_editor");
            }
            catch { }
            try
            {
                sys_calculator = ini.IniReadValue("programs", "calculator");
            }
            catch { }
            try
            {
                sys_terminal = ini.IniReadValue("programs", "terminal");
            }
            catch { }
            try
            {
                sys_notepad = ini.IniReadValue("programs", "notepad");
            }
            catch { }

            cmdWorkingFolder.Enabled = sys_fileManager.Trim() != "";
            btnHexEditor.Enabled = sys_hexEditor.Trim() != "";
            btnCalc.Enabled = sys_calculator.Trim() != "";
            btnCmd.Enabled = sys_terminal.Trim() != "";
            btnNotepad.Enabled = sys_notepad.Trim() != "";

            NewFile();

            openRecentToolStripMenuItem_Click(null, null);
        }

        private void do_paste()
        {
            if (list_names.SelectedIndex > -1)
            {

                clipboard.PasteCycle(instr_nbr, list_cycle.SelectedIndex, rommgr);
                update_display();

                send_msg("Last action: Cycles pasted.");
            }
        }

        private void load_instructions_list()
        {

            lstInstructions.Items.Clear();
            foreach (InstructionItem i in rommgr.InstructionList())
            {
                lstInstructions.Items.Add(i);
            }
            toolStripStatusLabel1.Text = lstInstructions.Items.Count == 1 ? "1 instruction loaded" : String.Format("{0} instructions loaded", lstInstructions.Items.Count);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string filename = "";

            IniFile ini = new IniFile(System.Environment.CurrentDirectory + "\\" + "config.ini");
            //filename = ini.IniReadValue("general", "last_microcode_open");

            openFileDialog1.FileName = filename;
            openFileDialog1.Filter = "Rom files (rom*.*)|rom*.*|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = rommgr.working_folder;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;

                //IniFile ini = new IniFile(System.Environment.CurrentDirectory + "\\" + "config.ini");
                ini.IniWriteValue("general", "last_microcode_open", filename);

                if (ReadFile(filename))
                {
                    this.Text = filename;
                    cycle_nbr = 0;
                    update_display();

                    load_instructions_list();
                }
            }

        }


        private void openRecentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string filename = "";

            IniFile ini = new IniFile(System.Environment.CurrentDirectory + "\\" + "config.ini");
            filename = ini.IniReadValue("general", "last_microcode_open");

            if (filename.Trim() != "")
            {
                if (ReadFile(filename))
                    this.Text = filename;
            }

            load_instructions_list();

            cycle_nbr = 0;
            update_display();

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "rom";
            saveFileDialog1.Filter = "Rom files (rom*.*)|rom*.*|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = rommgr.working_folder;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;

                IniFile ini = new IniFile(System.Environment.CurrentDirectory + "\\" + "config.ini");
                ini.IniWriteValue("general", "last_microcode_open", filename);

                if (SaveFile(filename))
                    this.Text = filename;
            }
        }

        private void generateTASMTableToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Generate table?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveFileDialog1.FileName = "minicomputer.tab";
                saveFileDialog1.Filter = "Tasm files (*.tab)|*.tab|Tasm Byte files (*.tbb)|*.tbb|All files (*.*)|*.*";
                saveFileDialog1.InitialDirectory = rommgr.working_folder;

                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    rommgr.GenerateTasmTable(saveFileDialog1.FileName, saveFileDialog1.FilterIndex == 2);
                }
            }

        }

        //----------

        void send_msg(String s)
        {
            toolStripStatusLabel1.Text = s;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rommgr.current_filename != "")
            {
                if (MessageBox.Show("Save file?", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    rommgr.Write(rommgr.current_filename);
                }
            }
        }

        private void lstInstructions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstInstructions.SelectedIndex != -1 && list_names.Items.IndexOf(((InstructionItem)lstInstructions.SelectedItem).Value) != -1)
            {
                list_names.SelectedIndex = list_names.Items.IndexOf(((InstructionItem)lstInstructions.SelectedItem).Value);
                tabControl1.SelectedIndex = 0;
            }

        }

        private void control_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //////////////////////////





        private bool ReadFile(string filename)
        {

            bool status = rommgr.Read(filename);
            if (status)
            {

                foreach (String n in rommgr.NamesList())
                {
                    list_names.Items.Add(n);
                }

                set_readonly(true);
            }

            return status;
        }


        private void set_readonly(bool chk)
        {
            mnu_readonly.Checked = chk;

            foreach (String item in control_list.Items)
            {
                //   item.Enabled = !mnu_readonly.Checked;
            }

            memo_name.ReadOnly = mnu_readonly.Checked;
            memo_info.ReadOnly = mnu_readonly.Checked;
            control_info.Visible = chk;

            calculateAvgCyclesPerInstructionToolStripMenuItem.Visible = chk;

        }



        public bool SaveFile(string filename)
        {
            return rommgr.Write(filename);
        }



        private void disable_lst_refresh()
        {
            control_list.ItemCheck -= control_list_ItemCheck;
            list_names.SelectedIndexChanged -= list_names_SelectedIndexChanged;
            //list_cycle.SelectedIndexChanged -= lstCycles_SelectedIndexChanged;
            disabledLst = true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enable_lst_refresh()
        {
            if (disabledLst)
            {
                control_list.ItemCheck += control_list_ItemCheck;
                list_names.SelectedIndexChanged += list_names_SelectedIndexChanged;
                //list_cycle.SelectedIndexChanged += lstCycles_SelectedIndexChanged;
                disabledLst = false;

                control_info_refresh();
            }
        }

        private void onlineInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://sol-1.baffasoft.com.br/");
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();

            frmAbout.ShowDialog(this);

        }

        private void list_cycle_DrawItem(object sender, DrawItemEventArgs e)
        {

            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list_cycle.Items.Count)
            {
                string text = list_cycle.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                Font cfont = e.Font;

                SolidBrush backgroundBrush = (selected) ? new SolidBrush(SystemColors.Highlight) : new SolidBrush(SystemColors.MenuBar);
                SolidBrush foregroundBrush = (selected) ? new SolidBrush(SystemColors.HighlightText) : new SolidBrush(SystemColors.GrayText);

                if (!selected && rommgr.UsedCycle(instr_nbr, index))
                {
                    backgroundBrush = new SolidBrush(SystemColors.Window);
                    foregroundBrush = new SolidBrush(SystemColors.WindowText);
                }

                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:                
                g.DrawString(text, cfont, foregroundBrush, list_cycle.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void memo_name_Leave(object sender, EventArgs e)
        {
            write_cycle();
        }


        private void disable_cmb_refresh()
        {
            //////////////////////////
            //Next Micro-Instruction
            cmb_next_inst.SelectedIndexChanged -= cmb_next_inst_SelectedIndexChanged;
            cmb_cond_sel.SelectedIndexChanged -= cmb_cond_sel_SelectedIndexChanged;
            cmb_flags_src.SelectedIndexChanged -= cmb_flags_src_SelectedIndexChanged;
            //////////////////////////
            //MAR IN SRC
            cmb_mar_in_src.SelectedIndexChanged -= cmb_mar_in_src_SelectedIndexChanged;
            //////////////////////////
            //MDR IN/OUT SRC
            cmb_mdr_in_src.SelectedIndexChanged -= cmb_mdr_in_src_SelectedIndexChanged;
            cmb_mdr_out_src.SelectedIndexChanged -= cmb_mdr_out_src_SelectedIndexChanged;
            //////////////////////////
            //ALU INPUTS A/B
            cmb_alu_a_mux.SelectedIndexChanged -= cmb_alu_a_mux_SelectedIndexChanged;
            cmb_next_inst.SelectedIndexChanged -= cmb_alu_b_mux_SelectedIndexChanged;
            //////////////////////////
            //ALU OPERATIONS IN 
            cmbAluOp.SelectedIndexChanged -= cmbAluOp_SelectedIndexChanged;
            cmb_alu_cf_in.SelectedIndexChanged -= cmb_alu_cf_in_SelectedIndexChanged;
            cmb_alu_cf_out_inv.SelectedIndexChanged -= cmb_alu_cf_out_inv_SelectedIndexChanged;
            //////////////////////////
            //ALU to ZBUS
            cmb_zbus.SelectedIndexChanged -= cmb_zbus_SelectedIndexChanged;
            //////////////////////////
            //SHIFT SRC IN
            cmb_shift_src.SelectedIndexChanged -= cmb_shift_src_SelectedIndexChanged;
            //////////////////////////
            /// MICROFLAGS IN
            cmb_uzf.SelectedIndexChanged -= cmb_uzf_SelectedIndexChanged;
            cmb_ucf.SelectedIndexChanged -= cmb_ucf_SelectedIndexChanged;
            cmb_usf.SelectedIndexChanged -= cmb_usf_SelectedIndexChanged;
            cmb_uof.SelectedIndexChanged -= cmb_uof_SelectedIndexChanged;
            //////////////////////////
            /// Arithmetic Flags In
            cmb_zf_in.SelectedIndexChanged -= cmb_zf_in_SelectedIndexChanged;
            cmb_cf_in.SelectedIndexChanged -= cmb_cf_in_SelectedIndexChanged;
            cmb_sf_in.SelectedIndexChanged -= cmb_sf_in_SelectedIndexChanged;
            cmb_of_in.SelectedIndexChanged -= cmb_of_in_SelectedIndexChanged;

            disabledCmb = true;
        }

        private void enable_cmb_refresh()
        {
            if (disabledCmb)
            {
                //////////////////////////
                //Next Micro-Instruction
                cmb_next_inst.SelectedIndexChanged += cmb_next_inst_SelectedIndexChanged;
                cmb_cond_sel.SelectedIndexChanged += cmb_cond_sel_SelectedIndexChanged;
                cmb_flags_src.SelectedIndexChanged += cmb_flags_src_SelectedIndexChanged;
                //////////////////////////
                //MAR IN SRC
                cmb_mar_in_src.SelectedIndexChanged += cmb_mar_in_src_SelectedIndexChanged;
                //////////////////////////
                //MDR IN/OUT SRC
                cmb_mdr_in_src.SelectedIndexChanged += cmb_mdr_in_src_SelectedIndexChanged;
                cmb_mdr_out_src.SelectedIndexChanged += cmb_mdr_out_src_SelectedIndexChanged;
                //////////////////////////
                //ALU INPUTS A/B
                cmb_alu_a_mux.SelectedIndexChanged += cmb_alu_a_mux_SelectedIndexChanged;
                cmb_next_inst.SelectedIndexChanged += cmb_alu_b_mux_SelectedIndexChanged;
                //////////////////////////
                //ALU OPERATIONS IN 
                cmbAluOp.SelectedIndexChanged += cmbAluOp_SelectedIndexChanged;
                cmb_alu_cf_in.SelectedIndexChanged += cmb_alu_cf_in_SelectedIndexChanged;
                cmb_alu_cf_out_inv.SelectedIndexChanged += cmb_alu_cf_out_inv_SelectedIndexChanged;
                //////////////////////////
                //ALU to ZBUS
                cmb_zbus.SelectedIndexChanged += cmb_zbus_SelectedIndexChanged;
                //////////////////////////
                //SHIFT SRC IN
                cmb_shift_src.SelectedIndexChanged += cmb_shift_src_SelectedIndexChanged;
                //////////////////////////
                /// MICROFLAGS IN
                cmb_uzf.SelectedIndexChanged += cmb_uzf_SelectedIndexChanged;
                cmb_ucf.SelectedIndexChanged += cmb_ucf_SelectedIndexChanged;
                cmb_usf.SelectedIndexChanged += cmb_usf_SelectedIndexChanged;
                cmb_uof.SelectedIndexChanged += cmb_uof_SelectedIndexChanged;
                //////////////////////////
                /// Arithmetic Flags In
                cmb_zf_in.SelectedIndexChanged += cmb_zf_in_SelectedIndexChanged;
                cmb_cf_in.SelectedIndexChanged += cmb_cf_in_SelectedIndexChanged;
                cmb_sf_in.SelectedIndexChanged += cmb_sf_in_SelectedIndexChanged;
                cmb_of_in.SelectedIndexChanged += cmb_of_in_SelectedIndexChanged;
                disabledCmb = false;
            }
        }
    }
}


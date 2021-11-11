namespace sol1_simu
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.microcodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_new = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openRecentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.microcodeEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_readonly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateAvgCyclesPerInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTASMTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNotepad = new System.Windows.Forms.Button();
            this.btnCmd = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnHexEditor = new System.Windows.Forms.Button();
            this.cmdWorkingFolder = new System.Windows.Forms.Button();
            this.cmdBitRight = new System.Windows.Forms.Button();
            this.btnNextCycle = new System.Windows.Forms.Button();
            this.btnPrevCycle = new System.Windows.Forms.Button();
            this.btnBitLeft = new System.Windows.Forms.Button();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdShiftRight = new System.Windows.Forms.Button();
            this.btnShiftLeft = new System.Windows.Forms.Button();
            this.cmdPaste = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstInstructions = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cmb_of_in = new System.Windows.Forms.ComboBox();
            this.cmb_sf_in = new System.Windows.Forms.ComboBox();
            this.cmb_cf_in = new System.Windows.Forms.ComboBox();
            this.cmb_zf_in = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cmb_uof = new System.Windows.Forms.ComboBox();
            this.cmb_usf = new System.Windows.Forms.ComboBox();
            this.cmb_ucf = new System.Windows.Forms.ComboBox();
            this.cmb_uzf = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmb_shift_src = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmb_zbus = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmb_alu_cf_out_inv = new System.Windows.Forms.ComboBox();
            this.cmb_alu_cf_in = new System.Windows.Forms.ComboBox();
            this.cmbAluOp = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmb_alu_b_mux = new System.Windows.Forms.ComboBox();
            this.cmb_alu_a_mux = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmb_mdr_out_src = new System.Windows.Forms.ComboBox();
            this.cmb_mdr_in_src = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_mar_in_src = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_imm = new System.Windows.Forms.Button();
            this.btn_offset = new System.Windows.Forms.Button();
            this.txtInteger = new System.Windows.Forms.TextBox();
            this.cmb_flags_src = new System.Windows.Forms.ComboBox();
            this.cmb_cond_sel = new System.Windows.Forms.ComboBox();
            this.cmb_next_inst = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.control_info = new System.Windows.Forms.TextBox();
            this.memo_name = new System.Windows.Forms.TextBox();
            this.control_list = new System.Windows.Forms.CheckedListBox();
            this.memo_info = new System.Windows.Forms.TextBox();
            this.list_cycle = new System.Windows.Forms.ListBox();
            this.list_names = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.microcodeFileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1299, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // microcodeFileToolStripMenuItem
            // 
            this.microcodeFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_new,
            this.toolStripMenuItem1,
            this.openRecentToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.quitToolStripMenuItem});
            this.microcodeFileToolStripMenuItem.Name = "microcodeFileToolStripMenuItem";
            this.microcodeFileToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.microcodeFileToolStripMenuItem.Text = "Microcode &File";
            // 
            // mnu_new
            // 
            this.mnu_new.Name = "mnu_new";
            this.mnu_new.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnu_new.Size = new System.Drawing.Size(183, 22);
            this.mnu_new.Text = "New";
            this.mnu_new.Click += new System.EventHandler(this.mnu_new_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // openRecentToolStripMenuItem
            // 
            this.openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            this.openRecentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.openRecentToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openRecentToolStripMenuItem.Text = "Open Recent";
            this.openRecentToolStripMenuItem.Click += new System.EventHandler(this.openRecentToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyInstructionToolStripMenuItem,
            this.pasteInstructionToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyInstructionToolStripMenuItem
            // 
            this.copyInstructionToolStripMenuItem.Name = "copyInstructionToolStripMenuItem";
            this.copyInstructionToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F9";
            this.copyInstructionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.copyInstructionToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.copyInstructionToolStripMenuItem.Text = "Copy Instruction";
            this.copyInstructionToolStripMenuItem.Click += new System.EventHandler(this.copyInstructionToolStripMenuItem_Click);
            // 
            // pasteInstructionToolStripMenuItem
            // 
            this.pasteInstructionToolStripMenuItem.Name = "pasteInstructionToolStripMenuItem";
            this.pasteInstructionToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F10";
            this.pasteInstructionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F10)));
            this.pasteInstructionToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.pasteInstructionToolStripMenuItem.Text = "Paste Instruction";
            this.pasteInstructionToolStripMenuItem.Click += new System.EventHandler(this.pasteInstructionToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.microcodeEditorToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // microcodeEditorToolStripMenuItem
            // 
            this.microcodeEditorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_readonly});
            this.microcodeEditorToolStripMenuItem.Name = "microcodeEditorToolStripMenuItem";
            this.microcodeEditorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.microcodeEditorToolStripMenuItem.Text = "Microcode Editor";
            // 
            // mnu_readonly
            // 
            this.mnu_readonly.Name = "mnu_readonly";
            this.mnu_readonly.Size = new System.Drawing.Size(164, 22);
            this.mnu_readonly.Text = "Read-Only Mode";
            this.mnu_readonly.Click += new System.EventHandler(this.mnu_readonly_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calculateAvgCyclesPerInstructionToolStripMenuItem,
            this.generateTASMTableToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // calculateAvgCyclesPerInstructionToolStripMenuItem
            // 
            this.calculateAvgCyclesPerInstructionToolStripMenuItem.Name = "calculateAvgCyclesPerInstructionToolStripMenuItem";
            this.calculateAvgCyclesPerInstructionToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.calculateAvgCyclesPerInstructionToolStripMenuItem.Text = "Calculate Avg. Cycles per Instruction";
            this.calculateAvgCyclesPerInstructionToolStripMenuItem.Click += new System.EventHandler(this.calculateAvgCyclesPerInstructionToolStripMenuItem_Click);
            // 
            // generateTASMTableToolStripMenuItem
            // 
            this.generateTASMTableToolStripMenuItem.Name = "generateTASMTableToolStripMenuItem";
            this.generateTASMTableToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.generateTASMTableToolStripMenuItem.Text = "Generate TASM Table";
            this.generateTASMTableToolStripMenuItem.Click += new System.EventHandler(this.generateTASMTableToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineInformationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // onlineInformationToolStripMenuItem
            // 
            this.onlineInformationToolStripMenuItem.Name = "onlineInformationToolStripMenuItem";
            this.onlineInformationToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.onlineInformationToolStripMenuItem.Text = "Online Information...";
            this.onlineInformationToolStripMenuItem.Click += new System.EventHandler(this.onlineInformationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(181, 6);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnNotepad);
            this.panel3.Controls.Add(this.btnCmd);
            this.panel3.Controls.Add(this.btnCalc);
            this.panel3.Controls.Add(this.btnHexEditor);
            this.panel3.Controls.Add(this.cmdWorkingFolder);
            this.panel3.Controls.Add(this.cmdBitRight);
            this.panel3.Controls.Add(this.btnNextCycle);
            this.panel3.Controls.Add(this.btnPrevCycle);
            this.panel3.Controls.Add(this.btnBitLeft);
            this.panel3.Controls.Add(this.cmdReset);
            this.panel3.Controls.Add(this.cmdShiftRight);
            this.panel3.Controls.Add(this.btnShiftLeft);
            this.panel3.Controls.Add(this.cmdPaste);
            this.panel3.Controls.Add(this.cmdCopy);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1299, 60);
            this.panel3.TabIndex = 3;
            // 
            // btnNotepad
            // 
            this.btnNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotepad.Location = new System.Drawing.Point(1217, 31);
            this.btnNotepad.Name = "btnNotepad";
            this.btnNotepad.Size = new System.Drawing.Size(75, 23);
            this.btnNotepad.TabIndex = 14;
            this.btnNotepad.Text = "Notepad";
            this.btnNotepad.UseVisualStyleBackColor = true;
            this.btnNotepad.Click += new System.EventHandler(this.btnNotepad_Click);
            // 
            // btnCmd
            // 
            this.btnCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCmd.Location = new System.Drawing.Point(1136, 31);
            this.btnCmd.Name = "btnCmd";
            this.btnCmd.Size = new System.Drawing.Size(75, 23);
            this.btnCmd.TabIndex = 13;
            this.btnCmd.Text = "Command";
            this.btnCmd.UseVisualStyleBackColor = true;
            this.btnCmd.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalc.Location = new System.Drawing.Point(1056, 31);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 12;
            this.btnCalc.Text = "Calculator";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnHexEditor
            // 
            this.btnHexEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHexEditor.Location = new System.Drawing.Point(975, 31);
            this.btnHexEditor.Name = "btnHexEditor";
            this.btnHexEditor.Size = new System.Drawing.Size(75, 23);
            this.btnHexEditor.TabIndex = 11;
            this.btnHexEditor.Text = "Hex editor";
            this.btnHexEditor.UseVisualStyleBackColor = true;
            this.btnHexEditor.Click += new System.EventHandler(this.btnHexEditor_Click);
            // 
            // cmdWorkingFolder
            // 
            this.cmdWorkingFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWorkingFolder.Location = new System.Drawing.Point(874, 31);
            this.cmdWorkingFolder.Name = "cmdWorkingFolder";
            this.cmdWorkingFolder.Size = new System.Drawing.Size(96, 23);
            this.cmdWorkingFolder.TabIndex = 10;
            this.cmdWorkingFolder.Text = "Working Folder";
            this.cmdWorkingFolder.UseVisualStyleBackColor = true;
            this.cmdWorkingFolder.Click += new System.EventHandler(this.cmdWorkingFolder_Click);
            // 
            // cmdBitRight
            // 
            this.cmdBitRight.Location = new System.Drawing.Point(245, 31);
            this.cmdBitRight.Name = "cmdBitRight";
            this.cmdBitRight.Size = new System.Drawing.Size(75, 23);
            this.cmdBitRight.TabIndex = 9;
            this.cmdBitRight.Text = ">>>";
            this.cmdBitRight.UseVisualStyleBackColor = true;
            this.cmdBitRight.Click += new System.EventHandler(this.cmdBitRight_Click);
            // 
            // btnNextCycle
            // 
            this.btnNextCycle.Location = new System.Drawing.Point(165, 31);
            this.btnNextCycle.Name = "btnNextCycle";
            this.btnNextCycle.Size = new System.Drawing.Size(75, 23);
            this.btnNextCycle.TabIndex = 8;
            this.btnNextCycle.Text = ">";
            this.btnNextCycle.UseVisualStyleBackColor = true;
            this.btnNextCycle.Click += new System.EventHandler(this.btnNextCycle_Click);
            // 
            // btnPrevCycle
            // 
            this.btnPrevCycle.Location = new System.Drawing.Point(84, 31);
            this.btnPrevCycle.Name = "btnPrevCycle";
            this.btnPrevCycle.Size = new System.Drawing.Size(75, 23);
            this.btnPrevCycle.TabIndex = 7;
            this.btnPrevCycle.Text = "<";
            this.btnPrevCycle.UseVisualStyleBackColor = true;
            this.btnPrevCycle.Click += new System.EventHandler(this.btnPrevCycle_Click);
            // 
            // btnBitLeft
            // 
            this.btnBitLeft.Location = new System.Drawing.Point(4, 31);
            this.btnBitLeft.Name = "btnBitLeft";
            this.btnBitLeft.Size = new System.Drawing.Size(75, 23);
            this.btnBitLeft.TabIndex = 6;
            this.btnBitLeft.Text = "<<<";
            this.btnBitLeft.UseVisualStyleBackColor = true;
            this.btnBitLeft.Click += new System.EventHandler(this.btnBitLeft_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(336, 3);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(75, 23);
            this.cmdReset.TabIndex = 4;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdShiftRight
            // 
            this.cmdShiftRight.Location = new System.Drawing.Point(246, 3);
            this.cmdShiftRight.Name = "cmdShiftRight";
            this.cmdShiftRight.Size = new System.Drawing.Size(75, 23);
            this.cmdShiftRight.TabIndex = 3;
            this.cmdShiftRight.Text = "Shift Right";
            this.cmdShiftRight.UseVisualStyleBackColor = true;
            this.cmdShiftRight.Click += new System.EventHandler(this.cmdShiftRight_Click);
            // 
            // btnShiftLeft
            // 
            this.btnShiftLeft.Location = new System.Drawing.Point(165, 3);
            this.btnShiftLeft.Name = "btnShiftLeft";
            this.btnShiftLeft.Size = new System.Drawing.Size(75, 23);
            this.btnShiftLeft.TabIndex = 2;
            this.btnShiftLeft.Text = "Shift Left";
            this.btnShiftLeft.UseVisualStyleBackColor = true;
            this.btnShiftLeft.Click += new System.EventHandler(this.btnShiftLeft_Click);
            // 
            // cmdPaste
            // 
            this.cmdPaste.Location = new System.Drawing.Point(84, 3);
            this.cmdPaste.Name = "cmdPaste";
            this.cmdPaste.Size = new System.Drawing.Size(75, 23);
            this.cmdPaste.TabIndex = 1;
            this.cmdPaste.Text = "Paste";
            this.cmdPaste.UseVisualStyleBackColor = true;
            this.cmdPaste.Click += new System.EventHandler(this.cmdPaste_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Location = new System.Drawing.Point(3, 3);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdCopy.TabIndex = 0;
            this.cmdCopy.Text = "Copy";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lstInstructions);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1291, 730);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Instruction List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstInstructions
            // 
            this.lstInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInstructions.FormattingEnabled = true;
            this.lstInstructions.Location = new System.Drawing.Point(3, 3);
            this.lstInstructions.Name = "lstInstructions";
            this.lstInstructions.Size = new System.Drawing.Size(1285, 724);
            this.lstInstructions.Sorted = true;
            this.lstInstructions.TabIndex = 1;
            this.lstInstructions.SelectedIndexChanged += new System.EventHandler(this.lstInstructions_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1291, 730);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Program data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.list_cycle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.list_names);
            this.splitContainer1.Size = new System.Drawing.Size(1285, 724);
            this.splitContainer1.SplitterDistance = 687;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.groupBox9);
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Controls.Add(this.groupBox7);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 721);
            this.panel2.TabIndex = 1;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cmb_of_in);
            this.groupBox9.Controls.Add(this.cmb_sf_in);
            this.groupBox9.Controls.Add(this.cmb_cf_in);
            this.groupBox9.Controls.Add(this.cmb_zf_in);
            this.groupBox9.Location = new System.Drawing.Point(3, 707);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(173, 130);
            this.groupBox9.TabIndex = 19;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Arithmetic Flags In";
            // 
            // cmb_of_in
            // 
            this.cmb_of_in.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_of_in.FormattingEnabled = true;
            this.cmb_of_in.Items.AddRange(new object[] {
            "unchanged",
            "ALU_OF",
            "ZBUS_7",
            "ZBUS_3",
            "(U_SF) XOR (ZBUS_7)"});
            this.cmb_of_in.Location = new System.Drawing.Point(6, 100);
            this.cmb_of_in.Name = "cmb_of_in";
            this.cmb_of_in.Size = new System.Drawing.Size(161, 21);
            this.cmb_of_in.TabIndex = 3;
            this.cmb_of_in.SelectedIndexChanged += new System.EventHandler(this.cmb_of_in_SelectedIndexChanged);
            // 
            // cmb_sf_in
            // 
            this.cmb_sf_in.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_sf_in.FormattingEnabled = true;
            this.cmb_sf_in.Items.AddRange(new object[] {
            "unchanged",
            "ZBUS_7",
            "GND",
            "ZBUS_2"});
            this.cmb_sf_in.Location = new System.Drawing.Point(6, 73);
            this.cmb_sf_in.Name = "cmb_sf_in";
            this.cmb_sf_in.Size = new System.Drawing.Size(161, 21);
            this.cmb_sf_in.TabIndex = 2;
            this.cmb_sf_in.SelectedIndexChanged += new System.EventHandler(this.cmb_sf_in_SelectedIndexChanged);
            // 
            // cmb_cf_in
            // 
            this.cmb_cf_in.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cf_in.FormattingEnabled = true;
            this.cmb_cf_in.Items.AddRange(new object[] {
            "unchanged",
            "ALU Final CF",
            "ALU_OUTPUT_0",
            "ZBUS_1",
            "ALU_OUTPUT_7"});
            this.cmb_cf_in.Location = new System.Drawing.Point(6, 46);
            this.cmb_cf_in.Name = "cmb_cf_in";
            this.cmb_cf_in.Size = new System.Drawing.Size(161, 21);
            this.cmb_cf_in.TabIndex = 1;
            this.cmb_cf_in.SelectedIndexChanged += new System.EventHandler(this.cmb_cf_in_SelectedIndexChanged);
            // 
            // cmb_zf_in
            // 
            this.cmb_zf_in.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_zf_in.FormattingEnabled = true;
            this.cmb_zf_in.Items.AddRange(new object[] {
            "unchanged",
            "ALU_ZF",
            "ALU_ZF && ZF",
            "ZBUS_0"});
            this.cmb_zf_in.Location = new System.Drawing.Point(6, 19);
            this.cmb_zf_in.Name = "cmb_zf_in";
            this.cmb_zf_in.Size = new System.Drawing.Size(161, 21);
            this.cmb_zf_in.TabIndex = 0;
            this.cmb_zf_in.SelectedIndexChanged += new System.EventHandler(this.cmb_zf_in_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cmb_uof);
            this.groupBox8.Controls.Add(this.cmb_usf);
            this.groupBox8.Controls.Add(this.cmb_ucf);
            this.groupBox8.Controls.Add(this.cmb_uzf);
            this.groupBox8.Location = new System.Drawing.Point(3, 571);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(173, 130);
            this.groupBox8.TabIndex = 18;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Micro Flags In";
            // 
            // cmb_uof
            // 
            this.cmb_uof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_uof.FormattingEnabled = true;
            this.cmb_uof.Items.AddRange(new object[] {
            "unchanged",
            "ALU_OF"});
            this.cmb_uof.Location = new System.Drawing.Point(6, 100);
            this.cmb_uof.Name = "cmb_uof";
            this.cmb_uof.Size = new System.Drawing.Size(161, 21);
            this.cmb_uof.TabIndex = 3;
            this.cmb_uof.SelectedIndexChanged += new System.EventHandler(this.cmb_uof_SelectedIndexChanged);
            // 
            // cmb_usf
            // 
            this.cmb_usf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_usf.FormattingEnabled = true;
            this.cmb_usf.Items.AddRange(new object[] {
            "unchanged",
            "Z_BUS_7"});
            this.cmb_usf.Location = new System.Drawing.Point(6, 73);
            this.cmb_usf.Name = "cmb_usf";
            this.cmb_usf.Size = new System.Drawing.Size(161, 21);
            this.cmb_usf.TabIndex = 2;
            this.cmb_usf.SelectedIndexChanged += new System.EventHandler(this.cmb_usf_SelectedIndexChanged);
            // 
            // cmb_ucf
            // 
            this.cmb_ucf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ucf.FormattingEnabled = true;
            this.cmb_ucf.Items.AddRange(new object[] {
            "unchanged",
            "ALU Final CF",
            "ALU_OUTPUT_0",
            "ALU_OUTPUT_7"});
            this.cmb_ucf.Location = new System.Drawing.Point(6, 46);
            this.cmb_ucf.Name = "cmb_ucf";
            this.cmb_ucf.Size = new System.Drawing.Size(161, 21);
            this.cmb_ucf.TabIndex = 1;
            this.cmb_ucf.SelectedIndexChanged += new System.EventHandler(this.cmb_ucf_SelectedIndexChanged);
            // 
            // cmb_uzf
            // 
            this.cmb_uzf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_uzf.FormattingEnabled = true;
            this.cmb_uzf.Items.AddRange(new object[] {
            "unchanged",
            "ALU_ZF",
            "ALU_ZF && uZF",
            "gnd"});
            this.cmb_uzf.Location = new System.Drawing.Point(6, 19);
            this.cmb_uzf.Name = "cmb_uzf";
            this.cmb_uzf.Size = new System.Drawing.Size(161, 21);
            this.cmb_uzf.TabIndex = 0;
            this.cmb_uzf.SelectedIndexChanged += new System.EventHandler(this.cmb_uzf_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmb_shift_src);
            this.groupBox7.Location = new System.Drawing.Point(3, 518);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(173, 47);
            this.groupBox7.TabIndex = 17;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Shift Src";
            // 
            // cmb_shift_src
            // 
            this.cmb_shift_src.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_shift_src.FormattingEnabled = true;
            this.cmb_shift_src.Items.AddRange(new object[] {
            "gnd",
            "uCF",
            "CF",
            "ALU Result [0]",
            "ALU Result [7]"});
            this.cmb_shift_src.Location = new System.Drawing.Point(6, 19);
            this.cmb_shift_src.Name = "cmb_shift_src";
            this.cmb_shift_src.Size = new System.Drawing.Size(161, 21);
            this.cmb_shift_src.TabIndex = 0;
            this.cmb_shift_src.SelectedIndexChanged += new System.EventHandler(this.cmb_shift_src_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmb_zbus);
            this.groupBox6.Location = new System.Drawing.Point(3, 465);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(173, 47);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "ALU to ZBUS";
            // 
            // cmb_zbus
            // 
            this.cmb_zbus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_zbus.FormattingEnabled = true;
            this.cmb_zbus.Items.AddRange(new object[] {
            "Normal ALU Result",
            "Shifted Right",
            "Shifted Left",
            "Sign Extend"});
            this.cmb_zbus.Location = new System.Drawing.Point(6, 19);
            this.cmb_zbus.Name = "cmb_zbus";
            this.cmb_zbus.Size = new System.Drawing.Size(161, 21);
            this.cmb_zbus.TabIndex = 0;
            this.cmb_zbus.SelectedIndexChanged += new System.EventHandler(this.cmb_zbus_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmb_alu_cf_out_inv);
            this.groupBox5.Controls.Add(this.cmb_alu_cf_in);
            this.groupBox5.Controls.Add(this.cmbAluOp);
            this.groupBox5.Location = new System.Drawing.Point(3, 357);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(173, 102);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ALU Operation / Carry In";
            // 
            // cmb_alu_cf_out_inv
            // 
            this.cmb_alu_cf_out_inv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_alu_cf_out_inv.FormattingEnabled = true;
            this.cmb_alu_cf_out_inv.Items.AddRange(new object[] {
            "Carry-out not inverted",
            "Carry-out inverted"});
            this.cmb_alu_cf_out_inv.Location = new System.Drawing.Point(6, 73);
            this.cmb_alu_cf_out_inv.Name = "cmb_alu_cf_out_inv";
            this.cmb_alu_cf_out_inv.Size = new System.Drawing.Size(161, 21);
            this.cmb_alu_cf_out_inv.TabIndex = 2;
            this.cmb_alu_cf_out_inv.SelectedIndexChanged += new System.EventHandler(this.cmb_alu_cf_out_inv_SelectedIndexChanged);
            // 
            // cmb_alu_cf_in
            // 
            this.cmb_alu_cf_in.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_alu_cf_in.FormattingEnabled = true;
            this.cmb_alu_cf_in.Items.AddRange(new object[] {
            "vcc",
            "cf",
            "u_cf",
            "",
            "gnd",
            "~cf",
            "~u_cf"});
            this.cmb_alu_cf_in.Location = new System.Drawing.Point(6, 46);
            this.cmb_alu_cf_in.Name = "cmb_alu_cf_in";
            this.cmb_alu_cf_in.Size = new System.Drawing.Size(161, 21);
            this.cmb_alu_cf_in.TabIndex = 1;
            this.cmb_alu_cf_in.SelectedIndexChanged += new System.EventHandler(this.cmb_alu_cf_in_SelectedIndexChanged);
            // 
            // cmbAluOp
            // 
            this.cmbAluOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAluOp.FormattingEnabled = true;
            this.cmbAluOp.Items.AddRange(new object[] {
            "ALU Operation",
            "",
            "plus",
            "minus",
            "and",
            "or",
            "xor",
            "A",
            "B",
            "not A",
            "not B",
            "nand",
            "nor",
            "nxor"});
            this.cmbAluOp.Location = new System.Drawing.Point(6, 19);
            this.cmbAluOp.Name = "cmbAluOp";
            this.cmbAluOp.Size = new System.Drawing.Size(161, 21);
            this.cmbAluOp.TabIndex = 0;
            this.cmbAluOp.SelectedIndexChanged += new System.EventHandler(this.cmbAluOp_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmb_alu_b_mux);
            this.groupBox4.Controls.Add(this.cmb_alu_a_mux);
            this.groupBox4.Location = new System.Drawing.Point(3, 274);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(173, 77);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ALU Inputs A/B";
            // 
            // cmb_alu_b_mux
            // 
            this.cmb_alu_b_mux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_alu_b_mux.FormattingEnabled = true;
            this.cmb_alu_b_mux.Items.AddRange(new object[] {
            "immediate",
            "",
            "",
            "",
            "mdr_l",
            "mdr_h",
            "tdr_l",
            "tdr_h"});
            this.cmb_alu_b_mux.Location = new System.Drawing.Point(6, 46);
            this.cmb_alu_b_mux.Name = "cmb_alu_b_mux";
            this.cmb_alu_b_mux.Size = new System.Drawing.Size(161, 21);
            this.cmb_alu_b_mux.TabIndex = 1;
            this.cmb_alu_b_mux.SelectedIndexChanged += new System.EventHandler(this.cmb_alu_b_mux_SelectedIndexChanged);
            // 
            // cmb_alu_a_mux
            // 
            this.cmb_alu_a_mux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_alu_a_mux.FormattingEnabled = true;
            this.cmb_alu_a_mux.Items.AddRange(new object[] {
            "0x00: al",
            "0x01: ah",
            "0x02: bl",
            "0x03: bh",
            "0x04: cl",
            "0x05: ch",
            "0x06: dl",
            "0x07: dh",
            "0x08: sp_l",
            "0x09: sp_h",
            "0x0A: bp_l",
            "0x0B: bp_h",
            "0x0C: si_l",
            "0x0D: si_h",
            "0x0E: di_l",
            "0x0F: di_h",
            "0x10: pc_l",
            "0x11: pc_h",
            "0x12: mar_l",
            "0x13: mar_h",
            "0x14: mdr_l",
            "0x15: mdr_h",
            "0x16: tdr_l",
            "0x17: tdr_h",
            "0x18: ksp_l",
            "0x19: ksp_h",
            "0x1A: int_vector",
            "0x1B: int_masks",
            "0x1C: int_status",
            "0x1D: ",
            "0x1E: ",
            "0x1F: ",
            "0x20: arithmetic_flags",
            "0x21: status_flags",
            "0x22: gl",
            "0x23: gh"});
            this.cmb_alu_a_mux.Location = new System.Drawing.Point(6, 19);
            this.cmb_alu_a_mux.Name = "cmb_alu_a_mux";
            this.cmb_alu_a_mux.Size = new System.Drawing.Size(161, 21);
            this.cmb_alu_a_mux.TabIndex = 0;
            this.cmb_alu_a_mux.SelectedIndexChanged += new System.EventHandler(this.cmb_alu_a_mux_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmb_mdr_out_src);
            this.groupBox3.Controls.Add(this.cmb_mdr_in_src);
            this.groupBox3.Location = new System.Drawing.Point(3, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 77);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MDR In/Out Src";
            // 
            // cmb_mdr_out_src
            // 
            this.cmb_mdr_out_src.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mdr_out_src.FormattingEnabled = true;
            this.cmb_mdr_out_src.Items.AddRange(new object[] {
            "MDR Low",
            "MDR High"});
            this.cmb_mdr_out_src.Location = new System.Drawing.Point(6, 46);
            this.cmb_mdr_out_src.Name = "cmb_mdr_out_src";
            this.cmb_mdr_out_src.Size = new System.Drawing.Size(161, 21);
            this.cmb_mdr_out_src.TabIndex = 1;
            this.cmb_mdr_out_src.SelectedIndexChanged += new System.EventHandler(this.cmb_mdr_out_src_SelectedIndexChanged);
            // 
            // cmb_mdr_in_src
            // 
            this.cmb_mdr_in_src.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mdr_in_src.FormattingEnabled = true;
            this.cmb_mdr_in_src.Items.AddRange(new object[] {
            "ZBus",
            "DataBus"});
            this.cmb_mdr_in_src.Location = new System.Drawing.Point(6, 19);
            this.cmb_mdr_in_src.Name = "cmb_mdr_in_src";
            this.cmb_mdr_in_src.Size = new System.Drawing.Size(161, 21);
            this.cmb_mdr_in_src.TabIndex = 0;
            this.cmb_mdr_in_src.SelectedIndexChanged += new System.EventHandler(this.cmb_mdr_in_src_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_mar_in_src);
            this.groupBox2.Location = new System.Drawing.Point(3, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 47);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MAR In Src";
            // 
            // cmb_mar_in_src
            // 
            this.cmb_mar_in_src.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mar_in_src.FormattingEnabled = true;
            this.cmb_mar_in_src.Items.AddRange(new object[] {
            "MAR IN : ZBus",
            "MAR IN : PC"});
            this.cmb_mar_in_src.Location = new System.Drawing.Point(6, 19);
            this.cmb_mar_in_src.Name = "cmb_mar_in_src";
            this.cmb_mar_in_src.Size = new System.Drawing.Size(161, 21);
            this.cmb_mar_in_src.TabIndex = 0;
            this.cmb_mar_in_src.SelectedIndexChanged += new System.EventHandler(this.cmb_mar_in_src_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_imm);
            this.groupBox1.Controls.Add(this.btn_offset);
            this.groupBox1.Controls.Add(this.txtInteger);
            this.groupBox1.Controls.Add(this.cmb_flags_src);
            this.groupBox1.Controls.Add(this.cmb_cond_sel);
            this.groupBox1.Controls.Add(this.cmb_next_inst);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 129);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Next Micro-Instruction";
            // 
            // btn_imm
            // 
            this.btn_imm.Location = new System.Drawing.Point(115, 100);
            this.btn_imm.Name = "btn_imm";
            this.btn_imm.Size = new System.Drawing.Size(50, 20);
            this.btn_imm.TabIndex = 9;
            this.btn_imm.Text = "Imm";
            this.btn_imm.UseVisualStyleBackColor = true;
            this.btn_imm.Click += new System.EventHandler(this.btn_imm_Click);
            // 
            // btn_offset
            // 
            this.btn_offset.Location = new System.Drawing.Point(59, 100);
            this.btn_offset.Name = "btn_offset";
            this.btn_offset.Size = new System.Drawing.Size(50, 20);
            this.btn_offset.TabIndex = 8;
            this.btn_offset.Text = "Offset";
            this.btn_offset.UseVisualStyleBackColor = true;
            this.btn_offset.Click += new System.EventHandler(this.btn_offset_Click);
            // 
            // txtInteger
            // 
            this.txtInteger.Location = new System.Drawing.Point(6, 100);
            this.txtInteger.Name = "txtInteger";
            this.txtInteger.Size = new System.Drawing.Size(47, 20);
            this.txtInteger.TabIndex = 7;
            this.txtInteger.Text = "0";
            // 
            // cmb_flags_src
            // 
            this.cmb_flags_src.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_flags_src.FormattingEnabled = true;
            this.cmb_flags_src.Items.AddRange(new object[] {
            "CPU Flags",
            "Microcode Flags"});
            this.cmb_flags_src.Location = new System.Drawing.Point(6, 73);
            this.cmb_flags_src.Name = "cmb_flags_src";
            this.cmb_flags_src.Size = new System.Drawing.Size(159, 21);
            this.cmb_flags_src.TabIndex = 6;
            this.cmb_flags_src.SelectedIndexChanged += new System.EventHandler(this.cmb_flags_src_SelectedIndexChanged);
            // 
            // cmb_cond_sel
            // 
            this.cmb_cond_sel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cond_sel.FormattingEnabled = true;
            this.cmb_cond_sel.Items.AddRange(new object[] {
            "zf",
            "cf / LU",
            "sf",
            "of",
            "L",
            "LE",
            "LEU",
            "DMA_REQ",
            "STATUS_MODE",
            "WAIT",
            "INT_PENDING",
            "EXT_INPUT",
            "STATUS_DIR",
            "DISPLAY_LOAD",
            "unused",
            "unused",
            "~zf",
            "~cf / GEU",
            "~sf",
            "~of",
            "GE",
            "G",
            "GU",
            "~DMA_REQ",
            "~STATUS_MODE",
            "~WAIT",
            "~INT_PENDING",
            "~EXT_PENDING",
            "~STATUS_DIR",
            "~DISPLAY_R_LOAD",
            "unused",
            "unused"});
            this.cmb_cond_sel.Location = new System.Drawing.Point(6, 46);
            this.cmb_cond_sel.Name = "cmb_cond_sel";
            this.cmb_cond_sel.Size = new System.Drawing.Size(159, 21);
            this.cmb_cond_sel.TabIndex = 5;
            this.cmb_cond_sel.SelectedIndexChanged += new System.EventHandler(this.cmb_cond_sel_SelectedIndexChanged);
            // 
            // cmb_next_inst
            // 
            this.cmb_next_inst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_next_inst.FormattingEnabled = true;
            this.cmb_next_inst.Items.AddRange(new object[] {
            "Next by Offset",
            "Branch",
            "Next is Fetch",
            "Next by IR"});
            this.cmb_next_inst.Location = new System.Drawing.Point(6, 19);
            this.cmb_next_inst.Name = "cmb_next_inst";
            this.cmb_next_inst.Size = new System.Drawing.Size(159, 21);
            this.cmb_next_inst.TabIndex = 4;
            this.cmb_next_inst.SelectedIndexChanged += new System.EventHandler(this.cmb_next_inst_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.control_info);
            this.panel1.Controls.Add(this.memo_name);
            this.panel1.Controls.Add(this.control_list);
            this.panel1.Controls.Add(this.memo_info);
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 743);
            this.panel1.TabIndex = 4;
            // 
            // control_info
            // 
            this.control_info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.control_info.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_info.Location = new System.Drawing.Point(0, 0);
            this.control_info.Multiline = true;
            this.control_info.Name = "control_info";
            this.control_info.ReadOnly = true;
            this.control_info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.control_info.Size = new System.Drawing.Size(438, 499);
            this.control_info.TabIndex = 10;
            this.control_info.Visible = false;
            this.control_info.WordWrap = false;
            // 
            // memo_name
            // 
            this.memo_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memo_name.Location = new System.Drawing.Point(0, 509);
            this.memo_name.Name = "memo_name";
            this.memo_name.Size = new System.Drawing.Size(438, 20);
            this.memo_name.TabIndex = 9;
            this.memo_name.TextChanged += new System.EventHandler(this.memo_name_TextChanged);
            this.memo_name.Leave += new System.EventHandler(this.memo_name_Leave);
            // 
            // control_list
            // 
            this.control_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.control_list.FormattingEnabled = true;
            this.control_list.Location = new System.Drawing.Point(0, 0);
            this.control_list.Name = "control_list";
            this.control_list.Size = new System.Drawing.Size(438, 499);
            this.control_list.TabIndex = 8;
            this.control_list.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.control_list_ItemCheck);
            this.control_list.SelectedIndexChanged += new System.EventHandler(this.control_list_SelectedIndexChanged);
            // 
            // memo_info
            // 
            this.memo_info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memo_info.Location = new System.Drawing.Point(0, 537);
            this.memo_info.Multiline = true;
            this.memo_info.Name = "memo_info";
            this.memo_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.memo_info.Size = new System.Drawing.Size(438, 184);
            this.memo_info.TabIndex = 4;
            this.memo_info.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memo_info_KeyDown);
            this.memo_info.KeyUp += new System.Windows.Forms.KeyEventHandler(this.memo_info_KeyUp);
            // 
            // list_cycle
            // 
            this.list_cycle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_cycle.Dock = System.Windows.Forms.DockStyle.Right;
            this.list_cycle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.list_cycle.FormattingEnabled = true;
            this.list_cycle.Location = new System.Drawing.Point(643, 0);
            this.list_cycle.Name = "list_cycle";
            this.list_cycle.Size = new System.Drawing.Size(44, 724);
            this.list_cycle.TabIndex = 3;
            this.list_cycle.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.list_cycle_DrawItem);
            this.list_cycle.SelectedIndexChanged += new System.EventHandler(this.lstCycles_SelectedIndexChanged);
            this.list_cycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.list_cycle_KeyPress);
            // 
            // list_names
            // 
            this.list_names.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_names.FormattingEnabled = true;
            this.list_names.Location = new System.Drawing.Point(0, 0);
            this.list_names.Name = "list_names";
            this.list_names.Size = new System.Drawing.Size(594, 724);
            this.list_names.TabIndex = 0;
            this.list_names.SelectedIndexChanged += new System.EventHandler(this.list_names_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1299, 756);
            this.tabControl1.TabIndex = 2;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 840);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1299, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 862);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(639, 479);
            this.Name = "FrmMain";
            this.Text = "Sol-1 Simu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem microcodeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_new;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openRecentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem microcodeEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_readonly;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNotepad;
        private System.Windows.Forms.Button btnCmd;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnHexEditor;
        private System.Windows.Forms.Button cmdWorkingFolder;
        private System.Windows.Forms.Button cmdBitRight;
        private System.Windows.Forms.Button btnNextCycle;
        private System.Windows.Forms.Button btnPrevCycle;
        private System.Windows.Forms.Button btnBitLeft;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Button cmdShiftRight;
        private System.Windows.Forms.Button btnShiftLeft;
        private System.Windows.Forms.Button cmdPaste;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.ToolStripMenuItem copyInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateAvgCyclesPerInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateTASMTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteInstructionToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstInstructions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cmb_of_in;
        private System.Windows.Forms.ComboBox cmb_sf_in;
        private System.Windows.Forms.ComboBox cmb_cf_in;
        private System.Windows.Forms.ComboBox cmb_zf_in;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cmb_uof;
        private System.Windows.Forms.ComboBox cmb_usf;
        private System.Windows.Forms.ComboBox cmb_ucf;
        private System.Windows.Forms.ComboBox cmb_uzf;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmb_shift_src;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmb_zbus;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmb_alu_cf_out_inv;
        private System.Windows.Forms.ComboBox cmb_alu_cf_in;
        private System.Windows.Forms.ComboBox cmbAluOp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmb_alu_b_mux;
        private System.Windows.Forms.ComboBox cmb_alu_a_mux;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_mdr_out_src;
        private System.Windows.Forms.ComboBox cmb_mdr_in_src;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_mar_in_src;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_imm;
        private System.Windows.Forms.Button btn_offset;
        private System.Windows.Forms.TextBox txtInteger;
        private System.Windows.Forms.ComboBox cmb_flags_src;
        private System.Windows.Forms.ComboBox cmb_cond_sel;
        private System.Windows.Forms.ComboBox cmb_next_inst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox memo_name;
        private System.Windows.Forms.CheckedListBox control_list;
        private System.Windows.Forms.TextBox memo_info;
        private System.Windows.Forms.ListBox list_cycle;
        private System.Windows.Forms.ListBox list_names;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.TextBox control_info;
    }
}


namespace SoundPathDemo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.profilesBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.dEMOPROFILESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspDemoNPacificToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspDemoSAtlanticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspDemoArcticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isAutoscreenshotBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.animationBtn = new System.Windows.Forms.ToolStripButton();
            this.infoBtn = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.parametersGroup = new System.Windows.Forms.GroupBox();
            this.latEdit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.pTimeEdit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.verticalPropagationPlot = new SoundPathDemo.VerticalPropagationPlot();
            this.vProfileView = new SoundPathDemo.VProfileView();
            this.mainToolStrip.SuspendLayout();
            this.parametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.latEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTimeEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilesBtn,
            this.isAutoscreenshotBtn,
            this.toolStripSeparator2,
            this.animationBtn,
            this.infoBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1227, 27);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // profilesBtn
            // 
            this.profilesBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.profilesBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dEMOPROFILESToolStripMenuItem,
            this.loadProfileToolStripMenuItem});
            this.profilesBtn.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.profilesBtn.Image = ((System.Drawing.Image)(resources.GetObject("profilesBtn.Image")));
            this.profilesBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.profilesBtn.Name = "profilesBtn";
            this.profilesBtn.Size = new System.Drawing.Size(89, 24);
            this.profilesBtn.Text = "PROFILES";
            // 
            // dEMOPROFILESToolStripMenuItem
            // 
            this.dEMOPROFILESToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspDemoNPacificToolStripMenuItem,
            this.tspDemoSAtlanticToolStripMenuItem,
            this.tspDemoArcticToolStripMenuItem});
            this.dEMOPROFILESToolStripMenuItem.Name = "dEMOPROFILESToolStripMenuItem";
            this.dEMOPROFILESToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.dEMOPROFILESToolStripMenuItem.Text = "DEMO PROFILES";
            // 
            // tspDemoNPacificToolStripMenuItem
            // 
            this.tspDemoNPacificToolStripMenuItem.Name = "tspDemoNPacificToolStripMenuItem";
            this.tspDemoNPacificToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.tspDemoNPacificToolStripMenuItem.Text = "NORTHERN PACIFIC (N39°)";
            this.tspDemoNPacificToolStripMenuItem.Click += new System.EventHandler(this.tspDemoNPacificToolStripMenuItem_Click);
            // 
            // tspDemoSAtlanticToolStripMenuItem
            // 
            this.tspDemoSAtlanticToolStripMenuItem.Name = "tspDemoSAtlanticToolStripMenuItem";
            this.tspDemoSAtlanticToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.tspDemoSAtlanticToolStripMenuItem.Text = "SOUTHERN ATLANTIC (S20°)";
            this.tspDemoSAtlanticToolStripMenuItem.Click += new System.EventHandler(this.tspDemoSAtlanticToolStripMenuItem_Click);
            // 
            // tspDemoArcticToolStripMenuItem
            // 
            this.tspDemoArcticToolStripMenuItem.Name = "tspDemoArcticToolStripMenuItem";
            this.tspDemoArcticToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.tspDemoArcticToolStripMenuItem.Text = "ARCTIC (N89°)";
            this.tspDemoArcticToolStripMenuItem.Click += new System.EventHandler(this.tspDemoArcticToolStripMenuItem_Click);
            // 
            // loadProfileToolStripMenuItem
            // 
            this.loadProfileToolStripMenuItem.Name = "loadProfileToolStripMenuItem";
            this.loadProfileToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.loadProfileToolStripMenuItem.Text = "LOAD...";
            this.loadProfileToolStripMenuItem.Click += new System.EventHandler(this.loadProfileBtn_Click);
            // 
            // isAutoscreenshotBtn
            // 
            this.isAutoscreenshotBtn.CheckOnClick = true;
            this.isAutoscreenshotBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isAutoscreenshotBtn.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isAutoscreenshotBtn.Image = ((System.Drawing.Image)(resources.GetObject("isAutoscreenshotBtn.Image")));
            this.isAutoscreenshotBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isAutoscreenshotBtn.Name = "isAutoscreenshotBtn";
            this.isAutoscreenshotBtn.Size = new System.Drawing.Size(147, 24);
            this.isAutoscreenshotBtn.Text = "AUTOSCREENSHOT";
            this.isAutoscreenshotBtn.Click += new System.EventHandler(this.isAutoscreenshotBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // animationBtn
            // 
            this.animationBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.animationBtn.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.animationBtn.Image = ((System.Drawing.Image)(resources.GetObject("animationBtn.Image")));
            this.animationBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.animationBtn.Name = "animationBtn";
            this.animationBtn.Size = new System.Drawing.Size(102, 24);
            this.animationBtn.Text = "ANIMATION";
            this.animationBtn.Click += new System.EventHandler(this.animationBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoBtn.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(232, 24);
            this.infoBtn.Text = "VISIT PROJECT\'S GITHUB PAGE";
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 624);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1227, 22);
            this.mainStatusStrip.TabIndex = 6;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // parametersGroup
            // 
            this.parametersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parametersGroup.Controls.Add(this.latEdit);
            this.parametersGroup.Controls.Add(this.label2);
            this.parametersGroup.Controls.Add(this.pTimeEdit);
            this.parametersGroup.Controls.Add(this.label1);
            this.parametersGroup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.parametersGroup.Location = new System.Drawing.Point(12, 30);
            this.parametersGroup.Name = "parametersGroup";
            this.parametersGroup.Size = new System.Drawing.Size(1203, 88);
            this.parametersGroup.TabIndex = 7;
            this.parametersGroup.TabStop = false;
            // 
            // latEdit
            // 
            this.latEdit.Location = new System.Drawing.Point(231, 52);
            this.latEdit.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.latEdit.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.latEdit.Name = "latEdit";
            this.latEdit.Size = new System.Drawing.Size(186, 30);
            this.latEdit.TabIndex = 3;
            this.latEdit.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.latEdit.ValueChanged += new System.EventHandler(this.latEdit_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Latitude, °";
            // 
            // pTimeEdit
            // 
            this.pTimeEdit.DecimalPlaces = 2;
            this.pTimeEdit.Location = new System.Drawing.Point(10, 52);
            this.pTimeEdit.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.pTimeEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.pTimeEdit.Name = "pTimeEdit";
            this.pTimeEdit.Size = new System.Drawing.Size(186, 30);
            this.pTimeEdit.TabIndex = 1;
            this.pTimeEdit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.pTimeEdit.ValueChanged += new System.EventHandler(this.pTimeEdit_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Full propagation time, s";
            // 
            // verticalPropagationPlot
            // 
            this.verticalPropagationPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.verticalPropagationPlot.AxisColor = System.Drawing.Color.Gainsboro;
            this.verticalPropagationPlot.AxisLblColor = System.Drawing.Color.LightGray;
            this.verticalPropagationPlot.AxisLblFntSize = 3;
            this.verticalPropagationPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.verticalPropagationPlot.Caption = "Vertical sound propagation simulation";
            this.verticalPropagationPlot.CaptionColor = System.Drawing.Color.WhiteSmoke;
            this.verticalPropagationPlot.CaptionLblFntSize = 4;
            this.verticalPropagationPlot.FrontGraphColor = System.Drawing.Color.Cyan;
            this.verticalPropagationPlot.FrontPenWidth = 3F;
            this.verticalPropagationPlot.FrontSegments = 10;
            this.verticalPropagationPlot.FrontSegmentSize = 4;
            this.verticalPropagationPlot.FullPropagationTime = 5D;
            this.verticalPropagationPlot.IsSimulationStepEvent = false;
            this.verticalPropagationPlot.Location = new System.Drawing.Point(332, 124);
            this.verticalPropagationPlot.Name = "verticalPropagationPlot";
            this.verticalPropagationPlot.Size = new System.Drawing.Size(883, 497);
            this.verticalPropagationPlot.TabIndex = 5;
            this.verticalPropagationPlot.Zmax = 1000D;
            this.verticalPropagationPlot.Zmin = 0D;
            this.verticalPropagationPlot.ZTicks = 10;
            // 
            // vProfileView
            // 
            this.vProfileView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vProfileView.AxisColor = System.Drawing.Color.LightGray;
            this.vProfileView.AxisLblColor = System.Drawing.Color.Gainsboro;
            this.vProfileView.AxisLblFntSize = 3;
            this.vProfileView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.vProfileView.Caption = "TS-Profile";
            this.vProfileView.CaptionColor = System.Drawing.Color.WhiteSmoke;
            this.vProfileView.CaptionLblFntSize = 4;
            this.vProfileView.GraphPenWidth = 2F;
            this.vProfileView.Location = new System.Drawing.Point(12, 124);
            this.vProfileView.Name = "vProfileView";
            this.vProfileView.Size = new System.Drawing.Size(314, 497);
            this.vProfileView.StyGraphColor = System.Drawing.Color.Cyan;
            this.vProfileView.TabIndex = 4;
            this.vProfileView.TempGraphColor = System.Drawing.Color.Lime;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 646);
            this.Controls.Add(this.verticalPropagationPlot);
            this.Controls.Add(this.vProfileView);
            this.Controls.Add(this.parametersGroup);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.Text = "Vertical Sound Path demo";
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.parametersGroup.ResumeLayout(false);
            this.parametersGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.latEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTimeEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton isAutoscreenshotBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton animationBtn;
        private VProfileView vProfileView;
        private VerticalPropagationPlot verticalPropagationPlot;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripDropDownButton profilesBtn;
        private System.Windows.Forms.GroupBox parametersGroup;
        private System.Windows.Forms.NumericUpDown pTimeEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem dEMOPROFILESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tspDemoNPacificToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tspDemoSAtlanticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tspDemoArcticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProfileToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown latEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton infoBtn;
    }
}


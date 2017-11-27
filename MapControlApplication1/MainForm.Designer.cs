namespace MapControlApplication1
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
            //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
            //Failure to do this may result in random crashes on exit due to the operating system unloading 
            //the libraries in the incorrect order. 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI2_loadFromSDE = new System.Windows.Forms.ToolStripMenuItem();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_ZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_DeleteLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmb_LoadRstDataset = new System.Windows.Forms.ComboBox();
            this.btn_ImportRstDataset = new System.Windows.Forms.Button();
            this.txb_NewRstDataset = new System.Windows.Forms.TextBox();
            this.btn_LoadRstDataset = new System.Windows.Forms.Button();
            this.btn_DeleteRstDataset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btn_NewRstCatalog = new System.Windows.Forms.Button();
            this.txb_NewRstCatalog = new System.Windows.Forms.TextBox();
            this.btn_DeleteRstCatalog = new System.Windows.Forms.Button();
            this.cmb_LoadRstCatalog = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmb_BBand = new System.Windows.Forms.ComboBox();
            this.cmb_GBand = new System.Windows.Forms.ComboBox();
            this.cmb_RBand = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cmb_RGBLayer = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_RGB = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.pb_ColorBar = new System.Windows.Forms.PictureBox();
            this.pb_ToColor = new System.Windows.Forms.PictureBox();
            this.pb_FromColor = new System.Windows.Forms.PictureBox();
            this.cmb_RenderBand = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmb_RenderLayer = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_Render = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_StretchBand = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_StretchMethod = new System.Windows.Forms.ComboBox();
            this.cmb_StretchLayer = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_Stretch = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_SingleBandHis = new System.Windows.Forms.Button();
            this.cmb_DrawHisBand = new System.Windows.Forms.ComboBox();
            this.cmb_DrawHisLayer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_MultiBandHis = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmb_NDVILayer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_CalculateNDVI = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_StatisticsBand = new System.Windows.Forms.ComboBox();
            this.cmb_StatisticsLayer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Statistics = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.txb_Transformangle = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.cmb_TransformLayer = new System.Windows.Forms.ComboBox();
            this.btn_Transform = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.cmb_TransformMethod = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cmb_FilterMethod = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_Filter = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cmb_MosaicCatalog = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.btn_Mosaic = new System.Windows.Forms.Button();
            this.图像融合 = new System.Windows.Forms.GroupBox();
            this.cmb_MultiLayer = new System.Windows.Forms.ComboBox();
            this.cmb_PanLayer = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.btn_PanSharpen = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.txb_ClipFeature = new System.Windows.Forms.TextBox();
            this.cmb_ClipLayer = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_Clip = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmb_GeneralMethod = new System.Windows.Forms.ComboBox();
            this.btn_Matrix = new System.Windows.Forms.Button();
            this.cmb_GeneralLayer = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmb_ClassifyMethod = new System.Windows.Forms.ComboBox();
            this.txb_ResultPath = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btn_Classify = new System.Windows.Forms.Button();
            this.txb_ClassNum = new System.Windows.Forms.TextBox();
            this.cmb_ClassifyLayer = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cd_FromColor = new System.Windows.Forms.ColorDialog();
            this.cd_ToColor = new System.Windows.Forms.ColorDialog();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.cmb_SlopeDEM = new System.Windows.Forms.ComboBox();
            this.btn_Slope = new System.Windows.Forms.Button();
            this.cmb_HillshadeDEM = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.cmb_AspectDEM = new System.Windows.Forms.ComboBox();
            this.btn_HillShade = new System.Windows.Forms.Button();
            this.btn_Aspect = new System.Windows.Forms.Button();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.btn_LineOfSight = new System.Windows.Forms.Button();
            this.cmb_LineOfSightDEM = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.btn_Visibility = new System.Windows.Forms.Button();
            this.cmb_VisibilityDEM = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.cmb_NeighborhoodLayer = new System.Windows.Forms.ComboBox();
            this.btn_Neighborhood = new System.Windows.Forms.Button();
            this.cmb_NeighborhoodMethod = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.btn_Extraction = new System.Windows.Forms.Button();
            this.cmb_ExtractionLayer = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ColorBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ToColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FromColor)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.图像融合.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.connectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1145, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAs,
            this.menuSeparator,
            this.menuExitApp});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(46, 24);
            this.menuFile.Text = "File";
            // 
            // menuNewDoc
            // 
            this.menuNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuNewDoc.Image")));
            this.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuNewDoc.Name = "menuNewDoc";
            this.menuNewDoc.Size = new System.Drawing.Size(210, 24);
            this.menuNewDoc.Text = "New Document";
            this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
            // 
            // menuOpenDoc
            // 
            this.menuOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuOpenDoc.Image")));
            this.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuOpenDoc.Name = "menuOpenDoc";
            this.menuOpenDoc.Size = new System.Drawing.Size(210, 24);
            this.menuOpenDoc.Text = "Open Document...";
            this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
            // 
            // menuSaveDoc
            // 
            this.menuSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDoc.Image")));
            this.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuSaveDoc.Name = "menuSaveDoc";
            this.menuSaveDoc.Size = new System.Drawing.Size(210, 24);
            this.menuSaveDoc.Text = "SaveDocument";
            this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(210, 24);
            this.menuSaveAs.Text = "Save As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuSeparator
            // 
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(207, 6);
            // 
            // menuExitApp
            // 
            this.menuExitApp.Name = "menuExitApp";
            this.menuExitApp.Size = new System.Drawing.Size(210, 24);
            this.menuExitApp.Text = "Exit";
            this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI2_loadFromSDE});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.connectToolStripMenuItem.Text = "Raster";
            // 
            // MI2_loadFromSDE
            // 
            this.MI2_loadFromSDE.Name = "MI2_loadFromSDE";
            this.MI2_loadFromSDE.Size = new System.Drawing.Size(191, 24);
            this.MI2_loadFromSDE.Text = "Connect to SDE";
            this.MI2_loadFromSDE.Click += new System.EventHandler(this.MI2_loadFromSDE_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.AllowDrop = true;
            this.axMapControl1.Location = new System.Drawing.Point(239, 60);
            this.axMapControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.axMapControl1.MaximumSize = new System.Drawing.Size(626, 592);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(590, 591);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1145, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTOCControl1.Location = new System.Drawing.Point(4, 56);
            this.axTOCControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(235, 595);
            this.axTOCControl1.TabIndex = 4;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(466, 278);
            this.axLicenseControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 56);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 620);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarXY});
            this.statusStrip1.Location = new System.Drawing.Point(4, 651);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1141, 25);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusBar1";
            // 
            // statusBarXY
            // 
            this.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarXY.Name = "statusBarXY";
            this.statusBarXY.Size = new System.Drawing.Size(71, 20);
            this.statusBarXY.Text = "Test 123";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_ZoomToLayer,
            this.TSMI_DeleteLayer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 52);
            // 
            // TSMI_ZoomToLayer
            // 
            this.TSMI_ZoomToLayer.Name = "TSMI_ZoomToLayer";
            this.TSMI_ZoomToLayer.Size = new System.Drawing.Size(183, 24);
            this.TSMI_ZoomToLayer.Text = "缩放到当前图层";
            this.TSMI_ZoomToLayer.Click += new System.EventHandler(this.TSMI_ZoomToLayer_Click);
            // 
            // TSMI_DeleteLayer
            // 
            this.TSMI_DeleteLayer.Name = "TSMI_DeleteLayer";
            this.TSMI_DeleteLayer.Size = new System.Drawing.Size(183, 24);
            this.TSMI_DeleteLayer.Text = "删除当前图层";
            this.TSMI_DeleteLayer.Click += new System.EventHandler(this.TSMI_DeleteLayer_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(826, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(361, 623);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(353, 594);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.label21);
            this.groupBox8.Controls.Add(this.cmb_LoadRstDataset);
            this.groupBox8.Controls.Add(this.btn_ImportRstDataset);
            this.groupBox8.Controls.Add(this.txb_NewRstDataset);
            this.groupBox8.Controls.Add(this.btn_LoadRstDataset);
            this.groupBox8.Controls.Add(this.btn_DeleteRstDataset);
            this.groupBox8.Location = new System.Drawing.Point(9, 134);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(294, 128);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "栅格图像";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 15);
            this.label20.TabIndex = 3;
            this.label20.Text = "栅格图像";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 88);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 15);
            this.label21.TabIndex = 3;
            this.label21.Text = "新栅格";
            // 
            // cmb_LoadRstDataset
            // 
            this.cmb_LoadRstDataset.FormattingEnabled = true;
            this.cmb_LoadRstDataset.Location = new System.Drawing.Point(83, 18);
            this.cmb_LoadRstDataset.Name = "cmb_LoadRstDataset";
            this.cmb_LoadRstDataset.Size = new System.Drawing.Size(179, 23);
            this.cmb_LoadRstDataset.TabIndex = 5;
            // 
            // btn_ImportRstDataset
            // 
            this.btn_ImportRstDataset.Location = new System.Drawing.Point(199, 78);
            this.btn_ImportRstDataset.Name = "btn_ImportRstDataset";
            this.btn_ImportRstDataset.Size = new System.Drawing.Size(65, 25);
            this.btn_ImportRstDataset.TabIndex = 10;
            this.btn_ImportRstDataset.Text = "导入";
            this.btn_ImportRstDataset.UseVisualStyleBackColor = true;
            this.btn_ImportRstDataset.Click += new System.EventHandler(this.btn_ImportRstDataset_Click);
            // 
            // txb_NewRstDataset
            // 
            this.txb_NewRstDataset.Location = new System.Drawing.Point(68, 78);
            this.txb_NewRstDataset.Name = "txb_NewRstDataset";
            this.txb_NewRstDataset.Size = new System.Drawing.Size(123, 25);
            this.txb_NewRstDataset.TabIndex = 8;
            this.txb_NewRstDataset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txb_NewRstDataset_MouseDown);
            // 
            // btn_LoadRstDataset
            // 
            this.btn_LoadRstDataset.Location = new System.Drawing.Point(83, 47);
            this.btn_LoadRstDataset.Name = "btn_LoadRstDataset";
            this.btn_LoadRstDataset.Size = new System.Drawing.Size(64, 25);
            this.btn_LoadRstDataset.TabIndex = 0;
            this.btn_LoadRstDataset.Text = "加载";
            this.btn_LoadRstDataset.UseVisualStyleBackColor = true;
            this.btn_LoadRstDataset.Click += new System.EventHandler(this.btn_LoadRstDataset_Click);
            // 
            // btn_DeleteRstDataset
            // 
            this.btn_DeleteRstDataset.Location = new System.Drawing.Point(199, 47);
            this.btn_DeleteRstDataset.Name = "btn_DeleteRstDataset";
            this.btn_DeleteRstDataset.Size = new System.Drawing.Size(65, 25);
            this.btn_DeleteRstDataset.TabIndex = 7;
            this.btn_DeleteRstDataset.Text = "删除";
            this.btn_DeleteRstDataset.UseVisualStyleBackColor = true;
            this.btn_DeleteRstDataset.Click += new System.EventHandler(this.btn_DeleteRstDataset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.btn_NewRstCatalog);
            this.groupBox1.Controls.Add(this.txb_NewRstCatalog);
            this.groupBox1.Controls.Add(this.btn_DeleteRstCatalog);
            this.groupBox1.Controls.Add(this.cmb_LoadRstCatalog);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 122);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "栅格目录";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(10, 91);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(52, 15);
            this.label23.TabIndex = 11;
            this.label23.Text = "新目录";
            // 
            // btn_NewRstCatalog
            // 
            this.btn_NewRstCatalog.Location = new System.Drawing.Point(197, 84);
            this.btn_NewRstCatalog.Name = "btn_NewRstCatalog";
            this.btn_NewRstCatalog.Size = new System.Drawing.Size(65, 25);
            this.btn_NewRstCatalog.TabIndex = 9;
            this.btn_NewRstCatalog.Text = "创建";
            this.btn_NewRstCatalog.UseVisualStyleBackColor = true;
            this.btn_NewRstCatalog.Click += new System.EventHandler(this.btn_NewRstCatalog_Click);
            // 
            // txb_NewRstCatalog
            // 
            this.txb_NewRstCatalog.Location = new System.Drawing.Point(68, 84);
            this.txb_NewRstCatalog.Name = "txb_NewRstCatalog";
            this.txb_NewRstCatalog.Size = new System.Drawing.Size(123, 25);
            this.txb_NewRstCatalog.TabIndex = 3;
            // 
            // btn_DeleteRstCatalog
            // 
            this.btn_DeleteRstCatalog.Location = new System.Drawing.Point(197, 54);
            this.btn_DeleteRstCatalog.Name = "btn_DeleteRstCatalog";
            this.btn_DeleteRstCatalog.Size = new System.Drawing.Size(65, 25);
            this.btn_DeleteRstCatalog.TabIndex = 6;
            this.btn_DeleteRstCatalog.Text = "删除";
            this.btn_DeleteRstCatalog.UseVisualStyleBackColor = true;
            this.btn_DeleteRstCatalog.Click += new System.EventHandler(this.btn_DeleteRstCatalog_Click);
            // 
            // cmb_LoadRstCatalog
            // 
            this.cmb_LoadRstCatalog.FormattingEnabled = true;
            this.cmb_LoadRstCatalog.Location = new System.Drawing.Point(77, 25);
            this.cmb_LoadRstCatalog.Name = "cmb_LoadRstCatalog";
            this.cmb_LoadRstCatalog.Size = new System.Drawing.Size(185, 23);
            this.cmb_LoadRstCatalog.TabIndex = 4;
            this.cmb_LoadRstCatalog.SelectedIndexChanged += new System.EventHandler(this.cmb_LoadRstCatalog_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 33);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 1;
            this.label19.Text = "栅格目录";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(353, 594);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图像处理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmb_BBand);
            this.groupBox7.Controls.Add(this.cmb_GBand);
            this.groupBox7.Controls.Add(this.cmb_RBand);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.cmb_RGBLayer);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.btn_RGB);
            this.groupBox7.Location = new System.Drawing.Point(9, 461);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(294, 93);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "多波段假彩色合成";
            // 
            // cmb_BBand
            // 
            this.cmb_BBand.FormattingEnabled = true;
            this.cmb_BBand.Location = new System.Drawing.Point(211, 58);
            this.cmb_BBand.Name = "cmb_BBand";
            this.cmb_BBand.Size = new System.Drawing.Size(65, 23);
            this.cmb_BBand.TabIndex = 12;
            // 
            // cmb_GBand
            // 
            this.cmb_GBand.FormattingEnabled = true;
            this.cmb_GBand.Location = new System.Drawing.Point(119, 58);
            this.cmb_GBand.Name = "cmb_GBand";
            this.cmb_GBand.Size = new System.Drawing.Size(65, 23);
            this.cmb_GBand.TabIndex = 11;
            // 
            // cmb_RBand
            // 
            this.cmb_RBand.FormattingEnabled = true;
            this.cmb_RBand.Location = new System.Drawing.Point(31, 58);
            this.cmb_RBand.Name = "cmb_RBand";
            this.cmb_RBand.Size = new System.Drawing.Size(65, 23);
            this.cmb_RBand.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(190, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 15);
            this.label18.TabIndex = 6;
            this.label18.Text = "B";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(102, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(15, 15);
            this.label17.TabIndex = 5;
            this.label17.Text = "G";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 15);
            this.label16.TabIndex = 4;
            this.label16.Text = "R";
            // 
            // cmb_RGBLayer
            // 
            this.cmb_RGBLayer.FormattingEnabled = true;
            this.cmb_RGBLayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_RGBLayer.Name = "cmb_RGBLayer";
            this.cmb_RGBLayer.Size = new System.Drawing.Size(98, 23);
            this.cmb_RGBLayer.TabIndex = 3;
            this.cmb_RGBLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_RGBLayer_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(0, 15);
            this.label14.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 15);
            this.label15.TabIndex = 1;
            this.label15.Text = "图层";
            // 
            // btn_RGB
            // 
            this.btn_RGB.Location = new System.Drawing.Point(204, 18);
            this.btn_RGB.Name = "btn_RGB";
            this.btn_RGB.Size = new System.Drawing.Size(65, 25);
            this.btn_RGB.TabIndex = 0;
            this.btn_RGB.Text = "合成";
            this.btn_RGB.UseVisualStyleBackColor = true;
            this.btn_RGB.Click += new System.EventHandler(this.btn_RGB_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.pb_ColorBar);
            this.groupBox6.Controls.Add(this.pb_ToColor);
            this.groupBox6.Controls.Add(this.pb_FromColor);
            this.groupBox6.Controls.Add(this.cmb_RenderBand);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.cmb_RenderLayer);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.btn_Render);
            this.groupBox6.Location = new System.Drawing.Point(9, 364);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(294, 91);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "单波段伪彩色渲染";
            // 
            // pb_ColorBar
            // 
            this.pb_ColorBar.Location = new System.Drawing.Point(53, 57);
            this.pb_ColorBar.Name = "pb_ColorBar";
            this.pb_ColorBar.Size = new System.Drawing.Size(82, 25);
            this.pb_ColorBar.TabIndex = 12;
            this.pb_ColorBar.TabStop = false;
            // 
            // pb_ToColor
            // 
            this.pb_ToColor.Location = new System.Drawing.Point(141, 57);
            this.pb_ToColor.Name = "pb_ToColor";
            this.pb_ToColor.Size = new System.Drawing.Size(34, 25);
            this.pb_ToColor.TabIndex = 11;
            this.pb_ToColor.TabStop = false;
            this.pb_ToColor.Click += new System.EventHandler(this.pb_ToColor_Click);
            // 
            // pb_FromColor
            // 
            this.pb_FromColor.Location = new System.Drawing.Point(13, 57);
            this.pb_FromColor.Name = "pb_FromColor";
            this.pb_FromColor.Size = new System.Drawing.Size(34, 25);
            this.pb_FromColor.TabIndex = 10;
            this.pb_FromColor.TabStop = false;
            this.pb_FromColor.Click += new System.EventHandler(this.pb_FromColor_Click);
            // 
            // cmb_RenderBand
            // 
            this.cmb_RenderBand.FormattingEnabled = true;
            this.cmb_RenderBand.Location = new System.Drawing.Point(196, 25);
            this.cmb_RenderBand.Name = "cmb_RenderBand";
            this.cmb_RenderBand.Size = new System.Drawing.Size(83, 23);
            this.cmb_RenderBand.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(158, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 15);
            this.label13.TabIndex = 9;
            this.label13.Text = "波段";
            // 
            // cmb_RenderLayer
            // 
            this.cmb_RenderLayer.FormattingEnabled = true;
            this.cmb_RenderLayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_RenderLayer.Name = "cmb_RenderLayer";
            this.cmb_RenderLayer.Size = new System.Drawing.Size(98, 23);
            this.cmb_RenderLayer.TabIndex = 3;
            this.cmb_RenderLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_RenderLayer_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "图层";
            // 
            // btn_Render
            // 
            this.btn_Render.Location = new System.Drawing.Point(204, 57);
            this.btn_Render.Name = "btn_Render";
            this.btn_Render.Size = new System.Drawing.Size(65, 25);
            this.btn_Render.TabIndex = 0;
            this.btn_Render.Text = "渲染";
            this.btn_Render.UseVisualStyleBackColor = true;
            this.btn_Render.Click += new System.EventHandler(this.btn_Render_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox11);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.cmb_StretchBand);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.cmb_StretchMethod);
            this.groupBox5.Controls.Add(this.cmb_StretchLayer);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.btn_Stretch);
            this.groupBox5.Location = new System.Drawing.Point(9, 267);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(294, 91);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "单波段灰度增强";
            // 
            // comboBox11
            // 
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Location = new System.Drawing.Point(193, 119);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(83, 23);
            this.comboBox11.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(155, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 15);
            this.label12.TabIndex = 7;
            this.label12.Text = "波段";
            // 
            // cmb_StretchBand
            // 
            this.cmb_StretchBand.FormattingEnabled = true;
            this.cmb_StretchBand.Location = new System.Drawing.Point(196, 24);
            this.cmb_StretchBand.Name = "cmb_StretchBand";
            this.cmb_StretchBand.Size = new System.Drawing.Size(83, 23);
            this.cmb_StretchBand.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(158, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 15);
            this.label11.TabIndex = 5;
            this.label11.Text = "波段";
            // 
            // cmb_StretchMethod
            // 
            this.cmb_StretchMethod.FormattingEnabled = true;
            this.cmb_StretchMethod.Items.AddRange(new object[] {
            "默认拉伸",
            "标准差拉伸",
            "最大最小值拉伸",
            "百分比最大最小值拉伸",
            "直方图均衡",
            "直方图匹配"});
            this.cmb_StretchMethod.Location = new System.Drawing.Point(54, 57);
            this.cmb_StretchMethod.Name = "cmb_StretchMethod";
            this.cmb_StretchMethod.Size = new System.Drawing.Size(144, 23);
            this.cmb_StretchMethod.TabIndex = 4;
            // 
            // cmb_StretchLayer
            // 
            this.cmb_StretchLayer.FormattingEnabled = true;
            this.cmb_StretchLayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_StretchLayer.Name = "cmb_StretchLayer";
            this.cmb_StretchLayer.Size = new System.Drawing.Size(98, 23);
            this.cmb_StretchLayer.TabIndex = 3;
            this.cmb_StretchLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_StretchLayer_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "方法";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "图层";
            // 
            // btn_Stretch
            // 
            this.btn_Stretch.Location = new System.Drawing.Point(204, 53);
            this.btn_Stretch.Name = "btn_Stretch";
            this.btn_Stretch.Size = new System.Drawing.Size(65, 25);
            this.btn_Stretch.TabIndex = 0;
            this.btn_Stretch.Text = "增强";
            this.btn_Stretch.UseVisualStyleBackColor = true;
            this.btn_Stretch.Click += new System.EventHandler(this.btn_Stretch_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_SingleBandHis);
            this.groupBox4.Controls.Add(this.cmb_DrawHisBand);
            this.groupBox4.Controls.Add(this.cmb_DrawHisLayer);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.btn_MultiBandHis);
            this.groupBox4.Location = new System.Drawing.Point(9, 170);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(294, 91);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "直方图绘制";
            // 
            // btn_SingleBandHis
            // 
            this.btn_SingleBandHis.Location = new System.Drawing.Point(204, 24);
            this.btn_SingleBandHis.Name = "btn_SingleBandHis";
            this.btn_SingleBandHis.Size = new System.Drawing.Size(68, 25);
            this.btn_SingleBandHis.TabIndex = 5;
            this.btn_SingleBandHis.Text = "单波段";
            this.btn_SingleBandHis.UseVisualStyleBackColor = true;
            this.btn_SingleBandHis.Click += new System.EventHandler(this.btn_SingleBandHis_Click);
            // 
            // cmb_DrawHisBand
            // 
            this.cmb_DrawHisBand.FormattingEnabled = true;
            this.cmb_DrawHisBand.Location = new System.Drawing.Point(54, 57);
            this.cmb_DrawHisBand.Name = "cmb_DrawHisBand";
            this.cmb_DrawHisBand.Size = new System.Drawing.Size(144, 23);
            this.cmb_DrawHisBand.TabIndex = 4;
            // 
            // cmb_DrawHisLayer
            // 
            this.cmb_DrawHisLayer.FormattingEnabled = true;
            this.cmb_DrawHisLayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_DrawHisLayer.Name = "cmb_DrawHisLayer";
            this.cmb_DrawHisLayer.Size = new System.Drawing.Size(144, 23);
            this.cmb_DrawHisLayer.TabIndex = 3;
            this.cmb_DrawHisLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_DrawHisLayer_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "波段";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "图层";
            // 
            // btn_MultiBandHis
            // 
            this.btn_MultiBandHis.Location = new System.Drawing.Point(204, 53);
            this.btn_MultiBandHis.Name = "btn_MultiBandHis";
            this.btn_MultiBandHis.Size = new System.Drawing.Size(68, 25);
            this.btn_MultiBandHis.TabIndex = 0;
            this.btn_MultiBandHis.Text = "多波段";
            this.btn_MultiBandHis.UseVisualStyleBackColor = true;
            this.btn_MultiBandHis.Click += new System.EventHandler(this.btn_MultiBandHis_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmb_NDVILayer);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btn_CalculateNDVI);
            this.groupBox3.Location = new System.Drawing.Point(9, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 61);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NDVI指数计算";
            // 
            // cmb_NDVILayer
            // 
            this.cmb_NDVILayer.FormattingEnabled = true;
            this.cmb_NDVILayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_NDVILayer.Name = "cmb_NDVILayer";
            this.cmb_NDVILayer.Size = new System.Drawing.Size(144, 23);
            this.cmb_NDVILayer.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "图层";
            // 
            // btn_CalculateNDVI
            // 
            this.btn_CalculateNDVI.Location = new System.Drawing.Point(204, 24);
            this.btn_CalculateNDVI.Name = "btn_CalculateNDVI";
            this.btn_CalculateNDVI.Size = new System.Drawing.Size(65, 25);
            this.btn_CalculateNDVI.TabIndex = 0;
            this.btn_CalculateNDVI.Text = "计算";
            this.btn_CalculateNDVI.UseVisualStyleBackColor = true;
            this.btn_CalculateNDVI.Click += new System.EventHandler(this.btn_CalculateNDVI_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_StatisticsBand);
            this.groupBox2.Controls.Add(this.cmb_StatisticsLayer);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_Statistics);
            this.groupBox2.Location = new System.Drawing.Point(9, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 91);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "波段信息统计";
            // 
            // cmb_StatisticsBand
            // 
            this.cmb_StatisticsBand.FormattingEnabled = true;
            this.cmb_StatisticsBand.Location = new System.Drawing.Point(54, 57);
            this.cmb_StatisticsBand.Name = "cmb_StatisticsBand";
            this.cmb_StatisticsBand.Size = new System.Drawing.Size(144, 23);
            this.cmb_StatisticsBand.TabIndex = 4;
            // 
            // cmb_StatisticsLayer
            // 
            this.cmb_StatisticsLayer.FormattingEnabled = true;
            this.cmb_StatisticsLayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_StatisticsLayer.Name = "cmb_StatisticsLayer";
            this.cmb_StatisticsLayer.Size = new System.Drawing.Size(144, 23);
            this.cmb_StatisticsLayer.TabIndex = 3;
            this.cmb_StatisticsLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_StatisticsLayer_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "波段";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "图层";
            // 
            // btn_Statistics
            // 
            this.btn_Statistics.Location = new System.Drawing.Point(204, 56);
            this.btn_Statistics.Name = "btn_Statistics";
            this.btn_Statistics.Size = new System.Drawing.Size(65, 25);
            this.btn_Statistics.TabIndex = 0;
            this.btn_Statistics.Text = "统计";
            this.btn_Statistics.UseVisualStyleBackColor = true;
            this.btn_Statistics.Click += new System.EventHandler(this.btn_Statistics_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox13);
            this.tabPage3.Controls.Add(this.groupBox12);
            this.tabPage3.Controls.Add(this.图像融合);
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Controls.Add(this.groupBox10);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(353, 594);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "图像分析";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.txb_Transformangle);
            this.groupBox13.Controls.Add(this.label36);
            this.groupBox13.Controls.Add(this.cmb_TransformLayer);
            this.groupBox13.Controls.Add(this.btn_Transform);
            this.groupBox13.Controls.Add(this.label35);
            this.groupBox13.Controls.Add(this.cmb_TransformMethod);
            this.groupBox13.Controls.Add(this.label34);
            this.groupBox13.Controls.Add(this.cmb_FilterMethod);
            this.groupBox13.Controls.Add(this.label33);
            this.groupBox13.Controls.Add(this.btn_Filter);
            this.groupBox13.Location = new System.Drawing.Point(6, 480);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(294, 111);
            this.groupBox13.TabIndex = 15;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "卷积（滤波）和变换";
            // 
            // txb_Transformangle
            // 
            this.txb_Transformangle.Location = new System.Drawing.Point(228, 22);
            this.txb_Transformangle.Name = "txb_Transformangle";
            this.txb_Transformangle.Size = new System.Drawing.Size(52, 25);
            this.txb_Transformangle.TabIndex = 19;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(191, 32);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(37, 15);
            this.label36.TabIndex = 18;
            this.label36.Text = "角度";
            // 
            // cmb_TransformLayer
            // 
            this.cmb_TransformLayer.AutoCompleteCustomSource.AddRange(new string[] {
            "聚合",
            "边界清理",
            "众数滤波"});
            this.cmb_TransformLayer.FormattingEnabled = true;
            this.cmb_TransformLayer.Items.AddRange(new object[] {
            "聚合",
            "边界清理",
            "众数滤波 "});
            this.cmb_TransformLayer.Location = new System.Drawing.Point(53, 24);
            this.cmb_TransformLayer.Name = "cmb_TransformLayer";
            this.cmb_TransformLayer.Size = new System.Drawing.Size(132, 23);
            this.cmb_TransformLayer.TabIndex = 17;
            // 
            // btn_Transform
            // 
            this.btn_Transform.Location = new System.Drawing.Point(223, 52);
            this.btn_Transform.Name = "btn_Transform";
            this.btn_Transform.Size = new System.Drawing.Size(65, 25);
            this.btn_Transform.TabIndex = 16;
            this.btn_Transform.Text = "变换";
            this.btn_Transform.UseVisualStyleBackColor = true;
            this.btn_Transform.Click += new System.EventHandler(this.btn_Transform_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 62);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(67, 15);
            this.label35.TabIndex = 15;
            this.label35.Text = "变换方法";
            // 
            // cmb_TransformMethod
            // 
            this.cmb_TransformMethod.AutoCompleteCustomSource.AddRange(new string[] {
            "聚合",
            "边界清理",
            "众数滤波"});
            this.cmb_TransformMethod.FormattingEnabled = true;
            this.cmb_TransformMethod.Items.AddRange(new object[] {
            "翻转",
            "镜像",
            "裁剪",
            "旋转"});
            this.cmb_TransformMethod.Location = new System.Drawing.Point(79, 54);
            this.cmb_TransformMethod.Name = "cmb_TransformMethod";
            this.cmb_TransformMethod.Size = new System.Drawing.Size(141, 23);
            this.cmb_TransformMethod.TabIndex = 14;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 90);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(67, 15);
            this.label34.TabIndex = 13;
            this.label34.Text = "卷积方法";
            // 
            // cmb_FilterMethod
            // 
            this.cmb_FilterMethod.AutoCompleteCustomSource.AddRange(new string[] {
            "聚合",
            "边界清理",
            "众数滤波"});
            this.cmb_FilterMethod.FormattingEnabled = true;
            this.cmb_FilterMethod.Items.AddRange(new object[] {
            "esriRasterFilterLineDetectionHorizontal",
            "esriRasterFilterLineDetectionVertical",
            "esriRasterFilterLineDetectionLeftDiagonal",
            "esriRasterFilterLineDetectionRightDiagonal",
            "esriRasterFilterGradientNorth",
            "esriRasterFilterGradientWest",
            "esriRasterFilterGradientEast",
            "esriRasterFilterGradientSouth",
            "esriRasterFilterGradientNorthEast",
            "esriRasterFilterGradientNorthWest",
            "esriRasterFilterSmoothArithmeticMean",
            "esriRasterFilterSmoothing3x3",
            "esriRasterFilterSmoothing5x5",
            "esriRasterFilterSharpening3x3",
            "esriRasterFilterSharpening5x5",
            "esriRasterFilterLaplacian3x3",
            "esriRasterFilterLaplacian5x5",
            "esriRasterFilterSobelHorizontal ",
            "esriRasterFilterSobelVertical",
            "esriRasterFilterSharpen",
            "esriRasterFilterSharpen2",
            "esriRasterFilterPointSpread "});
            this.cmb_FilterMethod.Location = new System.Drawing.Point(79, 82);
            this.cmb_FilterMethod.Name = "cmb_FilterMethod";
            this.cmb_FilterMethod.Size = new System.Drawing.Size(141, 23);
            this.cmb_FilterMethod.TabIndex = 12;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 32);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(37, 15);
            this.label33.TabIndex = 2;
            this.label33.Text = "图层";
            // 
            // btn_Filter
            // 
            this.btn_Filter.Location = new System.Drawing.Point(223, 80);
            this.btn_Filter.Name = "btn_Filter";
            this.btn_Filter.Size = new System.Drawing.Size(65, 25);
            this.btn_Filter.TabIndex = 0;
            this.btn_Filter.Text = "卷积";
            this.btn_Filter.UseVisualStyleBackColor = true;
            this.btn_Filter.Click += new System.EventHandler(this.btn_Filter_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cmb_MosaicCatalog);
            this.groupBox12.Controls.Add(this.label32);
            this.groupBox12.Controls.Add(this.btn_Mosaic);
            this.groupBox12.Location = new System.Drawing.Point(6, 414);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(294, 60);
            this.groupBox12.TabIndex = 14;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "图像镶嵌";
            // 
            // cmb_MosaicCatalog
            // 
            this.cmb_MosaicCatalog.FormattingEnabled = true;
            this.cmb_MosaicCatalog.Location = new System.Drawing.Point(79, 24);
            this.cmb_MosaicCatalog.Name = "cmb_MosaicCatalog";
            this.cmb_MosaicCatalog.Size = new System.Drawing.Size(138, 23);
            this.cmb_MosaicCatalog.TabIndex = 5;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(10, 29);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(67, 15);
            this.label32.TabIndex = 2;
            this.label32.Text = "栅格目录";
            // 
            // btn_Mosaic
            // 
            this.btn_Mosaic.Location = new System.Drawing.Point(223, 24);
            this.btn_Mosaic.Name = "btn_Mosaic";
            this.btn_Mosaic.Size = new System.Drawing.Size(65, 25);
            this.btn_Mosaic.TabIndex = 0;
            this.btn_Mosaic.Text = "镶嵌";
            this.btn_Mosaic.UseVisualStyleBackColor = true;
            this.btn_Mosaic.Click += new System.EventHandler(this.btn_Mosaic_Click);
            // 
            // 图像融合
            // 
            this.图像融合.Controls.Add(this.cmb_MultiLayer);
            this.图像融合.Controls.Add(this.cmb_PanLayer);
            this.图像融合.Controls.Add(this.label30);
            this.图像融合.Controls.Add(this.label31);
            this.图像融合.Controls.Add(this.btn_PanSharpen);
            this.图像融合.Location = new System.Drawing.Point(6, 317);
            this.图像融合.Name = "图像融合";
            this.图像融合.Size = new System.Drawing.Size(294, 91);
            this.图像融合.TabIndex = 14;
            this.图像融合.TabStop = false;
            this.图像融合.Text = "图像融合";
            // 
            // cmb_MultiLayer
            // 
            this.cmb_MultiLayer.FormattingEnabled = true;
            this.cmb_MultiLayer.Location = new System.Drawing.Point(68, 57);
            this.cmb_MultiLayer.Name = "cmb_MultiLayer";
            this.cmb_MultiLayer.Size = new System.Drawing.Size(149, 23);
            this.cmb_MultiLayer.TabIndex = 4;
            // 
            // cmb_PanLayer
            // 
            this.cmb_PanLayer.FormattingEnabled = true;
            this.cmb_PanLayer.Location = new System.Drawing.Point(68, 24);
            this.cmb_PanLayer.Name = "cmb_PanLayer";
            this.cmb_PanLayer.Size = new System.Drawing.Size(149, 23);
            this.cmb_PanLayer.TabIndex = 3;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(10, 66);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 15);
            this.label30.TabIndex = 2;
            this.label30.Text = "多波段";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(10, 33);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(52, 15);
            this.label31.TabIndex = 1;
            this.label31.Text = "单波段";
            // 
            // btn_PanSharpen
            // 
            this.btn_PanSharpen.Location = new System.Drawing.Point(223, 56);
            this.btn_PanSharpen.Name = "btn_PanSharpen";
            this.btn_PanSharpen.Size = new System.Drawing.Size(65, 25);
            this.btn_PanSharpen.TabIndex = 0;
            this.btn_PanSharpen.Text = "融合";
            this.btn_PanSharpen.UseVisualStyleBackColor = true;
            this.btn_PanSharpen.Click += new System.EventHandler(this.btn_PanSharpen_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.txb_ClipFeature);
            this.groupBox11.Controls.Add(this.cmb_ClipLayer);
            this.groupBox11.Controls.Add(this.label27);
            this.groupBox11.Controls.Add(this.label28);
            this.groupBox11.Controls.Add(this.btn_Clip);
            this.groupBox11.Location = new System.Drawing.Point(6, 220);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(297, 91);
            this.groupBox11.TabIndex = 13;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "图像裁剪";
            // 
            // txb_ClipFeature
            // 
            this.txb_ClipFeature.Location = new System.Drawing.Point(53, 58);
            this.txb_ClipFeature.Name = "txb_ClipFeature";
            this.txb_ClipFeature.Size = new System.Drawing.Size(164, 25);
            this.txb_ClipFeature.TabIndex = 12;
            this.txb_ClipFeature.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txb_ClipFeature_MouseDown);
            // 
            // cmb_ClipLayer
            // 
            this.cmb_ClipLayer.FormattingEnabled = true;
            this.cmb_ClipLayer.Location = new System.Drawing.Point(54, 24);
            this.cmb_ClipLayer.Name = "cmb_ClipLayer";
            this.cmb_ClipLayer.Size = new System.Drawing.Size(163, 23);
            this.cmb_ClipLayer.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(10, 66);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(37, 15);
            this.label27.TabIndex = 2;
            this.label27.Text = "矢量";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(10, 33);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(37, 15);
            this.label28.TabIndex = 1;
            this.label28.Text = "图层";
            // 
            // btn_Clip
            // 
            this.btn_Clip.Location = new System.Drawing.Point(223, 56);
            this.btn_Clip.Name = "btn_Clip";
            this.btn_Clip.Size = new System.Drawing.Size(65, 25);
            this.btn_Clip.TabIndex = 0;
            this.btn_Clip.Text = "裁剪";
            this.btn_Clip.UseVisualStyleBackColor = true;
            this.btn_Clip.Click += new System.EventHandler(this.btn_Clip_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Controls.Add(this.cmb_GeneralMethod);
            this.groupBox10.Controls.Add(this.btn_Matrix);
            this.groupBox10.Controls.Add(this.cmb_GeneralLayer);
            this.groupBox10.Controls.Add(this.label29);
            this.groupBox10.Location = new System.Drawing.Point(6, 126);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(297, 88);
            this.groupBox10.TabIndex = 12;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "栅格综合";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 61);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(37, 15);
            this.label26.TabIndex = 11;
            this.label26.Text = "方法";
            // 
            // cmb_GeneralMethod
            // 
            this.cmb_GeneralMethod.AutoCompleteCustomSource.AddRange(new string[] {
            "聚合",
            "边界清理",
            "众数滤波"});
            this.cmb_GeneralMethod.FormattingEnabled = true;
            this.cmb_GeneralMethod.Items.AddRange(new object[] {
            "聚合",
            "边界清理",
            "众数滤波 "});
            this.cmb_GeneralMethod.Location = new System.Drawing.Point(49, 53);
            this.cmb_GeneralMethod.Name = "cmb_GeneralMethod";
            this.cmb_GeneralMethod.Size = new System.Drawing.Size(168, 23);
            this.cmb_GeneralMethod.TabIndex = 10;
            // 
            // btn_Matrix
            // 
            this.btn_Matrix.Location = new System.Drawing.Point(223, 53);
            this.btn_Matrix.Name = "btn_Matrix";
            this.btn_Matrix.Size = new System.Drawing.Size(68, 25);
            this.btn_Matrix.TabIndex = 6;
            this.btn_Matrix.Text = "处理";
            this.btn_Matrix.UseVisualStyleBackColor = true;
            this.btn_Matrix.Click += new System.EventHandler(this.btn_Matrix_Click);
            // 
            // cmb_GeneralLayer
            // 
            this.cmb_GeneralLayer.FormattingEnabled = true;
            this.cmb_GeneralLayer.Location = new System.Drawing.Point(49, 24);
            this.cmb_GeneralLayer.Name = "cmb_GeneralLayer";
            this.cmb_GeneralLayer.Size = new System.Drawing.Size(168, 23);
            this.cmb_GeneralLayer.TabIndex = 2;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 30);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(37, 15);
            this.label29.TabIndex = 0;
            this.label29.Text = "图层";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label25);
            this.groupBox9.Controls.Add(this.cmb_ClassifyMethod);
            this.groupBox9.Controls.Add(this.txb_ResultPath);
            this.groupBox9.Controls.Add(this.label24);
            this.groupBox9.Controls.Add(this.btn_Classify);
            this.groupBox9.Controls.Add(this.txb_ClassNum);
            this.groupBox9.Controls.Add(this.cmb_ClassifyLayer);
            this.groupBox9.Controls.Add(this.label22);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Location = new System.Drawing.Point(6, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(297, 117);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "图像分类";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(86, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 15);
            this.label25.TabIndex = 11;
            this.label25.Text = "方法";
            // 
            // cmb_ClassifyMethod
            // 
            this.cmb_ClassifyMethod.FormattingEnabled = true;
            this.cmb_ClassifyMethod.Items.AddRange(new object[] {
            "极大似然法",
            "类概率法"});
            this.cmb_ClassifyMethod.Location = new System.Drawing.Point(129, 55);
            this.cmb_ClassifyMethod.Name = "cmb_ClassifyMethod";
            this.cmb_ClassifyMethod.Size = new System.Drawing.Size(118, 23);
            this.cmb_ClassifyMethod.TabIndex = 10;
            // 
            // txb_ResultPath
            // 
            this.txb_ResultPath.Location = new System.Drawing.Point(95, 84);
            this.txb_ResultPath.Name = "txb_ResultPath";
            this.txb_ResultPath.Size = new System.Drawing.Size(196, 25);
            this.txb_ResultPath.TabIndex = 9;
            this.txb_ResultPath.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txb_ResultPath_MouseDown);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 94);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 15);
            this.label24.TabIndex = 8;
            this.label24.Text = "结果路径";
            // 
            // btn_Classify
            // 
            this.btn_Classify.Location = new System.Drawing.Point(223, 22);
            this.btn_Classify.Name = "btn_Classify";
            this.btn_Classify.Size = new System.Drawing.Size(68, 25);
            this.btn_Classify.TabIndex = 7;
            this.btn_Classify.Text = "分类";
            this.btn_Classify.UseVisualStyleBackColor = true;
            this.btn_Classify.Click += new System.EventHandler(this.btn_Classify_Click);
            // 
            // txb_ClassNum
            // 
            this.txb_ClassNum.Location = new System.Drawing.Point(49, 53);
            this.txb_ClassNum.Name = "txb_ClassNum";
            this.txb_ClassNum.Size = new System.Drawing.Size(31, 25);
            this.txb_ClassNum.TabIndex = 3;
            this.txb_ClassNum.Text = "5";
            // 
            // cmb_ClassifyLayer
            // 
            this.cmb_ClassifyLayer.FormattingEnabled = true;
            this.cmb_ClassifyLayer.Location = new System.Drawing.Point(49, 24);
            this.cmb_ClassifyLayer.Name = "cmb_ClassifyLayer";
            this.cmb_ClassifyLayer.Size = new System.Drawing.Size(168, 23);
            this.cmb_ClassifyLayer.TabIndex = 2;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 63);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 15);
            this.label22.TabIndex = 1;
            this.label22.Text = "数量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "图层";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox16);
            this.tabPage4.Controls.Add(this.groupBox15);
            this.tabPage4.Controls.Add(this.groupBox14);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(353, 594);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "空间分析";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label41);
            this.groupBox14.Controls.Add(this.btn_Visibility);
            this.groupBox14.Controls.Add(this.cmb_VisibilityDEM);
            this.groupBox14.Controls.Add(this.label40);
            this.groupBox14.Controls.Add(this.btn_LineOfSight);
            this.groupBox14.Controls.Add(this.cmb_LineOfSightDEM);
            this.groupBox14.Controls.Add(this.label39);
            this.groupBox14.Controls.Add(this.btn_Aspect);
            this.groupBox14.Controls.Add(this.btn_HillShade);
            this.groupBox14.Controls.Add(this.cmb_AspectDEM);
            this.groupBox14.Controls.Add(this.label37);
            this.groupBox14.Controls.Add(this.cmb_SlopeDEM);
            this.groupBox14.Controls.Add(this.btn_Slope);
            this.groupBox14.Controls.Add(this.cmb_HillshadeDEM);
            this.groupBox14.Controls.Add(this.label38);
            this.groupBox14.Location = new System.Drawing.Point(6, 7);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(297, 169);
            this.groupBox14.TabIndex = 13;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "DEM";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 61);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(31, 15);
            this.label37.TabIndex = 11;
            this.label37.Text = "DEM";
            // 
            // cmb_SlopeDEM
            // 
            this.cmb_SlopeDEM.AutoCompleteCustomSource.AddRange(new string[] {
            "聚合",
            "边界清理",
            "众数滤波"});
            this.cmb_SlopeDEM.FormattingEnabled = true;
            this.cmb_SlopeDEM.Items.AddRange(new object[] {
            "聚合",
            "边界清理",
            "众数滤波 "});
            this.cmb_SlopeDEM.Location = new System.Drawing.Point(49, 53);
            this.cmb_SlopeDEM.Name = "cmb_SlopeDEM";
            this.cmb_SlopeDEM.Size = new System.Drawing.Size(152, 23);
            this.cmb_SlopeDEM.TabIndex = 10;
            // 
            // btn_Slope
            // 
            this.btn_Slope.Location = new System.Drawing.Point(207, 53);
            this.btn_Slope.Name = "btn_Slope";
            this.btn_Slope.Size = new System.Drawing.Size(84, 25);
            this.btn_Slope.TabIndex = 6;
            this.btn_Slope.Text = "坡度函数";
            this.btn_Slope.UseVisualStyleBackColor = true;
            this.btn_Slope.Click += new System.EventHandler(this.btn_Slope_Click);
            // 
            // cmb_HillshadeDEM
            // 
            this.cmb_HillshadeDEM.FormattingEnabled = true;
            this.cmb_HillshadeDEM.Location = new System.Drawing.Point(49, 24);
            this.cmb_HillshadeDEM.Name = "cmb_HillshadeDEM";
            this.cmb_HillshadeDEM.Size = new System.Drawing.Size(152, 23);
            this.cmb_HillshadeDEM.TabIndex = 2;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 30);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 15);
            this.label38.TabIndex = 0;
            this.label38.Text = "DEM";
            // 
            // cmb_AspectDEM
            // 
            this.cmb_AspectDEM.FormattingEnabled = true;
            this.cmb_AspectDEM.Location = new System.Drawing.Point(49, 82);
            this.cmb_AspectDEM.Name = "cmb_AspectDEM";
            this.cmb_AspectDEM.Size = new System.Drawing.Size(152, 23);
            this.cmb_AspectDEM.TabIndex = 12;
            // 
            // btn_HillShade
            // 
            this.btn_HillShade.Location = new System.Drawing.Point(207, 25);
            this.btn_HillShade.Name = "btn_HillShade";
            this.btn_HillShade.Size = new System.Drawing.Size(84, 25);
            this.btn_HillShade.TabIndex = 13;
            this.btn_HillShade.Text = "山地阴影";
            this.btn_HillShade.UseVisualStyleBackColor = true;
            this.btn_HillShade.Click += new System.EventHandler(this.btn_HillShade_Click);
            // 
            // btn_Aspect
            // 
            this.btn_Aspect.Location = new System.Drawing.Point(207, 80);
            this.btn_Aspect.Name = "btn_Aspect";
            this.btn_Aspect.Size = new System.Drawing.Size(84, 25);
            this.btn_Aspect.TabIndex = 14;
            this.btn_Aspect.Text = "坡向函数";
            this.btn_Aspect.UseVisualStyleBackColor = true;
            this.btn_Aspect.Click += new System.EventHandler(this.btn_Aspect_Click);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 90);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(31, 15);
            this.label39.TabIndex = 15;
            this.label39.Text = "DEM";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 119);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(31, 15);
            this.label40.TabIndex = 18;
            this.label40.Text = "DEM";
            // 
            // btn_LineOfSight
            // 
            this.btn_LineOfSight.Location = new System.Drawing.Point(207, 109);
            this.btn_LineOfSight.Name = "btn_LineOfSight";
            this.btn_LineOfSight.Size = new System.Drawing.Size(84, 25);
            this.btn_LineOfSight.TabIndex = 17;
            this.btn_LineOfSight.Text = "通视分析";
            this.btn_LineOfSight.UseVisualStyleBackColor = true;
            this.btn_LineOfSight.Click += new System.EventHandler(this.btn_LineOfSight_Click);
            // 
            // cmb_LineOfSightDEM
            // 
            this.cmb_LineOfSightDEM.FormattingEnabled = true;
            this.cmb_LineOfSightDEM.Location = new System.Drawing.Point(49, 111);
            this.cmb_LineOfSightDEM.Name = "cmb_LineOfSightDEM";
            this.cmb_LineOfSightDEM.Size = new System.Drawing.Size(152, 23);
            this.cmb_LineOfSightDEM.TabIndex = 16;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 146);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(31, 15);
            this.label41.TabIndex = 21;
            this.label41.Text = "DEM";
            // 
            // btn_Visibility
            // 
            this.btn_Visibility.Location = new System.Drawing.Point(207, 136);
            this.btn_Visibility.Name = "btn_Visibility";
            this.btn_Visibility.Size = new System.Drawing.Size(84, 25);
            this.btn_Visibility.TabIndex = 20;
            this.btn_Visibility.Text = "视域分析";
            this.btn_Visibility.UseVisualStyleBackColor = true;
            this.btn_Visibility.Click += new System.EventHandler(this.btn_Visibility_Click);
            // 
            // cmb_VisibilityDEM
            // 
            this.cmb_VisibilityDEM.FormattingEnabled = true;
            this.cmb_VisibilityDEM.Location = new System.Drawing.Point(49, 138);
            this.cmb_VisibilityDEM.Name = "cmb_VisibilityDEM";
            this.cmb_VisibilityDEM.Size = new System.Drawing.Size(152, 23);
            this.cmb_VisibilityDEM.TabIndex = 19;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 30);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(37, 15);
            this.label46.TabIndex = 0;
            this.label46.Text = "栅格";
            // 
            // cmb_NeighborhoodLayer
            // 
            this.cmb_NeighborhoodLayer.FormattingEnabled = true;
            this.cmb_NeighborhoodLayer.Location = new System.Drawing.Point(49, 24);
            this.cmb_NeighborhoodLayer.Name = "cmb_NeighborhoodLayer";
            this.cmb_NeighborhoodLayer.Size = new System.Drawing.Size(152, 23);
            this.cmb_NeighborhoodLayer.TabIndex = 2;
            // 
            // btn_Neighborhood
            // 
            this.btn_Neighborhood.Location = new System.Drawing.Point(207, 53);
            this.btn_Neighborhood.Name = "btn_Neighborhood";
            this.btn_Neighborhood.Size = new System.Drawing.Size(84, 25);
            this.btn_Neighborhood.TabIndex = 6;
            this.btn_Neighborhood.Text = "领域分析";
            this.btn_Neighborhood.UseVisualStyleBackColor = true;
            this.btn_Neighborhood.Click += new System.EventHandler(this.btn_Neighborhood_Click);
            // 
            // cmb_NeighborhoodMethod
            // 
            this.cmb_NeighborhoodMethod.AutoCompleteCustomSource.AddRange(new string[] {
            "聚合",
            "边界清理",
            "众数滤波"});
            this.cmb_NeighborhoodMethod.FormattingEnabled = true;
            this.cmb_NeighborhoodMethod.Items.AddRange(new object[] {
            "esriGeoAnalysisStatsMajority",
            "esriGeoAnalysisStatsMaximum",
            "esriGeoAnalysisStatsMean",
            "esriGeoAnalysisStatsMedian",
            "esriGeoAnalysisStatsMinimum",
            "esriGeoAnalysisStatsMinority",
            "esriGeoAnalysisStatsRange",
            "esriGeoAnalysisStatsStd",
            "esriGeoAnalysisStatsSum",
            "esriGeoAnalysisStatsVariety",
            "esriGeoAnalysisStatsLength",
            "esriGeoAnalysisFilter3x3LowPass",
            "esriGeoAnalysisFilter3x3HighPass"});
            this.cmb_NeighborhoodMethod.Location = new System.Drawing.Point(49, 53);
            this.cmb_NeighborhoodMethod.Name = "cmb_NeighborhoodMethod";
            this.cmb_NeighborhoodMethod.Size = new System.Drawing.Size(152, 23);
            this.cmb_NeighborhoodMethod.TabIndex = 10;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 61);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(37, 15);
            this.label45.TabIndex = 11;
            this.label45.Text = "方法";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label45);
            this.groupBox15.Controls.Add(this.cmb_NeighborhoodMethod);
            this.groupBox15.Controls.Add(this.btn_Neighborhood);
            this.groupBox15.Controls.Add(this.cmb_NeighborhoodLayer);
            this.groupBox15.Controls.Add(this.label46);
            this.groupBox15.Location = new System.Drawing.Point(6, 182);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(297, 85);
            this.groupBox15.TabIndex = 22;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "领域分析";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.btn_Extraction);
            this.groupBox16.Controls.Add(this.cmb_ExtractionLayer);
            this.groupBox16.Controls.Add(this.label43);
            this.groupBox16.Location = new System.Drawing.Point(6, 273);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(297, 53);
            this.groupBox16.TabIndex = 23;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "领域分析";
            // 
            // btn_Extraction
            // 
            this.btn_Extraction.Location = new System.Drawing.Point(207, 24);
            this.btn_Extraction.Name = "btn_Extraction";
            this.btn_Extraction.Size = new System.Drawing.Size(84, 25);
            this.btn_Extraction.TabIndex = 6;
            this.btn_Extraction.Text = "裁剪分析";
            this.btn_Extraction.UseVisualStyleBackColor = true;
            this.btn_Extraction.Click += new System.EventHandler(this.btn_Extraction_Click);
            // 
            // cmb_ExtractionLayer
            // 
            this.cmb_ExtractionLayer.FormattingEnabled = true;
            this.cmb_ExtractionLayer.Location = new System.Drawing.Point(49, 24);
            this.cmb_ExtractionLayer.Name = "cmb_ExtractionLayer";
            this.cmb_ExtractionLayer.Size = new System.Drawing.Size(152, 23);
            this.cmb_ExtractionLayer.TabIndex = 2;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 30);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(37, 15);
            this.label43.TabIndex = 0;
            this.label43.Text = "栅格";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 676);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.axMapControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "ArcEngine Controls Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ColorBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ToColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FromColor)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.图像融合.ResumeLayout(false);
            this.图像融合.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNewDoc;
        private System.Windows.Forms.ToolStripMenuItem menuOpenDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExitApp;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarXY;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI2_loadFromSDE;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_ZoomToLayer;
        private System.Windows.Forms.ToolStripMenuItem TSMI_DeleteLayer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Statistics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmb_DrawHisBand;
        private System.Windows.Forms.ComboBox cmb_DrawHisLayer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_MultiBandHis;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_NDVILayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_CalculateNDVI;
        private System.Windows.Forms.ComboBox cmb_StatisticsBand;
        private System.Windows.Forms.ComboBox cmb_StatisticsLayer;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmb_RenderLayer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Render;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmb_StretchBand;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_StretchMethod;
        private System.Windows.Forms.ComboBox cmb_StretchLayer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_Stretch;
        private System.Windows.Forms.Button btn_SingleBandHis;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmb_BBand;
        private System.Windows.Forms.ComboBox cmb_GBand;
        private System.Windows.Forms.ComboBox cmb_RBand;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmb_RGBLayer;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmb_RenderBand;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_LoadRstDataset;
        private System.Windows.Forms.ComboBox cmb_LoadRstCatalog;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btn_LoadRstDataset;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btn_ImportRstDataset;
        private System.Windows.Forms.Button btn_NewRstCatalog;
        private System.Windows.Forms.TextBox txb_NewRstDataset;
        private System.Windows.Forms.TextBox txb_NewRstCatalog;
        private System.Windows.Forms.Button btn_DeleteRstDataset;
        private System.Windows.Forms.Button btn_DeleteRstCatalog;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox pb_ColorBar;
        private System.Windows.Forms.PictureBox pb_ToColor;
        private System.Windows.Forms.PictureBox pb_FromColor;
        private System.Windows.Forms.Button btn_RGB;
        private System.Windows.Forms.ColorDialog cd_FromColor;
        private System.Windows.Forms.ColorDialog cd_ToColor;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btn_Classify;
        private System.Windows.Forms.TextBox txb_ClassNum;
        private System.Windows.Forms.ComboBox cmb_ClassifyLayer;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmb_ClassifyMethod;
        private System.Windows.Forms.TextBox txb_ResultPath;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmb_GeneralMethod;
        private System.Windows.Forms.Button btn_Matrix;
        private System.Windows.Forms.ComboBox cmb_GeneralLayer;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox txb_ClipFeature;
        private System.Windows.Forms.ComboBox cmb_ClipLayer;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btn_Clip;
        private System.Windows.Forms.GroupBox 图像融合;
        private System.Windows.Forms.ComboBox cmb_MultiLayer;
        private System.Windows.Forms.ComboBox cmb_PanLayer;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btn_PanSharpen;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button btn_Mosaic;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.ComboBox cmb_TransformLayer;
        private System.Windows.Forms.Button btn_Transform;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cmb_TransformMethod;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cmb_FilterMethod;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btn_Filter;
        private System.Windows.Forms.TextBox txb_Transformangle;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmb_MosaicCatalog;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmb_NeighborhoodMethod;
        private System.Windows.Forms.Button btn_Neighborhood;
        private System.Windows.Forms.ComboBox cmb_NeighborhoodLayer;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Button btn_Visibility;
        private System.Windows.Forms.ComboBox cmb_VisibilityDEM;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button btn_LineOfSight;
        private System.Windows.Forms.ComboBox cmb_LineOfSightDEM;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button btn_Aspect;
        private System.Windows.Forms.Button btn_HillShade;
        private System.Windows.Forms.ComboBox cmb_AspectDEM;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmb_SlopeDEM;
        private System.Windows.Forms.Button btn_Slope;
        private System.Windows.Forms.ComboBox cmb_HillshadeDEM;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Button btn_Extraction;
        private System.Windows.Forms.ComboBox cmb_ExtractionLayer;
        private System.Windows.Forms.Label label43;
    }
}


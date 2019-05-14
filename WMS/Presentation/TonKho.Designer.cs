namespace Presentation
{
    partial class TonKho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TonKho));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.GridTonKho = new DevExpress.XtraGrid.GridControl();
            this.gridViewTonKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTongTrongLuong = new DevExpress.XtraEditors.TextEdit();
            this.txtSoLuong = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTongTrongLuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInvoice);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 44);
            this.panel1.TabIndex = 2;
            // 
            // btnInvoice
            // 
            this.btnInvoice.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoice.Appearance.Options.UseFont = true;
            this.btnInvoice.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoice.ImageOptions.Image")));
            this.btnInvoice.Location = new System.Drawing.Point(306, 6);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(105, 32);
            this.btnInvoice.TabIndex = 5;
            this.btnInvoice.Text = "Report";
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.ImageOptions.Image")));
            this.btnExcel.Location = new System.Drawing.Point(165, 6);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(105, 32);
            this.btnExcel.TabIndex = 4;
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(15, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // GridTonKho
            // 
            this.GridTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridTonKho.Location = new System.Drawing.Point(0, 44);
            this.GridTonKho.MainView = this.gridViewTonKho;
            this.GridTonKho.Name = "GridTonKho";
            this.GridTonKho.Size = new System.Drawing.Size(639, 350);
            this.GridTonKho.TabIndex = 3;
            this.GridTonKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTonKho});
            // 
            // gridViewTonKho
            // 
            this.gridViewTonKho.GridControl = this.GridTonKho;
            this.gridViewTonKho.Name = "gridViewTonKho";
            this.gridViewTonKho.OptionsView.ShowGroupPanel = false;
            this.gridViewTonKho.ColumnFilterChanged += new System.EventHandler(this.gridViewTonKho_ColumnFilterChanged);
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.txtTongTrongLuong);
            this.layoutControl3.Controls.Add(this.txtSoLuong);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl3.Location = new System.Drawing.Point(0, 348);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(639, 46);
            this.layoutControl3.TabIndex = 5;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // txtTongTrongLuong
            // 
            this.txtTongTrongLuong.Location = new System.Drawing.Point(423, 12);
            this.txtTongTrongLuong.Name = "txtTongTrongLuong";
            this.txtTongTrongLuong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongTrongLuong.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtTongTrongLuong.Properties.Appearance.Options.UseFont = true;
            this.txtTongTrongLuong.Properties.Appearance.Options.UseForeColor = true;
            this.txtTongTrongLuong.Size = new System.Drawing.Size(204, 22);
            this.txtTongTrongLuong.StyleController = this.layoutControl3;
            this.txtTongTrongLuong.TabIndex = 5;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(114, 12);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoLuong.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtSoLuong.Properties.Appearance.Options.UseFont = true;
            this.txtSoLuong.Properties.Appearance.Options.UseForeColor = true;
            this.txtSoLuong.Size = new System.Drawing.Size(203, 22);
            this.txtSoLuong.StyleController = this.layoutControl3;
            this.txtSoLuong.TabIndex = 4;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(639, 46);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem10.Control = this.txtSoLuong;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(309, 26);
            this.layoutControlItem10.Text = "Số lượng";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(99, 16);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.Control = this.txtTongTrongLuong;
            this.layoutControlItem11.Location = new System.Drawing.Point(309, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(310, 26);
            this.layoutControlItem11.Text = "Tổng trọng lượng";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(99, 16);
            // 
            // TonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 394);
            this.Controls.Add(this.layoutControl3);
            this.Controls.Add(this.GridTonKho);
            this.Controls.Add(this.panel1);
            this.Name = "TonKho";
            this.Text = "Tồn kho";
            this.Load += new System.EventHandler(this.TonKho_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTongTrongLuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraGrid.GridControl GridTonKho;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTonKho;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.TextEdit txtTongTrongLuong;
        private DevExpress.XtraEditors.TextEdit txtSoLuong;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton btnInvoice;
    }
}
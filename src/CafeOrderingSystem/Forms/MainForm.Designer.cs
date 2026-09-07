namespace CafeOrderingSystem.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.lblTitle = new System.Windows.Forms.Label();

        this.grpOrder = new System.Windows.Forms.GroupBox();
        this.lblFood = new System.Windows.Forms.Label();
        this.cmbFood = new System.Windows.Forms.ComboBox();
        this.lblFoodQty = new System.Windows.Forms.Label();
        this.txtFoodQty = new System.Windows.Forms.TextBox();
        this.lblDrink = new System.Windows.Forms.Label();
        this.cmbDrink = new System.Windows.Forms.ComboBox();
        this.lblDrinkQty = new System.Windows.Forms.Label();
        this.txtDrinkQty = new System.Windows.Forms.TextBox();

        this.grpOptions = new System.Windows.Forms.GroupBox();
        this.radDineIn = new System.Windows.Forms.RadioButton();
        this.radTakeout = new System.Windows.Forms.RadioButton();
        this.radDelivery = new System.Windows.Forms.RadioButton();
        this.chkSenior = new System.Windows.Forms.CheckBox();
        this.lblOrderTypeInfo = new System.Windows.Forms.Label();

        this.grpPayment = new System.Windows.Forms.GroupBox();
        this.lblPayment = new System.Windows.Forms.Label();
        this.txtPayment = new System.Windows.Forms.TextBox();

        this.grpSummary = new System.Windows.Forms.GroupBox();
        this.lblSubtotalCap = new System.Windows.Forms.Label();
        this.lblSubtotal = new System.Windows.Forms.Label();
        this.lblDiscountCap = new System.Windows.Forms.Label();
        this.lblDiscount = new System.Windows.Forms.Label();
        this.lblChargeCap = new System.Windows.Forms.Label();
        this.lblCharge = new System.Windows.Forms.Label();
        this.lblTotalCap = new System.Windows.Forms.Label();
        this.lblTotal = new System.Windows.Forms.Label();
        this.lblChangeCap = new System.Windows.Forms.Label();
        this.lblChange = new System.Windows.Forms.Label();

        this.btnCalculate = new System.Windows.Forms.Button();
        this.btnClear = new System.Windows.Forms.Button();
        this.btnExit = new System.Windows.Forms.Button();

        this.grpReceipt = new System.Windows.Forms.GroupBox();
        this.txtReceipt = new System.Windows.Forms.TextBox();

        this.statusStrip = new System.Windows.Forms.StatusStrip();
        this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();

        this.grpOrder.SuspendLayout();
        this.grpOptions.SuspendLayout();
        this.grpPayment.SuspendLayout();
        this.grpSummary.SuspendLayout();
        this.grpReceipt.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();

        // ============================== lblTitle ==========================
        this.lblTitle.BackColor = System.Drawing.Color.FromArgb(62, 39, 35);
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitle.ForeColor = System.Drawing.Color.White;
        this.lblTitle.Location = new System.Drawing.Point(0, 0);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
        this.lblTitle.Size = new System.Drawing.Size(1004, 52);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "ABC CAFÉ — Ordering and Billing System";
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        // ============================== grpOrder ==========================
        this.grpOrder.Controls.Add(this.txtDrinkQty);
        this.grpOrder.Controls.Add(this.lblDrinkQty);
        this.grpOrder.Controls.Add(this.cmbDrink);
        this.grpOrder.Controls.Add(this.lblDrink);
        this.grpOrder.Controls.Add(this.txtFoodQty);
        this.grpOrder.Controls.Add(this.lblFoodQty);
        this.grpOrder.Controls.Add(this.cmbFood);
        this.grpOrder.Controls.Add(this.lblFood);
        this.grpOrder.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.grpOrder.Location = new System.Drawing.Point(16, 68);
        this.grpOrder.Name = "grpOrder";
        this.grpOrder.Size = new System.Drawing.Size(520, 140);
        this.grpOrder.TabIndex = 1;
        this.grpOrder.TabStop = false;
        this.grpOrder.Text = "Order";

        this.lblFood.AutoSize = true;
        this.lblFood.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblFood.Location = new System.Drawing.Point(18, 38);
        this.lblFood.Name = "lblFood";
        this.lblFood.Size = new System.Drawing.Size(45, 20);
        this.lblFood.TabIndex = 0;
        this.lblFood.Text = "Food:";

        this.cmbFood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbFood.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.cmbFood.Location = new System.Drawing.Point(100, 34);
        this.cmbFood.Name = "cmbFood";
        this.cmbFood.Size = new System.Drawing.Size(250, 28);
        this.cmbFood.TabIndex = 1;

        this.lblFoodQty.AutoSize = true;
        this.lblFoodQty.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblFoodQty.Location = new System.Drawing.Point(366, 38);
        this.lblFoodQty.Name = "lblFoodQty";
        this.lblFoodQty.Size = new System.Drawing.Size(30, 20);
        this.lblFoodQty.TabIndex = 2;
        this.lblFoodQty.Text = "Qty:";

        this.txtFoodQty.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.txtFoodQty.Location = new System.Drawing.Point(402, 34);
        this.txtFoodQty.MaxLength = 3;
        this.txtFoodQty.Name = "txtFoodQty";
        this.txtFoodQty.Size = new System.Drawing.Size(96, 27);
        this.txtFoodQty.TabIndex = 3;
        this.txtFoodQty.Text = "0";
        this.txtFoodQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.txtFoodQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Quantity_KeyPress);

        this.lblDrink.AutoSize = true;
        this.lblDrink.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblDrink.Location = new System.Drawing.Point(18, 84);
        this.lblDrink.Name = "lblDrink";
        this.lblDrink.Size = new System.Drawing.Size(47, 20);
        this.lblDrink.TabIndex = 4;
        this.lblDrink.Text = "Drink:";

        this.cmbDrink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbDrink.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.cmbDrink.Location = new System.Drawing.Point(100, 80);
        this.cmbDrink.Name = "cmbDrink";
        this.cmbDrink.Size = new System.Drawing.Size(250, 28);
        this.cmbDrink.TabIndex = 5;

        this.lblDrinkQty.AutoSize = true;
        this.lblDrinkQty.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblDrinkQty.Location = new System.Drawing.Point(366, 84);
        this.lblDrinkQty.Name = "lblDrinkQty";
        this.lblDrinkQty.Size = new System.Drawing.Size(30, 20);
        this.lblDrinkQty.TabIndex = 6;
        this.lblDrinkQty.Text = "Qty:";

        this.txtDrinkQty.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.txtDrinkQty.Location = new System.Drawing.Point(402, 80);
        this.txtDrinkQty.MaxLength = 3;
        this.txtDrinkQty.Name = "txtDrinkQty";
        this.txtDrinkQty.Size = new System.Drawing.Size(96, 27);
        this.txtDrinkQty.TabIndex = 7;
        this.txtDrinkQty.Text = "0";
        this.txtDrinkQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.txtDrinkQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Quantity_KeyPress);

        // ============================= grpOptions =========================
        this.grpOptions.Controls.Add(this.lblOrderTypeInfo);
        this.grpOptions.Controls.Add(this.chkSenior);
        this.grpOptions.Controls.Add(this.radDelivery);
        this.grpOptions.Controls.Add(this.radTakeout);
        this.grpOptions.Controls.Add(this.radDineIn);
        this.grpOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.grpOptions.Location = new System.Drawing.Point(16, 218);
        this.grpOptions.Name = "grpOptions";
        this.grpOptions.Size = new System.Drawing.Size(520, 128);
        this.grpOptions.TabIndex = 2;
        this.grpOptions.TabStop = false;
        this.grpOptions.Text = "Order Type && Discounts";

        // The GroupBox scopes these three radios into ONE exclusive group.
        this.radDineIn.AutoSize = true;
        this.radDineIn.Checked = true;
        this.radDineIn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.radDineIn.Location = new System.Drawing.Point(22, 34);
        this.radDineIn.Name = "radDineIn";
        this.radDineIn.Size = new System.Drawing.Size(80, 24);
        this.radDineIn.TabIndex = 0;
        this.radDineIn.TabStop = true;
        this.radDineIn.Text = "Dine-in";
        this.radDineIn.UseVisualStyleBackColor = true;
        this.radDineIn.CheckedChanged += new System.EventHandler(this.OrderType_CheckedChanged);

        this.radTakeout.AutoSize = true;
        this.radTakeout.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.radTakeout.Location = new System.Drawing.Point(140, 34);
        this.radTakeout.Name = "radTakeout";
        this.radTakeout.Size = new System.Drawing.Size(146, 24);
        this.radTakeout.TabIndex = 1;
        this.radTakeout.Text = "Takeout (+₱20)";
        this.radTakeout.UseVisualStyleBackColor = true;
        this.radTakeout.CheckedChanged += new System.EventHandler(this.OrderType_CheckedChanged);

        this.radDelivery.AutoSize = true;
        this.radDelivery.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.radDelivery.Location = new System.Drawing.Point(310, 34);
        this.radDelivery.Name = "radDelivery";
        this.radDelivery.Size = new System.Drawing.Size(150, 24);
        this.radDelivery.TabIndex = 2;
        this.radDelivery.Text = "Delivery (+₱50)";
        this.radDelivery.UseVisualStyleBackColor = true;
        this.radDelivery.CheckedChanged += new System.EventHandler(this.OrderType_CheckedChanged);

        this.chkSenior.AutoSize = true;
        this.chkSenior.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.chkSenior.Location = new System.Drawing.Point(22, 68);
        this.chkSenior.Name = "chkSenior";
        this.chkSenior.Size = new System.Drawing.Size(258, 24);
        this.chkSenior.TabIndex = 3;
        this.chkSenior.Text = "Senior Citizen (additional 20% off)";
        this.chkSenior.UseVisualStyleBackColor = true;

        this.lblOrderTypeInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        this.lblOrderTypeInfo.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
        this.lblOrderTypeInfo.Location = new System.Drawing.Point(22, 96);
        this.lblOrderTypeInfo.Name = "lblOrderTypeInfo";
        this.lblOrderTypeInfo.Size = new System.Drawing.Size(480, 22);
        this.lblOrderTypeInfo.TabIndex = 4;
        this.lblOrderTypeInfo.Text = "Orders of ₱500 or more receive 10% off. Delivery is free from ₱1,000.";

        // ============================= grpPayment =========================
        this.grpPayment.Controls.Add(this.txtPayment);
        this.grpPayment.Controls.Add(this.lblPayment);
        this.grpPayment.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.grpPayment.Location = new System.Drawing.Point(16, 356);
        this.grpPayment.Name = "grpPayment";
        this.grpPayment.Size = new System.Drawing.Size(520, 82);
        this.grpPayment.TabIndex = 3;
        this.grpPayment.TabStop = false;
        this.grpPayment.Text = "Payment";

        this.lblPayment.AutoSize = true;
        this.lblPayment.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblPayment.Location = new System.Drawing.Point(18, 38);
        this.lblPayment.Name = "lblPayment";
        this.lblPayment.Size = new System.Drawing.Size(126, 20);
        this.lblPayment.TabIndex = 0;
        this.lblPayment.Text = "Amount received:";

        this.txtPayment.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.txtPayment.Location = new System.Drawing.Point(160, 32);
        this.txtPayment.MaxLength = 10;
        this.txtPayment.Name = "txtPayment";
        this.txtPayment.PlaceholderText = "0.00";
        this.txtPayment.Size = new System.Drawing.Size(190, 32);
        this.txtPayment.TabIndex = 1;
        this.txtPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.txtPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Payment_KeyPress);

        // ============================= grpSummary =========================
        this.grpSummary.Controls.Add(this.lblChange);
        this.grpSummary.Controls.Add(this.lblChangeCap);
        this.grpSummary.Controls.Add(this.lblTotal);
        this.grpSummary.Controls.Add(this.lblTotalCap);
        this.grpSummary.Controls.Add(this.lblCharge);
        this.grpSummary.Controls.Add(this.lblChargeCap);
        this.grpSummary.Controls.Add(this.lblDiscount);
        this.grpSummary.Controls.Add(this.lblDiscountCap);
        this.grpSummary.Controls.Add(this.lblSubtotal);
        this.grpSummary.Controls.Add(this.lblSubtotalCap);
        this.grpSummary.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.grpSummary.Location = new System.Drawing.Point(16, 448);
        this.grpSummary.Name = "grpSummary";
        this.grpSummary.Size = new System.Drawing.Size(520, 176);
        this.grpSummary.TabIndex = 4;
        this.grpSummary.TabStop = false;
        this.grpSummary.Text = "Summary";

        this.lblSubtotalCap.AutoSize = true;
        this.lblSubtotalCap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblSubtotalCap.Location = new System.Drawing.Point(22, 34);
        this.lblSubtotalCap.Name = "lblSubtotalCap";
        this.lblSubtotalCap.Size = new System.Drawing.Size(66, 20);
        this.lblSubtotalCap.TabIndex = 0;
        this.lblSubtotalCap.Text = "Subtotal";

        this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblSubtotal.Location = new System.Drawing.Point(300, 34);
        this.lblSubtotal.Name = "lblSubtotal";
        this.lblSubtotal.Size = new System.Drawing.Size(196, 22);
        this.lblSubtotal.TabIndex = 1;
        this.lblSubtotal.Text = "₱0.00";
        this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblDiscountCap.AutoSize = true;
        this.lblDiscountCap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblDiscountCap.Location = new System.Drawing.Point(22, 62);
        this.lblDiscountCap.Name = "lblDiscountCap";
        this.lblDiscountCap.Size = new System.Drawing.Size(68, 20);
        this.lblDiscountCap.TabIndex = 2;
        this.lblDiscountCap.Text = "Discount";

        this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(29, 92, 70);
        this.lblDiscount.Location = new System.Drawing.Point(300, 62);
        this.lblDiscount.Name = "lblDiscount";
        this.lblDiscount.Size = new System.Drawing.Size(196, 22);
        this.lblDiscount.TabIndex = 3;
        this.lblDiscount.Text = "₱0.00";
        this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblChargeCap.AutoSize = true;
        this.lblChargeCap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblChargeCap.Location = new System.Drawing.Point(22, 90);
        this.lblChargeCap.Name = "lblChargeCap";
        this.lblChargeCap.Size = new System.Drawing.Size(110, 20);
        this.lblChargeCap.TabIndex = 4;
        this.lblChargeCap.Text = "Service charge";

        this.lblCharge.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblCharge.Location = new System.Drawing.Point(300, 90);
        this.lblCharge.Name = "lblCharge";
        this.lblCharge.Size = new System.Drawing.Size(196, 22);
        this.lblCharge.TabIndex = 5;
        this.lblCharge.Text = "₱0.00";
        this.lblCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblTotalCap.AutoSize = true;
        this.lblTotalCap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTotalCap.Location = new System.Drawing.Point(22, 118);
        this.lblTotalCap.Name = "lblTotalCap";
        this.lblTotalCap.Size = new System.Drawing.Size(64, 28);
        this.lblTotalCap.TabIndex = 6;
        this.lblTotalCap.Text = "TOTAL";

        this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTotal.Location = new System.Drawing.Point(280, 118);
        this.lblTotal.Name = "lblTotal";
        this.lblTotal.Size = new System.Drawing.Size(216, 30);
        this.lblTotal.TabIndex = 7;
        this.lblTotal.Text = "₱0.00";
        this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblChangeCap.AutoSize = true;
        this.lblChangeCap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        this.lblChangeCap.Location = new System.Drawing.Point(22, 150);
        this.lblChangeCap.Name = "lblChangeCap";
        this.lblChangeCap.Size = new System.Drawing.Size(59, 20);
        this.lblChangeCap.TabIndex = 8;
        this.lblChangeCap.Text = "Change";

        this.lblChange.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblChange.Location = new System.Drawing.Point(300, 150);
        this.lblChange.Name = "lblChange";
        this.lblChange.Size = new System.Drawing.Size(196, 22);
        this.lblChange.TabIndex = 9;
        this.lblChange.Text = "₱0.00";
        this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        // ============================== buttons ===========================
        this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(46, 125, 90);
        this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
        this.btnCalculate.ForeColor = System.Drawing.Color.White;
        this.btnCalculate.Location = new System.Drawing.Point(16, 636);
        this.btnCalculate.Name = "btnCalculate";
        this.btnCalculate.Size = new System.Drawing.Size(240, 46);
        this.btnCalculate.TabIndex = 5;
        this.btnCalculate.Text = "Calculate && Print Receipt";
        this.btnCalculate.UseVisualStyleBackColor = false;
        this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

        this.btnClear.Font = new System.Drawing.Font("Segoe UI", 11.25F);
        this.btnClear.Location = new System.Drawing.Point(264, 636);
        this.btnClear.Name = "btnClear";
        this.btnClear.Size = new System.Drawing.Size(140, 46);
        this.btnClear.TabIndex = 6;
        this.btnClear.Text = "New Order";
        this.btnClear.UseVisualStyleBackColor = true;
        this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

        this.btnExit.Font = new System.Drawing.Font("Segoe UI", 11.25F);
        this.btnExit.Location = new System.Drawing.Point(412, 636);
        this.btnExit.Name = "btnExit";
        this.btnExit.Size = new System.Drawing.Size(124, 46);
        this.btnExit.TabIndex = 7;
        this.btnExit.Text = "Exit";
        this.btnExit.UseVisualStyleBackColor = true;
        this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

        // ============================= grpReceipt =========================
        this.grpReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.grpReceipt.Controls.Add(this.txtReceipt);
        this.grpReceipt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.grpReceipt.Location = new System.Drawing.Point(552, 68);
        this.grpReceipt.Name = "grpReceipt";
        this.grpReceipt.Size = new System.Drawing.Size(436, 614);
        this.grpReceipt.TabIndex = 8;
        this.grpReceipt.TabStop = false;
        this.grpReceipt.Text = "Receipt";

        // Consolas is monospaced — essential so the receipt columns line up.
        this.txtReceipt.BackColor = System.Drawing.Color.White;
        this.txtReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtReceipt.Font = new System.Drawing.Font("Consolas", 10.5F);
        this.txtReceipt.Location = new System.Drawing.Point(3, 23);
        this.txtReceipt.Multiline = true;
        this.txtReceipt.Name = "txtReceipt";
        this.txtReceipt.ReadOnly = true;
        this.txtReceipt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtReceipt.Size = new System.Drawing.Size(430, 588);
        this.txtReceipt.TabIndex = 0;

        // ============================= statusStrip ========================
        this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
        this.statusStrip.Location = new System.Drawing.Point(0, 698);
        this.statusStrip.Name = "statusStrip";
        this.statusStrip.Size = new System.Drawing.Size(1004, 26);
        this.statusStrip.TabIndex = 9;

        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(60, 20);
        this.lblStatus.Text = "Ready";

        // =============================== MainForm =========================
        this.AcceptButton = this.btnCalculate;
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(250, 248, 246);
        this.ClientSize = new System.Drawing.Size(1004, 724);
        this.Controls.Add(this.grpReceipt);
        this.Controls.Add(this.btnExit);
        this.Controls.Add(this.btnClear);
        this.Controls.Add(this.btnCalculate);
        this.Controls.Add(this.grpSummary);
        this.Controls.Add(this.grpPayment);
        this.Controls.Add(this.grpOptions);
        this.Controls.Add(this.grpOrder);
        this.Controls.Add(this.lblTitle);
        this.Controls.Add(this.statusStrip);
        this.MinimumSize = new System.Drawing.Size(1020, 770);
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Café Ordering and Billing System";
        this.Load += new System.EventHandler(this.MainForm_Load);

        this.grpOrder.ResumeLayout(false);
        this.grpOrder.PerformLayout();
        this.grpOptions.ResumeLayout(false);
        this.grpOptions.PerformLayout();
        this.grpPayment.ResumeLayout(false);
        this.grpPayment.PerformLayout();
        this.grpSummary.ResumeLayout(false);
        this.grpSummary.PerformLayout();
        this.grpReceipt.ResumeLayout(false);
        this.grpReceipt.PerformLayout();
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label lblTitle;

    private System.Windows.Forms.GroupBox grpOrder;
    private System.Windows.Forms.Label lblFood;
    private System.Windows.Forms.ComboBox cmbFood;
    private System.Windows.Forms.Label lblFoodQty;
    private System.Windows.Forms.TextBox txtFoodQty;
    private System.Windows.Forms.Label lblDrink;
    private System.Windows.Forms.ComboBox cmbDrink;
    private System.Windows.Forms.Label lblDrinkQty;
    private System.Windows.Forms.TextBox txtDrinkQty;

    private System.Windows.Forms.GroupBox grpOptions;
    private System.Windows.Forms.RadioButton radDineIn;
    private System.Windows.Forms.RadioButton radTakeout;
    private System.Windows.Forms.RadioButton radDelivery;
    private System.Windows.Forms.CheckBox chkSenior;
    private System.Windows.Forms.Label lblOrderTypeInfo;

    private System.Windows.Forms.GroupBox grpPayment;
    private System.Windows.Forms.Label lblPayment;
    private System.Windows.Forms.TextBox txtPayment;

    private System.Windows.Forms.GroupBox grpSummary;
    private System.Windows.Forms.Label lblSubtotalCap;
    private System.Windows.Forms.Label lblSubtotal;
    private System.Windows.Forms.Label lblDiscountCap;
    private System.Windows.Forms.Label lblDiscount;
    private System.Windows.Forms.Label lblChargeCap;
    private System.Windows.Forms.Label lblCharge;
    private System.Windows.Forms.Label lblTotalCap;
    private System.Windows.Forms.Label lblTotal;
    private System.Windows.Forms.Label lblChangeCap;
    private System.Windows.Forms.Label lblChange;

    private System.Windows.Forms.Button btnCalculate;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnExit;

    private System.Windows.Forms.GroupBox grpReceipt;
    private System.Windows.Forms.TextBox txtReceipt;

    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
}

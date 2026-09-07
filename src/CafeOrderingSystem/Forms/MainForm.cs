using CafeOrderingSystem.Models;
using CafeOrderingSystem.Services;

namespace CafeOrderingSystem.Forms;

/// <summary>
/// Cashier screen for the café.
///
/// The form's only jobs are: gather input, VALIDATE it, hand it to
/// <see cref="BillingCalculator"/>, and display what comes back. No money
/// arithmetic happens in this file.
/// </summary>
public partial class MainForm : Form
{
    // FIELDS, not locals. Each button click is a separate event and each
    // handler returns immediately, so anything that must survive between
    // clicks has to live at class level.
    private readonly BillingCalculator _calculator = new();
    private readonly ReceiptBuilder _receiptBuilder = new();

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        LoadMenu();
        UpdateChargeHint();
        SetStatus("Ready. Select a food item and a drink to begin.");
    }

    /// <summary>
    /// Loads the menu from the brief. The ComboBoxes hold MenuItem OBJECTS,
    /// not strings — so the selected item already knows its own price and no
    /// name-to-price lookup is needed. MenuItem.ToString() controls display.
    /// </summary>
    private void LoadMenu()
    {
        cmbFood.Items.AddRange(new object[]
        {
            new MenuItem("Burger",            85m),
            new MenuItem("Chicken Sandwich",  75m),
            new MenuItem("Spaghetti",         95m),
            new MenuItem("French Fries",      50m)
        });

        cmbDrink.Items.AddRange(new object[]
        {
            new MenuItem("Soft Drink",     35m),
            new MenuItem("Iced Tea",       40m),
            new MenuItem("Coffee",         45m),
            new MenuItem("Bottled Water",  25m)
        });

        // Deliberately left unselected so validation steps 1 and 2 can be
        // demonstrated. SelectedIndex stays -1 until the cashier chooses.
    }

    // =================================================================
    //  Main handler — the eleven required steps, in order
    // =================================================================
    private void btnCalculate_Click(object sender, EventArgs e)
    {
        // ---- Step 1: was a food item selected? ----
        if (cmbFood.SelectedItem is not MenuItem food)
        {
            ShowError("Please select a food item.", cmbFood);
            return;
        }

        // ---- Step 2: was a drink selected? ----
        if (cmbDrink.SelectedItem is not MenuItem drink)
        {
            ShowError("Please select a drink item.", cmbDrink);
            return;
        }

        // ---- Step 3: are the quantities valid? ----
        // TryParse, never Parse. Parse throws on "abc" and crashes the app;
        // TryParse returns false and lets us show a proper message.
        if (!int.TryParse(txtFoodQty.Text.Trim(), out int foodQty))
        {
            ShowError("Food quantity must be a whole number.\n\n" +
                      "Letters and symbols are not allowed.", txtFoodQty);
            return;
        }

        if (foodQty <= 0)
        {
            ShowError("Food quantity must be greater than zero.", txtFoodQty);
            return;
        }

        if (!int.TryParse(txtDrinkQty.Text.Trim(), out int drinkQty))
        {
            ShowError("Drink quantity must be a whole number.\n\n" +
                      "Letters and symbols are not allowed.", txtDrinkQty);
            return;
        }

        if (drinkQty <= 0)
        {
            ShowError("Drink quantity must be greater than zero.", txtDrinkQty);
            return;
        }

        // ---- Steps 4–8: prices, totals, discounts, final amount ----
        // All delegated to the calculator; the form does no arithmetic.
        OrderType orderType = GetSelectedOrderType();

        BillResult bill = _calculator.Calculate(
            food, foodQty,
            drink, drinkQty,
            chkSenior.Checked,
            orderType);

        // Show the figures even if payment then fails, so the cashier can tell
        // the customer what is owed.
        DisplaySummary(bill);

        // ---- Step 9: check the payment ----
        if (!decimal.TryParse(txtPayment.Text.Trim(), out decimal payment))
        {
            ShowError("Please enter a valid payment amount.", txtPayment);
            return;
        }

        if (payment <= 0)
        {
            ShowError("Payment must be greater than zero.", txtPayment);
            return;
        }

        // ---- Step 10: change, or insufficient payment ----
        if (payment < bill.Total)
        {
            decimal shortfall = bill.Total - payment;

            MessageBox.Show(
                $"Insufficient payment.\n\n" +
                $"Total due:  ₱{bill.Total:N2}\n" +
                $"Received:   ₱{payment:N2}\n" +
                $"Short by:   ₱{shortfall:N2}",
                "Insufficient Payment",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            lblChange.Text = "INSUFFICIENT";
            lblChange.ForeColor = Color.FromArgb(170, 40, 40);
            txtPayment.Focus();
            txtPayment.SelectAll();
            SetStatus($"Insufficient payment — short by ₱{shortfall:N2}.");
            return;
        }

        decimal change = payment - bill.Total;

        lblChange.Text = $"₱{change:N2}";
        lblChange.ForeColor = Color.FromArgb(29, 92, 70);

        // ---- Step 11: display the receipt ----
        txtReceipt.Text = _receiptBuilder.Build(
            food, foodQty, drink, drinkQty,
            bill, payment, change,
            orderType, chkSenior.Checked);

        SetStatus($"Transaction complete. Change due: ₱{change:N2}");
    }

    // =================================================================
    //  Display
    // =================================================================
    private void DisplaySummary(BillResult bill)
    {
        lblSubtotal.Text = $"₱{bill.Subtotal:N2}";
        lblDiscount.Text = bill.TotalDiscount > 0
            ? $"−₱{bill.TotalDiscount:N2}"
            : "₱0.00";

        lblCharge.Text = bill.ServiceCharge > 0
            ? $"₱{bill.ServiceCharge:N2}"
            : bill.FreeDeliveryApplied ? "FREE" : "₱0.00";

        lblTotal.Text = $"₱{bill.Total:N2}";

        // Tell the cashier which rules fired — useful when a customer queries
        // the bill, and it demonstrates the branching logic is working.
        var notes = new List<string>();
        if (bill.BulkDiscountApplied)   notes.Add("10% bulk discount");
        if (bill.SeniorDiscountApplied) notes.Add("20% senior discount");
        if (bill.FreeDeliveryApplied)   notes.Add("free delivery");

        SetStatus(notes.Count > 0
            ? $"Applied: {string.Join(", ", notes)}."
            : "No discounts applied.");
    }

    // =================================================================
    //  Input helpers
    // =================================================================
    /// <summary>Reads the order-type radio group.</summary>
    private OrderType GetSelectedOrderType()
    {
        if (radTakeout.Checked)  return OrderType.Takeout;
        if (radDelivery.Checked) return OrderType.Delivery;
        return OrderType.DineIn;
    }

    /// <summary>
    /// CheckedChanged fires TWICE per change — once for the radio switching
    /// off, once for the one switching on. Guarding on Checked means the work
    /// happens only once.
    /// </summary>
    private void OrderType_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton { Checked: true })
            UpdateChargeHint();
    }

    private void UpdateChargeHint()
    {
        lblOrderTypeInfo.Text = GetSelectedOrderType() switch
        {
            OrderType.Takeout  => "Takeout adds a ₱20 charge. Orders of ₱500+ get 10% off.",
            OrderType.Delivery => "Delivery is ₱50, free for orders of ₱1,000 or more.",
            _                  => "Dine-in has no service charge. Orders of ₱500+ get 10% off."
        };
    }

    /// <summary>
    /// Rejects non-digits in the quantity boxes as they are typed.
    /// e.Handled = true swallows the keystroke entirely.
    ///
    /// This is belt-and-braces: btnCalculate_Click still calls TryParse,
    /// because a user can paste text straight past a KeyPress filter.
    /// </summary>
    private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            e.Handled = true;
    }

    /// <summary>Digits plus a single decimal point for the payment box.</summary>
    private void Payment_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            return;

        // allow one decimal point only
        if (e.KeyChar == '.' && !txtPayment.Text.Contains('.'))
            return;

        e.Handled = true;
    }

    // =================================================================
    //  Buttons
    // =================================================================
    private void btnClear_Click(object sender, EventArgs e)
    {
        cmbFood.SelectedIndex = -1;
        cmbDrink.SelectedIndex = -1;
        txtFoodQty.Text = "0";
        txtDrinkQty.Text = "0";
        txtPayment.Clear();

        radDineIn.Checked = true;
        chkSenior.Checked = false;

        lblSubtotal.Text = "₱0.00";
        lblDiscount.Text = "₱0.00";
        lblCharge.Text = "₱0.00";
        lblTotal.Text = "₱0.00";
        lblChange.Text = "₱0.00";
        lblChange.ForeColor = SystemColors.ControlText;

        txtReceipt.Clear();

        cmbFood.Focus();
        SetStatus("New order started.");
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Close the Café Ordering System?",
            "Exit",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
            Close();
    }

    // =================================================================
    //  Utilities
    // =================================================================
    private void ShowError(string message, Control focus)
    {
        MessageBox.Show(message, "Invalid Input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

        focus.Focus();
        if (focus is TextBox tb) tb.SelectAll();

        SetStatus("Validation failed — " + message.Split('\n')[0]);
    }

    private void SetStatus(string message) =>
        lblStatus.Text = $"{DateTime.Now:HH:mm:ss}  —  {message}";
}

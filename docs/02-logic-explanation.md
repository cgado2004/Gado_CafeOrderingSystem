# Programming Logic Explanation

**Café Ordering and Billing System** — submission requirement #5.

---

## 1. Program structure

The project separates **calculation** from **presentation**:

```
Program.cs                    entry point, starts the message loop
Forms/MainForm.cs             UI: gathers input, validates, displays
Services/BillingCalculator.cs ALL money arithmetic
Services/ReceiptBuilder.cs    receipt formatting
Models/MenuItem.cs            a menu item (name + price)
Models/OrderType.cs           enumeration: DineIn, Takeout, Delivery
```

**Why separate?** The form does no arithmetic at all. It reads controls,
validates, and calls `BillingCalculator.Calculate(...)`. That means the billing
rules can be checked without clicking a single button, and the UI could be
replaced entirely without touching the money logic.

---

## 2. Variables and data types

| Data | Type | Why |
|---|---|---|
| Prices, totals, payment | **`decimal`** | Exact base-10. **Never `double`/`float` for money** |
| Quantities | `int` | Whole items only |
| Senior citizen flag | `bool` | Two states |
| Order type | `enum OrderType` | Makes invalid types impossible |
| Menu item | `class MenuItem` | Bundles name + price together |

### Why `decimal` and not `double`

`double` is binary floating point. 0.1 has no exact binary representation, so
errors accumulate:

```csharp
double d = 0;
for (int i = 0; i < 10; i++) d += 0.1;
// d == 0.9999999999999999, not 1.0

decimal m = 0;
for (int i = 0; i < 10; i++) m += 0.1m;
// m == 1.0  ✔
```

For a billing system that difference is the whole point — the café hired us
because manual totals were wrong.

### Objects in the ComboBox, not strings

```csharp
cmbFood.Items.Add(new MenuItem("Burger", 85m));
```

The ComboBox holds `MenuItem` **objects**, so the selected item already carries
its price. No `if (name == "Burger") price = 85;` lookup chain is needed —
which would be a maintenance trap the moment a price changes.
`MenuItem.ToString()` controls what the user sees.

---

## 3. Event handling

| Control | Event | Purpose |
|---|---|---|
| `btnCalculate` | `Click` | Runs the eleven-step transaction |
| `btnClear` | `Click` | Resets for a new order |
| `btnExit` | `Click` | Confirms, then closes |
| `txtFoodQty`, `txtDrinkQty` | `KeyPress` | Rejects non-digits as typed |
| `txtPayment` | `KeyPress` | Digits + one decimal point |
| `radDineIn/Takeout/Delivery` | `CheckedChanged` | Updates the charge hint |
| `MainForm` | `Load` | Populates the menu |

### State must live in fields

```csharp
private readonly BillingCalculator _calculator = new();   // FIELD
```

Each click is a separate event and each handler returns immediately. A local
variable would be destroyed before the next click, so anything that must
persist between events belongs at class level.

### The `CheckedChanged` double-fire

Changing a radio selection raises `CheckedChanged` **twice** — once for the
button switching off, once for the one switching on:

```csharp
if (sender is RadioButton { Checked: true })
    UpdateChargeHint();          // only respond to the switch-ON
```

### RadioButtons are grouped by container

The three order-type radios sit inside `grpOptions`. **Grouping is by
container, not by name** — three radios placed loose on the form would still be
mutually exclusive, but adding a second independent set would then be
impossible without another GroupBox or Panel.

---

## 4. Validation

Two layers, deliberately:

**Layer 1 — block it at the keystroke**

```csharp
private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
{
    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        e.Handled = true;        // swallow the keystroke
}
```

`char.IsControl` keeps Backspace and Delete working.

**Layer 2 — verify before using**

```csharp
if (!int.TryParse(txtFoodQty.Text.Trim(), out int foodQty))
{
    ShowError("Food quantity must be a whole number.", txtFoodQty);
    return;
}
```

**Why both?** A `KeyPress` filter can be bypassed by **pasting** with Ctrl+V.
Layer 2 is what actually guarantees safety.

### `TryParse` vs `Parse`

```csharp
int q = int.Parse("abc");                  // ❌ throws FormatException → crash
if (!int.TryParse("abc", out int q)) { }   // ✔ returns false
```

`Parse` throws; `TryParse` reports. In a UI reading user text, always `TryParse`.

### Validation coverage

| Situation | Handled by | Result |
|---|---|---|
| No food selected | `is not MenuItem food` | Error message |
| No drink selected | `is not MenuItem drink` | Error message |
| Quantity 0 or negative | `foodQty <= 0` | Error message |
| Quantity contains letters | `int.TryParse` + `KeyPress` | Error message |
| Payment not a number | `decimal.TryParse` | Error message |
| Payment < total | `payment < bill.Total` | "Insufficient payment" + shortfall |
| Payment sufficient | else branch | Change calculated, receipt printed |

Every failure calls `ShowError`, which displays the message, **focuses the
offending control**, and selects its text so the cashier can retype immediately.

---

## 5. The calculation — the eleven required steps

```
 1. Food selected?      →  cmbFood.SelectedItem is not MenuItem  → error
 2. Drink selected?     →  cmbDrink.SelectedItem is not MenuItem → error
 3. Quantities valid?   →  int.TryParse, then > 0                → error
 4. Get prices          →  food.Price, drink.Price  (from the objects)
 5. Line totals         →  foodTotal  = price × qty
 6. Subtotal            →  foodTotal + drinkTotal
 7. Discounts           →  bulk (≥₱500), senior (20%)
 8. Final total         →  subtotal − discounts + service charge
 9. Check payment       →  decimal.TryParse, then > 0            → error
10. Change / shortfall  →  payment < total ? warn : payment − total
11. Display receipt     →  ReceiptBuilder.Build(...)
```

### Selection logic — `if/else` and `switch`

Bulk discount (`if`):

```csharp
if (subtotal >= BulkDiscountThreshold)          // 500
    bulkDiscount = Round(subtotal * BulkDiscountRate);   // 10%
```

Service charge (`switch` — the C# equivalent of `Select Case`):

```csharp
switch (orderType)
{
    case OrderType.DineIn:
        break;                                   // no charge

    case OrderType.Takeout:
        serviceCharge = TakeoutCharge;           // ₱20
        break;

    case OrderType.Delivery:
        if (subtotal >= FreeDeliveryThreshold)   // ₱1,000
            serviceCharge = 0m;                  // free
        else
            serviceCharge = DeliveryCharge;      // ₱50
        break;
}
```

### Rounding

```csharp
Math.Round(value, 2, MidpointRounding.AwayFromZero)
```

C#'s **default** rounding is `ToEven` (banker's rounding), so `0.125` → `0.12`.
Currency convention is away-from-zero: `0.125` → `0.13`. Specifying it
explicitly avoids centavo discrepancies.

---

## 6. ⚠️ Two policy decisions the brief does not specify

**Raise both with your instructor before submitting.** Each changes the total.

### 6.1 How do the two discounts stack?

The brief says "10% when subtotal ≥ ₱500" and, separately, "Senior Citizen
Discount: an **additional** 20%". It never says how they combine.

| Reading | Subtotal ₱1,000 | Result |
|---|---|---|
| **A. Sequential** *(implemented)* | −10% → ₱900, then −20% of ₱900 = −₱180 | **₱720** |
| **B. Additive** | −30% of ₱1,000 in one step | **₱700** |

**₱20 apart.** Sequential is implemented because the word *"additional"*
implies the second discount applies to an already-reduced amount, and because
it matches real Philippine retail practice.

To switch, one line in `MainForm`:

```csharp
_calculator.UseSequentialDiscounts = false;   // additive
```

> Real senior-citizen rules under RA 9994 also involve VAT exemption and are
> more complex than a flat 20%. This implements the simplified version the
> activity asks for.

### 6.2 Is free delivery judged on the subtotal or the discounted total?

An order with a ₱995 subtotal and a 10% discount comes to ₱895.50.

- **Judged on subtotal (implemented):** ₱995 < ₱1,000 → charge ₱50
- **Judged on discounted total:** ₱895.50 < ₱1,000 → also ₱50

They differ only when the subtotal is over ₱1,000 but the discount pulls it
under. Subtotal is used because "if the **order reaches** ₱1,000" most
naturally describes order value before discounts — and because it's the
customer-friendlier reading.

---

## 7. Verified test results

| Scenario | Input | Expected | Actual |
|---|---|---|---|
| **Brief's example** | 3 Burgers, 2 Iced Teas, ₱500 | Total ₱335, change ₱165 | ✅ ₱335 / ₱165 |
| Bulk boundary (at) | Subtotal ₱500 | 10% off → ₱450 | ✅ ₱450.00 |
| Bulk boundary (below) | Subtotal ₱450 | No discount | ✅ ₱450 |
| Takeout | Subtotal ₱335 | +₱20 → ₱355 | ✅ ₱355 |
| Delivery | Subtotal ₱335 | +₱50 → ₱385 | ✅ ₱385 |
| Free delivery (below) | Subtotal ₱995 | Charge ₱50 | ✅ ₱945.50 |
| Free delivery (at/above) | Subtotal ₱1,090 | Free | ✅ ₱981.00 |
| Senior + bulk | Subtotal ₱1,000 | Sequential → ₱720 | ✅ ₱720.00 |
| Everything | Senior + delivery, ₱1,000 | ₱720, free delivery | ✅ ₱720.00 |

---

## 8. Screenshots to capture

Submission requirements #3 and #4:

1. **Completed interface** — the app on startup, nothing selected yet
2. **Successful transaction** — 3 Burgers + 2 Iced Teas, ₱500 payment, receipt
   showing ₱335 total and ₱165 change (matches the brief exactly)

Worth adding to show validation working:

3. Error dialog with no food selected
4. "Insufficient payment" dialog
5. A senior + delivery order with both discounts on the receipt

Use **Win + Shift + S** (Snipping Tool) and save into `docs/screenshots/`.

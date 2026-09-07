# Café Ordering and Billing System

A Windows Forms desktop application for **ABC Café**. The cashier selects food
and drinks, enters quantities, and the app calculates the subtotal, applies
discounts, adds any service charge, validates payment, computes change, and
prints a receipt.

**No database required** — everything runs in memory.

---

## Running it

**Visual Studio 2022:** open `CafeOrderingSystem.sln`, press **F5**.

**Command line:**
```bash
dotnet run --project src/CafeOrderingSystem
```

Requires the **.NET 8 SDK** and the **".NET desktop development"** workload.

> Only one project in this solution, so no startup-project setting is needed.

---

## The example from the brief

3 Burgers, 2 Iced Teas, ₱500 payment:

```
================================
           ABC CAFÉ
            DINE-IN
================================

2026-09-07 14:32

Burger        3 x P85     P255
Iced Tea      2 x P40      P80
--------------------------------
Subtotal                P335.00
Discount                  P0.00
--------------------------------
TOTAL                   P335.00

Payment                 P500.00
Change                  P165.00
================================
          THANK YOU!
================================
```

✅ Verified: subtotal ₱335, total ₱335, change ₱165 — exactly as specified.

---

## Menu

| Food | Price | | Drink | Price |
|---|---|---|---|---|
| Burger | ₱85 | | Soft Drink | ₱35 |
| Chicken Sandwich | ₱75 | | Iced Tea | ₱40 |
| Spaghetti | ₱95 | | Coffee | ₱45 |
| French Fries | ₱50 | | Bottled Water | ₱25 |

## Rules

| Rule | Condition | Effect |
|---|---|---|
| Bulk discount | Subtotal ≥ ₱500 | −10% |
| Senior citizen | Checkbox ticked | −20% additional |
| Dine-in | — | No charge |
| Takeout | — | +₱20 |
| Delivery | Subtotal < ₱1,000 | +₱50 |
| Free delivery | Subtotal ≥ ₱1,000 | Delivery waived |

---

## Validation

| Situation | Result |
|---|---|
| No food selected | Error message |
| No drink selected | Error message |
| Quantity 0 or negative | Error message |
| Quantity contains letters | Blocked at keystroke **and** on submit |
| Payment less than total | "Insufficient payment" + shortfall |
| Payment sufficient | Change calculated, receipt printed |

---

## Project layout

```
CafeOrderingSystem/
├── CafeOrderingSystem.sln       ← open this in Visual Studio
├── README.md
├── docs/
│   ├── 02-logic-explanation.md  ← submission requirement #5
│   └── screenshots/             ← put your screenshots here
└── src/CafeOrderingSystem/
    ├── Program.cs               entry point + message loop
    ├── Forms/MainForm.cs        UI, validation, the 11 steps
    ├── Services/
    │   ├── BillingCalculator.cs ALL money arithmetic
    │   └── ReceiptBuilder.cs    receipt formatting
    └── Models/
        ├── MenuItem.cs          name + price
        └── OrderType.cs         DineIn / Takeout / Delivery
```

**The form contains no arithmetic.** It gathers input, validates, calls
`BillingCalculator`, and displays the result — so the billing rules can be
verified without clicking anything.

---

## ⚠️ Two things to confirm with your instructor

Both are genuinely ambiguous in the brief and **both change the total**.

### 1. How do the discounts stack?

Subtotal ₱1,000 with a senior citizen:

- **Sequential** *(implemented)* — 10% off → ₱900, then 20% off that → **₱720**
- **Additive** — flat 30% off → **₱700**

₱20 apart. Sequential is used because "**additional** 20%" implies it applies
on top of an already-discounted amount, matching Philippine retail practice.
Switch with one line: `_calculator.UseSequentialDiscounts = false;`

### 2. Is free delivery judged on the subtotal or the discounted total?

Implemented on the **subtotal** — "if the order reaches ₱1,000" most naturally
means order value before discounts.

Full reasoning in [`docs/02-logic-explanation.md`](docs/02-logic-explanation.md).

---

## Submission checklist

- [x] Visual Studio project / source code
- [x] Working application
- [x] Logic explanation → `docs/02-logic-explanation.md`
- [ ] Push to GitHub *(keep it **private** until the module ends)*
- [ ] Screenshot of the completed interface
- [ ] Screenshot of a successful transaction + receipt

Use **Win + Shift + S** and save into `docs/screenshots/`.

---

## Notable techniques

- **`decimal` for all money** — never `double`; binary floats can't represent
  0.1 exactly and drift over repeated addition
- **`TryParse`, never `Parse`** — `Parse` throws and crashes on "abc"
- **Two-layer validation** — `KeyPress` blocks typing, `TryParse` catches
  pasted text
- **`MenuItem` objects in the ComboBox** — the selection carries its own price;
  no name→price lookup chain
- **`switch` on an `enum`** for order type — the C# `Select Case`
- **`MidpointRounding.AwayFromZero`** — C#'s default is banker's rounding,
  which gives ₱0.12 for ₱0.125 instead of the ₱0.13 currency convention
- **Monospaced Consolas** in the receipt box so columns align

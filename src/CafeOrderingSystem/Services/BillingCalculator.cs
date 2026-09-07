using CafeOrderingSystem.Models;

namespace CafeOrderingSystem.Services;

/// <summary>
/// The result of costing an order. A single object carrying every figure the
/// receipt needs, so the UI does no arithmetic of its own.
/// </summary>
public class BillResult
{
    public decimal FoodTotal { get; init; }
    public decimal DrinkTotal { get; init; }
    public decimal Subtotal { get; init; }

    public decimal BulkDiscount { get; init; }      // 10% when subtotal >= 500
    public decimal SeniorDiscount { get; init; }    // 20% when applicable
    public decimal TotalDiscount => BulkDiscount + SeniorDiscount;

    public decimal ServiceCharge { get; init; }     // takeout / delivery fee
    public string ServiceChargeLabel { get; init; } = string.Empty;

    public decimal Total { get; init; }

    public bool BulkDiscountApplied => BulkDiscount > 0;
    public bool SeniorDiscountApplied => SeniorDiscount > 0;
    public bool FreeDeliveryApplied { get; init; }
}

/// <summary>
/// All billing arithmetic for the café, deliberately kept OUT of the form.
///
/// Why separate? The same reason microsoft/calculator keeps its engine apart
/// from its UI: these rules can be verified without clicking a single button,
/// and the UI can be replaced without touching the money logic.
///
/// ─────────────────────────────────────────────────────────────────────────
///  DISCOUNT STACKING POLICY  (the brief does not specify this — see docs)
/// ─────────────────────────────────────────────────────────────────────────
/// The specification says "Subtotal >= ₱500 → 10% discount" and, separately,
/// "Senior Citizen Discount: additional 20%". It never says how the two
/// combine when both apply. Two defensible readings:
///
///   A. SEQUENTIAL (implemented here, and how PH retail actually works)
///      10% off the subtotal, then 20% off what remains.
///      ₱1000 → −₱100 → ₱900 → −₱180 → ₱720
///
///   B. ADDITIVE
///      30% off the original subtotal in one step.
///      ₱1000 → −₱300 → ₱700
///
/// Sequential is used because the word "additional" implies the senior
/// discount applies on top of an already-discounted amount, and because that
/// mirrors real Philippine practice. Flip <see cref="UseSequentialDiscounts"/>
/// to false for additive. CONFIRM WITH YOUR INSTRUCTOR before submitting —
/// this changes the final figure.
///
/// NOTE: Real BIR rules for senior citizens (RA 9994) also involve VAT
/// exemption and are more complex than a flat 20%. This implements the
/// simplified version the activity asks for.
/// </summary>
public class BillingCalculator
{
    public const decimal BulkDiscountThreshold = 500m;
    public const decimal BulkDiscountRate      = 0.10m;
    public const decimal SeniorDiscountRate    = 0.20m;

    public const decimal TakeoutCharge         = 20m;
    public const decimal DeliveryCharge        = 50m;
    public const decimal FreeDeliveryThreshold = 1000m;

    /// <summary>See the class remarks. true = sequential, false = additive.</summary>
    public bool UseSequentialDiscounts { get; set; } = true;

    /// <summary>
    /// Costs an order. Follows the eleven steps in the brief, in order.
    /// </summary>
    public BillResult Calculate(
        MenuItem food, int foodQty,
        MenuItem drink, int drinkQty,
        bool isSeniorCitizen,
        OrderType orderType)
    {
        ArgumentNullException.ThrowIfNull(food);
        ArgumentNullException.ThrowIfNull(drink);

        // Steps 4–5: prices and line totals
        decimal foodTotal  = food.Price  * foodQty;
        decimal drinkTotal = drink.Price * drinkQty;

        // Step 6: subtotal
        decimal subtotal = foodTotal + drinkTotal;

        // Step 7: discounts
        decimal bulkDiscount   = 0m;
        decimal seniorDiscount = 0m;

        if (UseSequentialDiscounts)
        {
            // 10% off the subtotal…
            if (subtotal >= BulkDiscountThreshold)
                bulkDiscount = Round(subtotal * BulkDiscountRate);

            // …then 20% off whatever is left.
            if (isSeniorCitizen)
                seniorDiscount = Round((subtotal - bulkDiscount) * SeniorDiscountRate);
        }
        else
        {
            // Both rates applied to the original subtotal.
            if (subtotal >= BulkDiscountThreshold)
                bulkDiscount = Round(subtotal * BulkDiscountRate);

            if (isSeniorCitizen)
                seniorDiscount = Round(subtotal * SeniorDiscountRate);
        }

        decimal discountedSubtotal = subtotal - bulkDiscount - seniorDiscount;

        // Additional task: service charge by order type.
        // Select Case / switch — the brief asks for If...Else or Select Case.
        decimal serviceCharge = 0m;
        string chargeLabel = string.Empty;
        bool freeDelivery = false;

        switch (orderType)
        {
            case OrderType.DineIn:
                // no charge
                break;

            case OrderType.Takeout:
                serviceCharge = TakeoutCharge;
                chargeLabel = "Takeout Charge";
                break;

            case OrderType.Delivery:
                // Free delivery is judged on the SUBTOTAL (order value), not
                // the discounted amount — see docs/02-logic-explanation.md.
                if (subtotal >= FreeDeliveryThreshold)
                {
                    serviceCharge = 0m;
                    chargeLabel = "Delivery (FREE)";
                    freeDelivery = true;
                }
                else
                {
                    serviceCharge = DeliveryCharge;
                    chargeLabel = "Delivery Charge";
                }
                break;
        }

        // Step 8: final total
        decimal total = discountedSubtotal + serviceCharge;

        return new BillResult
        {
            FoodTotal          = foodTotal,
            DrinkTotal         = drinkTotal,
            Subtotal           = subtotal,
            BulkDiscount       = bulkDiscount,
            SeniorDiscount     = seniorDiscount,
            ServiceCharge      = serviceCharge,
            ServiceChargeLabel = chargeLabel,
            Total              = total,
            FreeDeliveryApplied = freeDelivery
        };
    }

    /// <summary>
    /// Rounds to two decimal places using banker's-rounding-away-from-zero,
    /// i.e. 0.125 → 0.13. MidpointRounding.AwayFromZero is the convention for
    /// currency; C#'s default (ToEven) would give 0.12.
    /// </summary>
    private static decimal Round(decimal value) =>
        Math.Round(value, 2, MidpointRounding.AwayFromZero);
}

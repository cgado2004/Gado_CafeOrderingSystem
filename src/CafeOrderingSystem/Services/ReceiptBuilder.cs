using System.Text;
using CafeOrderingSystem.Models;

namespace CafeOrderingSystem.Services;

/// <summary>
/// Formats a <see cref="BillResult"/> into the fixed-width receipt shown in
/// the brief. Kept separate from both the calculator and the form so the
/// layout can change without touching any arithmetic.
/// </summary>
public class ReceiptBuilder
{
    private const int Width = 32;
    private const string CafeName = "ABC CAFÉ";

    public string Build(
        MenuItem food, int foodQty,
        MenuItem drink, int drinkQty,
        BillResult bill,
        decimal payment,
        decimal change,
        OrderType orderType,
        bool isSeniorCitizen)
    {
        var sb = new StringBuilder();

        sb.AppendLine(new string('=', Width));
        sb.AppendLine(Centre(CafeName));
        sb.AppendLine(Centre(OrderTypeText(orderType)));
        sb.AppendLine(new string('=', Width));
        sb.AppendLine();
        sb.AppendLine($"{DateTime.Now:yyyy-MM-dd HH:mm}");
        sb.AppendLine();

        // Line items: "Burger       3 x P85    P255"
        sb.AppendLine(LineItem(food.Name, foodQty, food.Price, bill.FoodTotal));
        sb.AppendLine(LineItem(drink.Name, drinkQty, drink.Price, bill.DrinkTotal));

        sb.AppendLine(new string('-', Width));
        sb.AppendLine(Amount("Subtotal", bill.Subtotal));

        if (bill.BulkDiscountApplied)
            sb.AppendLine(Amount("Discount (10%)", -bill.BulkDiscount));

        if (bill.SeniorDiscountApplied)
            sb.AppendLine(Amount("Senior Disc. (20%)", -bill.SeniorDiscount));

        // Always show a zero discount line when none applied — the sample
        // receipt in the brief includes "Discount   ₱0".
        if (!bill.BulkDiscountApplied && !bill.SeniorDiscountApplied)
            sb.AppendLine(Amount("Discount", 0m));

        if (!string.IsNullOrEmpty(bill.ServiceChargeLabel))
            sb.AppendLine(Amount(bill.ServiceChargeLabel, bill.ServiceCharge));

        sb.AppendLine(new string('-', Width));
        sb.AppendLine(Amount("TOTAL", bill.Total));
        sb.AppendLine();
        sb.AppendLine(Amount("Payment", payment));
        sb.AppendLine(Amount("Change", change));
        sb.AppendLine(new string('=', Width));

        if (isSeniorCitizen)
        {
            sb.AppendLine(Centre("Senior Citizen Discount"));
            sb.AppendLine(Centre("Applied"));
            sb.AppendLine(new string('=', Width));
        }

        sb.AppendLine(Centre("THANK YOU!"));
        sb.AppendLine(new string('=', Width));

        return sb.ToString();
    }

    private static string OrderTypeText(OrderType type) => type switch
    {
        OrderType.DineIn   => "DINE-IN",
        OrderType.Takeout  => "TAKEOUT",
        OrderType.Delivery => "DELIVERY",
        _                  => string.Empty
    };

    /// <summary>Centres text within the receipt width.</summary>
    private static string Centre(string text)
    {
        if (text.Length >= Width) return text;
        int pad = (Width - text.Length) / 2;
        return new string(' ', pad) + text;
    }

    /// <summary>
    /// "Burger        3 x P85      P255"
    /// Name left-aligned, amount right-aligned to the full width.
    /// </summary>
    private static string LineItem(string name, int qty, decimal price, decimal lineTotal)
    {
        string left = $"{Truncate(name, 13),-13} {qty} x P{price:N0}";
        string right = $"P{lineTotal:N0}";
        int gap = Math.Max(1, Width - left.Length - right.Length);
        return left + new string(' ', gap) + right;
    }

    /// <summary>"Subtotal                  P335" — label left, amount right.</summary>
    private static string Amount(string label, decimal value)
    {
        string right = value < 0 ? $"-P{Math.Abs(value):N2}" : $"P{value:N2}";
        int gap = Math.Max(1, Width - label.Length - right.Length);
        return label + new string(' ', gap) + right;
    }

    private static string Truncate(string text, int max) =>
        text.Length <= max ? text : text[..max];
}

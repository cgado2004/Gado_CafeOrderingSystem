namespace CafeOrderingSystem.Models;

/// <summary>
/// One item on the café menu. Immutable — a menu item's name and price do not
/// change once defined.
///
/// Price is <see cref="decimal"/>, NOT double or float. Money must never be
/// stored in binary floating point: 0.1 cannot be represented exactly, so
/// repeated addition drifts (₱99.99999998). decimal stores digits exactly.
/// </summary>
public class MenuItem
{
    public string Name { get; }
    public decimal Price { get; }

    public MenuItem(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    /// <summary>
    /// Shown in the ComboBox. Overriding ToString means we can bind the objects
    /// themselves rather than strings, so no name-to-price lookup table is
    /// needed — the selected item already carries its price.
    /// </summary>
    public override string ToString() => $"{Name} — {Price:N0}";
}

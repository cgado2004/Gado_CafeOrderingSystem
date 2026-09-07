namespace CafeOrderingSystem.Models;

/// <summary>
/// How the customer is taking the order. Drives the service charge:
///   Dine-in   → no charge
///   Takeout   → flat ₱20
///   Delivery  → ₱50, waived when the order reaches ₱1,000
///
/// An enumeration rather than a string so invalid order types cannot exist.
/// </summary>
public enum OrderType
{
    DineIn,
    Takeout,
    Delivery
}

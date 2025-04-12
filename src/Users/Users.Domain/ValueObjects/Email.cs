using System.Text.RegularExpressions;

namespace Users.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
            throw new ArgumentException("Invalid email address", nameof(value));

        Value = value.ToLowerInvariant(); // normalize
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is Email e && e.Value == Value;
    public override int GetHashCode() => Value.GetHashCode();
}
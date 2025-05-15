using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.ValueObjects.Exceptions;


namespace MarketplaceSale.Domain.ValueObjects.Base
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        public T Value { get; }

        protected ValueObject(IValidator<T> validator, T value)
        {
            if (validator == null)
                throw new ValidatorNullException(
                    GetType().FullName ?? string.Empty,
                    ExceptionMessages.VALIDATOR_MUST_BE_SPECIFIED);

            validator.Validate(value);
            Value = value;
        }

        public override string ToString()
            => Value?.ToString() ?? GetType().ToString();

        public override int GetHashCode()
            => Value?.GetHashCode() ?? 0;

        public override bool Equals(object? obj)
            => Equals(obj as ValueObject<T>);

        public bool Equals(ValueObject<T>? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
            => Equals(left, right);

        public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
            => !(left == right);
    }
}
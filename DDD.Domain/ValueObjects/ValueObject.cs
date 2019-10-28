

namespace DDD.Domain.ValueObjects
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var vo = obj as T;          //objをTとみなして、voに代入
            if (vo == null)             //voがnullならば
            {
                return false;           //falseを返す
            }

            return EqualsCore(vo);
        }

        public static bool operator ==(ValueObject<T> vo1,
            ValueObject<T> vo2)
        {
            return Equals(vo1, vo2);
        }

        public static bool operator !=(ValueObject<T> vo1,
            ValueObject<T> vo2)
        {
            return !Equals(vo1, vo2);
        }

        protected abstract bool EqualsCore(T other);

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

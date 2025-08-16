namespace ClientManagerBackend.Dominio.ValueObjects
{
    public abstract class ValueObject
    {
        protected abstract object GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().Equals(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()?.GetHashCode() ?? 0;
        }
    }

}

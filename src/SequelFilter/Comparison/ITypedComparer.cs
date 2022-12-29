namespace SequelFilter.Comparison
{
    public interface ITypedComparer
    {
        bool Equals(object left, object right, string operatorName);
        bool GreaterThan(object left, object right, string operatorName);
        bool GreaterThanEqualTo(object left, object right, string operatorName);
    }
}

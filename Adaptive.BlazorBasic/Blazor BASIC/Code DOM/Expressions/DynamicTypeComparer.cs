namespace Adaptive.Intelligence.BlazorBasic;

public static class DynamicTypeComparer
{
    #region Equal To
    public static bool EqualTo(object a, object b)
    {
        return EqualTo((IComparable)a, (IComparable)b);
    }
    public static bool EqualTo(IComparable a, IComparable b)
    {
        return a == b;
    }
    #endregion

    #region GreaterThan
    public static bool GreaterThan(object a, object b)
    {
        return GreaterThan((IComparable)a, (IComparable)b);
    }
    public static bool GreaterThan(IComparable a, IComparable b)
    {
        int r = a.CompareTo(b);
        return (r > 0);
    }
    #endregion


    #region GreaterThanOrEqualTo
    public static bool GreaterThanOrEqualTo(object a, object b)
    {
        return GreaterThanOrEqualTo((IComparable)a, (IComparable)b);
    }
    public static bool GreaterThanOrEqualTo(IComparable a, IComparable b)
    {
        int r = a.CompareTo(b);
        return (r >= 0);
    }
    #endregion


    #region LessThan
    public static bool LessThan(object a, object b)
    {
            return LessThan((IComparable)a, (IComparable)b);
    }
    public static bool LessThan(int a, int b)
    {
        int r = a.CompareTo(b);
        return (r < 0);
    }
    #endregion

    #region LessThanOrEqualTo
    public static bool LessThanOrEqualTo(object a, object b)
    {
        return LessThanOrEqualTo((IComparable)a, (IComparable)b);
    }
    public static bool LessThanOrEqualTo(int a, int b)
    {
        int r = a.CompareTo(b);
        return (r <= 0);
    }
    #endregion


    #region NotEqualTo
    public static bool NotEqualTo(object a, object b)
    {
            return NotEqualTo((IComparable)a, (IComparable)b);
    }
    public static bool NotEqualTo(IComparable a, IComparable b)
    {
        int r = a.CompareTo(b);
        return (r != 0);
    }
    #endregion
}

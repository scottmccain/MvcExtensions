namespace TabTest.Repository.Filters
{
    public enum FilterCondition
    {
        Contains,
        ContainsCaseSensitive,
        DoesNotContain,
        DoesNotContainCaseSensitive,
        Equal,
        EqualCaseSensitive,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        StartsWith,
        StartsWithCaseSensitive,
        EndsWith,
        EndsWithCaseSensitive,
        Null,
        NotNull,
        Empty,
        NotEmpty
    }
}
namespace TabTest.Repository.Filters
{
    public class ColumnFilter
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public FilterCondition Condition { get; set; }
        public FilterOperator Operator { get; set; }
    }
}
namespace KnockoutHelpers
{
    public class TabListItem
    {
        public string TabHeader
        {
            get; set;
        }

        public bool IsPartial
        {
            get; set;
        }

        public string TemplatePath
        {
            get; set;
        }

        public dynamic Model
        {
            get; set;
        }
    }
}
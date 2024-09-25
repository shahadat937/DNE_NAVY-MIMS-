namespace RMS.Utility
{
    public class SearchModel<T> where T : class
    {
        public int TotalRecords { get; set; }

        /// <summary>
        /// Sort column name
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Sort direction
        /// </summary>
        public string Sortdir { get; set; }

        /// <summary>
        /// Set Report type excel,pdf
        /// </summary>
        public string ReportType { get; set; }

        public int? Page { get; set; }

        public int PageIndex
        {
            get
            {
                int index = 0;
                int pageSize = AppConfig.PageSize;
                if (Page.HasValue && Page.Value > 0)
                {
                    index = Page.Value - 1;
                }
                return index;
            }
        }
    }
}

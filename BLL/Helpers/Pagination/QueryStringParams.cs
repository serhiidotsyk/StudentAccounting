namespace BLL.Helpers.Pagination
{
	public class QueryStringParams
    {
		const int maxPageSize = 50;
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 2;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}

		public string SearchString { get; set; }
		public string SortOrder { get; set; }
		public string SortField { get; set; }
	}
}

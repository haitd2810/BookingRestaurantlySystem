using booking.Models;

namespace booking.DAO
{
    public class TableTypeDAO
    {
		private readonly bookingDBContext context = new bookingDBContext();
		private static readonly object instaceLock = new object();
		private static TableTypeDAO instance = null;

		public static TableTypeDAO Instance
		{
			get
			{
				lock (instaceLock)
				{
					instance ??= new TableTypeDAO();
					return instance;
				}
			}
		}

		public List<Tabletype> getList()
		{
			return context.Tabletypes
						  .Where(type => type.Status[0] == 1)
						  .ToList();
		}
	}
}

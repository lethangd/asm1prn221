using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class StaffDAO
	{
		private readonly MyStoreContext _storeContext = new();
		private static StaffDAO instance = null;
		private static readonly object instancelock = new object();
		public static StaffDAO Instance
		{
			get
			{
				lock (instancelock)
				{
					if (instance == null)
					{
						instance = new StaffDAO();
					}
					return instance;
				}
			}
		}

		public Staff GetStaffLogin(string username, string password)
		{
			var Staff = new Staff();
			try
			{
				Staff = _storeContext.Staffs.FirstOrDefault(s=> s.Name.Equals(username) && s.Password.Equals(password));
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return Staff;
		}

		public Staff GetStaffById(int id)
		{
			var Staff = new Staff();
			try
			{
				Staff = _storeContext.Staffs.FirstOrDefault(s => s.StaffId==id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return Staff;
		}

	}
}

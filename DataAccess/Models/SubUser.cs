using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class SubUser
	{
		public int ID { get; set; }
		public int? UserID { get; set; }
		public int? LocationID { get; set; }
		public string? UserType { get; set; }
		public string? BusinessType { get; set; }
		public string? UserName { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Designation { get; set; }
		public string? ImagePath { get; set; }
		public string? Password { get; set; }
		public string? Email { get; set; }
		public string? ContactNo { get; set; }
		public string? Address { get; set; }
		public int? CityID { get; set; }
		public int? CountryID { get; set; }
		public bool? Subscribe { get; set; }
		public int? GroupID { get; set; }
		public string? Passcode { get; set; }
		public System.DateTime? DateFrom { get; set; }
		public System.DateTime? DateTo { get; set; }
		public int? TimeZoneID { get; set; }
		public string? LastUpdatedBy { get; set; }
		public System.DateTime? LastUpdatedDate { get; set; }
		public int? StatusID { get; set; }
		public string? States { get; set; }
		public string? Zipcode { get; set; }
		public System.DateTime? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public string? Country { get; set; }
		public string? LocationName { get; set; }
		public bool? AllowNegativeInventory { get; set; }
		public bool? IsCustomerRequired { get; set; }
	}
}

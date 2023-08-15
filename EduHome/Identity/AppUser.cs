using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Identity
{
	public class AppUser :IdentityUser
	{
		
		[MaxLength(256)]
		[Required]
			
		public string FullName { get; set; }
		public bool IsActive { get; set; }

	}
}

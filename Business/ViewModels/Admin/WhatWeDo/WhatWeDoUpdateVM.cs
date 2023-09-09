using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.WhatWeDo
{
	public class WhatWeDoUpdateVM
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string SubTitle { get; set; }
		[Required]
		public string Description { get; set; }
	}
}

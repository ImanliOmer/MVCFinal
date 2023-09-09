using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OurVision
{
	public class OurVisionUpdateVM
	{
        [Required]
        public string SubHead { get; set; }
		[Required]
		public string Head { get; set; }
		[Required]
		public string Desc { get; set; }
    }
}

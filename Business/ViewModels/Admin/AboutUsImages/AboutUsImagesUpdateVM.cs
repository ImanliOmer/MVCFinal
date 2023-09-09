using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.AboutUsImages
{
	public class AboutUsImagesUpdateVM
	{
		[Required]
		public IFormFile PhotoFile { get; set; }
        public bool IsMain { get; set; }
        public string? CurrentPhoto { get; set; }
    }
}

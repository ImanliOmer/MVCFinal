using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public string BtnLink { get; set; }

        [Required]
        public string BtnText { get; set; }
        [Required]
        public IFormFile PhotoFile { get; set; }
    }
}

using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Home
{
	public class HomeVM
	{
		public ICollection<Common.Entities.Slider> Sliders { get; set; }
	}
}

using Business.ViewModels.Admin.OurVision;
using Business.ViewModels.Admin.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
	public interface IOurVisionService
	{
		Task<OurVisionIndexVM> GetAllAsync();
		Task<bool> UpdateAsync(OurVisionUpdateVM model, int id);
		Task<OurVisionUpdateVM> UpdateAsync(int id);

	}
}

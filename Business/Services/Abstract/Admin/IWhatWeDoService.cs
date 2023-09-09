using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Admin.WhatWeDo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
	public interface IWhatWeDoService
	{
		Task<bool> CreateAsync(WhatWeDoCreateVM model);
		Task<bool> UpdateAsync(WhatWeDoUpdateVM model, int id);
		Task<bool> DeleteAsync(int id);
	}
}

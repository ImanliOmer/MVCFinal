using Business.ViewModels.Admin.WhatWeDo;
using Business.ViewModels.Admin.WhatWeDoComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IWhatWeDoComponentService
	{
		Task<bool> CreateAsync(WhatWeDoComponentCreateVM model);
		Task<bool> UpdateAsync(WhatWeDoComponentUpdateVM model, int id);
		Task<bool> DeleteAsync(int id);
	}
}

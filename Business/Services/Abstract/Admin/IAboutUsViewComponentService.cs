using Business.ViewModels.Admin.AboutUsImages;
using Business.ViewModels.Admin.AboutUsViewComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
	public interface IAboutUsViewComponentService
	{
		Task<bool> CreateAsync(AboutUsViewComponentCreateVM model);
		Task<bool> UpdateAsync(AboutUsViewComponentUpdateVM model, int id);
		Task<bool> DeleteAsync(int id);
	}
}

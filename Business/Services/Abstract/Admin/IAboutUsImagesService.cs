using Business.ViewModels.Admin.AboutUsImages;
using Business.ViewModels.Admin.OurVisionGoal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
	public interface IAboutUsImagesService
	{
		Task<bool> CreateAsync(AboutUsImagesCreateVM model);
		Task<bool> UpdateAsync(AboutUsImagesUpdateVM model, int id);
		Task<bool> DeleteAsync(int id);
	}
}

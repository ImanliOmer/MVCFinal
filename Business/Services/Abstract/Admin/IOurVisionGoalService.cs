using Business.ViewModels.Admin.OurVisionGoal;
using Business.ViewModels.Admin.Slider;
using DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
	public interface IOurVisionGoalService
	{
		Task<OurVisionGoalIndexVM> GetAllAsync();
		Task<bool> CreateAsync(OurVisionGoalCreateVM model);
		Task<bool> UpdateAsync(OurVisionGoalUpdatedVM model, int id);
		Task<OurVisionGoalUpdatedVM> UpdateAsync(int id);
		Task<bool> DeleteAsync(int id);
	}
}

using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.WhatWeDo;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Business.ViewModels.Admin.Slider;

namespace Business.Services.Concrete.Admin
{
	public class WhatWeDoService : IWhatWeDoService
	{

		private readonly IWhatWeDoRepository _whatWeDoRepositroy;
		private readonly IActionContextAccessor _contextAccsser;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		public WhatWeDoService(IWhatWeDoRepository whatWeDoRepositroy,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
		{
			_modelstate = _contextAccsser.ActionContext.ModelState;
			_whatWeDoRepositroy = whatWeDoRepositroy;
			_contextAccsser = contextAccsser;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> CreateAsync(WhatWeDoCreateVM model)
		{
			if (!_modelstate.IsValid) return false;

			var WhatWeDo = new WhatWeDo
			{
				Title = model.Title,
				SubTitle = model.SubTitle,
				Description = model.Description,
				CreatedAt = DateTime.Now,
			};

			_whatWeDoRepositroy.CreateAsync(WhatWeDo);
			 _unitOfWork.CommitAsync();


			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var slider = await _whatWeDoRepositroy.GetByIdAsync(id);
			if (slider is null)
			{
				_modelstate.AddModelError("SliderNotFound", "Slider doesn't exist");
				return false;
			}


			_whatWeDoRepositroy.SofDeleteAsync(slider);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<WhatWeDoUpdateVM> UpdateAsync(int id)
		{
			var slider = await _whatWeDoRepositroy.GetByIdAsync(id);
			if (slider is null) return null;

			var model = new WhatWeDoUpdateVM
			{
				Title = slider.Title,
				SubTitle=slider.SubTitle,
				Description = slider.Description,
			};
			return model;
		}

		public async Task<bool> UpdateAsync(WhatWeDoUpdateVM model, int id)
		{
			if (!_modelstate.IsValid) return false;

			var whatWeDo = await _whatWeDoRepositroy.GetByIdAsync(id);
			if (whatWeDo is null)
			{
				_modelstate.AddModelError(string.Empty, "slider doesn't exist");
				return false;
			}

			whatWeDo.Title = model.Title;
			whatWeDo.SubTitle = model.SubTitle;
			whatWeDo.Description = model.Description;
			whatWeDo.ModfiedAt = DateTime.Now;

			_whatWeDoRepositroy.Update(whatWeDo);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}

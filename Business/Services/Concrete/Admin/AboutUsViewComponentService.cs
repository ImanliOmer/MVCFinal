using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.AboutUsViewComponent;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Common.Entities;
using DataAccess.Repositories.Concrete;
using Business.ViewModels.Admin.Slider;

namespace Business.Services.Concrete.Admin
{
	public class AboutUsViewComponentService : IAboutUsViewComponentService
	{

		private readonly IAboutUsViewComponentRepository _aboutUsViewComponentRepository;
		private readonly IActionContextAccessor _contextAccsser;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		public AboutUsViewComponentService(IAboutUsViewComponentRepository aboutUsViewComponentRepository,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
		{
			_modelstate = _contextAccsser.ActionContext.ModelState;
			_aboutUsViewComponentRepository = aboutUsViewComponentRepository;
			_contextAccsser = contextAccsser;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> CreateAsync(AboutUsViewComponentCreateVM model)
		{
			if (!_modelstate.IsValid) return false;
	

			var AboutUsViewComponent = new AboutUsViewComponent
			{

				Title = model.Title,
				Header = model.Header,
				Description = model.Description,
				CreatedAt = DateTime.Now,
				
			};


			await _aboutUsViewComponentRepository.CreateAsync(AboutUsViewComponent);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var slider = await _aboutUsViewComponentRepository.GetByIdAsync(id);
			if (slider is null)
			{
				_modelstate.AddModelError(string.Empty, "Slider doesn't exist");
				return false;
			}


			_aboutUsViewComponentRepository.SofDeleteAsync(slider);
			_unitOfWork.CommitAsync();

			return true;
		}

		public async Task<AboutUsViewComponentUpdateVM> UpdateAsync(int id)
		{
			var aboutUsViewComponent = await _aboutUsViewComponentRepository.GetByIdAsync(id);
			if (aboutUsViewComponent is null) return null;

			var model = new AboutUsViewComponentUpdateVM
			{

				Title = aboutUsViewComponent.Title,
				Header = aboutUsViewComponent.Header,
				Description = aboutUsViewComponent.Description,
				
			};
			return model;
		}

		public async Task<bool> UpdateAsync(AboutUsViewComponentUpdateVM model, int id)
		{
			if (!_modelstate.IsValid) return false;

			var aboutUsViewComponent = await _aboutUsViewComponentRepository.GetByIdAsync(id);
			if (aboutUsViewComponent is null)
			{
				_modelstate.AddModelError(string.Empty, "slider doesn't exist");
				return false;
			}

			aboutUsViewComponent.Title = model.Title;
			aboutUsViewComponent.Description = model.Description;
			aboutUsViewComponent.Header = model.Header;
			aboutUsViewComponent.ModfiedAt = DateTime.Now;


			_aboutUsViewComponentRepository.Update(aboutUsViewComponent);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}

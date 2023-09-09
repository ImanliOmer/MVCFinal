using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Common.Entities;
using Business.ViewModels.Admin.AboutUsImages;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Concrete;
using Business.ViewModels.Admin.Slider;

namespace Business.Services.Concrete.Admin
{
	public class AboutUsImagesService : IAboutUsImagesService
	{
		private readonly IAboutUsImagesRepository _aboutUsImagesRepository;
		private readonly IActionContextAccessor _contextAccsser;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		public AboutUsImagesService(IAboutUsImagesRepository aboutUsImagesRepository,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
		{
			_modelstate = _contextAccsser.ActionContext.ModelState;
			_aboutUsImagesRepository = aboutUsImagesRepository;
			_contextAccsser = contextAccsser;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> CreateAsync(AboutUsImagesCreateVM model)
		{
           
				if (!_fileService.IsImage(model.PhotoFile))
				{
					_modelstate.AddModelError("Photo", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.PhotoFile, 200))
				{
					_modelstate.AddModelError("Photo", "File size is over 200kb");
					return false;
				}

			var AboutUsImages = new AboutUsImages
			{
				PhotoName = _fileService.Upload(model.PhotoFile),
				IsMain = model.IsMain,
				
			};
			_aboutUsImagesRepository.CreateAsync(AboutUsImages);
			_unitOfWork.CommitAsync();

			return true;
        }

		public async Task<bool> DeleteAsync(int id)
		{
			var aboutUsImages = await _aboutUsImagesRepository.GetByIdAsync(id);
			if (aboutUsImages is null)
			{
				_modelstate.AddModelError("SliderNotFound", "Slider doesn't exist");
				return false;
			}


			_aboutUsImagesRepository.SofDeleteAsync(aboutUsImages);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<AboutUsImagesUpdateVM> UpdateAsync(int id)
		{
			var aboutUsImages = await _aboutUsImagesRepository.GetByIdAsync(id);
			if (aboutUsImages is null) return null;

			var model = new AboutUsImagesUpdateVM
			{
				CurrentPhoto = aboutUsImages.PhotoName
			};
			return model;
		}

		public async Task<bool> UpdateAsync(AboutUsImagesUpdateVM model, int id)
		{
			if (!_modelstate.IsValid) return false;

			var aboutUsImages = await _aboutUsImagesRepository.GetByIdAsync(id);
			if (aboutUsImages is null)
			{
				_modelstate.AddModelError(string.Empty, "slider doesn't exist");
				return false;
			}

			

			aboutUsImages.ModfiedAt = DateTime.Now;
			aboutUsImages.IsMain = model.IsMain;

			if (model.PhotoFile != null)
			{
				if (!_fileService.IsImage(model.PhotoFile))
				{
					_modelstate.AddModelError(string.Empty, "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.PhotoFile, 200))
				{
					_modelstate.AddModelError(string.Empty, "File size is over 200kb");
					return false;
				}
				_fileService.Delete(aboutUsImages.PhotoName);
				aboutUsImages.PhotoName = _fileService.Upload(model.PhotoFile);
			}
			_aboutUsImagesRepository.Update(aboutUsImages);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}

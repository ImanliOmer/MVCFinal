using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Business.ViewModels.Admin.OurVisionGoal;
using Business.ViewModels.Admin.WhatWeDoComponent;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Services.Concrete.Admin
{
    public class WhatWeDoComponentService : IWhatWeDoComponentService
	{

		private readonly IActionContextAccessor _contextAccsser;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		private readonly IWhatWeDoComponentRepository _whatWeDoComponentRepository;

		public WhatWeDoComponentService(IWhatWeDoComponentRepository whatWeDoComponentRepository,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
		{
			_modelstate = _contextAccsser.ActionContext.ModelState;
			_whatWeDoComponentRepository = whatWeDoComponentRepository;
			_contextAccsser = contextAccsser;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> CreateAsync(WhatWeDoComponentCreateVM model)
		{

			if (!_modelstate.IsValid) return false;
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
			var WhatWeDoComponent = new WhatWeDoComponent()
			{
				Title = model.Title,
				Description = model.Desc,
				PhotoName = _fileService.Upload(model.PhotoFile),
				CreatedAt = DateTime.Now,
			};

			await _whatWeDoComponentRepository.CreateAsync(WhatWeDoComponent);
			await _unitOfWork.CommitAsync();

			return true;

		}

		public async Task<bool> DeleteAsync(int id)
		{
			var whatWeDoComponent = await _whatWeDoComponentRepository.GetByIdAsync(id);
			if (whatWeDoComponent is null)
			{
				_modelstate.AddModelError(string.Empty, "whatWeDoComponent doesn't exist");
				return false;
			}


			_whatWeDoComponentRepository.SoftDeleteAsync(whatWeDoComponent);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<WhatWeDoComponentUpdateVM> UpdateAsync( int id)
		{
			var whatWeDoComponent = await _whatWeDoComponentRepository.GetByIdAsync(id);
			if (whatWeDoComponent is null) return null;

			var model = new WhatWeDoComponentUpdateVM
			{
				Title = whatWeDoComponent.Title,
				Desc = whatWeDoComponent.Description,
				CurrentPhoto = whatWeDoComponent.PhotoName
			};
			return model;
		}

		public async Task<bool> UpdateAsync(WhatWeDoComponentUpdateVM model, int id)
		{
			if (!_modelstate.IsValid) return false;

			var whatWeDoComponent = await _whatWeDoComponentRepository.GetByIdAsync(id);
			if (whatWeDoComponent is null)
			{
				_modelstate.AddModelError(string.Empty, "whatWeDoComponent doesn't exist");
				return false;
			}

			whatWeDoComponent.Title = model.Title;
			whatWeDoComponent.Description = model.Desc;
			whatWeDoComponent.ModfiedAt = DateTime.Now;

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
				_fileService.Delete(whatWeDoComponent.PhotoName);
				whatWeDoComponent.PhotoName = _fileService.Upload(model.PhotoFile);
			}
			_whatWeDoComponentRepository.Update(whatWeDoComponent);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}

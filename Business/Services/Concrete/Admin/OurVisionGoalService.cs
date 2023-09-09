using Business.Utilities.File;
using Business.Services.Abstract.Admin;
using Common.Entities;
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
using Business.ViewModels.Admin.OurVisionGoal;
using Business.ViewModels.Admin.Slider;
using DataAccess.Migrations;

namespace Business.Services.Concrete.Admin
{
	public class OurVisionGoalService : IOurVisionGoalService
	{
		private readonly IOurVisionGoalRepository _ourVisionGoalRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		public OurVisionGoalService(IOurVisionGoalRepository ourVisionGoalRepository,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
		{
			_modelstate = contextAccsser.ActionContext.ModelState;
			_ourVisionGoalRepository = ourVisionGoalRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> CreateAsync(OurVisionGoalCreateVM model)
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
			var OurVisionGoal = new OurVisionGoal
			{
				Title = model.Title,
				Description = model.Desc,
				PhotoName = _fileService.Upload(model.PhotoFile),
				CreatedAt = DateTime.Now,
				
			};

			await _ourVisionGoalRepository.CreateAsync(OurVisionGoal);
			await _unitOfWork.CommitAsync();

			return true;

		}
		public async Task<bool> DeleteAsync(int id)
		{
			var ourVisionGoal = await _ourVisionGoalRepository.GetByIdAsync(id);
			if (ourVisionGoal is null)
			{
				_modelstate.AddModelError(string.Empty, "ourVisionGoal doesn't exist");
				return false;
			}


			_ourVisionGoalRepository.SofDeleteAsync(ourVisionGoal);
			await _unitOfWork.CommitAsync();
			return true;
		}


		public async Task<OurVisionGoalUpdatedVM> UpdateAsync(int id)
		{
			var ourVisionGoal = await _ourVisionGoalRepository.GetByIdAsync(id);
			if (ourVisionGoal is null) return null;

			var model = new OurVisionGoalUpdatedVM
			{
				Title = ourVisionGoal.Title,
				Desc = ourVisionGoal.Description,
				CurrentPhoto = ourVisionGoal.PhotoName
			};

			return model;
		}

		public async Task<bool> UpdateAsync(OurVisionGoalUpdatedVM model, int id)
		{
			if (!_modelstate.IsValid) return false;

			var ourVisionGoal = await _ourVisionGoalRepository.GetByIdAsync(id);
			if (ourVisionGoal is null)
			{
				_modelstate.AddModelError(string.Empty, "ourVisionGoal doesn't exist");
				return false;
			}

			ourVisionGoal.Title = model.Title;
			ourVisionGoal.Description = model.Desc;
			ourVisionGoal.ModfiedAt = DateTime.Now;

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

				ourVisionGoal.PhotoName = _fileService.Upload(model.PhotoFile);
			}
			_ourVisionGoalRepository.Update(ourVisionGoal);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<OurVisionGoalIndexVM> GetAllAsync()
		{
			var model = new OurVisionGoalIndexVM
			{
				VisionGoals = await _ourVisionGoalRepository.GetAll()
			};

			return model;
		}
	}
}

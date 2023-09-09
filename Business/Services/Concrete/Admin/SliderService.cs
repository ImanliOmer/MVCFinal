using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class SliderService : ISliderService	
    {
		private readonly ISliderRepository _sliderRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		public SliderService(ISliderRepository sliderRepository,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
        {
			_modelstate = contextAccsser.ActionContext.ModelState;
			_sliderRepository = sliderRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}


		public async Task<bool> CreateAsync(SliderCreateVM model)
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

			var Slider = new Slider
			{
				Title = model.Title,
				Desc = model.Desc,
				BtnLink = model.BtnLink,
				BtnText = model.BtnText,
				PhotoName = _fileService.Upload(model.PhotoFile),
				CreatedAt = DateTime.Now,
			};


			await _sliderRepository.CreateAsync(Slider);
			await _unitOfWork.CommitAsync();
	

			return true;
		}

		public async Task<bool> DeleteAsync( int id)
		{
			var slider = await _sliderRepository.GetByIdAsync(id);
			if (slider is null)
			{
				_modelstate.AddModelError("SliderNotFound", "Slider doesn't exist");
				return false;
			}


			_sliderRepository.SofDeleteAsync(slider);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<SliderIndexItemVM> GelAllASync()
		{
			var slider = (await _sliderRepository.GetAllAsync()).FirstOrDefault();
			if (slider is null) return null;
			
			return new SliderIndexItemVM
			{ 
				Title = slider.Title,
				Photo = slider.PhotoName,
			};
		}

		public async Task<SliderUpdateVM> UpdateAsync(int id)
		{
			var slider = await _sliderRepository.GetByIdAsync(id);
			if (slider is null) return null;

			var model = new SliderUpdateVM
			{
				Title = slider.Title,
				Desc = slider.Desc,
				CurrentPhoto = slider.PhotoName,
				BtnLink = slider.BtnLink,
				BtnText = slider.BtnText,
				

			};
			return model;
		}
		public async Task<bool> UpdateAsync(SliderUpdateVM model, int id)
		{
			if (!_modelstate.IsValid) return false;
			
			var slider = await _sliderRepository.GetByIdAsync(id);
			if (slider is null)
			{
				_modelstate.AddModelError("SliderNotFound", "slider doesn't exist");
				return false;
			}

			slider.Title = model.Title;
			slider.Desc = model.Desc;
			slider.BtnText = model.Title;
			slider.BtnLink = model.Title;
			slider.ModfiedAt = DateTime.Now;

			if(model.PhotoFile != null)
			{
				if (!_fileService.IsImage(model.PhotoFile))
				{
					_modelstate.AddModelError("NewPhoto", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.PhotoFile, 200))
				{
					_modelstate.AddModelError("NewPhoto", "File size is over 200kb");
					return false;
				}

				_fileService.Delete(slider.PhotoName);
				slider.PhotoName = _fileService.Upload(model.PhotoFile);
			}
			_sliderRepository.Update(slider);
			await _unitOfWork.CommitAsync();
			return true;
		}


		


	}

}

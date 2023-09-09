using Business.Services.Abstract.Admin;
using Business.Utilities.File;
using Business.ViewModels.Admin.OurVision;
using Business.ViewModels.Admin.OurVisionGoal;
using Business.ViewModels.Admin.Slider;
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

namespace Business.Services.Concrete.Admin
{
	public class OurVisionService: IOurVisionService
	{
		private readonly IOurVisionRepository _ourVisionRepository;
		private readonly IActionContextAccessor _contextAccsser;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelstate;
		public OurVisionService(IOurVisionRepository ourVisionRepository,
							IActionContextAccessor contextAccsser,
							IFileService fileService,
							IUnitOfWork unitOfWork)
		{
			_modelstate = _contextAccsser.ActionContext.ModelState;
			_ourVisionRepository = ourVisionRepository;
			_contextAccsser = contextAccsser;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<OurVisionIndexVM> GetAllAsync()
		{
			var model = new OurVisionIndexVM
			{
				Vision = await _ourVisionRepository.GetAllAsync()
			};

			return model;
		}

		public async Task<OurVisionUpdateVM> UpdateAsync(int id)
		{
			var ourVision = await _ourVisionRepository.GetByIdAsync(id);
			if (ourVision is null) return null;

			var model = new OurVisionUpdateVM
			{
				
				Desc = ourVision.Desc,
				Head = ourVision.Head,
				SubHead = ourVision.SubHead

			};
			return model;
		}

		public async Task<bool> UpdateAsync(OurVisionUpdateVM model, int id)
		{
			if (!_modelstate.IsValid) return false;

			var ourVision = await _ourVisionRepository.GetByIdAsync(id);
			if (ourVision is null)
			{
				_modelstate.AddModelError(string.Empty, "ourVision doesn't exist");
				return false;
			}

			ourVision.Head = model.Head;
			ourVision.SubHead = model.SubHead;
			ourVision.Desc = model.Desc;
			ourVision.ModfiedAt = DateTime.Now;

			
			_ourVisionRepository.Update(ourVision);
			await _unitOfWork.CommitAsync();
			return true;
		}

		
	}
}

using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public static class DbInitializer
	{
		public async static Task SeedAsync(IOurVisionRepository ourVisionRepository, UnitOfWork.IUnitOfWork unitOfWork)
		{
			if ((await ourVisionRepository.GetAllAsync()).Count == 0)
			{
				await ourVisionRepository.CreateAsync(new OurVision
				{
					SubHead = "Our vision",
					Head = "Combining Quality Care& Modern Conveniences.",
					Desc = "We started One Medical with the belief that clinical excellence\r\n commitment to service and\r\n  a modern approach make for a truly great experience. To bring our vision to life, we\r\n listened to our patients. thoughtfully applied technology, and hired the best doctors to\r\n                            create a practice that is designed specifically to meet your needs.",
					CreatedAt = DateTime.Now,

				});
				await unitOfWork.CommitAsync();
			}
		}

	}
}

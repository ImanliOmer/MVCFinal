using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class SliderRepository: Repository<Slider>, ISliderRepository
	{
		private readonly AppDbContext _context;

		public SliderRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public void SofDeleteAsync(Slider slider)
		{
			slider.IsDeleted = true;
			_context.Update(slider);
		}
	}
}

using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class AboutUsViewComponentRepository: Repository<AboutUsViewComponent>, IAboutUsViewComponentRepository
	{
		private readonly AppDbContext _context;

		public AboutUsViewComponentRepository(AppDbContext context): base(context)
		{
			_context = context;
		}

		public void SofDeleteAsync(AboutUsViewComponent aboutUsViewComponent)
		{
			aboutUsViewComponent.IsDeleted = true;
			_context.Update(aboutUsViewComponent);
		}
	}
}

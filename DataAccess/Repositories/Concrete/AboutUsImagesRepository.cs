using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class AboutUsImagesRepository: Repository<AboutUsImages>, IAboutUsImagesRepository
	{
		private readonly AppDbContext _context;

		public AboutUsImagesRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public void SofDeleteAsync(AboutUsImages aboutUsImages)
		{
			aboutUsImages.IsDeleted = true;
			_context.Update(aboutUsImages);
		}
	}
}

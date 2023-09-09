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
	public class WhatWeDoRepository : Repository<WhatWeDo>, IWhatWeDoRepository
	{
		private readonly AppDbContext _context;

		public WhatWeDoRepository(AppDbContext context): base(context) 
        {
			_context = context;
		}

		public void SofDeleteAsync(WhatWeDo whatWeDo)
		{
			whatWeDo.IsDeleted = true;
			_context.Update(whatWeDo);
		}
	}
}

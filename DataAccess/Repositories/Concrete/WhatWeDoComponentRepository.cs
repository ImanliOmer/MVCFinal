using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class WhatWeDoComponentRepository: Repository<WhatWeDoComponent>
	{

		private readonly AppDbContext _context;

		public WhatWeDoComponentRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public void SoftDeleteAsync(WhatWeDoComponent whatWeDoComponent)
		{
			whatWeDoComponent.IsDeleted = true;
			_context.Update(whatWeDoComponent);
		}
	}
}

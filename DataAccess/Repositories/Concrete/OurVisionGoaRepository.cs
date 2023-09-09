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
	public class OurVisionGoaRepository : Repository<OurVisionGoal>, IOurVisionGoalRepository
	{
		private readonly AppDbContext _context;

		public OurVisionGoaRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public void SofDeleteAsync(OurVisionGoal ourVisionGoal)
		{
			ourVisionGoal.IsDeleted = true;
			_context.Update(ourVisionGoal);
		}

		public async Task<List<OurVisionGoal>> GetAll()
		{
			return await _context.OurVisionGoal.Where(x => !x.IsDeleted).ToListAsync();
		}

	} 
}

using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class OurVisionGoal: BaseEntity
	{
		[MaxLength(100)]
		public string Title { get; set; }
		[MaxLength(1000)]
		public string Description { get; set; }
		[MaxLength(500)]
		public string PhotoName { get; set; }
		public bool IsDeleted { get; set; }
	}
}

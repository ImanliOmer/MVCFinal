using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class AboutUsImages:BaseEntity
	{
		[MaxLength(500)]
		public string PhotoName { get; set; }
		public bool IsMain { get; set; }
        public bool IsDeleted { get; set; }
		public AboutUsViewComponent AboutUsViewComponent { get; set; }
		public int AboutUsViewComponentId { get; set; }

	}
}

using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class OurVision : BaseEntity
	{
		[MaxLength(50)]
        public string SubHead { get; set; }
		[MaxLength(350)]
		public string Head { get; set; }
		[MaxLength(2000)]
		public string Desc { get; set; }
		public bool IsDeleted { get; set; }

    }
}

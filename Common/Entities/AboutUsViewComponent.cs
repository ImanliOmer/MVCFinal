using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class AboutUsViewComponent: BaseEntity
	{
        [MaxLength(100)]
        public string Header { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(2000)]
		public string Description { get; set; }
		public bool IsDeleted { get; set; }

        public ICollection<AboutUsImages> AboutUsImages { get; set; }
    }
}

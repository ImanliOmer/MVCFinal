using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class WhatWeDo: BaseEntity
	{
        [MaxLength(100)]
        public string Title { get; set; }
		[MaxLength(100)]
		public string SubTitle { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}

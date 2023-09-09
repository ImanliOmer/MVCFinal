using Common.Entities.Base;
using System.ComponentModel.DataAnnotations;


namespace Common.Entities
{
    public class Slider : BaseEntity
    {
        [MaxLength(50)]
        public string Title { get; set; }
		[MaxLength(50)]
		public string Desc { get; set; }
		[MaxLength(300)]
		public string BtnLink { get; set; }
		[MaxLength(25)]
		public string BtnText { get; set; }
		[MaxLength(150)]
		public string PhotoName { get; set; }

        public bool IsDeleted { get; set; }
	}
}

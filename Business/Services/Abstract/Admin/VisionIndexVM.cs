namespace Business.Services.Abstract.Admin
{
	public class OurVisionIndexVM
	{

		public OurVisionIndexVM()
		{
			Vision = new List<Common.Entities.OurVision>();
		}

		public List<Common.Entities.OurVision> Vision { get; set; }

	}
}
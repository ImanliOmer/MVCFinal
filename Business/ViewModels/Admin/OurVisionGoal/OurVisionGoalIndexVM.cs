using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.ViewModels.Admin.OurVisionGoal
{
	public class OurVisionGoalIndexVM
	{


		public OurVisionGoalIndexVM()
		{
			VisionGoals = new List<Common.Entities.OurVisionGoal>();
		}

		public List<Common.Entities.OurVisionGoal> VisionGoals { get; set; }
        public List<SelectListItem> Visions { get; set; }

    }
}
using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Actions
{
	public class CheckAction : InputAction, IInputAction
	{
        public CheckAction()
			:base(ActionEnum.Check)
        {
        }
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			var verb = commands[1];
			//int enemy = 0;
			int enemyNumber = commands.Length == 3 ?
							    int.Parse(commands[2]): 
								1;
			if(verb == "enemy")
			{
				controller.ShowEnemy(enemyNumber);
			}

			if(verb == "enemies")
			{
				controller.ShowEnemies();
			}
		}
	}
}
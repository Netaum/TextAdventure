using System.Collections.Generic;

namespace TextAdventure.Interfaces.Controllers
{
    public interface ILoadController
	{
		IList<Entities.IInteractableObject> LoadGameItens(string filePath);
		Scenes.IScene LoadScene(string sceneName);
	}
}
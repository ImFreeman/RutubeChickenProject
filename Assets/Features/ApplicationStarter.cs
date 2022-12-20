namespace Features.Game
{
    public class ApplicationStarter
    {
        public ApplicationStarter(GameManager gameManager)
        {
            gameManager.Init();
            gameManager.StartGame();
        }
    }
}
using Features.Player;
using Zenject;
using Features.Egg;

namespace Features.Game
{
    public class GameManager
    {
        private readonly SignalBus _signalBus;
        private readonly EggSpawnManager _eggSpawnManager;
        private readonly UIService _uiService;
        private readonly PlayerInputHandler _playerInputHandler;
        private readonly PlayerDataHandler _playerDataHandler;
        private readonly IInstantiator _instantiator;        

        public GameManager(
            SignalBus signalBus,
            EggSpawnManager eggSpawnManager,
            UIService uiService,
            PlayerInputHandler playerInputHandler,
            PlayerDataHandler playerDataHandler,
            IInstantiator instantiator)
        {
            _signalBus = signalBus;
            _eggSpawnManager = eggSpawnManager;
            _uiService = uiService;
            _playerInputHandler = playerInputHandler;
            _playerDataHandler = playerDataHandler;
            _instantiator = instantiator;
        }

        public void Init()
        {
            _instantiator.Instantiate<PlayerViewCreateCommand>().Do();
            _signalBus.Subscribe<HealthChangeSignal>(HealthChangeSignalHandler);
            _uiService.Init();
            var win = _uiService.Get<UILoseWindow>();
            win.RestartButtonClickEvent += RestartButtonClickEventHandler;
        }

        public void StartGame()
        {
            _eggSpawnManager.Start();
            _uiService.Hide<UILoseWindow>();
            _uiService.Show<UIHud>();
            _playerInputHandler.SetActive(true);
            _playerDataHandler.Restart();
        }

        private void RestartButtonClickEventHandler(object sender, System.EventArgs e)
        {
            StartGame();
        }        

        private void GameOver()
        {
            _eggSpawnManager.Stop();
            _uiService.Hide<UIHud>();
            var loseWindow = _uiService.Show<UILoseWindow>();
            loseWindow.SetScore(_playerDataHandler.CurrentScore);
            _playerInputHandler.SetActive(false);
        }

        private void HealthChangeSignalHandler(HealthChangeSignal signal)
        {
            if(signal.CurrentValue <= 0)
            {
                GameOver();
            }
        }
    }
}
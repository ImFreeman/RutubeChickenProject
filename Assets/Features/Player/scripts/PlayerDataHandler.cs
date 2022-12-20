using Features.Egg;
using UnityEngine;
using Zenject;

namespace Features.Player
{
    public readonly struct HealthChangeSignal
    {
        public readonly int CurrentValue;

        public HealthChangeSignal(int currentValue)
        {
            CurrentValue = currentValue;
        }
    }
    public readonly struct ScoreChangeSignal
    {
        public readonly int CurrentValue;

        public ScoreChangeSignal(int currentValue)
        {
            CurrentValue = currentValue;
        }
    }
    public class PlayerDataHandler
    {
        public int CurrentScore => _currentScore;

        private const int MaxHealth = 3;

        private readonly SignalBus _signalBus;
        private readonly PlayerInputHandler playerInputHandler;

        private int _currentScore;
        private int _currentHealth;

        public PlayerDataHandler(SignalBus signalBus, PlayerInputHandler playerInputHandler)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<EndOfEggWaySignal>(EndOfEggWaySignalHandler);
            this.playerInputHandler = playerInputHandler;
        }

        public void Restart()
        {
            _currentScore = 0;
            _currentHealth = MaxHealth;
            _signalBus.Fire(new ScoreChangeSignal(_currentScore));
            _signalBus.Fire(new HealthChangeSignal(_currentHealth));
        }

        private void EndOfEggWaySignalHandler(EndOfEggWaySignal signal)
        {
            if(signal.PositionHorizontal == playerInputHandler.CurrentHorizontalPosition && signal.PositionVertical == playerInputHandler.CurrentVerticalPosition)
            {
                _currentScore++;
                _signalBus.Fire(new ScoreChangeSignal(_currentScore));
            }
            else
            {
                _currentHealth = Mathf.Max(0, _currentHealth - 1);
                _signalBus.Fire(new HealthChangeSignal(_currentHealth));                
            }
        }
    }
}
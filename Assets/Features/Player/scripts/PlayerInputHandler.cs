using UnityEngine;
using Zenject;

namespace Features.Player
{
    public readonly struct PositionUpdateSignal
    {
        public readonly PositionHorizontal HorizontalPosition;
        public readonly PositionVertical VerticalPosition;
        public PositionUpdateSignal(
            PositionHorizontal horizontalPosition,
            PositionVertical verticalPosition)
        {
            HorizontalPosition = horizontalPosition;
            VerticalPosition = verticalPosition;
        }
    }

    public class PlayerInputHandler : ITickable
    {
        public PositionVertical CurrentVerticalPosition => _currentVerticalPosition;
        public PositionHorizontal CurrentHorizontalPosition => _currentHorizontalPosition;

        private readonly TickableManager _tickableManager;
        private readonly PlayerConfig _playerConfig;
        private readonly SignalBus _signalBus;

        private PositionHorizontal _currentHorizontalPosition = PositionHorizontal.Left;
        private PositionVertical _currentVerticalPosition = PositionVertical.Up;

        private bool _isActive;

        public PlayerInputHandler(
            TickableManager tickableManager,
            PlayerConfig playerConfig,
            SignalBus signalBus)
        {
            _tickableManager = tickableManager;
            _playerConfig = playerConfig;
            _signalBus = signalBus;            
        }

        public void SetActive(bool value)
        {
            if(value != _isActive)
            {
                _isActive = value;
                if(_isActive)
                {
                    _tickableManager.Add(this);
                }
                else
                {
                    _tickableManager.Remove(this);
                }
            }
        }

        public void Tick()
        {
            foreach (var item in _playerConfig.Buttons)
            {
                if (Input.GetKeyDown(item.Button))
                {
                    if (item.PositionHorizontal != PositionHorizontal.None)
                    {
                        _currentHorizontalPosition = item.PositionHorizontal;
                    }
                    if (item.PositionVertical != PositionVertical.None)
                    {
                        _currentVerticalPosition = item.PositionVertical;
                    }
                    _signalBus.Fire(new PositionUpdateSignal(_currentHorizontalPosition, _currentVerticalPosition));
                    break;
                }
            }
        }
    }
}
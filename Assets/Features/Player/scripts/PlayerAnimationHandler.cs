using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Player
{
    public class PlayerAnimationHandler
    {
        private readonly SignalBus _signalBus;
        private readonly Dictionary<PositionHorizontal, Dictionary<PositionVertical, Vector3>> _dict;
        private PlayerView _playerView;

        public PlayerAnimationHandler(
            SignalBus signalBus,
            PlayerConfig config)
        {
            _signalBus = signalBus;
            _dict = new Dictionary<PositionHorizontal, Dictionary<PositionVertical, Vector3>>();
            foreach (var item in config.NestPositions)
            {
                if (!_dict.ContainsKey(item.PositionHorizontal))
                {
                    _dict.Add(item.PositionHorizontal, new Dictionary<PositionVertical, Vector3>());
                    _dict[item.PositionHorizontal].Add(item.PositionVertical, item.Position);
                }
                else if (!_dict[item.PositionHorizontal].ContainsKey(item.PositionVertical))
                {
                    _dict[item.PositionHorizontal].Add(item.PositionVertical, item.Position);
                }
            }
        }

        public void Start(PlayerView view)
        {
            _playerView = view;
            _signalBus.Subscribe<PositionUpdateSignal>(PositionUpdateSignalHandler);
        }

        private void PositionUpdateSignalHandler(PositionUpdateSignal signal)
        {
            _playerView.SetViewDirection(signal.HorizontalPosition == PositionHorizontal.Right);
            _playerView.SetNestPosition(_dict[signal.HorizontalPosition][signal.VerticalPosition]);
        }

    }
}
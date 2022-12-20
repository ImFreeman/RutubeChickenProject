using UnityEngine;
using System;

namespace Features.Player
{
    public enum PositionHorizontal
    {
        None,
        Left,
        Right
    }
    public enum PositionVertical
    {
        None,
        Up,
        Down
    }

    [Serializable]
    public struct PlayerButtonModel
    {
        public KeyCode Button;
        public PositionHorizontal PositionHorizontal;
        public PositionVertical PositionVertical;
    }
    [Serializable]
    public struct NestPositionModel
    {
        public Vector3 Position;
        public PositionHorizontal PositionHorizontal;
        public PositionVertical PositionVertical;
    }

    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PlayerButtonModel[] buttons;
        [SerializeField] private NestPositionModel[] nest;

        public PlayerButtonModel[] Buttons => buttons;
        public NestPositionModel[] NestPositions => nest;

    }
}
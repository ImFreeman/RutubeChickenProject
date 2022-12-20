using UnityEngine;
using System;
using Features.Player;

namespace Features.Egg
{
    [Serializable]
    public struct Waypoint
    {
        public Vector3 Position;
        public Vector3 Rotation;
    }
    [Serializable]
    public struct EggDropWayModel
    {
        public Waypoint[] Waypoints;
        public PositionVertical PositionVertical;
        public PositionHorizontal PositionHorizontal;
    }

    [CreateAssetMenu(fileName = "EggDropConfig", menuName = "Configs/EggDropConfig", order = 1)]
    public class EggDropConfig : ScriptableObject
    {
        [SerializeField] private EggDropWayModel[] waypoints;

        public EggDropWayModel[] Waypoints => waypoints;
    }
}
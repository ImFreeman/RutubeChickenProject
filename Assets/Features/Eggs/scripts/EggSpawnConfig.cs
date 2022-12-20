using UnityEngine;

namespace Features.Egg
{
    [CreateAssetMenu(fileName = "EggSpawnConfig", menuName = "Configs/EggSpawnConfig", order = 4)]
    public class EggSpawnConfig : ScriptableObject
    {
        [SerializeField] private float maxDifficulty;
        [SerializeField] private float deltaDifficulty;
        [SerializeField] private float startDifficulty;

        [SerializeField] private float minSpawnTimeOffset;
        [SerializeField] private float deltaSpawnTimeOffset;
        [SerializeField] private float startSpawnTimeOffset;

        [SerializeField] private int eggsForNextLevel;

        public float MaxDifficulty => maxDifficulty;
        public float DeltaDifficulty => deltaDifficulty;
        public float StartDifficulty => startDifficulty;
        public float MinSpawnTimeOffset => minSpawnTimeOffset;
        public float DeltaSpawnTimeOffset => deltaSpawnTimeOffset;
        public float StartSpawnTimeOffset => startSpawnTimeOffset;
        public int EggsForNextLevel => eggsForNextLevel;
    }
}
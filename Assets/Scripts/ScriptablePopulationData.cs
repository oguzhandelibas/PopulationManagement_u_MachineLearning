using UnityEngine;

namespace MachineLearningBasic.Data
{
    [CreateAssetMenu(menuName = "PopulationManagement/ScriptablePopulationData", order = 0)]
    public class ScriptablePopulationData : ScriptableObject
    {
        public GameObject personPrefab;

        [Header("Population Data")]
        [SerializeField] private int _populationSize = 10;
        public int PopulationSize { get { return _populationSize; } }

        [SerializeField] private int _trialTime = 10;
        public int TrialTime { get { return _trialTime; } }


        [Header("Person Spawn Range")]
        [SerializeField] private Vector2 horizontalSpawnRange = new Vector2(-9, 9);
        [SerializeField] private Vector2 verticalSpawnRange = new Vector2(-4.5f, 4.5f);



        public GameObject CreatePerson()
        {
            Vector2 pos = new Vector3(
                Random.Range(horizontalSpawnRange.x, horizontalSpawnRange.y),
                Random.Range(verticalSpawnRange.x, verticalSpawnRange.y)
            );

            GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
            return offspring;
        }
    }
}

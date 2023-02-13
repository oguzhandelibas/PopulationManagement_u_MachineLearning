using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MachineLearningBasic.Data;

namespace MachineLearningBasic
{
    public class PopulationManager : MonoBehaviour
    {
        public static float elapsed = 0;

        [SerializeField] private ScriptablePopulationData _scriptablePopulationData;
        [SerializeField] private int _generation = 1;

        List<GameObject> population = new List<GameObject>();
        GUIStyle guiStyle = new GUIStyle();

        private void OnGUI()
        {
            guiStyle.fontSize = 50;
            guiStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + _generation, guiStyle);
            GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
        }

        private void Start()
        {
            for (var i = 0; i < _scriptablePopulationData.PopulationSize; i++)
            {
                GameObject go = _scriptablePopulationData.CreatePerson();
                go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
                go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
                go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
                population.Add(go);
            }
        }

        private void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed > _scriptablePopulationData.TrialTime)
            {
                BreedNewPopulation();
                elapsed = 0;
            }
        }

        GameObject Breed(GameObject parent1, GameObject parent2)
        {
            GameObject offspring = _scriptablePopulationData.CreatePerson();

            DNA dna1 = parent1.GetComponent<DNA>();
            DNA dna2 = parent2.GetComponent<DNA>();

            //swap parent dna
            if (Random.Range(0, 10) < 8)
            {
                offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
                offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
                offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            }
            else
            {
                offspring.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
                offspring.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
                offspring.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            }

            return offspring;
        }

        private void BreedNewPopulation()
        {
            List<GameObject> newPopulation = new List<GameObject>();
            //get rid of unfit individuals
            List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

            population.Clear();

            //breed uoer half of sorted list
            for (var i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
            {
                population.Add(Breed(sortedList[i], sortedList[i + 1]));
                population.Add(Breed(sortedList[i + 1], sortedList[i]));
            }

            //destroy all parents and previous population
            for (var i = 0; i < sortedList.Count; i++)
            {
                Destroy(sortedList[i]);
            }
            _generation++;
        }
    }
}


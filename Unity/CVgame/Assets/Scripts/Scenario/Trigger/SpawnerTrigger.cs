using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class SpawnerTrigger : MonoBehaviour, ScenarioTrigger
    {
        public bool isSpawning;
        public GameObject spawnerContainer;
        public iSpawner spawner;

        public void Trigger()
        {
            if(isSpawning){
                spawner.SpawnUnit();
            }
            else{
                spawner.CleanUnit();
            }
        }

        private void Start() {
            spawner = spawnerContainer.GetComponent<iSpawner>();
        }
    }
}


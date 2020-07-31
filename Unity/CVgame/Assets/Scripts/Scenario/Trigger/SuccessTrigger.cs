using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class SuccessTrigger : MonoBehaviour, ScenarioTrigger
    {
        public string identifier;
        public void Trigger()
        {
            TropheesStat tStats = TropheesStat.getInstance();
            tStats.unlockTropheeItem(identifier);
        }
    }
}


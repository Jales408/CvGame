using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class ExplanationTrigger : MonoBehaviour, ScenarioTrigger
    {
        public Explanation explanation;
        public void Trigger()
        {
            FindObjectOfType<ExplanationManager>().ShowExplication(explanation);
        }
    }
}

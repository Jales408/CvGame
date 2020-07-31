using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class HideExplanationTrigger : MonoBehaviour, ScenarioTrigger
    {
        public void Trigger()
        {
            FindObjectOfType<ExplanationManager>().HideExplanation();
        }
    }
}

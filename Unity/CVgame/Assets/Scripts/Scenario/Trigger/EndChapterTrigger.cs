using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scenario{
    public class EndChapterTrigger : MonoBehaviour, ScenarioTrigger
    {
        public void Trigger()
        {
            FindObjectOfType<ScenarioManager>().EndChapter();
        }
    }
}


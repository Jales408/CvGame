using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class DoorTrigger : MonoBehaviour, ScenarioTrigger
    {
        public MoveSideWay doorControl;
        public void Trigger()
        {
            doorControl.SmoothlyMoveObjects();
        }
    }
}

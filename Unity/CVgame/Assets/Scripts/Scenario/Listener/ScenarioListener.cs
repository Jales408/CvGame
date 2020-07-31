using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scenario{
    public interface ScenarioListener 
    {
        void StartListeningFor(ScenarioManager manager);
        void StopListening();
    }
}


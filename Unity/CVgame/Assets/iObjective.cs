using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenario;
public interface iObjective
{
    void listenForObjective(ObjectiveListener listener);
    void resetObjective();
}

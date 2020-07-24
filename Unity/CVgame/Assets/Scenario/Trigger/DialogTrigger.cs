using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class DialogTrigger : MonoBehaviour, ScenarioTrigger
    {
        public Dialog dialog;
        public void Trigger()
        {
            FindObjectOfType<DialogManager>().StartDialogue(dialog);
        }
    }
}


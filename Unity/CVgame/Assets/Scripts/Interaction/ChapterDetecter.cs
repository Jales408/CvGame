using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenario;

public class ChapterDetecter : MonoBehaviour
{
    public int chapter;

    private ScenarioManager scenarioManager;

    private CameraFollowTarget cameraFollow;
    private void Start() {
        scenarioManager = FindObjectOfType<ScenarioManager>();
        cameraFollow = FindObjectOfType<CameraFollowTarget>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<AstronauteControler>()!=null){
            scenarioManager.StartChapter(chapter);
            cameraFollow.ChangeChapter(chapter);
        }
    }
}

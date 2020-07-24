using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventCallbacks;

public class DialogManager : MonoBehaviour
{
    public EasyTween dialogAnimationTween;
    public Text content;
    public Text speakerName;
    // Start is called before the first frame update
    private Queue<string> sentences;

    private List<AstronauteControler> interactors;
    public float timeTapping = 0.05f;

    private void Awake() {
        sentences = new Queue<string>();
        interactors = new List<AstronauteControler>();
    }

    public void SaveInteractor(AstronauteControler interactor){
        interactors.Add(interactor);
    }

    IEnumerator AnimateText(string text){
        content.text = "";
        foreach (char letter in text.ToCharArray()){
            content.text+=letter;
            yield return new WaitForSeconds(timeTapping);
        }
    }

    public void StartDialogue(Dialog dialog){
        foreach(AstronauteControler interactor in interactors){
            interactor.OnDialog(true);
        }
        sentences.Clear();
        speakerName.text = dialog.speakerName;
        dialogAnimationTween.OpenCloseObjectAnimation();
        foreach(string sentence in dialog.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count==0){
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(AnimateText(sentence));
    }

    public void EndDialog(){
        dialogAnimationTween.OpenCloseObjectAnimation();
        foreach(AstronauteControler interactor in interactors){
            interactor.OnDialog(false);
        }
        EndDialogInfo di = new EndDialogInfo();
        di.EventDescription = "Fin d'un dialogue";
        EventSystem.Current.FireEvent(di);
    }
}

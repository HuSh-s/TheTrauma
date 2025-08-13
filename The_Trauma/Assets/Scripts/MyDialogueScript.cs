using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDialogueScript : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public FocusEffect _FocusEffect;
    public CarInnerLight _CarInnerLight;

    public AudioClip dia1_sound;
    public AudioClip dia2_sound;
    public AudioClip dia3_sound;
    public AudioClip dia4_sound;
    public AudioClip dia5_sound;
    public AudioClip dia6_sound;
    public AudioClip dia7_sound;

    private void Start()
    {
        StartCoroutine(FirstDialogueSequence());
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) diaLine1();
    }

    public void diaLine1()
    {
        dialogueManager.PlayDialogue(dia1_sound, "Where... where am I?", dia1_sound.length);
    }

    public void diaLine2()
    {
        dialogueManager.PlayDialogue(dia2_sound, "Takin’ the long road, Mike… You still don’t know where we’re going, do you?", dia2_sound.length);
        _FocusEffect.FocusObject(dia2_sound.length);
        _CarInnerLight.TurnInnerLight();
    }

    public void diaLine3()
    {
        dialogueManager.PlayDialogue(dia3_sound, "Mike: I just… need to get further away. You got any bright idea?", dia3_sound.length);
    }

    IEnumerator FirstDialogueSequence()
    {
        yield return new WaitForSeconds(2f);

        diaLine1();
        yield return new WaitForSeconds(7f);

        diaLine2();
        yield return new WaitForSeconds(dia2_sound.length + 0.5f);

        diaLine3();
        yield return new WaitForSeconds(dia3_sound.length + 0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDialogueScript : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public FocusEffect _FocusEffect;
    public CarInnerLight _CarInnerLight;
    public BreathScript _BreathScript;
    public CassettePlay _CassettePlay;

    public AudioClip dia1_sound;
    public AudioClip dia2_sound;
    public AudioClip dia3_sound;
    public AudioClip dia4_sound;
    public AudioClip dia5_sound;
    public AudioClip dia6_sound;
    public AudioClip dia7_sound;
    public AudioClip dia8_sound;

    [HideInInspector] public bool hasPlayedDia7 = false;
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
        dialogueManager.PlayDialogue(dia1_sound, "Where... where am I? How long have I been drivin’?.. My head’s killin’ me", dia1_sound.length);
    }
    public void diaLine2()
    {
        dialogueManager.PlayDialogue(dia2_sound, "Takin’ the long road, Mike… You still don’t know where we’re going, do you?", dia2_sound.length);
        _FocusEffect.FocusObject(dia2_sound.length);
        _CarInnerLight.TurnInnerLight();
    }
    public void diaLine3()
    {
        dialogueManager.PlayDialogue(dia3_sound, "I just… need to get further away. I need time to figure things out. You got any bright ideas?", dia3_sound.length);
    }
    public void diaLine4()
    {
        dialogueManager.PlayDialogue(dia4_sound, "The body’s in the back… We gotta bury it before it starts stinkink.", dia4_sound.length);
    }
    public void diaLine5()
    {
        dialogueManager.PlayDialogue(dia5_sound, "I... can't breathe", dia5_sound.length);
    }
    public void diaLine6()
    {
        dialogueManager.PlayDialogue(dia6_sound, "You need to calm down, Mike. Try the tapes… You need that music.", dia6_sound.length);
    }
    public void diaLine7()
    {
        hasPlayedDia7 = true;
        dialogueManager.PlayDialogue(dia7_sound, "What the Fuckk!! what are these voices? You heard that too, right?", dia7_sound.length);
    }
    public void diaLine8()
    {
        dialogueManager.PlayDialogue(dia8_sound, "What voices, Mike are you talking to yourself?", dia8_sound.length);
    }
    IEnumerator FirstDialogueSequence()
    {
        yield return new WaitForSeconds(2f);
        diaLine1();
        yield return new WaitForSeconds(dia1_sound.length + 8f);
        diaLine2();
        yield return new WaitForSeconds(dia2_sound.length + 3f);
        diaLine3();
        yield return new WaitForSeconds(dia3_sound.length + 2f);
        diaLine4();
        yield return new WaitForSeconds(dia4_sound.length + 3f);
        diaLine5();
        _BreathScript.StartBreathSequence();
        yield return new WaitForSeconds(dia5_sound.length + 4f);
        diaLine6();
        _CassettePlay.dialogueScript = this;
        _CassettePlay.brokenTapeMode = true;
        _CassettePlay.EnableAfterDialogue6();
        yield return new WaitUntil(() => hasPlayedDia7);
        //diaLine7(); cassetteplayde oynatýldý
        yield return new WaitForSeconds(dia7_sound.length + 1f);
        diaLine8();
    }
}

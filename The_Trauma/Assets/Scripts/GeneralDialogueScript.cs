using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDialogueScript : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public FocusEffect _FocusEffect;
    public CarInnerLight _CarInnerLight;
    public BreathScript _BreathScript;

    public AudioClip dia1_sound;

    [HideInInspector] public bool hasPlayedDia7 = false;
    private void Start()
    {
        //StartCoroutine(FirstDialogueSequence());
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) diaLine1();
    }

    public void diaLine1()
    {
        dialogueManager.PlayDialogue(dia1_sound, "Where... where am I? How long have I been drivin’?.. My head’s killin’ me", dia1_sound.length);
    }
    /*
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
    }*/
}

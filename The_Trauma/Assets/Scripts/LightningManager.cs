using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public Light lightningLight; // Spot Light burada atanacak
    public AudioClip[] thunderClips; // Farklý gök gürültüsü sesleri
    public AudioSource thunderAudioSource; // Sesin çalýnacaðý kaynak

    public float minDelay = 15f; // Yýldýrým arasý minimum saniye
    public float maxDelay = 25f; // Yýldýrým arasý maksimum saniye

    public float minsoundDelay = 0.1f;
    public float maxsoundDelay = 0.5f; 

    private void Start()
    {
        lightningLight.gameObject.SetActive(false);
        StartCoroutine(ThunderRoutine());
    }

    IEnumerator ThunderRoutine()
    {
        while (true)
        {
            // Bir sonraki yýldýrýma kadar bekleme (rastgele)
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < 3; i++)
            {
                lightningLight.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.07f);
                lightningLight.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }
            float waitTimesound = Random.Range(minsoundDelay, maxsoundDelay);
            yield return new WaitForSeconds(waitTimesound);
            if (thunderClips.Length > 0)
            {
                thunderAudioSource.PlayOneShot(thunderClips[Random.Range(0, thunderClips.Length)]);
            }
        }
    }
}

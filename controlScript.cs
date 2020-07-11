using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlScript : MonoBehaviour
{

    AudioSource audio;
    public AudioClip[] clips;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(introJingle());
    }


    private void playSound(int sound)
    {
        audio.clip = clips[sound];
        audio.Play();
    }
    private IEnumerator introJingle() {
        yield return new WaitForSeconds(3f);
        playSound(0);
        StartCoroutine(quack());

    }

    private IEnumerator quack() {
        yield return new WaitForSeconds(1.8f);
        playSound(1);
        StartCoroutine(dog());
    }

    private IEnumerator dog()
    {
        yield return new WaitForSeconds(0.5f);
        playSound (2);
        StartCoroutine(gunShot());
    }

    private IEnumerator gunShot() {
        yield return new WaitForSeconds(0.6f);
        playSound(3);

    }

    public void ChangeScene() {
        SceneManager.LoadScene("MAIN");
    }


}

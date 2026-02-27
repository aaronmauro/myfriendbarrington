using System.Collections;
using UnityEngine;

public class SecretVideoScript : MonoBehaviour
{
    
    private void Start()
    {
        StartCoroutine(secretVideo(78f));
    }

    private IEnumerator secretVideo(float timer)
    {
        yield return new WaitForSeconds(timer);
        //Debug.Log("done");
        SceneManagerScript.instance.nextScene("Lvl2");
    }
}

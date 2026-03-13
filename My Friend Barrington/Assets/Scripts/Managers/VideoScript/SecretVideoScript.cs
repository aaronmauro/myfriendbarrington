using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SecretVideoScript : MonoBehaviour
{
    private VideoPlayer vp;
    private void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        vp.clip = Resources.Load<VideoClip>("Video/UnityVideoFolder/Scary-Barry");
    }

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

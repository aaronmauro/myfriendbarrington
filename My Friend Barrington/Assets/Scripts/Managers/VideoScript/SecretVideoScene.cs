using UnityEngine;

public class SecretVideoScene : MonoBehaviour
{

    // secret video
    private void OnTriggerEnter(Collider other)
    {
        SceneManagerScript.instance.nextScene("SecretVideo");
    }
}

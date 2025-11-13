using UnityEngine;

public class TriggerSoundWaves : MonoBehaviour
{
    // Getting Sound Waves
    [SerializeField]
    private SoundWaves waves;

    // Activate Sound Wave
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waves.isRun = true;
        }
    }
}

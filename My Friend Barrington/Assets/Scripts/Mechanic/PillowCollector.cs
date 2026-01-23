using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PillowCollector : MonoBehaviour
{
    [SerializeField]
    private int pillowsCollected = 0;

    [SerializeField]
    private Image FadeToBlack;
    private bool FadeStart = false;
    private float fade = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            EndLevel();
        }
        if (FadeStart)
        {
            fade += 0.01f;
            Debug.Log(fade);
            FadeToBlack.color = new Color(0, 0, 0, fade);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PillowCollectable>() != null)
        {
            other.GetComponent<PillowCollectable>().Collect();
            pillowsCollected += 1;
            Debug.Log(pillowsCollected);
            if(pillowsCollected >= 4)
            {
                EndLevel();
            }
        }

    }

    private void EndLevel()
    {
        FadeStart = true;
        StartCoroutine(ToNextLevel(3f));
    }
    private IEnumerator ToNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Lvl3");
    }
}

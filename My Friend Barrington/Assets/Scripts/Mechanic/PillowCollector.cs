using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PillowCollector : MonoBehaviour
{
    [SerializeField]
    private int pillowsNeeded = 4;

    [SerializeField]
    private Image FadeToBlack;
    private bool FadeStart = false;
    private float fade = 0;

    public TMP_Text pillowText;

    [SerializeField]
    GameObject one;
    [SerializeField]
    GameObject two;
    [SerializeField]
    GameObject three;
    [SerializeField]
    GameObject four;

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
            pillowsNeeded -= 1;
            pillowText.text = pillowsNeeded.ToString();
            if(pillowsNeeded <= 3)
            {
                one.SetActive(true);
            }
            if (pillowsNeeded <= 2)
            {
                two.SetActive(true);
            }
            if (pillowsNeeded <= 1)
            {
                three.SetActive(true);
            }
            if (pillowsNeeded <= 0)
            {
                four.SetActive(true);
                EndLevel();
            }
        }

    }

    private void EndLevel()
    {
        FadeToBlack.gameObject.SetActive(true);
        FadeStart = true;
        StartCoroutine(ToNextLevel(3f));
    }
    private IEnumerator ToNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Video");
        VideoManager.adsNumber = 1;
    }
}

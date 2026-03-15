using UnityEngine;

public class ResetSpecialPlatform : MonoBehaviour
{

    [SerializeField]
    GameObject SpecialPlatform;
    [SerializeField]
    Transform SpecialPlatformStartPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SpecialPlatform.transform.position = SpecialPlatformStartPoint.position;
        SpecialPlatform.GetComponent<NewMonoBehaviourScript>().speed = 0f;
    }
}

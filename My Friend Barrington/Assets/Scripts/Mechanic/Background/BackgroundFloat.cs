using UnityEngine;

public class BackgroundFloat : MonoBehaviour
{
    [SerializeField]
    public float floatHeight = 0.5f;   // How high it moves
    [SerializeField]
    public float floatSpeed = 1f;      // How fast it moves

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
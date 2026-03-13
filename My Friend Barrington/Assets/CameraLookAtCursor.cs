using System.Drawing;
using UnityEngine;

public class CameraLookAtCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Camera mycam = GetComponent<Camera>();
        Vector3 target = mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane));
        //transform.LookAt(target, Vector3.up);

        Vector3 direction = target - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f * Time.fixedDeltaTime);
    }
}

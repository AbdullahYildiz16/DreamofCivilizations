using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private void Update()
    {
        var dir = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-dir);
    }
}

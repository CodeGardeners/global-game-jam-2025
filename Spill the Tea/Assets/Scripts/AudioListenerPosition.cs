using UnityEngine;

public class AudioListenerPosition : MonoBehaviour
{
    void Update()
    {
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }
}

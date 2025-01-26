using UnityEngine;

public class AudioListenerPosition : MonoBehaviour
{
    [SerializeField] private float listenerHeight = 0;
    void Update()
    {
        var pos = transform.position;
        pos.y = listenerHeight;
        transform.position = pos;
    }
}

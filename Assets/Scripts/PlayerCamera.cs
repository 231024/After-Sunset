using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;
    Vector3 offset;
    
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}

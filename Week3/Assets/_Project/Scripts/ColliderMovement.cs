using UnityEngine;

public class ColliderMovement : MonoBehaviour
{
    public Transform playerPos;

    void FixedUpdate()
    {
        Tracker();
    }

    void Tracker()
    {
        if(playerPos != null)
        {
            if(transform.position.y <= playerPos.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, playerPos.position.y, transform.position.z);
            }
            else return;
        }

    }
}

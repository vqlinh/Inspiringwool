using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float offsetY = 0f;
    public float moveSpeed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wool").transform;
    }

    void Update()
    {
        //if(player != null)
        //{
        //    player = GameObject.FindGameObjectWithTag("Wool").transform;

        //}
        /// calculate the new position of camera
        //Vector3 targetPosition = new Vector3(player.position.x + offsetX, player.position.y + offsetY, transform.position.z);
        Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
        // Move camera to new position smoothly
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}


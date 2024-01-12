using UnityEngine;

public class BasketMove : MonoBehaviour
{
    public float speed ; 
    public float leftPoint ;
    public float rightPoint ; 
    private bool movingRight = true;// Biến xác định hướng di chuyển

    void Update()
    {
        // Tính toán vị trí mới cho đối tượng
        float newX = transform.localPosition.x;
        if (movingRight)
        {
            newX += speed * Time.deltaTime;
            if (newX >= rightPoint)
            {
                movingRight = false;
            }
        }
        else
        {
            newX -= speed * Time.deltaTime;
            if (newX <= leftPoint)
            {
                movingRight = true;
            }
        }
        // Đặt vị trí mới cho đối tượng
        transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
    }
}

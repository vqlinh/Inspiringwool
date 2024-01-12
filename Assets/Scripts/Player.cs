using UnityEditor.SearchService;
using UnityEngine;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    bool isFly=false;
    public GameObject Wool;
    bool isFirstFly=false;
    public float speedFly;
    public Transform Die;
    public float speedRotate;
    private float timeOnTorn = 0f; // Thời gian bóng đã ở trên lưới rách
    private bool isOnTorn = false; // Kiểm tra xem bóng có nằm trên lưới rách không
    private Vector3 previousBasketPosition;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        previousBasketPosition = Vector3.zero; // vị trí hiện tại
    }

    void Update()
    {
        if (isOnTorn)
        {
            timeOnTorn += Time.deltaTime;
            if (timeOnTorn>=15f)
            {
                GameManager.instance.GameOver();
            }
        }
        // nếu đang đứng trên ro?
        if (isFly && rb.velocity.y <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(Vector2.up * speedFly,ForceMode2D.Impulse);
                isFly = false;
            }
        }
        else
        {
            GameObject BallRotate = transform.Find("Ball").gameObject;
            BallRotate.transform.Rotate(new Vector3(0, 0, speedRotate));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Basket") || collision.collider.CompareTag("BasketTorn"))
        {
            if (collision.collider.CompareTag("BasketTorn"))
            {
                isOnTorn = true;
            }
            else {
                isOnTorn= false;
                timeOnTorn = 0f;

            }
            isFly = true;
            if (rb.velocity.y <= 0)
            {
                // nếu bóng hạ xuống chạm rổ thì cho vào giữa rổ đã rồi tính sau..
                Transform upperBasket = collision.collider.transform.Find("BallCenter");
                if (upperBasket != null)
                {
                    transform.SetParent(upperBasket);
                    transform.localPosition = Vector3.zero;
                }
                // nếu vị trí va chạm Y > Y hiện tại
                if (collision.transform.position.y > previousBasketPosition.y)
                {
                    GameManager.instance.SetScore(10);
                    isFirstFly = true; // nhảy lên lần đầu tiên nếu không khi start sẽ mất máu
                }
                else
                {
                    if (isFirstFly)
                    {
                        GameManager.instance.SetLives();
                    }
                }
                // vị trí hiện tại bằng vị trí mới
                previousBasketPosition = collision.transform.position;
            }
        }

        else if (collision.collider.CompareTag("Die"))
        {
            GameManager.instance.GameOver();
            //GameManager.instance.SetLives();

            //// Lấy vị trí của rổ hiện tại (upperBasket)
            //Vector3 woolSpawnPosition = previousBasketPosition;
            //if (transform.parent != null)
            //{
            //    woolSpawnPosition = transform.parent.position;
            //    // Đặt chỉ tọa độ Y của vị trí hồi sinh thành previousBasketPosition.y
            //    woolSpawnPosition.y = previousBasketPosition.y;
            //}

            //Instantiate(Wool, woolSpawnPosition, Quaternion.identity);
            //Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Basket") || collision.collider.CompareTag("BasketTorn"))
        {
            // Hủy bỏ việc gắn Wool vào Basket khi rời khỏi Basket
            transform.SetParent(null);
        }
    }
}

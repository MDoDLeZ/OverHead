using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    public float radCir;
    public float climbSpeed;
    public Transform circleTarget;
    public LayerMask all_but_Player; // Все слои кроме слоя игрока для поиска вокруг объектов
    
    void Start () {
		
	}

    public void Move(float ax) { // Движение игрока, "ax" - это направление например если налево, то ax == -1

        Vector3 direction = transform.right * ax; 
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);

        if ((ax > 0 && transform.localScale.x < 0) || (ax < 0 && transform.localScale.x > 0)) // Поворот персонажа
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (ax != 0) { // Если игрок движется, то отключаем хук
            GetComponent<LineRenderer>().enabled = false; 
            GetComponent<TargetJoint2D>().enabled = false;
            GetComponent<Hook>().isPressed = false;
        }

    }

    public void Jump() {

        if (isGround()) { 
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private bool isGround() { // Метод для обнаружение вокруг объектов с тегом "Platform"

        Collider2D[] gh = Physics2D.OverlapCircleAll(circleTarget.position, radCir, all_but_Player);
        int j = 0;
        for (int i = 0; i < gh.Length; i++) {
            if (gh[i].tag == "Platform") {
                j++;
            }
        }

        return j > 0; // Дает true если j больше 0
    }

    public void Climb(float ax) // Движение вверх или низ, по лестнице
    {
        Vector3 direction = transform.up * ax;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, climbSpeed * Time.deltaTime);
    }

}

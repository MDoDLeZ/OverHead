using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed_1X;
    public float moveSpeed_2X;
    public float jumpForce_haveHead;
    public float jumpForce_NotHaveHead;
    public float BoostForce_forJump;
    public float radCir;
    public float climbSpeed;
    public Transform circleTarget;
    public LayerMask all_but_Player; // Все слои кроме слоя игрока для поиска вокруг объектов
    private float moveSpeed;
    private bool isBoost;
    
    void Start () {
        
    }

    public void Move(float ax, bool isBoost) { // Движение игрока, "ax" - это направление например если налево, то ax == -1

         this.isBoost = isBoost;

         //Vector3 direction = transform.right * ax;
         //if (isBoost && isGround()){
         //    transform.position = Vector3.Lerp(transform.position, transform.position + direction, moveSpeed_2X * Time.deltaTime);
         //}
         //else {
         //    transform.position = Vector3.Lerp(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
         //}

         if ((ax > 0 && transform.localScale.x < 0) || (ax < 0 && transform.localScale.x > 0)) // Поворот персонажа
         {
             transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
         }

        
         if (isBoost && isGround())
         {
            moveSpeed = moveSpeed_2X;
         }
         else {
            moveSpeed = moveSpeed_1X;
         }

         transform.Translate(ax * moveSpeed * Time.deltaTime, 0, 0);

         if (ax != 0) { // Если игрок движется, то отключаем хук

            GetComponent<LineRenderer>().enabled = false; 
            GetComponent<TargetJoint2D>().enabled = false;
            GetComponent<Hook>().isPressed = false;

         }

    }

    public void Jump() {

        if (isGround()) {
            if (GetComponent<Head>().haveHead){ // Если есть голова
                if (isBoost){ // Если нажат буст кнопка
                    GetComponent<Rigidbody2D>().AddForce(transform.up * (jumpForce_haveHead + BoostForce_forJump), ForceMode2D.Impulse);
                }
                else {
                    GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce_haveHead, ForceMode2D.Impulse);
                }
            }
            else {
                GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce_NotHaveHead, ForceMode2D.Impulse);
            }
        }

    }

    public bool isGround() { // Метод для обнаружение вокруг объектов с тегом "Platform"

        Collider2D[] gh = Physics2D.OverlapCircleAll(circleTarget.position, radCir, all_but_Player);
        int j = 0;
        for (int i = 0; i < gh.Length; i++) {
            j++;
        }

        return j > 0; // Дает true если j больше 0
    }

    public void Climb(float ax) { // Движение вверх или низ, по лестнице

        Vector3 direction = transform.up * ax;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, climbSpeed * Time.deltaTime);

    }

}

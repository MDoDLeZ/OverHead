using UnityEngine;

public class Head : MonoBehaviour {

    public Transform circleTarget;
    public Transform pos_Head;
    public float radCir;
    public bool haveHead;


    Vector2 directionImpulse;//Направление вектора броска головы

    float horizontal;//Направление игрока

    [SerializeField]
    float throwForce;//сила броска головы

    Movement moveScript;

    private void Start() {

        if (transform.Find("Head") != null) { // Если игрок имеет дочерний объект "Head"
            haveHead = true;
            transform.Find("Head").transform.localPosition = pos_Head.localPosition; 
        }
        else
            haveHead = false;

        moveScript = GetComponent<Movement>();
    }


    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        DirectionHead(horizontal);
    }


    public void Drop() { // Бросить голову

        if (haveHead) {

            Transform head = transform.Find("Head");
            head.SetParent(null); // Объект head теперь не имеет родителей =(
            head.GetComponent<Collider2D>().isTrigger = false;
            head.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            haveHead = false;

            //Импульс при броске
            head.GetComponent<Rigidbody2D>().AddForce(directionImpulse * throwForce, ForceMode2D.Impulse);
        }

        
    }

    public void Take() { // Взять голову

        Collider2D[] gh = Physics2D.OverlapCircleAll(circleTarget.position, radCir);
        for (int i = 0; i < gh.Length; i++) { // Цикл для поиска вокруг голову

            if (gh[i].name == "Head" && !haveHead) { 

                Transform head = gh[i].transform;
                head.GetComponent<Collider2D>().isTrigger = true;
                head.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                head.SetParent(transform); // Игроку добавлен объект head
                head.localPosition = pos_Head.localPosition;
                haveHead = true;

            }

        }

    }



    void DirectionHead(float h)
    {//Направление вектора броска головы

        if (h < 0 && moveScript.isGround())//налево
        {

            directionImpulse = Vector2.left;

        }

        if (h > 0 && moveScript.isGround())//направо
        {

            directionImpulse = Vector2.right;

        }


        if (h > 0 && !moveScript.isGround())//вверх направо
        {

            directionImpulse = Vector2.up + Vector2.right;

        }


        if (h < 0 && !moveScript.isGround())//вверх налево
        {

            directionImpulse = Vector2.up + Vector2.left;

        }

        if (h == 0)//стоит или прыгает
        {

            directionImpulse = Vector2.up;

        }


    }

}

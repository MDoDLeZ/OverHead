using UnityEngine;

public class Head : MonoBehaviour {

    public Transform circleTarget;
    public Transform pos_Head;
    public float radCir;
    public bool haveHead;

    private void Start() {

        if (transform.Find("Head") != null) { // Если игрок имеет дочерний объект "Head"
            haveHead = true;
            transform.Find("Head").transform.localPosition = pos_Head.localPosition; 
        }
        else
            haveHead = false;

    }

    public void Drop() { // Бросить голову

        if (haveHead) {

            Transform head = transform.Find("Head");
            head.SetParent(null); // Объект head теперь не имеет родителей =(
            head.GetComponent<Collider2D>().isTrigger = false;
            head.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            haveHead = false;

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

}

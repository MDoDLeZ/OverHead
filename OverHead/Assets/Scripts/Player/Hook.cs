using UnityEngine;

public class Hook : MonoBehaviour {

    public Transform vertexHook; // Вершина веревки (хука), для визуального 
    public LayerMask all_but_Player; // Все слои кроме слоя игрока для поиска вокруг объектов
    public bool isPressed; // Проверка на нажатие
    private Vector3 hitPos; // Позиция где должен быть хук
    private LineRenderer lr;

    public void Start(){

        isPressed = false;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        GetComponent<TargetJoint2D>().enabled = false;

    }

    public void Update(){

        if (isPressed) {
            
            vertexHook.position = Vector3.Lerp(vertexHook.position, hitPos, 0.2f); // Вершина хука (отдельный объект), направляется на место падение луча
            lr.SetPosition(0, transform.position); // Первая точка хука это игрок
            if (!GetComponent<TargetJoint2D>().enabled) // Если игрок стоит 
                lr.SetPosition(1, vertexHook.position); // Вторая точка хука (постоянно меняется идет на место где упал луч)

        }

        if (Vector3.Distance(vertexHook.position, hitPos) < 0.5f) { // Если расстояние между вершиной хука и местом где упал луч остался меньше чем 0,5f

            GetComponent<TargetJoint2D>().target = hitPos;
            GetComponent<TargetJoint2D>().enabled = true; // Игрок начинает движение

        }

        if (!isPressed) 
            vertexHook.localPosition = Vector3.zero;

    }

    public void Hooking() {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Позиция мыши
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, 10f, all_but_Player); // Луч, который смотрит все слои кроме игрока

        if (hit != false && hit.collider.tag == "PlaceHook") { // Если луч не пустой и объект на котором упал луч имеет тег "PlaceHook"
            isPressed = true; // Начался рисование хука
            hitPos = hit.point;
            lr.enabled = true;
        }

    }

}

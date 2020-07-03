using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // Need to know what objects are clickable
    public LayerMask clickableLayer;
    // Need to swap cursors per object (if enemy make cursor red, etc.)
    public Texture2D pointer;   // normal pointer
    public Texture2D target;    // cursor for clickable objects, like the wall
    public Texture2D doorway;   // cursor for doorways
    public Texture2D combat;    // for combat actions

    public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool item = false;

            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                }
                else if (item)
                {
                    Transform itemPos = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(itemPos.position);
                    Debug.Log("ITEM");
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }                                   
            }
        } else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3: UnityEvent<Vector3>
{

}

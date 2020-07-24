using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    public Rigidbody2D rb;
    public GameObject crosshair;
    public GameObject FOV;
    public Vector3 lookDir;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    private FieldOfView fov;

    bool fovMode = false;
    bool fovFirst = false;

    private void Start()
    {
        Cursor.visible = false;
        fov = FOV.GetComponent<FieldOfView>();
        fov.updateFOV(360f, 70f);

    }
    void Update()
    {
        //-1 = links, 1 = rechts, 0 = nichts   w,a,s,d + pfeiltaste + controller
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // maus x,y,z zu screen coordinaten x,y
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = new Vector2(mousePos.x, mousePos.y);

        if(!fovFirst && GlobalVar.instance.darkMode)
        {
            fov.withLayer = true;
            fov.updateFOV(360f, 4f);
            fovFirst = true;
        }
        else if(fovFirst && !GlobalVar.instance.darkMode)
        {
            fov.updateFOV(360f, 70f);
            fov.withLayer = false;
            fovFirst = false;
        }

        if (Input.GetMouseButtonDown(1) && GlobalVar.instance.darkMode)
        {
            if (fovMode)
            { 
                fov.updateFOV(360f, 4f);
            }
            else {
                fov.updateFOV(90f, 7f);                    
            }
            fovMode = !fovMode;
        }
    }

    void FixedUpdate()
    {
        if (GlobalVar.instance.paused)
        {
            return;
        }
        // Time.fixedDeltaTime = zeit seit dem letzten call der funktion, damit das movement gleich bleibt
        rb.MovePosition(rb.position + movement * GlobalVar.instance.playerMoveSpeed * Time.fixedDeltaTime);

        //Vector von aktueller position zum maus punkt
        lookDir = mousePos - rb.position;

        // atan -> https://prnt.sc/pgvqxd
        // -90 da player bild anders gedreht ist lol
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;

    }

    void OnTriggerEnter2D(Collider2D collider) {
        GlobalVar.instance.playerMoveSpeed /= 2f;
    }

    void OnTriggerExit2D(Collider2D collider) {
        GlobalVar.instance.playerMoveSpeed *= 2f;
    }

}

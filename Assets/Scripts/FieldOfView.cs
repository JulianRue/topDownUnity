using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public LayerMask layerMask;
    public LayerMask zombieMask;
    public LayerMask defaultMask;

    private Mesh mesh;
    public float FOV;
    public int rayCount;
    public float viewDistance;


    private Vector3 origin = Vector3.zero;
    private float angle = 0f;
    private float angleIncrease;

    private Transform player;
    private PlayerMovement pMovement;
    public bool withLayer = false;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        rayCount = 90;

        this.GetComponent<MeshRenderer>().material.color = new Color(2f, 2f, 2f, 0.2f);

        updateFOV(360f, 70f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    
    public void updateFOV(float fov, float distance)
    {
        this.FOV = fov;
        this.viewDistance = distance;
        //
    }
    

    void LateUpdate()
    {
        angle = 0;
        if (pMovement.lookDir == null)
        {
            return;
        }
        else
        {
            origin = player.position;
            angleIncrease = FOV / rayCount;
            setDirection(pMovement.lookDir.normalized);
        }

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        Vector3 vertex;
        RaycastHit2D rayCastHit2D;
        for (int i = 0; i <= rayCount; i++)
        {
            
            vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            
            if (withLayer) { 
                rayCastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            }
            else { 
                rayCastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, defaultMask);
            }

            if (rayCastHit2D.collider == null) //no hit
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else//hit
            {
                vertex = rayCastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

            

        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    
    private void setDirection(Vector3 aimDirection)
    {
        angle = VectorToFloat(aimDirection) - FOV / 2f + 90f;
    }




    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float VectorToFloat(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

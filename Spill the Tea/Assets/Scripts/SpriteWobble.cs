using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SpriteWobble : MonoBehaviour
{
    
    private Mesh mesh;

    public float wobbleSpeed = 0.2f;
    public float wobbleFactor = 0.2f; 
    public Vector3 danceAnimation;
    private Vector3[] vertices;
    private Vector3[] copyVertices;
    public Animator animator;
    public Texture characterTexture;
    private Camera mainCamera;
    private const string AnimatorSpeed ="speed"; 
    private static readonly int Speed = Animator.StringToHash(AnimatorSpeed);
    void Start()
    {
        // Make the quad face the camera
       mainCamera = Camera.main;
        
       // Create the Mesh
        CreateMesh();
    
        // Copy mesh
        copyVertices = new Vector3[vertices.Length];
        vertices.CopyTo(copyVertices,0);
    }

    private void CreateMesh()
    {
        // Create a new mesh
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        // Define the quad vertices
        vertices = new[]
        {
            new Vector3(-0.5f, -0.75f, 0), // Bottom-left
            new Vector3(0.5f, -0.75f, 0),  // Bottom-right
            new Vector3(-0.5f, 0.75f, 0),  // Top-left
            new Vector3(0.5f, 0.75f, 0)    // Top-right
        };

        // Define the UVs to map the texture correctly
        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0), // Bottom-left
            new Vector2(1, 0), // Bottom-right
            new Vector2(0, 1), // Top-left
            new Vector2(1, 1)  // Top-right
        };

        // Define the triangles (2 triangles form the quad)
        int[] triangles = new int[]
        {
            0, 1, 2, // First triangle
            2, 1, 3  // Second triangle
        };
        
        // Assign vertices, UVs, and triangles to the mesh
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        
        // Assign a default material (you can replace this with your sprite material)
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = characterTexture;
    }

    void Update()
    {
        RotateQuadToCamera();
        SetCharacterRendererPosition();
        WobbleCharacter();
    }

    private void SetCharacterRendererPosition()
    {
        Vector3 newVertices = new Vector3(0, -transform.parent.position.y, 0);
        transform.localPosition = newVertices;
    }

    private void WobbleCharacter()
    {
        // Update the animation speed 
        animator.SetFloat(Speed, wobbleSpeed);
        // Zeitbasierte Animation
        vertices[2] = copyVertices[2] + danceAnimation*wobbleFactor;
        vertices[3] = copyVertices[3] + danceAnimation*wobbleFactor;
        
        // Aktualisiere das Mesh
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    private void RotateQuadToCamera()
    {
        //Rotate Canmera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(directionToCamera.normalized);
        Quaternion rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z); // Billboard only on Y-axis
    }
}
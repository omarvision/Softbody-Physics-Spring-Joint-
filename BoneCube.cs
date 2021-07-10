using UnityEngine;

//complete list of unity inpector attributes https://docs.unity3d.com/ScriptReference/AddComponentMenu.html?_ga=2.45747431.2107391006.1601167752-1733939537.1520033247
//inspector attributes https://unity3d.college/2017/05/22/unity-attributes/
//Nvidia Flex video https://youtu.be/TNAKv1dkYyQ

public class BoneCube : MonoBehaviour
{
    /*
        E --------- F
        |           |
        |   A --------- B
        |   |       |   |
        |   |       |   |
        H --|------ G   |
            |           |
            D --------- C
    */
    [Header("Bones")]
    public GameObject A = null;
    public GameObject B = null;
    public GameObject C = null;
    public GameObject D = null;
    public GameObject E = null;
    public GameObject F = null;
    public GameObject G = null;
    public GameObject H = null;
    [Header("Spring Joint Settings")]
    [Tooltip("Strength of spring")]
    public float Spring = 100f;
    [Tooltip("Higher the value the faster the spring oscillation stops")]
    public float Damper = 0.2f;
    [Header("Other Settings")]
    public Softbody.ColliderShape Shape = Softbody.ColliderShape.Box;
    public float ColliderSize = 0.002f;
    public float RigidbodyMass = 1f; 
    public LineRenderer PrefabLine = null;
    public bool ViewLines = true;

    private void Start()
    {
        Softbody.Init(Shape, ColliderSize, RigidbodyMass, Spring, Damper, RigidbodyConstraints.None, PrefabLine, ViewLines);

        Softbody.AddCollider(ref A);
        Softbody.AddCollider(ref B);
        Softbody.AddCollider(ref C);
        Softbody.AddCollider(ref D);
        Softbody.AddCollider(ref E);
        Softbody.AddCollider(ref F);
        Softbody.AddCollider(ref G);
        Softbody.AddCollider(ref H);
        
        //down
        Softbody.AddSpring(ref A, ref D);
        Softbody.AddSpring(ref B, ref C);
        Softbody.AddSpring(ref F, ref G);
        Softbody.AddSpring(ref E, ref H);

        //across
        Softbody.AddSpring(ref A, ref G);
        Softbody.AddSpring(ref B, ref H);
        Softbody.AddSpring(ref F, ref D);
        Softbody.AddSpring(ref E, ref C);

        //top
        Softbody.AddSpring(ref A, ref B);
        Softbody.AddSpring(ref B, ref F);
        Softbody.AddSpring(ref F, ref E);
        Softbody.AddSpring(ref E, ref A);

        //bottom
        Softbody.AddSpring(ref D, ref C);
        Softbody.AddSpring(ref C, ref G);
        Softbody.AddSpring(ref G, ref H);
        Softbody.AddSpring(ref H, ref D);
    }
}

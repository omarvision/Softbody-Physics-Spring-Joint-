using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public static class Softbody 
{
    #region --- helpers ---
    public enum ColliderShape
    {
        Box,
        Sphere,
    }
    #endregion

    public static ColliderShape Shape;
    public static float ColliderSize;
    public static float RigidbodyMass;    
    public static float Spring;
    public static float Damper;
    public static RigidbodyConstraints Constraints;
    public static LineRenderer PrefabLine;
    public static bool ViewLines;

    public static void Init(ColliderShape shape, float collidersize, float rigidbodymass, float spring, float damper, RigidbodyConstraints constraints)
    {
        Shape = shape;
        ColliderSize = collidersize;
        RigidbodyMass = rigidbodymass;
        Spring = spring;
        Damper = damper;
        Constraints = constraints;
        ViewLines = false;
    }
    public static void Init(ColliderShape shape, float collidersize, float rigidbodymass, float spring, float damper, RigidbodyConstraints constraints, LineRenderer prefabline, bool viewlines)
    {
        Shape = shape;
        ColliderSize = collidersize;
        RigidbodyMass = rigidbodymass;
        Spring = spring;
        Damper = damper;
        Constraints = constraints;
        PrefabLine = prefabline;
        ViewLines = viewlines;
    }
    public static Rigidbody AddCollider(ref GameObject go)
    {
        return AddCollider(ref go, Shape, ColliderSize, RigidbodyMass);        
    }
    public static SpringJoint AddSpring(ref GameObject go1, ref GameObject go2)
    {
        SpringJoint sp = AddSpring(ref go1, ref go2, Spring, Damper);

        if (ViewLines == true)
            AddLine(ref go1, ref go2);

        return sp;
    }
    public static LineRenderer AddLine(ref GameObject go1, ref GameObject go2)
    {
        return AddLine(ref go1, ref go2, ref PrefabLine);
    }

    public static Rigidbody AddCollider(ref GameObject go, ColliderShape shape, float size, float mass)
    {
        switch (shape)
        {
            case ColliderShape.Box:
                BoxCollider bc = go.AddComponent<BoxCollider>();
                bc.size = new Vector3(size, size, size);
                break;
            case ColliderShape.Sphere:
                SphereCollider sc = go.AddComponent<SphereCollider>();
                sc.radius = size;
                break;
        }

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = 0f;
        rb.angularDrag = 10f;
        rb.constraints = Constraints;
        return rb;
    }
    public static SpringJoint AddSpring(ref GameObject go1, ref GameObject go2, float spring, float damper)
    {
        SpringJoint sp = go1.AddComponent<SpringJoint>();
        sp.connectedBody = go2.GetComponent<Rigidbody>();
        sp.spring = spring;
        sp.damper = damper;
        return sp;
    }
    public static LineRenderer AddLine(ref GameObject go1, ref GameObject go2, ref LineRenderer prefab)
    {
        LineRenderer line = Object.Instantiate(prefab);
        line.positionCount = 2;
        line.SetPosition(0, go1.transform.position);
        line.SetPosition(1, go2.transform.position);
        return line;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Shape
{
    Sphere = 0,
    Box
}

public class MyGizmos : MonoBehaviour
{
    public Color gizmoColor = Color.yellow;
    public Shape shape = Shape.Sphere;

    [Header("��ü ��� ��")]
    public float radius = 0.1f;

    [Header("�ڽ� ��� ��")]
    public Vector3 boxSize = Vector3.one;
    public Vector3 boxOffset = Vector3.zero;

    private void OnDrawGizmos()
    {
        //���� ����
        Gizmos.color = gizmoColor;

        switch(shape)
        {
            case Shape.Sphere:
                Gizmos.DrawSphere(transform.position, radius);
                break;
            case Shape.Box:
                Gizmos.DrawWireCube(transform.position + boxOffset, boxSize);
                break;
        }
    }
}

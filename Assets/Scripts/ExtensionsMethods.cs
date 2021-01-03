using UnityEngine;
using System.Collections;


public static class ExtensionMethods
{

    public static void MyRotate(this Transform transform,Vector3 eulers)
    {
        transform.rotation = Quaternion.Euler(eulers) * transform.rotation;
    }
    public static void MyRotateAround(this Transform transform,Vector3 point, Vector3 axis, float angle)
    {
        transform.position -= point;
        transform.rotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
        transform.position += point;
    }
    public static void MyTranslate(this Transform transform,Vector3 translation)
    {
        transform.position += translation;
    }
}
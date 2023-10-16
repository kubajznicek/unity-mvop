using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldConversion : MonoBehaviour
{
    public Transform worldSpace;
    public Transform localSpace;
    public Transform point;

    void OnDrawGizmos() {
        DrawTransformHandeler(worldSpace);
        DrawTransformHandeler(localSpace);
        DrawTransformHandeler(point);

        Gizmos.DrawSphere(point.position, 0.3f);

        // point.localPosition = WorldtoLocal(worldSpace.position, localSpace);
        worldSpace.position = LocalToWorld(point.localPosition, localSpace);
    }

    Vector3 LocalToWorld(Vector3 localPosition, Transform space) {
        Vector3 worldOffset = space.right * localPosition.x +
                              space.up * localPosition.y +
                              space.forward * localPosition.z;

        return worldOffset + space.position;
    }

    Vector3 WorldtoLocal(Vector3 position, Transform space) {
        Vector3 relative = position - space.position;
        

        float x = Vector3.Dot(relative, space.right);
        float y = Vector3.Dot(relative, space.up);
        float z = Vector3.Dot(relative, space.forward);

        return new Vector3(x, y, z);
    }

    void DrawTransformHandeler(Transform transform) {
        Vector3 position = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, transform.right);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, transform.up);
    }
}

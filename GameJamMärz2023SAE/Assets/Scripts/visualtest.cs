using UnityEngine;

[ExecuteAlways]
public class visualtest : MonoBehaviour
{
    public Color color;

    [Range(0, 1)] public float alpha;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r,color.g,color.b, alpha);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
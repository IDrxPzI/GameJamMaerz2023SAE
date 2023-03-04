using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Mesh mesh;

    public GameObject prefabs;
    public int spawnAmount;

    [Range(0, 1)] public float alpha;
    public Vector3 scale;

    public Color color;

    private float transformX, transformY, transformZ;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();

        for (int i = 0; i < spawnAmount; i++)
        {
            transformX = transform.position.x + Random.insideUnitSphere.x * 22.5f;
            transformY = transform.position.y + Random.insideUnitSphere.y * 25;
            transformZ = transform.position.z + Random.insideUnitSphere.z * 22.5f;

            Vector3 spawnPos = new Vector3(transformX, transformY, transformZ);
            var clone = Instantiate(prefabs);
            clone.transform.parent = transform;
            clone.transform.position = spawnPos;
            clone.transform.forward = Random.insideUnitSphere;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, alpha);
        Gizmos.DrawMesh(mesh, transform.position, transform.rotation, scale);
    }
}
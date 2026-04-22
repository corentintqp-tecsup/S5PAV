using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float minSize = 5f;
    public float maxSize = 50f;
    public GameObject player1;
    public GameObject player2;
    void Start()
    {
    }

    void Update()
    {
        DynamicPosition();
    }

    private void DynamicPosition()
    {
        float size = Vector3.Distance(player1.transform.position, player2.transform.position) / 2;
        Vector3 center = (player1.transform.position + player2.transform.position) / 2;
        transform.position = new Vector3(center.x, center.y, transform.position.z);
        Camera.main.orthographicSize = Mathf.Clamp(size, minSize, maxSize);
    }
}

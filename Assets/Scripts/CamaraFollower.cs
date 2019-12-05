using UnityEngine;

public class CamaraFollower : MonoBehaviour
{
    [SerializeField] private Transform camara = null;
    private Player player;
    private float moveSpeed;
    private float camaraOffSet;

    public void SetUp(Player p, float speed)
    {
        player = p;
        moveSpeed = speed;
        camaraOffSet = camara.localPosition.y;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed) * Time.fixedDeltaTime, Space.World);
    }

    public void UpdateCamara(float playerY)
    {
        camara.position = new Vector3(camara.position.x, playerY + camaraOffSet, camara.position.z);
    }
}

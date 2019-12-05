using UnityEngine;

public class CamaraFollower : MonoBehaviour
{
    private Player player;
    private float moveSpeed;
    private float playerOffSet;

    public void SetUp(Player p, float speed, float offSet)
    {
        player = p;
        moveSpeed = speed;
        playerOffSet = offSet;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed) * Time.fixedDeltaTime, Space.World);
    }

    public void CheckOffSet()
    {
        
    }
}

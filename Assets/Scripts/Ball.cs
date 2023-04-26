using System.Threading.Tasks;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;
    private float spawnOffsetFromEdge = 0.5f;

    void Start()
    {
        gameManager = GameManager.Instance;
        Init();
    }

    private void Init()
    {
        //TODO: Balls won't spawn too close to other balls
        var maxCorner = gameManager.GameBoard.GetMaxCorner();
        var minCorner = gameManager.GameBoard.GetMinCorner();

        transform.position = new Vector2(Random.Range(minCorner.x + spawnOffsetFromEdge, maxCorner.x - spawnOffsetFromEdge), Random.Range(minCorner.y + spawnOffsetFromEdge, maxCorner.y - spawnOffsetFromEdge));
        transform.parent = gameManager.GameBoard.transform;
    }
}

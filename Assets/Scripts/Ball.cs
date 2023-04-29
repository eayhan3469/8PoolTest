using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject modelObject;
    [SerializeField] private float sizeFactor;
    [SerializeField] private AudioClip hitSFX;

    public int Number { get; set; }
    public bool HasCollided { get; set; }

    private GameManager gameManager;
    private Rigidbody2D rigidbody2d;
    private CircleCollider2D circleCollider2d;
    float circumference;
    private float spawnOffsetFromEdge = 0.5f;

    void Start()
    {
        gameManager = GameManager.Instance;
        rigidbody2d = GetComponent<Rigidbody2D>();
        circleCollider2d = GetComponent<CircleCollider2D>();

        Init();
        SetPosition();
        circumference = 2 * Mathf.PI * circleCollider2d.radius;
    }

    private void Update()
    {
        var axis = Vector3.Cross(rigidbody2d.velocity, Vector3.forward);
        var angle = (rigidbody2d.velocity.magnitude * 360 / circumference);
        modelObject.transform.Rotate(axis, angle * Time.deltaTime, Space.World);
    }

    private void Init()
    {
        modelObject.transform.localScale = modelObject.transform.localScale * sizeFactor;
        GetComponent<CircleCollider2D>().radius *= sizeFactor;
        modelObject.GetComponent<MeshRenderer>().sharedMaterial = gameManager.BallMaterials[Number];
    }

    private void SetPosition()
    {
        //TODO: Balls won't spawn too close to other balls
        var maxCorner = gameManager.GameBoard.GetMaxCorner();
        var minCorner = gameManager.GameBoard.GetMinCorner();

        transform.position = new Vector2(Random.Range(minCorner.x + spawnOffsetFromEdge, maxCorner.x - spawnOffsetFromEdge), Random.Range(minCorner.y + spawnOffsetFromEdge, maxCorner.y - spawnOffsetFromEdge));
        transform.parent = gameManager.GameBoard.transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 force = randomDirection * 10f;
        rigidbody2d.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            HasCollided = true;

            if (!collision.collider.GetComponent<Ball>().HasCollided)
            {
                gameManager.OnScoreUpdated?.Invoke(gameManager.Score++);
                SoundManager.Instance.PlaySoundEffect(hitSFX);
            }
        }

        if (collision.collider.CompareTag("GameBoardEdge"))
        {
            gameManager.OnScoreUpdated?.Invoke(gameManager.Score++); 
            SoundManager.Instance.PlaySoundEffect(hitSFX);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            HasCollided = false;
        }
    }
}

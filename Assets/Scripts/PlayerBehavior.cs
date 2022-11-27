using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour{
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Text text;

    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private PolygonCollider2D playerCollider;

    private bool transformable = false;

    private Sprite objectSprite;
    private SpriteRenderer objectSpriteRenderer;
    private PolygonCollider2D objectCollider;
    private Transform objectTransform;
    private int triggerCount = 0;
    public PolygonCollider2D polygon;
    private GameObject first;
    private GameObject second;
    private GameObject third;

    private void Update(){
        if (transformable && Input.GetKeyDown("e") && playerSpriteRenderer.sprite != first.GetComponentInParent<sprite>().sprites[0])
        {

            playerSpriteRenderer.sprite = first.GetComponentInParent<sprite>().sprites[0];
            playerCollider.points = first.GetComponentInParent<PolygonCollider2D>().points;
            transform.localScale = first.GetComponentInParent<Transform>().lossyScale;
            if (Physics2D.BoxCast(polygon.bounds.center, polygon.bounds.size, 0f, Vector2.down, .1f, groundLayer))
                    transform.position = new Vector2(transform.position.x, first.GetComponentInParent<Transform>().position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.layer == LayerMask.NameToLayer("objectTrigger")) {
            triggerCount++;

            if (triggerCount == 2)
            {
                second = first;
            Debug.Log("second : " + second.GetComponentInParent<SpriteRenderer>().sprite.name);
            }else if (triggerCount == 3)
            {
                third = second;
                second = first;
            Debug.Log("second : " + second.GetComponentInParent<SpriteRenderer>().sprite.name);
            Debug.Log("third : " + third.GetComponentInParent<SpriteRenderer>().sprite.name);
            }

            first = collision.gameObject;

            Debug.Log("first : " + first.GetComponentInParent<SpriteRenderer>().sprite.name);
            Debug.Log(triggerCount);

            collision.GetComponentInParent<SpriteRenderer>().sprite = collision.GetComponentInParent<sprite>().sprites[1];
            objectSprite = collision.GetComponentInParent<sprite>().sprites[0];
            objectSpriteRenderer = collision.GetComponentInParent<SpriteRenderer>();
            objectCollider = collision.GetComponentInParent<PolygonCollider2D>();
            objectTransform = collision.transform;
            transformable = true;
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.layer == LayerMask.NameToLayer("objectTrigger")){
            triggerCount--;
            Debug.Log(triggerCount);

                collision.GetComponentInParent<SpriteRenderer>().sprite = collision.GetComponentInParent<sprite>().sprites[0];
            if (triggerCount == 0)
            {
                transformable = false;
                text.enabled = false;

                first = null;
            }else if (triggerCount == 1)
            {
                first = second;
                second = null;
                Debug.Log("first : " + first.GetComponentInParent<SpriteRenderer>().sprite.name);
            }
            else if (triggerCount == 2)
            {
                first = second;
                second = third;
                Debug.Log("first : " + first.GetComponentInParent<SpriteRenderer>().sprite.name);
                Debug.Log("second : " + second.GetComponentInParent<SpriteRenderer>().sprite.name);
            }

        }
    }
}

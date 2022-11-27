using UnityEngine;
using UnityEngine.UI;

public class Behaviour : MonoBehaviour{
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask groundLayer, wallLayer;
    [SerializeField] private Image clickToSwapImage;
    [SerializeField] private Image outOfRangeImage;

    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer, playerSpriteRenderer;
    private PolygonCollider2D polygonCollider, playerPolygonCollider2D;
    private bool outOfRange = false;
    private bool mouseEnter = false;

    private void Start(){
        sprites = GetComponent<sprite>().sprites;
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerPolygonCollider2D = player.GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (mouseEnter)
        {
            Vector3 fromPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 toPosition = player.transform.position;
            if (Physics2D.Linecast(fromPosition, toPosition, groundLayer | wallLayer).collider)
            {
                outOfRange = true;
                outOfRangeImage.enabled = true;
                clickToSwapImage.enabled = false;
            }
            else
            {
                outOfRange = false;
                outOfRangeImage.enabled = false;
                clickToSwapImage.enabled = true;
            }
        }
    }

    private void OnMouseEnter(){
        spriteRenderer.sprite = sprites[1];
        mouseEnter = true;
    }

    private void OnMouseExit(){
        spriteRenderer.sprite = sprites[0];
        mouseEnter = false;
        clickToSwapImage.enabled = false;
        outOfRangeImage.enabled = false;
    }

    private void OnMouseDown(){
        if (sprites[0] != playerSpriteRenderer.sprite && !outOfRange){
            if (sprites[0].texture.height > playerSpriteRenderer.sprite.texture.height)
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1f);

            playerSpriteRenderer.sprite = sprites[0];
            playerPolygonCollider2D.points = polygonCollider.points;
        }
    }
}

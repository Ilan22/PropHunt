using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        transform.position = cursorPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "prop")
        //{
        Debug.Log("dd");
            collision.GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<sprite>().sprites[1];
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("dd");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "prop")
        {
            collision.GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<sprite>().sprites[0];
        }
    }
}

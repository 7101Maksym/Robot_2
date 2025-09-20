using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class DynamicCollider : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private Animator animator;

    List<Vector2> points = new List<Vector2>();

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        animator.SetFloat("HorizontalOrVertical", -1f);
    }

    void LateUpdate()
    {
        if (spriteRenderer.sprite != null && spriteRenderer.sprite.GetPhysicsShapeCount() != 0)
        {
            spriteRenderer.sprite.GetPhysicsShape(0, points);
            polygonCollider.SetPath(0, points);
        }
    }
}

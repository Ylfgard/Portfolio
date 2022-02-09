using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour, IWithPosition
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private SpriteRenderer _leafSprite;
    [SerializeField] private Transform _transform;
    [SerializeField] private Collider2D _collider;
    private bool _colorized = false;
    private Paint _paint;
    private LeafColorizedCommand command = new LeafColorizedCommand();

    public bool Colorized => _colorized;

    public Paint Paint => _paint;

    private void Start()
    {
        command.Leaf = this;
    } 
    
    public void Colorize(Paint paint)
    {
        _paint = paint;
        _leafSprite.color = _paint.Color;
        _colorized = true;
        _collider.enabled = false;
        _mediator.Publish(command);
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    public void Uncolorize()
    {
        _paint = null;
        _leafSprite.color = Color.white;
        _colorized = false;
        _collider.enabled = true;
    }
}

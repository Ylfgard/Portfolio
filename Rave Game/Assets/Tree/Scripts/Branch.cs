using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour, IWithPosition
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private Transform _transform;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private List<Leaf> _leafs;

    private void Start() 
    {
        _mediator.Subscribe<LeafColorizedCommand>(CompliteCombination);
        _collider.enabled = false;
    }
    
    private void CheckForCombination(bool add)
    {
        CombinationIsAssembledCommand command = new CombinationIsAssembledCommand();
        foreach(Leaf leaf in _leafs)
            if(leaf.Colorized)
                command.Paints.Add(leaf.Paint);
        if(command.Paints.Count == _leafs.Count)
        {
            command.AddCombination = add;
            _mediator.Publish(command);
        } 
    }

    private void CompliteCombination(LeafColorizedCommand callback)
    {
        if(_leafs.Contains(callback.Leaf))
        {
            CheckForCombination(true);
            _collider.enabled = true;
        }
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    public void UncolorizeLeafs()
    {
        CheckForCombination(false);
        foreach(Leaf leaf in _leafs)
            leaf.Uncolorize();
        _collider.enabled = false;
    }
}

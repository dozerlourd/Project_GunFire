using UnityEngine;

[System.Serializable]
public class DropItem
{
    [SerializeField] GameObject item;
    [SerializeField] float rand;

    public GameObject Item => item;
    public float Rand => rand;
}

public class ItemDrop : MonoBehaviour
{
    [SerializeField] DropItem[] dropItems;

    [SerializeField] int maxDropRepeat;
    protected Vector2 randomVec;

    public virtual void Drop()
    {

    }

    protected virtual void RandomVec()
    {

    }
}

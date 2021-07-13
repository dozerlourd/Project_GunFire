using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    [SerializeField] GameObject item;
    [SerializeField] float weight;

    public GameObject Item => item;
    public float Weight => weight;
}

public class ItemDropSystem : MonoBehaviour
{
    public List<DropItem> dropItems = new List<DropItem>();
    float weightAmount = 0;

    [SerializeField] int maxDropRepeat;
    protected Vector2 randomVec;

    private void Start()
    {
        weightAmount = GetWeightAmount();
    }

    /// <summary> 나올 수 있는 아이템의 가중치 총량을 계산해주는 함수 </summary>
    /// <returns> 나올 수 있는 아이템의 가중치의 총 합계 </returns>
    float GetWeightAmount()
    {
        float _weightAmount = 0;
        for (int i = 0; i < dropItems.Count; i++)
        {
            _weightAmount += dropItems[i].Weight;
        }
        return _weightAmount;
    }

    public void Drop()
    {

    }

    protected void RandomVec()
    {

    }
}

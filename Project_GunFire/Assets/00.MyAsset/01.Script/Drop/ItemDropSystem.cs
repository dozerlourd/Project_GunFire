using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    [SerializeField] GameObject item;
    [SerializeField] public float weight;

    public GameObject Item => item;
    public float Weight => weight;
}

public class ItemDropSystem : MonoBehaviour
{
    public List<DropItem> dropItems = new List<DropItem>();

    [SerializeField] int maxDropRepeat;
    protected Vector2 randomVec;

    private void Start()
    {
        GetWeightAmount();
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
        float total = 0;
        for (int i = 0; i < dropItems.Count; i++) total += dropItems[i].weight;

        for (int itemDropRepeat = 0; itemDropRepeat < maxDropRepeat; itemDropRepeat++)
        {
            float rand = Random.value * total;
            //if (this.gameObject.name == "Boss") Debug.Log("돌아간다");
            for (int i = 0; i < dropItems.Count; i++)
            {
                if (dropItems[i].weight > rand)
                {
                    if (!dropItems[i].Item) break;
                    GameObject itemClone = Instantiate(dropItems[i].Item);
                    itemClone.transform.position = transform.position;
                    ForceRandomVec(itemClone);
                    break;
                }
                else rand -= dropItems[i].weight;
            }
        }
    }

    protected void ForceRandomVec(GameObject _item)
    {
        if (_item.GetComponent<ItemInfo>().E_ItemType == ItemInfo.ItemType.Gold)
        {
            _item.transform.position = new Vector3(_item.transform.position.x, 1.25f, _item.transform.position.z);
            _item.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0, 0.3f), Random.Range(-1f, 1f)).normalized, ForceMode.Impulse);
        }

        if(_item.GetComponent<ItemInfo>().E_ItemType == ItemInfo.ItemType.Weapon)
        {
            _item.transform.position = new Vector3(_item.transform.position.x, 0.25f, _item.transform.position.z);
        }
    }
}
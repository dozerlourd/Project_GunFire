using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSystem : SceneObject<ParentSystem>
{
    public Transform MainUI => mainUI;
    [SerializeField] Transform mainUI;

    public Transform DamageUICanvas => damageUICanvas;
    [SerializeField] Transform damageUICanvas;
}

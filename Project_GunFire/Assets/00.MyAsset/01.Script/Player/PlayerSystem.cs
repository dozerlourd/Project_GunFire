using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : SceneObject<PlayerSystem>
{
    [SerializeField] GameObject m_player;
    PlayerHP m_playerHP;

    public GameObject Player => m_player;
    public PlayerHP PlayerHP => m_playerHP = m_playerHP ? m_playerHP : Player.GetComponent<PlayerHP>();
}
    
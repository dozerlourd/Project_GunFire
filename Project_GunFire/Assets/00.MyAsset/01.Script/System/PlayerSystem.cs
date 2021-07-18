using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : SceneObject<PlayerSystem>
{
    [SerializeField] GameObject m_player;
    PlayerHP m_playerHP;
    PlayerSkill m_playerSkill;
    PlayerAttack m_playerAttack;
    PlayerMove m_playerMove;
    PlayerLootingItem m_playerLootingWeapon;
    PlayerArousal m_playerArousal;
    PlayerMovement m_playerMovement;

    public GameObject Player => m_player;
    public PlayerHP PlayerHP => m_playerHP = m_playerHP ? m_playerHP : Player.GetComponent<PlayerHP>();
    public PlayerSkill PlayerSkill => m_playerSkill = m_playerSkill ? m_playerSkill : Player.GetComponent<PlayerSkill>();
    public PlayerAttack PlayerAttack => m_playerAttack = m_playerAttack ? m_playerAttack : Player.GetComponent<PlayerAttack>();
    public PlayerMove PlayerMove => m_playerMove = m_playerMove ? m_playerMove : Player.GetComponent<PlayerMove>();
    public PlayerLootingItem PlayerLootingWeapon => m_playerLootingWeapon = m_playerLootingWeapon ? m_playerLootingWeapon : Player.GetComponent<PlayerLootingItem>();
    public PlayerArousal PlayerArousal => m_playerArousal = m_playerArousal ? m_playerArousal : Player.GetComponent<PlayerArousal>();
    public PlayerMovement PlayerMovement => m_playerMovement = m_playerMovement ? m_playerMovement : Player.GetComponent<PlayerMovement>();
}
    
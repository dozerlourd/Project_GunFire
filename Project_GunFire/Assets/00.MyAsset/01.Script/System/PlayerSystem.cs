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
    PlayerLootingWeapon m_playerLootingWeapon;

    public GameObject Player => m_player;
    public PlayerHP PlayerHP => m_playerHP = m_playerHP ? m_playerHP : Player.GetComponent<PlayerHP>();
    public PlayerSkill PlayerSkill => m_playerSkill = m_playerSkill ? m_playerSkill : Player.GetComponent<PlayerSkill>();
    public PlayerAttack PlayerAttack => m_playerAttack = m_playerAttack ? m_playerAttack : Player.GetComponent<PlayerAttack>();
    public PlayerMove PlayerMove => m_playerMove = m_playerMove ? m_playerMove : Player.GetComponent<PlayerMove>();
    public PlayerLootingWeapon PlayerLootingWeapon => m_playerLootingWeapon = m_playerLootingWeapon ? m_playerLootingWeapon : Player.GetComponent<PlayerLootingWeapon>();
}
    
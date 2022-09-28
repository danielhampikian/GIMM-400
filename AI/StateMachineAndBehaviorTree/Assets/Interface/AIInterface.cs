using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackAI
{
    string _name { get; set; }
    float _health { get; set; } // health = vision/speed
    float _Vision { get; set; } // Damage = Attack Range/ Fire Rate
    float _damage { get; set; }
    float _fireRate { get; set; }
    float _AttackRange { get; set; }
    float _Speed { get; set; }
    float _arenaRadius { get; set; }
    public void TakeDamage(float amt);
    public void Attack(GameObject proj);
    public bool DeathCheck();
    public bool CheckValue();
    public void Die();
    public void Move();
    public bool CanShoot();
}
public interface IProjectile
{
    float _projDamage { get; }
    float _projSpeed { get; set; }
    public void DealDamage();
    public void inheritDamage();
    public void calculateSpeed();
    public void shoot();
}

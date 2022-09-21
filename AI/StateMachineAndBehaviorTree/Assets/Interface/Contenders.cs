using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Contender : IAttackAI, IProjectile
{
    public abstract float _projDamage { get; }
    public abstract string _name { get; set; }
    public abstract float _health { get; set; }
    private static float HEALTHMULTIPLIER = 100;
    public abstract float _Vision { get; set; }
    public abstract float _damage { get; set; }
    private static float STANDARDMULTIPLIER = 10;
    public abstract float _fireRate { get; set; }
    public abstract float _AttackRange { get; set; }
    public abstract float _Speed { get; set; }
    public abstract float _arenaRadius { get; set; }
    public float shotTime { get; set; }
    public float _projSpeed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public abstract void Attack(GameObject proj); //shoots out projectile
    public bool CheckValue() //checks to see if ratio's work
    {
        if (_health <= .1 || _health >= 1)
        {
            Debug.LogException(new System.Exception("Health needs to be in between 10 and 90"));
            return false;
        }
        else if (_health != _Vision / _Speed) {
            Debug.LogException(new System.Exception("Review your health ratio(Health = Vision / Speed)"));
            return false;
        }
        if (_damage < .1 || _damage > 1)
        {
            Debug.LogException(new System.Exception("Health needs to be in between 2 and 10"));
            return false; 
        }
        else if (_damage != _AttackRange / _fireRate)
        {
            Debug.LogException(new System.Exception("Review your damage ratio (Damage = Attack Range/ Fire Rate)"));
            return false;
        }
        if (_fireRate < .1 || _fireRate > 1)
        {
            Debug.LogException(new System.Exception("Fire Rate needs to be between .1 and 1"));
            return false;
        }
        _health *= HEALTHMULTIPLIER;
        _damage *= STANDARDMULTIPLIER;
        _Speed *= STANDARDMULTIPLIER;
        _fireRate = 1 / (STANDARDMULTIPLIER * _fireRate);
        _Vision = _arenaRadius / (1 / _Vision);
        _AttackRange *= STANDARDMULTIPLIER;
        return true;
        
    }
    public abstract void DealDamage(); //projectile applies damage on collision
    public bool DeathCheck() //boolean to check if you are dead
    {
        if (_health <= 0) return true;
        else return false;
    }
    public abstract void Die(); //way to kill AI
    public abstract void Move(); //move your AI, recommended to use vision and attack range to determine
    public void TakeDamage(float amt) //takes the damage from the projectile
    {
        _health -= amt;
        if(DeathCheck()) Die();
    }
    public abstract void shoot(Vector3 projDirection); 
    /// <summary>
    /// Shoots the projectile
    /// Groups should calculate this based on time.deltatime and the timeShot variable based on fire rate
    /// </summary>
    public abstract void inheritDamage(); //let the projectile inherit the ai's damage

    public abstract void calculateSpeed();

    public abstract bool CanShoot(); //fire rate should be calculated as FireRate = 1/_fireRate
    public abstract void shoot();
}

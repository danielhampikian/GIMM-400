using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplementedInterfaceAI : Contender
{
    public override float _projDamage { get; }

public override string _name { get; set; }
    public override float _health { get; set; }
    public override float _Vision { get; set; }
    public override float _damage { get; set; }
    public override float _fireRate { get; set; }
    public override float _timeRemaining { get; set; }
    public override float _AttackRange { get; set; }
    public override float _Speed { get; set; }
    public override float _arenaRadius { get; set; }
    public float _startingHealth;


    //New variables:
    public GameObject proj;
    private StateController sc;   

    
    
    public override void Attack(GameObject proj)
    {
        //TODO 1. Check remaining time from last shot with canSHoot()
        //      2. create projectile 
        //      3. use forward direction of transform and speed in shoot
        //      4. Set the timer back to the fireRate (_timeRemaining = fireRate)
    }
 
    // Start is called before the first frame update
    void Start()
    {
        _name = "Dan's AI";
        _Vision = .5f;
        _Speed = .5f;
        _AttackRange = .3f;
        _fireRate = .7f;
        _startingHealth = _health;
        //health = 2.5f * 100
        _health = _Vision / _Speed;
        _damage = _AttackRange / _fireRate;
        if (!CheckValue())
        {
            throw new System.Exception("Values not correctly balanced");

        }
        else
        {
            //find a new spawn point with SC
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void calculateSpeed()
    {
        throw new System.NotImplementedException();
    }

    public override bool CanShoot()
    {
        throw new System.NotImplementedException();
    }

    public override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void inheritDamage()
    {
        throw new System.NotImplementedException();
    }

   
    public override void shoot(Vector3 projDirection)
    {
        GameObject go = Instantiate(proj, transform.position, transform.rotation);
        Rigidbody goRB = new Rigidbody();
        //go.AddComponent(Rigi);
        goRB.velocity = transform.forward * _Speed ;
  }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}

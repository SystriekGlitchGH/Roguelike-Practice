using UnityEngine;

public class Weapon
{
	public string type; // type of weapon: blade, gun, beam, summon
	public string name; // name of the weapon: sword, shotgun, dual beam
	public float baseAttack; // base attack of weapon
    public float baseAttackSpeed; // base attack speed of weapon
    public float baseKnockback; // base knockback of weapon
    // only for melee
    public Vector2 baseAttackSize; // size of melee weapon hitbox
    public float baseAttackDistance; // distance away from player at center hitbox of melee weapon
    // only for gun
    public float bullets; // amount of bullets fired per shot input
    public float pierce; // amount of enemies the bullets can hit before being destroyed
    public float spread;


	public Weapon(string n, string t)
    {
		type = t;
		name = n;
		if (t == "blade")
        {
            if(n == "sword")
            {
                baseAttack = 6;
                baseAttackSpeed = 6;
                baseKnockback = 10;
                baseAttackSize = new Vector2(2.5f,3);
                baseAttackDistance = 2.5f;
            }
            else if(n == "axe")
            {
				baseAttack = 8;
				baseAttackSpeed = 2;
				baseKnockback = 20;
				baseAttackSize = new Vector2(3.5f, 2);
				baseAttackDistance = 2f;
			}
            else if(n == "spear")
            {
				baseAttack = 5;
				baseAttackSpeed = 4f;
				baseKnockback = 8;
				baseAttackSize = new Vector2(2f, 4);
				baseAttackDistance = 3f;
			}
        }
        if (t == "gun")
        {
            if(n == "auto")
            {
                baseAttack = 4;
                baseAttackSpeed = 10;
                baseKnockback = 4;
                bullets = 1;
                pierce = 2;
                spread = 10;
            }
            else if(n == "shotgun")
            {
				baseAttack = 2;
				baseAttackSpeed = 6;
				baseKnockback = 2;
				bullets = 12;
				pierce = 1;
                spread = 30;
			}
            else if(n == "sniper")
            {
				baseAttack = 10;
				baseAttackSpeed = 2;
				baseKnockback = 8;
				bullets = 1;
				pierce = 5;
			}
        }
        if (t == "tome")
        {
            if(n == "single")
            {
                baseAttack = 2;
                baseAttackSpeed = 10;
                baseKnockback = 42;
            }
            else if(n == "spread")
            {
				baseAttack = 1;
				baseAttackSpeed = 6;
				baseKnockback = 2;
			}
            else if(n == "point")
            {
				baseAttack = 1.5f;
				baseAttackSpeed = 2;
				baseKnockback = 1;
			}
        }
    }
}

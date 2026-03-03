using UnityEngine;

public class Weapon
{
	public string type;
	public string name;
	public float baseAttack;
    public float baseAttackSpeed;
    public float baseKnockback;
    //only for melee
    public Vector2 baseAttackSize;
    public float baseAttackDistance;

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
    }
}

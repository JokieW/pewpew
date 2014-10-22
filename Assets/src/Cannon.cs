using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
    public GameObject Bullet;

    public void ShootTowardPlayer()
    {
        GameObject root = new GameObject("BulletRoot");
        root.transform.position = transform.position;
        
        Vector3 playerPosition = Player.GetPlayer().transform.position;
        root.transform.up = Vector3.Normalize(playerPosition - transform.position);
        GameObject bgo = (GameObject)Instantiate(Bullet, root.transform.position, root.transform.rotation);
        bgo.transform.parent = root.transform;
        bgo.GetComponent<Bullet>().owner = GetComponent<Actor>();
    }

    public void ShootUp()
    {
        GameObject root = new GameObject("BulletRoot");
        root.transform.position = transform.position + new Vector3(0.0f, 0.5f, 0.0f);

        GameObject bgo = (GameObject)Instantiate(Bullet, root.transform.position, root.transform.rotation);
        bgo.transform.parent = root.transform;
        bgo.GetComponent<Bullet>().owner = GetComponent<Actor>();
    }

    /*public void Shoot(Vector3 position, Vector3 direction, AnimationClip clip)
    {
        GameObject root = new GameObject("BulletRoot");
        root.transform.position = position;
        root.transform.rotation = Quaternion.Euler(direction * 360);
        GameObject bgo = (GameObject)GameObject.Instantiate(Bullet);
        Bullet b = bgo.GetComponent<Bullet>();
        if (clip != null)
        {
            b.animation = clip;
        }
    }

    public void Shoot(Vector3 position, Vector3 direction)
    {
        Shoot(position, direction, null);
    }*/

    /*public void Shoot()
    {
        Shoot(transform.position, transform.up, null);
    }*/
}

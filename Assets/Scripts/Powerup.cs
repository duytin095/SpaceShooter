using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField] // 0 = Triple Shot, 1 == Speed, 2 = Shield
    private float powerupID;
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if(transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        player.ActiveSpeedBoots();
                        break;
                    case 2:
                        player.ActiveShield();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
using System;
using GameSystem;
using Player;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private Vector3 newPosition;
        private void Start()
        {           
            newPosition=GameService.Instance.FindNewPosition(this.transform.position,this.gameObject);          
        }
        private void FixedUpdate()
        {
            if (this.transform.position != newPosition)
            {
                iTween.MoveTo(this.gameObject, newPosition, 1f);
            }
            else
            {
                newPosition = GameService.Instance.FindNewPosition(this.transform.position,this.gameObject);
                //Debug.Log("new new position" + newPosition);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerView>())
            {
                GameService.Instance.InvokePlayerKilled();
            }
        }

    }
}
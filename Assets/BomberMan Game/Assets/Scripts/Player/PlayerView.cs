using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer),typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Animator playerAnimator;
        private Transform playerParent;
        private Rigidbody2D rb;
        private float speed=5f;
        private void Start()
        {
            if (playerAnimator == null)
            {
                playerAnimator=this.GetComponent<Animator>();                
            }
            playerParent = this.transform.parent;
            rb = GetComponent<Rigidbody2D>();
        }
        public void MoveLeft()
        {
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetFloat("xDirection",-1);
            rb.velocity = new Vector3(-1, 0, 0) * speed;
        }
        public void MoveRight()
        {
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetFloat("xDirection",1);
            rb.velocity = new Vector3(1, 0, 0) * speed;
        }
        public void MoveUp()
        {
            rb.velocity = new Vector3(0, 1, 0) * speed;
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetFloat("yDirection", 1);
        }
        public void MoveDown()
        {
            rb.velocity = new Vector3(0, -1, 0) * speed;
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetFloat("yDirection", -1);
        }

        public void StayIdle()
        {
            rb.velocity = Vector3.zero;
            playerAnimator.SetBool("isIdle", true);
            playerAnimator.SetFloat("xDirection", 0);
            playerAnimator.SetFloat("yDirection", 0);
        }

        public void DestroyView()
        {
            Destroy(this.gameObject);
        }
    }
}
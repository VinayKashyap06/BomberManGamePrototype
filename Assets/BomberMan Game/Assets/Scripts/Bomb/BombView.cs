using System;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;

namespace Bomb
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BombView : MonoBehaviour
    {
        private void OnDestroy()
        {
            GameService.Instance.InvokeBombDestroyed(this.transform.position);   
        }
    }
}

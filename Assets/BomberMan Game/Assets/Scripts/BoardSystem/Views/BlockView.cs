using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
    public class BlockView : MonoBehaviour
    {
        public bool isDestructible;
    }
}
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        [HideInInspector] public Vector3 moveDir;
    }
}
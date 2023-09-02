using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }
    }
}
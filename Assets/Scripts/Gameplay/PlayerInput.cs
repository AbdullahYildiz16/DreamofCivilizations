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

        private void Update()
        {
            var dir = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
            dir.Normalize();
            _player.PlayerMovement.SetDir(dir);
            _player.PlayerMovement.SetMouseX(Input.GetAxis("Mouse X"));
            _player.PlayerMovement.SetMouseY(Input.GetAxis("Mouse Y"));
            if (Input.GetButtonUp("Fire1")) _player.TryCollectObject();
            //if (Input.GetButtonDown("Jump")) _player.PlayerMovement.TryJump();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueGravityTest.Scripts.MVC
{
    public class PlayerView : MonoBehaviour
    {
        public enum BodyParts {Body, Shirt, Shoes, Pants}

        
        [SerializeField] private List<AnimatorParts> playerAnimators;

        private static readonly string MOVE_ANIMATION ="OnMove";
        private static readonly string X_MAGNITUDE ="X";
        private static readonly string Y_MAGNITUDE ="Y";
        
        public void Subscribe(PlayerModel model)
        {
            model.OnMove += MoveAnimation;
        }

        private void MoveAnimation(Vector2 dir)
        {
            if (dir == Vector2.zero)
            {
                foreach (var animatorParts in playerAnimators)
                    animatorParts.animator.SetBool(MOVE_ANIMATION, false);
                return;
            }
            
            foreach (var animatorParts in playerAnimators)
            {
                animatorParts.animator.SetBool(MOVE_ANIMATION, true);
                animatorParts.animator.SetFloat(X_MAGNITUDE, dir.x);
                animatorParts.animator.SetFloat(Y_MAGNITUDE, dir.y);
                animatorParts.spriteRenderer.flipX = !(dir.x > 0);
            }
        }
    }

    [Serializable]
    public class AnimatorParts
    {
        public PlayerView.BodyParts bodyParts;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
    }
}


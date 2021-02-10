using UnityEngine;

    public abstract class EnemyBase : MonoBehaviour
    {
        #region Components
        [SerializeField]
        protected LayerMask enemyLayer;
        [SerializeField]
        protected Sprite deathSprite;
        [SerializeField]
        protected Animator anim;
        [SerializeField]
        protected SpriteRenderer enemyRenderer;
        protected Rigidbody2D Rb2D;
        #endregion

        #region Atributes
        protected float MovementSpeed;
        protected int MAXHealth;
        protected int CurrentHealth;
        protected Transform Target;
        [SerializeField]
        protected Transform attackPoint;
        protected float AttackRange;
        protected int WeaponDamage;
        public bool isDead = false;
        protected bool CanAttack = true;
        #endregion

   
    }


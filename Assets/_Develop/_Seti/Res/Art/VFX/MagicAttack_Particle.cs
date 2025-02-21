using System.Collections;
using System.Xml;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 
    /// </summary>
    public class MagicAttack_Particle : MonoBehaviour
    {
        // 필드
        [SerializeField]
        //private float magicDamage = 10f;
        private Actor attacker;
        private ParticleSystem magic;
        //public Damagable.DamageMessage DamageData { get; private set; }

        // 라이프 사이클
        private void Start()
        {
            magic = GetComponent<ParticleSystem>();

            StartCoroutine(Timer_Col(0.6f));

            if (!magic.isPlaying)
                magic.Play();
        }

        public void SetAttacker(Actor actor)
        {
            attacker = actor;
            Vector3 hitDirection = attacker.transform.forward;

            // 데미지 데이터 가공
            /*DamageData = new()
            {
                damager = this,
                owner = attacker,
                amount = (int)magicDamage,
                direction = hitDirection.normalized,
                damageSource = attacker.transform.position,
                throwing = true,
                stopCamera = false
            };*/
        }

        IEnumerator Timer_Col(float t)
        {
            var col = magic.collision;
            col.collidesWith |= (1 << LayerMask.GetMask("Actor"));

            yield return new WaitForSeconds(t);
            col.collidesWith &= ~(1 << LayerMask.GetMask("Actor"));
        }
    }
}
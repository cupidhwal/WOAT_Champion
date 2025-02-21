using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seti
{
    /// <summary>
    /// HealthBar 기능 클래스
    /// </summary>
    public class HealthBar : Singleton<HealthBar>
    {
        // 필드
        #region Variables
        // UI
        private Player player;
        private Slider healthSlider;
        private TextMeshProUGUI healthText;

        // 컴포넌트
        //private Damagable damagable;

        // 값
        private readonly float fillSharpness = 10f;
        [SerializeField]
        private float fillDuration = 0.3f;
        private float currentRate;

        // 기타
        private IEnumerator fillCor;
        #endregion


        private void Start()
        {
            player = FindAnyObjectByType<Player>();
            healthSlider = GetComponentInChildren<Slider>();
            healthText = GetComponentInChildren<TextMeshProUGUI>();

            /*damagable = player.GetComponent<Damagable>();
            damagable.OnReceiveDamage += UpdateHealth;
            damagable.OnResetDamage += UpdateHealth;
            damagable.OnRevive += UpdateHealthInvoke;*/

            UpdateHealth();
        }

        private void UpdateHealthInvoke() => Invoke("UpdateHealth", 0.1f);

        public void UpdateHealth()
        {
            /*float goalRate = damagable.CurrentHitRate;
            healthText.text = damagable.CurrentHitPoints.ToString() + " / " + damagable.MaxHitPoint.ToString();*/

            // SmoothHealth
            //UpdateSmoothHealth(goalRate);
        }

        void UpdateSmoothHealth(float goalRate)
        {
            if (fillCor != null)
            {
                StopCoroutine(fillCor);
                fillCor = null;
            }

            fillCor = SmoothHealth(goalRate);
            StartCoroutine(fillCor);
        }

        IEnumerator SmoothHealth(float goalRate)
        {
            // 연출
            float timeStamp = Time.time;
            while (timeStamp + fillDuration > Time.time)
            {
                currentRate = Mathf.Lerp(currentRate, goalRate, fillSharpness * Time.deltaTime);
                healthSlider.value = currentRate;
                yield return null;
            }

            // 마감
            currentRate = goalRate;
            currentRate = Mathf.Clamp01(currentRate);
            healthSlider.value = goalRate;

            // 후처리
            fillCor = null;
            yield break;
        }
    }
}
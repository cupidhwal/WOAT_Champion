using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seti
{
    public class Scenario_Test : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Elements : Test")]
        [SerializeField]
        private Image background;
        [SerializeField]
        private TextMeshProUGUI testText;
        #endregion

        public void Test(string text)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            StopAllCoroutines();
            StartCoroutine(TypingSentence(text));
        }

        // 텍스트 타이핑 연출
        IEnumerator TypingSentence(string text)
        {
            if (background.color.a < 1)
            {
                yield return StartCoroutine(Fade(Switch.On, 0.1f));
            }

            testText.text = "";

            foreach (char letter in text)
            {
                testText.text += letter;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(Fade(Switch.Off, 0.5f));

            testText.text = "";

            gameObject.SetActive(false);

            yield break;
        }

        IEnumerator Fade(Switch flag, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float alpha = flag switch
                {
                    Switch.On => t,
                    Switch.Off => 1f - t,
                    _ => 0f
                };

                ColorUtility.SetAlpha(background, alpha);
                ColorUtility.SetAlpha(testText, alpha);

                yield return null;
            }
        }

        private enum Switch
        {
            On,
            Off
        }
    }
}
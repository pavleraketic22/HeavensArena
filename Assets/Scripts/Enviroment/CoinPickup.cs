using UnityEngine;

namespace Enviroment
{
    public class CoinPickup : MonoBehaviour
    {
        public int coinValue = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Music.Instance.PlaySFX("Gems",0.7f);
                Stats stats = other.GetComponent<Stats>();
                if (stats != null)
                {
                    stats.AddCoins(coinValue);
                    Debug.Log($"Player picked up {coinValue} coins! Total: {stats.Coins}");
                }
                Destroy(gameObject);
            }
        }
    }

}
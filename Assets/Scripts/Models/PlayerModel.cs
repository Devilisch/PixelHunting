using System;

namespace Models
{
    public class PlayerModel
    {
        private const int MAX_HEALTH = 10;

        private int _health = MAX_HEALTH;
        private Action<PlayerModel> _updateAction;

        public int Health
        {
            get => _health;

            private set
            {
                if (value < 0)
                    return;

                _health = Math.Max(0, value);
            
                _updateAction?.Invoke(this);
            }
        }




        public void OnDamage(int value)
        {
            if (value > 0 && Health > 0)
                Health -= value;
        }

        public void OnSpawn() => Health = MAX_HEALTH;
    
        public void AddListener(Action<PlayerModel> action) => _updateAction += action;
        public void RemoveListener(Action<PlayerModel> action) => _updateAction -= action;
    }
}

namespace TextGameAttempt
{
    public class Enemy
    {
        public string name;
        private int health = 3;

        public int Health
        {
            get 
            { 
                return health; 
            }
            set 
            {
                health = value;
            }
        }
    }
}
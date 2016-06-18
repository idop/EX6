namespace Ex6GameLogic
{
    public class PlayerInfo
    {
        private string m_Name;
        private int m_Score;

        public PlayerInfo(string i_PlayerName)
        {
            m_Name = i_PlayerName.Equals(string.Empty) ? "Unnamed" : i_PlayerName;
            m_Score = 0;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public override string ToString()
        {
            return m_Name + ": " + m_Score;
        }
    }
}

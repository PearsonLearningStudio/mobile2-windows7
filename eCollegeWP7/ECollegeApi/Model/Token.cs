using System;

namespace ECollegeAPI.Model
{
    public class Token
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public DateTime Expires { get; set; }
        public DateTime CreatedAt { get; set; }

        public Token()
        {
            CreatedAt = DateTime.Now;
        }

        public bool NeedsToBeRefreshed()
        {
            if (CreatedAt.AddSeconds(ExpiresIn) < DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}

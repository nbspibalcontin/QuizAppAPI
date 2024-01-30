﻿namespace QuizApp.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<QuizAnswer> Answers { get; set; }
        public virtual QuizScore Score { get; set; }
    }
}

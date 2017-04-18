using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstBot.Models
{
    public enum Feedback
    {
        Happy,
        Sad
    }
    [Serializable]
    public class UserFeedBack
    {
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Message {  get; set; }

        public Feedback? Feedback { get; set; }


        public static IForm<UserFeedBack> BuildForm()
        {
            return new FormBuilder<UserFeedBack>()
                             .Message("Tell me your opinion, Thank you")
                             .Build();
        }
    }
}
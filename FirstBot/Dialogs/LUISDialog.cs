using FirstBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FirstBot.Dialogs
{
    [LuisModel("appID", "appKey")]
    [Serializable]
    public class LUISDialog : LuisDialog<UserFeedBack>
    {
        private readonly BuildFormDelegate<UserFeedBack> UserFB;

        public LUISDialog(BuildFormDelegate<UserFeedBack> UserFB)
        {
            this.UserFB = UserFB;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context,LuisResult result)
        {
            await context.PostAsync($" I'm sorry but i don't understand command {result.Query} what do you mean.\ncan you try something else!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Welcoming(IDialogContext context, LuisResult result)
        {
            context.Call(new WelcomingDialog(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        //TODO: Try this by using Text Analytics API
        [LuisIntent("Sadness")]
        public async Task SadUser(IDialogContext context, LuisResult result)
        {
            context.UserData.TryGetValue("Name", out string name);
            //معلش
            await context.PostAsync($"Ma3lesh ya {name}");
            context.Wait(MessageReceived);
        }

        //TODO: Try this by using Text Analytics API
        [LuisIntent("Loneliness")]
        public async Task AloneUser(IDialogContext context, LuisResult result)
        {
            context.UserData.TryGetValue("Name", out string name);
            await context.PostAsync($"Forever alone: {name}");
            context.Wait(MessageReceived);
        }

        [LuisIntent("GetFeedback")]
        public async Task Feedback(IDialogContext context, LuisResult result)
        {
            var form = new FormDialog<UserFeedBack>(new UserFeedBack(), this.UserFB, FormOptions.PromptInStart);
            context.Call(form, FormCallback);

        }

        private async Task FormCallback(IDialogContext context, IAwaitable<UserFeedBack> result)
        {
            var feedback = await result as UserFeedBack;
            var message = $@"
                    Message from : {feedback.Name},
                    Message content: {feedback.Feedback}
                    Email : {feedback.Email}
                ";
            //TODO Send Message
            await context.PostAsync("Thank you for your opnion");
            await context.PostAsync("Do you need any thing else");
            context.Wait(MessageReceived);

        }

        [LuisIntent("Ending")]
        public async Task EndConv(IDialogContext context, LuisResult result)
        {
            context.UserData.TryGetValue<string>("Name", out string name);

            await context.PostAsync($"Thank you for using Ahmads bot: {name}");

        }



    }
}
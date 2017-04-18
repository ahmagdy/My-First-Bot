using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FirstBot.Dialogs
{
    [Serializable]
    public class WelcomingDialog:IDialog<object>
    {
        
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi i'm Ahmads First Bot");
            await Respond(context);
            context.Wait(MessageRecivedAsync);

        }

        public async Task MessageRecivedAsync(IDialogContext context,IAwaitable<IMessageActivity> args)
        {
            var message = await args as Activity;
            
            context.UserData.TryGetValue("Name", out string name);
            context.UserData.TryGetValue("GetName", out bool getName);
            if (getName)
            {
                name = message.Text;
                context.UserData.SetValue("Name", name);
                context.UserData.SetValue("GetName", false);
            }
            await Respond(context);
            context.Done(message);
        }

        private static async Task Respond(IDialogContext context)
        {
            context.UserData.TryGetValue<string>("Name", out string name);
            if (string.IsNullOrEmpty(name))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue("GetName", true);
            }
            else
            {
                await context.PostAsync($"Hi {name}, how do you feel today?");
            }
        }

    }
}
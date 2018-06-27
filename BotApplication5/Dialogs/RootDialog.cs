using System;
using System.Threading.Tasks;
//using Goodreads;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
namespace BotApplication5.Dialogs
{

    [LuisModel("2f0a49fb-a9a3-4203-9185-c3e7eeeee50f", "c272eab449364677a01c67a6d038190a")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
       
        [LuisIntent("BookSearch")]
        public async Task Search(IDialogContext context, IAwaitable<object> result,LuisResult res)
        {

           // string message = $"yeah!!!, understood .";
           const string ApiKey = "MdkOmbDoxa2vczCWSRJIw";
            const string ApiSecret = "s8JXg023iZaezn9oIpkPWWl5ThQux1HznNOSuX9OLU";
            var client = Goodreads.GoodreadsClient.Create(ApiKey, ApiSecret);
            var activity = await result as Activity;

            // Calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // Goodreads.Models.Response.Book book = await client.Books.GetByBookId(bookId: 15979976);
            Goodreads.Models.Response.Book book = await client.Books.GetByTitle(activity.Text);

            // Get a list of groups by search keyword.
            var groups = await client.Groups.GetGroups(search: "Arts");
            Attachment attachment = new Attachment();
            attachment.ContentType = "image/jpg";
            attachment.ContentUrl = book.ImageUrl;
            var mes = context.MakeMessage();
            mes.Attachments.Add(attachment);
            // Return our reply to the user
            await context.PostAsync($"{book.Title}");
            await context.PostAsync(mes);
            await context.PostAsync($"Rating:{book.AverageRating}");
            // await context.PostAsync($"You sent {activity.Text} which was {length} characters and {book.Title}");
            context.Wait(this.MessageReceived);



            //  await context.PostAsync(message);

            //   context.Wait(MessageReceivedAsync);
        }
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry,Nothing is found...Try another book";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }
        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            string message = $"Hello...Try asking me about the books";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }


        /* public Task StartAsync(IDialogContext context)
         {
             context.Wait(MessageReceivedAsync);

             return Task.CompletedTask;
         }

         private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
         {
          const string ApiKey = "MdkOmbDoxa2vczCWSRJIw";
          const string ApiSecret = "s8JXg023iZaezn9oIpkPWWl5ThQux1HznNOSuX9OLU";
             var client = Goodreads.GoodreadsClient.Create(ApiKey, ApiSecret);

             var activity = await result as Activity;

             // Calculate something for us to return
             int length = (activity.Text ?? string.Empty).Length;
             // Goodreads.Models.Response.Book book = await client.Books.GetByBookId(bookId: 15979976);
             Goodreads.Models.Response.Book book=  await client.Books.GetByTitle(activity.Text);
             // Get a list of groups by search keyword.
             var groups = await client.Groups.GetGroups(search: "Arts");
             Attachment attachment = new Attachment();
             attachment.ContentType = "image/jpg";
             attachment.ContentUrl = book.ImageUrl;
             var message = context.MakeMessage();
             message.Attachments.Add(attachment);
             // Return our reply to the user
             await context.PostAsync($"{book.Title}");
             await context.PostAsync(message);
             await context.PostAsync($"Rating:{book.AverageRating}");
             // await context.PostAsync($"You sent {activity.Text} which was {length} characters and {book.Title}");
             context.Wait(MessageReceivedAsync);
         }*/
    }
    
}
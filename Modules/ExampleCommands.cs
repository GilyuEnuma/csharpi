using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace csharpi.Modules
{
    // for commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class ExampleCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;
            
            // build out the reply
            sb.AppendLine($"Hello [{user.Username}], nice to meet you.");
            sb.AppendLine(" /bow");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }

        [Command("ping")]
        public async Task PingCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;
            
            // build out the reply
            sb.AppendLine("... pong!");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }

        [Command("8ball")]
        [Alias("ask")]
        //[RequireUserPermission(GuildPermission.KickMembers)]
        public async Task AskEightBall([Remainder]string args = null)
        {
            // I like using StringBuilder to build out the reply
            var sb = new StringBuilder();
            // let's use an embed for this one!
            var embed = new EmbedBuilder();

            // now to create a list of possible replies
            var replies = new List<string>();

            // add our possible replies
            replies.Add("I mean, sure");
            replies.Add("Ha ha ha, no.");
            replies.Add("Ehhhh, who knows.");
            replies.Add("......... :-D");

            // time to add some options to the embed (like color and title)
            embed.WithColor(new Color(0, 255,0));
            embed.Title = "Hom-chan casts Divination!";
            
            // we can get lots of information from the Context that is passed into the commands
            // here I'm setting up the preface with the user's name and a comma
            sb.AppendLine($"{Context.User.Username} dared ask a question...");
            sb.AppendLine();

            // let's make sure the supplied question isn't null 
            if (args == null)
            {
                // if no question is asked (args are null), reply with the below text
                sb.AppendLine("I can't answer a question you didn't ask...");
            }
            else 
            {
                // if we have a question, let's give an answer!
                // get a random number to index our list with (arrays start at zero so we subtract 1 from the count)
                var answer = replies[new Random().Next(replies.Count)];
                
                // build out our reply with the handy StringBuilder
                sb.AppendLine($"You asked: [**{args}**]...");
                sb.AppendLine();
                sb.AppendLine($"...and my answer shall be [**{answer}**]");

                // bonus - let's switch out the reply and change the color based on it
                switch (answer) 
                {
                    case "I mean, sure":
                    {
                        embed.WithColor(new Color(0, 255, 0));
                        break;
                    }
                    case "Ha ha ha, no.":
                    {
                        embed.WithColor(new Color(255, 0, 0));
                        break;
                    }
                    case "Ehhhh, who knows.":
                    {
                        embed.WithColor(new Color(255,255,0));
                        break;
                    }
                    case "......... :-D":
                    {
                        embed.WithColor(new Color(255,0,255));
                        break;
                    }
                }
            }

            // now we can assign the description of the embed to the contents of the StringBuilder we created
            embed.Description = sb.ToString();

            // this will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
    }
}

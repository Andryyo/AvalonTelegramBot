using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Avalon.Web.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly ITelegramBotClient client;
        private readonly ICallbackQueryHandler callbackQueryHandler;

        public UserInteractionService(
            ITelegramBotClient client,
            ICallbackQueryHandler callbackQueryHandler)
        {
            this.client = client;
            this.callbackQueryHandler = callbackQueryHandler;
        }

        public async Task<QuestCard> SelectQuestCard(long id)
        {
            var tcs = new TaskCompletionSource<QuestCard>();
            Message message = null;

            EventHandler<CallbackQueryEventArgs> handler = (s, e) =>
            {
                if (message.MessageId == e.MessageId)
                {
                    tcs.TrySetResult((QuestCard)Enum.Parse(typeof(QuestCard), e.Data));
                }
            };

            try
            {
                callbackQueryHandler.CallbackQuery += handler;

                message = await client.SendTextMessageAsync(
                    chatId: new Telegram.Bot.Types.ChatId(id),
                    text: "Please select quest card",
                    replyMarkup: new InlineKeyboardMarkup(
                        new InlineKeyboardButton[] {
                        new InlineKeyboardButton() { Text = "Success", CallbackData = QuestCard.MissionSuccess.ToString() },
                        new InlineKeyboardButton() { Text = "Failed", CallbackData = QuestCard.MissionFailed.ToString() } }));

                var result = await tcs.Task;

                await client.EditMessageReplyMarkupAsync(new ChatId(message.Chat.Id), message.MessageId);

                return result;
            }
            finally
            {
                callbackQueryHandler.CallbackQuery -= handler;
            }
        }

        public async Task<IEnumerable<IUser>> SelectUsers(long id, IEnumerable<IUser> users, int count)
        {
            var tcs = new TaskCompletionSource<IEnumerable<IUser>>();
            Message message = null;
            var selectedUsers = new HashSet<IUser>();

            EventHandler<CallbackQueryEventArgs> handler = (s, e) =>
            {
                if (message.MessageId == e.MessageId)
                {
                    selectedUsers.Add(users.FirstOrDefault(x => x.Id.ToString() == e.Data));

                    client
                        .EditMessageReplyMarkupAsync(
                            new ChatId(message.Chat.Id),
                            message.MessageId,
                            new InlineKeyboardMarkup(
                                users
                                    .Where(x => !selectedUsers.Contains(x))
                                    .Select(x => new InlineKeyboardButton[] { new InlineKeyboardButton() { Text = x.Name, CallbackData = x.Id.ToString() } })))
                        .Wait();

                    if (selectedUsers.Count == count)
                    {
                        tcs.TrySetResult(selectedUsers);
                    }
                }
            };

            try
            {
                callbackQueryHandler.CallbackQuery += handler;

                message = await client.SendTextMessageAsync(
                    chatId: new Telegram.Bot.Types.ChatId(id),
                    text: string.Format("Please select {0}", count),
                    replyMarkup: new InlineKeyboardMarkup(
                        users.Select(x => new InlineKeyboardButton[] { new InlineKeyboardButton() { Text = x.Name, CallbackData = x.Id.ToString() } })));

                var result = await tcs.Task;

                await client.EditMessageReplyMarkupAsync(new ChatId(message.Chat.Id), message.MessageId);

                return result;
            }
            finally
            {
                callbackQueryHandler.CallbackQuery -= handler;
            }
        }

        public async Task<VoteToken> SelectVoteToken(long id)
        {
            var tcs = new TaskCompletionSource<VoteToken>();
            Message message = null;

            EventHandler<CallbackQueryEventArgs> handler = (s, e) =>
            {
                if (message.MessageId == e.MessageId)
                {
                    tcs.TrySetResult((VoteToken)Enum.Parse(typeof(VoteToken), e.Data));
                }
            };

            try
            {
                callbackQueryHandler.CallbackQuery += handler;

                message = await client.SendTextMessageAsync(
                    chatId: new Telegram.Bot.Types.ChatId(id),
                    text: "Please vote for team",
                    replyMarkup: new InlineKeyboardMarkup(
                        new InlineKeyboardButton[] {
                        new InlineKeyboardButton() { Text = "Approve", CallbackData = VoteToken.VoteApproved.ToString() },
                        new InlineKeyboardButton() { Text = "Reject", CallbackData = VoteToken.VoteRejected.ToString() } }));

                var result = await tcs.Task;

                await client.EditMessageReplyMarkupAsync(new ChatId(message.Chat.Id), message.MessageId);

                return result;
            }
            finally
            {
                callbackQueryHandler.CallbackQuery -= handler;
            }
        }

        public async Task SendMessage(long id, string message)
        {
            await Task.Delay(2000);

            await client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(id), message);
        }
    }
}

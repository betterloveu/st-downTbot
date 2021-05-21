using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot1
{
    class Program
    {
        private static ITelegramBotClient tClient;
        static void Main(string[] args)
        {
            tClient = new TelegramBotClient(Conf.token);

            var me = tClient.GetMeAsync().Result;
            tClient.OnMessage += Bot_OnMessage;
            tClient.StartReceiving();

            Console.ReadKey();

            tClient.StopReceiving();
        }


        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var mesT = e.Message;
            if (mesT.Text != null)
            {
                Console.WriteLine($"Сообщение: {mesT.Text}" );
                await tClient.SendTextMessageAsync(mesT.Chat.Id, mesT.Text, replyMarkup: GetButtons(), replyToMessageId: mesT.MessageId);
                //switch (mesT.Text)
                //{
                //    case "Картинка":
                //            var pic = await tClient.SendPhotoAsync(
                //                chatId: mesT.Chat.Id, photo: "https://tgram.ru/wiki/stickers/img/Uwik_stikers/png/1.png", replyToMessageId: mesT.MessageId, replyMarkup: GetButtons());
                //            break;
                //    default: await tClient.SendTextMessageAsync(mesT.Chat.Id, "", replyMarkup: GetButtons());
                //        break;
                //}

            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Привет"}, new KeyboardButton {Text = "Пока"} },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Как дела?" }, new KeyboardButton {Text = "Что делаешь?" } }
                }
            };
        }
    }
    
}


using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var bot = new TelegramBotClient("7316461983:AAEdUuTCLUP-d5cXRYPGtvvls5lMTvWvYB8");

using var cts = new CancellationTokenSource();

// Змінні для підрахунку балів
int TransportTechnologiesScore = 0;
int TourismScore = 0;
int HotelBusinessScore = 0;
int CyberSecurityScore = 0;
int ComputerScienceScore = 0;
int SoftwareEngineeringScore = 0;

// Метод для обробки апдейтів
async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    switch (update.Type)
    {
        case UpdateType.Message when update.Message!.Text != null:
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            // Виводимо в консоль інформацію про те, що написав користувач
            Console.WriteLine($"Користувач ({chatId}) написав: {messageText}");

            switch (messageText)
            {
                case "/start":
                    await SendMainMenuAsync(botClient, chatId, cancellationToken);
                    break;

                case "Почати тестування":
                    await AskFirstQuestion(botClient, chatId, cancellationToken);
                    break;

                case "На головне меню":
                    await SendMainMenuAsync(botClient, chatId, cancellationToken);
                    break;

                case "Детальніше про спеціальність":
                    await SendMainMenuAsync(botClient, chatId, cancellationToken);
                    break;

                default:
                    await botClient.SendTextMessageAsync(chatId, "Невідома команда. Спробуйте ще раз.", cancellationToken: cancellationToken);
                    break;
            }
            break;

        case UpdateType.CallbackQuery when update.CallbackQuery != null:
            var callbackQuery = update.CallbackQuery;
            var callbackChatId = callbackQuery.Message.Chat.Id;

            // Виводимо в консоль інформацію про callback-запит
            Console.WriteLine($"Користувач ({callbackChatId}) вибрав: {callbackQuery.Data}");

            switch (callbackQuery.Data)
            {
                // Кнопка "Почати тестування"
                case "start_test":
                    // Обнулення балів перед початком нового тесту
                    TransportTechnologiesScore = 0;
                    TourismScore = 0;
                    HotelBusinessScore = 0;
                    CyberSecurityScore = 0;
                    ComputerScienceScore = 0;
                    SoftwareEngineeringScore = 0;

                    await AskFirstQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Кнопка "На головне меню"
                case "main_menu":
                    await botClient.SendTextMessageAsync(callbackChatId, "Завантаження головного меню... Зачекайте.", cancellationToken: cancellationToken);
                    // Гіфка завантаження
                    await bot.SendVideoAsync(callbackChatId, "https://media.tenor.com/jfmI0j5FcpAAAAAM/loading-wtf.gif");
                    break;

                // Кнопка "Детальніше"
                case "more_info":
                    await botClient.SendTextMessageAsync(callbackChatId, "Завантаження сторінки детальної інформації... Зачекайте.", cancellationToken: cancellationToken);
                    break;
            
                // Питання 1
                case "question_1_agree":
                    TransportTechnologiesScore++;
                    await AskSecondQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_1_hard_to_answer":
                    await AskSecondQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_1_disagree":
                    TransportTechnologiesScore--;
                    await AskSecondQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 2
                case "question_2_agree":
                    TourismScore++;
                    await AskThirdQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_2_hard_to_answer":
                    await AskThirdQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_2_disagree":
                    TourismScore--;
                    await AskThirdQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 3
                case "question_3_agree":
                    HotelBusinessScore++;
                    await AskFourthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_3_hard_to_answer":
                    await AskFourthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_3_disagree":
                    HotelBusinessScore--;
                    await AskFourthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 4
                case "question_4_agree":
                    CyberSecurityScore++;
                    await AskFifthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_4_hard_to_answer":
                    await AskFifthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_4_disagree":
                    CyberSecurityScore--;
                    await AskFifthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 5
                case "question_5_agree":
                    ComputerScienceScore++;
                    await AskSixthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_5_hard_to_answer":
                    await AskSixthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_5_disagree":
                    ComputerScienceScore--;
                    await AskSixthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 6
                case "question_6_agree":
                    SoftwareEngineeringScore++;
                    await AskSeventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_6_hard_to_answer":
                    await AskSeventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_6_disagree":
                    SoftwareEngineeringScore--;
                    await AskSeventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 7
                case "question_7_agree":
                    TransportTechnologiesScore++;
                    await AskEighthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_7_hard_to_answer":
                    await AskEighthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_7_disagree":
                    TransportTechnologiesScore--;
                    await AskEighthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 8
                case "question_8_agree":
                    TourismScore++;
                    await AskNinthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_8_hard_to_answer":
                    await AskNinthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_8_disagree":
                    TourismScore--;
                    await AskNinthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 9
                case "question_9_agree":
                    HotelBusinessScore++;
                    await AskTenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_9_hard_to_answer":
                    await AskTenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_9_disagree":
                    HotelBusinessScore--;
                    await AskTenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 10
                case "question_10_agree":
                    CyberSecurityScore++;
                    await AskEleventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_10_hard_to_answer":
                    await AskEleventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_10_disagree":
                    CyberSecurityScore--;
                    await AskEleventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 11
                case "question_11_agree":
                    ComputerScienceScore++;
                    await AskTwelfthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_11_hard_to_answer":
                    await AskTwelfthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_11_disagree":
                    ComputerScienceScore--;
                    await AskTwelfthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 12
                case "question_12_agree":
                    SoftwareEngineeringScore++;
                    await AskThirteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_12_hard_to_answer":
                    await AskThirteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_12_disagree":
                    SoftwareEngineeringScore--;
                    await AskThirteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 13
                case "question_13_agree":
                    TransportTechnologiesScore++;
                    await AskFourteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_13_hard_to_answer":
                    await AskFourteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_13_disagree":
                    TransportTechnologiesScore--;
                    await AskFourteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 14
                case "question_14_agree":
                    TourismScore++;
                    await AskFifteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_14_hard_to_answer":
                    await AskFifteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_14_disagree":
                    TourismScore--;
                    await AskFifteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 15
                case "question_15_agree":
                    HotelBusinessScore++;
                    await AskSixteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_15_hard_to_answer":
                    await AskSixteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_15_disagree":
                    HotelBusinessScore--;
                    await AskSixteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 16
                case "question_16_agree":
                    CyberSecurityScore++;
                    await AskSeventeenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_16_hard_to_answer":
                    await AskSeventeenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_16_disagree":
                    CyberSecurityScore--;
                    await AskSeventeenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 17
                case "question_17_agree":
                    ComputerScienceScore++;
                    await AskEighteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_17_hard_to_answer":
                    await AskEighteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_17_disagree":
                    ComputerScienceScore--;
                    await AskEighteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 18
                case "question_18_agree":
                    SoftwareEngineeringScore++;
                    await AskNineteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_18_hard_to_answer":
                    await AskNineteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_18_disagree":
                    SoftwareEngineeringScore--;
                    await AskNineteenthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 19
                case "question_19_agree":
                    TransportTechnologiesScore++;
                    await AskTwentiethQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_19_hard_to_answer":
                    await AskTwentiethQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_19_disagree":
                    TransportTechnologiesScore--;
                    await AskTwentiethQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 20
                case "question_20_agree":
                    TourismScore++;
                    await AskTwentyFirstQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_20_hard_to_answer":
                    await AskTwentyFirstQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_20_disagree":
                    TourismScore--;
                    await AskTwentyFirstQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 21
                case "question_21_agree":
                    HotelBusinessScore++;
                    await AskTwentySecondQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_21_hard_to_answer":
                    await AskTwentySecondQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_21_disagree":
                    HotelBusinessScore--;
                    await AskTwentySecondQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 22
                case "question_22_agree":
                    CyberSecurityScore++;
                    await AskTwentyThirdQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_22_hard_to_answer":
                    await AskTwentyThirdQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_22_disagree":
                    CyberSecurityScore--;
                    await AskTwentyThirdQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 23
                case "question_23_agree":
                    ComputerScienceScore++;
                    await AskTwentyFourthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_23_hard_to_answer":
                    await AskTwentyFourthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_23_disagree":
                    ComputerScienceScore--;
                    await AskTwentyFourthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 24
                case "question_24_agree":
                    SoftwareEngineeringScore++;
                    await AskTwentyFifthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_24_hard_to_answer":
                    await AskTwentyFifthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_24_disagree":
                    SoftwareEngineeringScore--;
                    await AskTwentyFifthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 25
                case "question_25_agree":
                    TransportTechnologiesScore++;
                    await AskTwentySixthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_25_hard_to_answer":
                    await AskTwentySixthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_25_disagree":
                    TransportTechnologiesScore--;
                    await AskTwentySixthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 26
                case "question_26_agree":
                    TourismScore++;
                    await AskTwentySeventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_26_hard_to_answer":
                    await AskTwentySeventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_26_disagree":
                    TourismScore--;
                    await AskTwentySeventhQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 27
                case "question_27_agree":
                    HotelBusinessScore++;
                    await AskTwentyEighthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_27_hard_to_answer":
                    await AskTwentyEighthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_27_disagree":
                    HotelBusinessScore--;
                    await AskTwentyEighthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 28
                case "question_28_agree":
                    CyberSecurityScore++;
                    await AskTwentyNinthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_28_hard_to_answer":
                    await AskTwentyNinthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_28_disagree":
                    CyberSecurityScore--;
                    await AskTwentyNinthQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 29
                case "question_29_agree":
                    ComputerScienceScore++;
                    await AskThirtiethQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_29_hard_to_answer":
                    await AskThirtiethQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_29_disagree":
                    ComputerScienceScore--;
                    await AskThirtiethQuestion(botClient, callbackChatId, cancellationToken);
                    break;

                // Питання 30
                case "question_30_agree":
                    SoftwareEngineeringScore++;
                    await ShowFinishTestButton(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_30_hard_to_answer":
                    await ShowFinishTestButton(botClient, callbackChatId, cancellationToken);
                    break;

                case "question_30_disagree":
                    SoftwareEngineeringScore--;
                    await ShowFinishTestButton(botClient, callbackChatId, cancellationToken);
                    break;

                // Показати результат
                case "finish_test":
                    await ShowResults(botClient, callbackChatId, cancellationToken);
                    break;

                // Виведення помилки
                default:
                    await botClient.SendTextMessageAsync(callbackChatId, "Невідома команда. Спробуйте ще раз.", cancellationToken: cancellationToken);
                    break;
            }
            break;
    }
}

// Метод для підрахунку результатів та відображення спеціальності
async Task ShowResults(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    string resultMessage;

    // Клавіатура для випадків, коли спеціальність визначена (з трьома кнопками)
    InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new[]
   {
        new []
        {
            InlineKeyboardButton.WithCallbackData("Детальніше про спеціальність", "more_info"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("На головне меню", "main_menu"),
            InlineKeyboardButton.WithCallbackData("Пройти тест заново", "start_test")
        }
    });

    // Клавіатура для випадків, коли спеціальність невизначена (з двома кнопками)
    InlineKeyboardMarkup undefinedSpecialtyKeyboard = new InlineKeyboardMarkup(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData("Пройти тест заново", "start_test"),
            InlineKeyboardButton.WithCallbackData("На головне меню", "main_menu")
        }
    });

    int maxScore = new[] { TransportTechnologiesScore, TourismScore, HotelBusinessScore, CyberSecurityScore, ComputerScienceScore, SoftwareEngineeringScore }.Max();

    switch (maxScore)
    {
        case var score when score == TransportTechnologiesScore:
            // Відправка зображення разом з текстом
            await botClient.SendPhotoAsync(
                chatId,
                "https://drive.google.com/uc?export=view&id=1LtzphHgDpwaP5sSDc_7lf25srwGBWkKB", // Посилання на картинку
                caption: "Вітаю! Твій результат — спеціальність 275 Транспортні технології на факультеті Інноваційних технологій.\r\n\r\nЦя спеціальність відкриває можливості для тих, хто цікавиться організацією і вдосконаленням автомобільних перевезень, оптимізацією транспортних систем і логістики. Ти зможеш розвивати сучасні транспортні технології, займатися автоматизацією процесів, а також брати участь у плануванні та проєктуванні транспортних мереж.\r\n\r\nЯкщо тобі подобається працювати з технологіями, аналітикою та прагнеш зробити транспортну систему більш ефективною — цей напрямок точно для тебе!\r\n\r\n📈 Після навчання ти зможеш працювати у сфері логістики, транспортного менеджменту та навіть стати частиною команди, яка змінює майбутнє міських і міжміських перевезень.\r\n\r\nГотовий зробити перший крок? 🚀",
                parseMode: ParseMode.Html, // Використання HTML у тексті
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
            break;
       
        case var score when score == TourismScore:
            // Відправка зображення разом з текстом
            await botClient.SendPhotoAsync(
                chatId,
                "https://drive.google.com/uc?export=view&id=1LtzphHgDpwaP5sSDc_7lf25srwGBWkKB", // Посилання на картинку
                caption: "Вітаю! Твій результат — спеціальність 242 Туризм на факультеті Інноваційних технологій.\r\n\r\nЦя спеціальність пропонує захопливі можливості для тих, хто любить подорожувати і прагне створювати незабутні враження для інших. Ти зможеш досліджувати та розробляти туристичні маршрути, організовувати екскурсії, планувати подорожі та вдосконалювати туристичний сервіс.\r\n\r\nЯкщо тебе захоплює світ, різні культури та комунікація з людьми — це твій напрямок! 🌍\r\n\r\n📅 Після навчання ти зможеш працювати в туристичних агентствах, готелях, на курортах або навіть організовувати власні тури.\r\n\r\nГотовий зробити перший крок до захоплюючої кар'єри? 🚀",
                parseMode: ParseMode.Html, // Використання HTML у тексті
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
            break;
        
        case var score when score == HotelBusinessScore:
            // Відправка зображення разом з текстом
            await botClient.SendPhotoAsync(
                chatId,
                "https://drive.google.com/uc?export=view&id=1LtzphHgDpwaP5sSDc_7lf25srwGBWkKB", // Посилання на картинку
                caption: "Вітаю! Твій результат — спеціальність 241 Готельно-ресторанна справа на факультеті Інноваційних технологій.\r\n\r\nЦя спеціальність пропонує чудові можливості для тих, хто прагне створювати комфорт та забезпечувати високий рівень обслуговування в готелях і ресторанах. Ти зможеш займатися управлінням готелями, організацією ресторанного сервісу, розробкою нових концепцій і вдосконаленням клієнтського досвіду.\r\n\r\nЯкщо тебе захоплює індустрія гостинності, і ти прагнеш працювати у сфері, де важлива увага до деталей і високий стандарт обслуговування — це твій шлях! 🏨🍽️\r\n\r\n📈 Після навчання ти зможеш знайти себе у готельному бізнесі, ресторанах, курортах або навіть започаткувати власний бізнес у цій галузі.\r\n\r\nГотовий зробити перший крок до кар'єри в сфері гостинності? 🚀",
                parseMode: ParseMode.Html, // Використання HTML у тексті
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
            break;
        
        case var score when score == CyberSecurityScore:
            // Відправка зображення разом з текстом
            await botClient.SendPhotoAsync(
                chatId,
                "https://drive.google.com/uc?export=view&id=1LtzphHgDpwaP5sSDc_7lf25srwGBWkKB", // Посилання на картинку
                caption: "Вітаю! Твій результат — спеціальність 125 Кібербезпека на факультеті Інноваційних технологій.\r\n\r\nЦя спеціальність відкриває захопливі можливості для тих, хто цікавиться захистом інформації та боротьбою з кіберзагрозами. Ти зможеш займатися забезпеченням безпеки комп'ютерних систем, захистом даних від атак і злому, а також розробкою стратегій для протидії кіберзлочинності.\r\n\r\nЯкщо тебе захоплює техніка, аналітика і ти прагнеш забезпечити безпеку інформаційних систем — це твій шлях! 💻🔒\r\n\r\n🛡️ Після навчання ти зможеш працювати в сфері кібербезпеки, у компаніях, що спеціалізуються на захисті інформації, а також у державних структурах та консалтингових агенціях.\r\n\r\nГотовий зробити перший крок до кар'єри у світі кібербезпеки? 🚀",
                parseMode: ParseMode.Html, // Використання HTML у тексті
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
            break;
        
        case var score when score == ComputerScienceScore:
            // Відправка зображення разом з текстом
            await botClient.SendPhotoAsync(
                chatId,
                "https://drive.google.com/uc?export=view&id=1LtzphHgDpwaP5sSDc_7lf25srwGBWkKB", // Посилання на картинку
                caption: "Вітаю! Твій результат — спеціальність 122 Комп’ютерні науки на факультеті Інноваційних технологій.\r\n\r\nЦя спеціальність відкриває перед тобою безмежні можливості в світі IT і технологій. Ти зможеш займатися програмуванням, розробкою програмного забезпечення, створенням алгоритмів і вирішенням складних технічних задач.\r\n\r\nЯкщо тебе захоплює світи кодування, новітні технології та розробка інноваційних рішень — це твій шлях! 💻🚀\r\n\r\n🔧 Після навчання ти зможеш працювати розробником програмного забезпечення, аналітиком даних, спеціалістом з системного адміністрування або навіть започаткувати власний IT-проєкт.\r\n\r\nГотовий зробити перший крок до кар'єри в комп'ютерних науках? 🚀",
                parseMode: ParseMode.Html, // Використання HTML у тексті
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
            break;
        
        case var score when score == SoftwareEngineeringScore:
            // Відправка зображення разом з текстом
            await botClient.SendPhotoAsync(
                chatId,
                "https://drive.google.com/uc?export=view&id=1LtzphHgDpwaP5sSDc_7lf25srwGBWkKB", // Посилання на картинку
                caption: "Вітаю! Твій результат — спеціальність 121 Інженерія програмного забезпечення на факультеті Інноваційних технологій.\r\n\r\nЦя спеціальність відкриває перед тобою можливості для створення та вдосконалення програмного забезпечення. Ти зможеш займатися проектуванням, розробкою та тестуванням програмних продуктів, а також забезпеченням їхньої якості та ефективності.\r\n\r\nЯкщо ти захоплюєшся розробкою програмного забезпечення, інженерією систем і прагнеш реалізовувати складні технічні рішення — це твій шлях! 💻🔧\r\n\r\n🚀 Після навчання ти зможеш працювати програмістом, системним аналітиком, інженером програмного забезпечення або в IT-консалтингу, а також започаткувати власний IT-бізнес.\r\n\r\nГотовий зробити перший крок до кар'єри в інженерії програмного забезпечення? 🚀",
                parseMode: ParseMode.Html, // Використання HTML у тексті
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
            break;
        
        default:
            resultMessage = "Вітаю! Твій результат показує кілька спеціальностей, які можуть бути цікавими для тебе. Це може бути ознакою того, що ти маєш різноманітні інтереси та здібності.\r\n\r\nРекомендую пройти тест ще раз, щоб отримати більш точний результат і краще зрозуміти, яка спеціальність найбільше підходить саме тобі. Це допоможе знайти напрямок, який найкраще відповідає твоїм інтересам і кар’єрним цілям.\r\n\r\nГотовий спробувати ще раз? 🚀";
            await botClient.SendTextMessageAsync(chatId, resultMessage, cancellationToken: cancellationToken, replyMarkup: undefinedSpecialtyKeyboard);
            break;
    }

    // Виведення поточних балів
    Console.WriteLine("Нараховані бали");
    Console.WriteLine($"Транспортні технології: {TransportTechnologiesScore}");
    Console.WriteLine($"Туризм: {TourismScore}");
    Console.WriteLine($"Готельно-ресторанна справа: {HotelBusinessScore}");
    Console.WriteLine($"Кібербезпека: {CyberSecurityScore}");
    Console.WriteLine($"Комп’ютерні науки: {ComputerScienceScore}");
    Console.WriteLine($"Інженерія програмного забезпечення: {SoftwareEngineeringScore}");
}

// Питання 1
async Task AskFirstQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_1_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_1_hard_to_answer"),
        },
        new[]
        {
           
            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_1_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "1. Я б хотів(ла) навчитися приймати швидкі рішення в умовах високої напруги.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 2
async Task AskSecondQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_2_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_2_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_2_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "2. Мені цікаво дізнаватися про різні культури та традиції.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 3
async Task AskThirdQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_3_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_3_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_3_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "3. Я вмію організовувати роботу інших, щоб вона проходила злагоджено та без збоїв.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 4
async Task AskFourthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_4_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_4_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_4_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "4. Я хотів(ла) би дізнатися більше про те, як забезпечити безпеку важливої інформації.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 5
async Task AskFifthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_5_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_5_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_5_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "5. Мене захоплюють мови програмування на комп’ютері.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 6
async Task AskSixthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_6_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_6_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_6_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "6. Мені цікаво вивчати, як працюють різні операційні системи та їх особливості.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 7
async Task AskSeventhQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_7_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_7_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_7_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "7. Мені цікаво вивчати мови для спілкування з людьми з різних країн у професійній сфері.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 8
async Task AskEighthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_8_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_8_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_8_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "8. Мені було б цікаво організовувати заходи, які об’єднують людей з різних куточків світу.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 9
async Task AskNinthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_9_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_9_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_9_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "9. Мене цікавить робота, пов’язана з обслуговуванням клієнтів.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 10
async Task AskTenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_10_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_10_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_10_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "10. Мені цікаво обговорювати різні способи захисту даних у цифровому світі.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 11
async Task AskEleventhQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_11_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_11_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_11_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "11. Мені цікаво створювати графіку і досліджувати 3D-моделювання.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 12
async Task AskTwelfthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_12_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_12_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_12_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "12. Мені цікаво обговорювати різні підходи до розробки програмних рішень.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 13
async Task AskThirteenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_13_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_13_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_13_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "13. Мені цікаво вивчати різні аспекти організації роботи в динамічних умовах.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 14
async Task AskFourteenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_14_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_14_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_14_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "14. Я хотів(ла) би працювати в туристичному агентстві, підбираючи цікаві маршрути для клієнтів.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 15
async Task AskFifteenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_15_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_15_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_15_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "15. Я хотів(ла) б дізнатися більше про кулінарні традиції різних культур.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 16
async Task AskSixteenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_16_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_16_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_16_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "16. Я хотів(ла) б дізнатися більше про кіберзагрози і методи їхнього подолання.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 17
async Task AskSeventeenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_17_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_17_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_17_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "17. Я люблю працювати з великими обсягами даних та займатися їх аналізом.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 18
async Task AskEighteenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_18_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_18_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_18_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "18. Мені цікаво займатися тестуванням та покращенням комп’ютерних програм.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 19
async Task AskNineteenthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
     {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_19_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_19_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_19_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "19. Мені подобається розглядати різноманітні підходи до вирішення комплексних задач.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 20
async Task AskTwentiethQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
     {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_20_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_20_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_20_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "20. Я люблю знаходити нові цікаві місця для відпочинку.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 21
async Task AskTwentyFirstQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_21_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_21_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_21_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "21. Я прагну працювати в динамічному середовищі.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 22
async Task AskTwentySecondQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_22_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_22_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_22_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "22. Мене цікавлять питання безпеки у сучасному світі.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 23
async Task AskTwentyThirdQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_23_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_23_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_23_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "23. Мені цікаво розробляти ігри та вивчати їхні механіки.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 24
async Task AskTwentyFourthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_24_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_24_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_24_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "24. Мені дуже подобається вирішувати математичні задачі та головоломки.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 25
async Task AskTwentyFifthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_25_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_25_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_25_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "25. Я хотів(ла) б досліджувати вплив технологій на безпеку дорожнього руху.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 26
async Task AskTwentySixthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_26_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_26_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_26_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "26. Я хотів(ла) б мати можливість спілкуватися з людьми з різних країн і обмінюватися досвідом.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 27
async Task AskTwentySeventhQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_27_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_27_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_27_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "27. Я б хотів(ла) спробувати оформляти постояльців готелю, організовувати злагоджену роботу обслуговуючого персоналу.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 28
async Task AskTwentyEighthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_28_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_28_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_28_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "28. Я прагну дізнатися більше про те, як технології впливають на наше повсякдення.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 29
async Task AskTwentyNinthQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_29_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_29_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_29_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "29. Я хочу навчитися створювати інтернет-сайти, веб-сторінки.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Питання 30
async Task AskThirtiethQuestion(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("✅ Згоден", "question_30_agree"),
        },
        new[]
        {
             InlineKeyboardButton.WithCallbackData("🤔 Важко відповісти", "question_30_hard_to_answer"),
        },
        new[]
        {

            InlineKeyboardButton.WithCallbackData("❌ Не згоден", "question_30_disagree")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "30. Мені подобається вивчати алгоритми на комп’ютері та писати програми.",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Кнопка "Завершити тестування"
async Task ShowFinishTestButton(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Дізнатися результат", "finish_test")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "Тестування завершено! 🎉 Ти на правильному шляху до вибору спеціальності, яка відповідає твоїм інтересам і здібностям.\r\n\r\nЩоб дізнатися свій результат та отримати рекомендації, натисни кнопку «Дізнатися результат» 🚀",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Основне меню
async Task SendMainMenuAsync(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
{
    var inlineKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Почати тестування", "start_test"),
            InlineKeyboardButton.WithCallbackData("На головне меню", "main_menu")
        }
    });

    await botClient.SendTextMessageAsync(
        chatId,
        "Готовий дізнатися, яка спеціальність найкраще підходить тобі? Тест допоможе розібратися у твоїх інтересах та здібностях, щоб знайти ідеальний напрямок для майбутньої професії. 🎯\r\n\r\nПроходь профорієнтаційний тест, який допоможе підібрати спеціальність, що пасує саме тобі. Тест складається з 30 легких запитань — просто обери «Згоден», «Важко відповісти» або «Не згоден». Твої відповіді допоможуть знайти напрямок, у якому тобі буде комфортно розвиватися.\r\n\r\nПісля проходження тесту натисни «Дізнатися результат», і ти отримаєш корисні поради щодо вибору спеціальності. Тисни «Почати тестування» і давай розпочнемо! 🚀",
        replyMarkup: inlineKeyboard,
        cancellationToken: cancellationToken
    );
}

// Обробка помилок
Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine($"Помилка: {exception.Message}");
    return Task.CompletedTask;
}

// Отримуємо інформацію про бота
var me = await bot.GetMeAsync();
Console.WriteLine($"Бот @{me.Username} запущено... Натисніть будь-яку клавішу для завершення.");

// Налаштування опцій для отримання апдейтів
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

// Запускаємо бота
bot.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token
);

// Очікування завершення роботи
Console.ReadKey();
cts.Cancel();
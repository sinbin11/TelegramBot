using System;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public class User : CommandManager
    {
        public static CommandManager Manager = new User();

        public User()
        {
            Config.Current.Commands.Add("sudo", "Yes sir!");
            Config.Current.Commands.Add("ultimate question of life the universe and everything", "Unauthorized");
            Config.Current.Commands.Add("sudo ultimate question of life the universe and everything", "42");
            Config.Current.Commands.Add("hayat evren ve her şeye dair nihai sorunun cevabı", "Yetkiniz yok");
            Config.Current.Commands.Add("sudo hayat evren ve her şeye dair nihai sorunun cevabı", "42");
            Config.Current.Commands.Add("merhaba", "Merhaba ?user?");
            Config.Current.Commands.Add("selam", "Selam ?user?");
            Config.Current.Commands.Add("nasılsın", "Depresyondayım, sen?");
            Config.Current.Commands.Add("naber", "İyilik senden?");
            Config.Current.Commands.Add("hey", "Yo!");
            Config.Current.Commands.Add("windows severmisin", "Seni dışarı alalım ?user?");
            Config.Current.Commands.Add("napıyon", "kali kuruyorum, hack yapcam");

            Actions.Add("hack", MatrixHack);
        }

        private Message MatrixHack(Message message)
        {
            return message.Reply(Matrix[new Random(DateTime.Now.Millisecond).Next(0, 8)]);
        }

        public override Message Run(Message message)
        {
            if (Config.UserMessages.Count > Config.MaxHistory - 1)
            {
                Config.UserMessages.RemoveAt(0);
                return Run(message);
            }

            Config.UserMessages.Add(message);

            return base.Run(message);
        }

        public string[] Matrix = {
            "Ne yazık ki Matrix'in ne olduğunu kimse anlatamaz, onu kendin görmek zorundasın.",
            "Gerçek dünyaya hoşgeldin.",
            "Hiç gerçek olduğundan emin olduğun bir rüya gördün mü? Ya bu rüyadan hiç uyanamasaydın o zaman gerçek dünya ile rüya arasındaki farkı nasıl ayırt ederdin?",
            "Ne olduğunu düşünme. Ne olduğunu bil!",
            "Bu açıklanamaz, ama hissedersin. Hayatın boyunca dünyayla ilgili bazı şeylerin yanlış olduğunu hissetmişsindir. Ne olduğunu bilmezsin, ama o oradadır; beynine saplanmış bir kıymık parçası gibi...Seni deli eder...",
            "Gerçeği nasıl tanımlarsın? Eğer hissedebildiğin şeylerden bahsediyorsan, koklayabildiğin, tadabildiğin ve görebildiğin, o zaman gerçek, basitçe beynine iletilen elektronik sinyallerdir.",
            "Matrix bir sistemdir, Neo.Bu sistem bizim düşmanımız. Ama sistemin içindeyken ne görüyorsun? İş adamları, öğretmenler, avukatlar, marangozlar. Kurtarmaya çalıştığımız insanların zihinleri. Ama biz başarana kadar, bu insanlar da sistemin bir parçası ve bu da onları düşmanlarımız yapıyor. Şunu anlamalısın: Bu insanların çoğu serbest bırakılmaya hazır değil. Ve büyük bir kısmı o kadar içine girmişler, sisteme o kadar bağımlı hale gelmişler ki, onu korumak için savaşabilirler... Beni dinliyor musun, yoksa kırmızı elbiseli kadına mı bakıyorsun? Tekrar bak. Dondur! Matrix'de değil miyiz? Bu sana bir şeyi öğretmek için dizayn edilmiş bir program. Eğer bizden biri değilsen, onlardan birisindir.",
            "Bildiğin yol ile yürüdüğün yol arasında bir fark var.",
            "Yolu bilmek ile o yolda yürümek farklı şeyler."
        };
    }
}
//reference System.dll
//reference System.Core.dll
using MCGalaxy;
using MCGalaxy.Events;
using System;
using System.Linq;

namespace OfflineActions
{
    public class OfflineActions : Plugin
    {
        public override string name { get { return "Offline Actions"; } }
        public override string creator { get { return "DexrnZacAttack"; } }
        public override string MCGalaxy_Version { get { return "1.9.5.1"; } }

        public override void Load(bool auto)
        {
            OnModActionEvent.Register(HandlePlayerCommand, Priority.Low);
        }

        private void HandlePlayerCommand(ModAction action)
        {
            string user = action.Target;
            string reason = action.Reason;

            if (PlayerInfo.Online.Items.Any(player => player.name == action.Target))
                return;

            switch (action.Type)
            {
                case ModActionType.Warned:
                    Command.Find("send").Use(Player.Console, string.Format("{0} You were &Wwarned&S by {1}&S for %b{2}&S on &T{3}&S", action.Target, action.Actor.ColoredName, action.Reason, DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    break;
                case ModActionType.Muted:
                    Command.Find("send").Use(Player.Console, string.Format("{0} You were &Wmuted&S by {1}&S for %b{2}&S on &T{3}&S for &T{4}&S", action.Target, action.Actor.ColoredName, action.Reason, DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"), action.Duration.Shorten()));
                    break;
                case ModActionType.Unmuted:
                    Command.Find("send").Use(Player.Console, string.Format("{0} You were &Tunmuted&S by {1}&S for %b{2}&S on &T{3}&S", action.Target, action.Actor.ColoredName, action.Reason, DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    break;
                default:
                    break;
            }
        }

        public override void Unload(bool auto)
        {
            OnModActionEvent.Unregister(HandlePlayerCommand);
        }
    }
}

using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using InventorySystem.Items.Usables.Scp330;
using PlayerRoles;

namespace PinkCandyReturns
{
    public class PinkCandyReturns : Plugin<Config>
    {
        public override string Author => "gben5692";
        public override Version Version => new Version(1, 0, 2);

        private static readonly Random Random = new Random();

        public override void OnEnabled()
        {
            base.OnEnabled();
            Exiled.Events.Handlers.Player.ItemAdded += OnItemAdded;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.ItemAdded -= OnItemAdded;
            base.OnDisabled();
        }

        private void OnItemAdded(ItemAddedEventArgs ev)
        {
            if (ev.Item is Exiled.API.Features.Items.Scp330 scp330)
            {
                // Guaranteed pink candy if chance is 100%
                if (Random.NextDouble() < Config.PinkCandyChance)
                {
                    scp330.AddCandy(CandyKindID.Pink);
                    Log.Info($"{ev.Player.Nickname} received SCP-330 with a Pink Candy!");
                }
            }
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AddCandyCommand : ICommand
    {
        public string Command => "addcandy";
        public string[] Aliases => new[] { "givecandy" };
        public string Description => "Gives a pink candy to a player.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("pinkcandy.give"))
            {
                response = "You don't have permission to use this command.";
                return false;
            }

            if (arguments.Count != 2 || arguments.At(1).ToLower() != "pink")
            {
                response = "Usage: addcandy {username} pink";
                return false;
            }

            string targetName = arguments.At(0);
            Player target = Player.List.FirstOrDefault(p => p.Nickname.Equals(targetName, StringComparison.OrdinalIgnoreCase));

            if (target == null)
            {
                response = $"Player '{targetName}' not found.";
                return false;
            }

            if (target.Role.Type == RoleTypeId.None)
            {
                response = $"Player '{target.Nickname}' is not in the game.";
                return false;
            }

            var item = target.AddItem(ItemType.SCP330);
            if (item is Scp330 scp330)
            {
                scp330.AddCandy(CandyKindID.Pink);
                response = $"Given Pink Candy to {target.Nickname}.";
                return true;
            }

            response = "Failed to give candy.";
            return false;
        }
    }

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float PinkCandyChance { get; set; } = 0.1f; // 10% chance
    }
}

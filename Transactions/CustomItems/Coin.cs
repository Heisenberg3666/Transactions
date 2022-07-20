using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using Transactions.API;
using UnityEngine;

namespace Transactions.CustomItems
{
    [CustomItem(ItemType.Coin)]
    public class Coin : CustomItem
    {
        private static Dictionary<GameObject, int> _values = new Dictionary<GameObject, int>();

        public override uint Id { get; set; } = 100;
        public override string Name { get; set; } = "Coin";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = .75f;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public void ChangeCoinWorth(Pickup pickup, int coinWorth) => _values[pickup.GameObject] = coinWorth;

        public Pickup Spawn(Player player, int coinWorth, Player previousOwner = null)
        {
            Pickup item = Spawn(player, previousOwner);
            _values.Add(item.GameObject, coinWorth);

            return item;
        }

        protected override void OnPickingUp(PickingUpItemEventArgs e)
        {
            if (!Check(e.Pickup))
                return;

            int coinWorth = _values[e.Pickup.GameObject];

            e.IsAllowed = false;

            if (TransactionsApi.PlayerExists(e.Player))
            {
                TransactionsApi.AddMoney(e.Player, coinWorth);
                e.Pickup.Destroy();
            }
            else
            {
                e.Player.ShowHint("You cannot pickup this coin (custom item), disable DNT and try again.");
            }

            base.OnPickingUp(e);
        }
    }
}

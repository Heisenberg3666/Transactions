using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;

namespace Transactions.API.CustomItems
{
    [CustomItem(ItemType.Coin)]
    internal class Coin : CustomItem
    {
        public override uint Id { get; set; } = 100;
        public override string Name { get; set; } = "Coin";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = .75f;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public int Value = 1;

        protected override void OnPickingUp(PickingUpItemEventArgs e)
        {
            if (!Check(e.Pickup))
                return;

            e.IsAllowed = false;

            if (TransactionsApi.PlayerExists(e.Player))
            {
                TransactionsApi.AddPoints(e.Player, Value);
                e.Pickup.Destroy();
            }
            else
            {
                e.Player.ShowHint("You cannot pickup these points (custom item), disable DNT and try again.");
            }

            base.OnPickingUp(e);
        }
    }
}

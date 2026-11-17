using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using global_events;

namespace model.entity
{
    public class EnemyBoss : Enemy
    {
        public override void TakeAction()
        {
            base.TakeAction();
        }
        protected override void Die()
        {
            base.Die();
            GlobalEvents.RaiseEnemyBossDied(this);
        }
    }
}
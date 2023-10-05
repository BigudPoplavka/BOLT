using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IWeapon
    {
        public string Name { get; set; }
        bool CanAttack();
        void Attack();
    }
}

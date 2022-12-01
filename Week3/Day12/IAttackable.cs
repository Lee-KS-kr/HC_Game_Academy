using System;
using UnityEngine;

namespace Mizu
{
    public delegate void HpDelegate(int hp);
    public delegate void DieDelegate();

    public interface IAttackable
    {
        public int HP { set; get; }
        public HpDelegate hpDelegate { get; set; }
        public DieDelegate onDieDelegate { get; set; }

        public void OnHit(int dmg, Vector3 pos) { }
        public void OnDie() { }
    }
}
using System;
using System.Collections.Generic;

namespace Day3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public enum ItemType
    {
        Dummy = 0,
        Weapon,
        Skin
    }

    public struct ItemInfo
    {
        public ItemType _itemType;
        public string _itemName;
        public string _rarity;
        public object _thumbnail; // 유니티에서 작업시 image로 바꿀 것.
        public int _itemLevel;
        public int _itemCost;
        public bool _isSold;
    }

    public delegate void ItemSoldEvent(ItemInfo info);

    #region Shop
    abstract class BaseShop
    {
        protected virtual void Init()
        {

        }

        protected bool PlayAds()
        {
            // 광고를 실행하는 코드
            bool isSuccessful = false;
            return isSuccessful;
        }

        protected bool HaveEnoughMoney()
        {
            // 돈이 충분히 있는지를 확인하는 코드
            bool isEnough = true;
            return isEnough;
        }

        protected virtual void ShowItem()
        {
            // 아이템 표시
            // foreach로 한바퀴 돌면서 isSold가 true면 SoldOut표시, false면 일반 표기
            // 리스트 내부의 모든 아이템들의 isSold가 true이면 dummy 표시
        }

        protected virtual void SellItem()
        {
            // base.HaveEnoughMoney로 돈이 충분한지 확인
            // 돈이 충분하지 않으면 base.PlayAd()
            // 판매된 아이템의 isSold를 true로 변환
            // ItemSold Event 뿌리기
        }

        protected abstract void InputItems();
    } // 데이터를 추상화하여 파는 아이템만 바꾸면 될 것 같음.

    class WeaponShop : BaseShop
    {
        private List<ItemInfo> sellItemInfo = new List<ItemInfo>(); // 딕셔너리로 할까?

        protected override void Init() { }

        protected override void InputItems()
        {
            // sellItem list에 판매할 아이템들을 추가하는 코드 구현
        }

        protected override void ShowItem() { }

        protected override void SellItem() { }
    }

    class SkinShop : BaseShop
    {
        private List<ItemInfo> sellItemInfo = new List<ItemInfo>(); // 딕셔너리로 할까?
        event ItemSoldEvent SkinSoldMsg;

        protected override void Init() { }
        protected override void InputItems() { }
        protected override void ShowItem() { }
        protected override void SellItem() { }
    }
    #endregion Shop

    #region Item
    abstract class BaseItem
    {
        protected bool CheckBought(ItemInfo info)
        {
            // 이미 구매한 아이템인지 확인하는 코드
            return false;
        }

        protected abstract void GetItem();
        protected abstract void OnClicked();
        protected virtual void DisplayItem()
        {
            // 구매한 아이템들을 전시하는 코드
        }
        
    }

    class Weapon : BaseItem
    {
        private List<ItemInfo> boughtItem = new List<ItemInfo>();

        protected override void GetItem()
        {
            // 구독한 이벤트를 통해 구매 아이템 리스트에 아이템을 넣는 코드
        }

        protected override void DisplayItem() { }

        protected override void OnClicked()
        {
            // base.CheckBought()를 활용하여 구매 여부를 확인
            // 구매했으면 장착버튼, 미구매면 구매 버튼 출력
        }
    }

    class Skin : BaseItem
    {
        private List<ItemInfo> boughtItem = new List<ItemInfo>();

        protected override void GetItem() { }

        protected override void DisplayItem() { }

        protected override void OnClicked() { }
    }
    #endregion Item
}

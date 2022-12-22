
public interface IPurchaseAble {
    
    int Price{ get; }
    bool IsShopItem { get; set; }

    void PurchaseCallBack();
}
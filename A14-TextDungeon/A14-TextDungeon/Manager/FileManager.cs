using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static A14_TextDungeon.Item;
namespace A14_TextDungeon
{
    public class FileManager
    {
        //public static string path = 사용자의 A14_TextDungeon >  A14_TextDungeon > A14_TextDungeon > bin > Debug  > net 위치에 생성됨

        public string path = AppDomain.CurrentDomain.BaseDirectory;
        public void SaveData()
        {
            string userData = JsonConvert.SerializeObject(Manager.Instance.gameManager.user);
            File.WriteAllText(path + "\\UserData.json", userData);

            string inventoryData = JsonConvert.SerializeObject(Manager.Instance.inventoryManager.items);
            File.WriteAllText(path + "\\UserInventoryData.json", inventoryData);

            string storedata = JsonConvert.SerializeObject(Manager.Instance.shopManager.products);
            File.WriteAllText(path + "\\StoreItemData.json", storedata);

            // 퀘스트 구현 후 연동 - 진행 중인 퀘스트 정보도 저장해야함
        }

        public void LoadData()
        {
            // 유저 데이터가 없을 때 -> 세이브 데이터가 없을 때
            if (!File.Exists(path + "\\UserData.json"))
            {
                Manager.Instance.userDataManager.SetName();
                Manager.Instance.gameManager.Init();
                SaveData();
                Manager.Instance.gameManager.village.ShowVillage(); 
                return;
            }
            else
            {
                // 유저 데이터 Load
                string userLData = File.ReadAllText(path + "\\UserData.json");
                User userLoadData = JsonConvert.DeserializeObject<User>(userLData);
                Manager.Instance.gameManager.user = userLoadData;

                // 인벤토리 데이터 Load
                string inventoryLData = File.ReadAllText(path + "\\UserInventoryData.json");
                List<Item> inventoryLoadData = JsonConvert.DeserializeObject<List<Item>>(inventoryLData);
                if(Manager.Instance.inventoryManager.items!= null)
                {
                    Manager.Instance.inventoryManager.ClearInventory();
                }
                foreach (Item item in inventoryLoadData)
                {
                    if (item != null)
                    {
                        Manager.Instance.inventoryManager.items.Add(item);
                    }
                }

                //상점 데이터 Load
                string storeLData = File.ReadAllText(path + "\\StoreItemData.json");
                List<ShopProduct> storeLoadData = JsonConvert.DeserializeObject<List<ShopProduct>>(storeLData);
                if (Manager.Instance.shopManager.products != null)
                {
                    Manager.Instance.shopManager.ClearShop();
                }
                foreach (ShopProduct shopitem in storeLoadData)
                {
                    if (shopitem != null)
                    {
                        Manager.Instance.shopManager.AddProduct(shopitem);
                    }
                }

                //퀘스트 구현 후 연동

            }
        }

        // Game Over 시 데이터 리셋 & LoadData()
        public void ResetData()
        {
            File.Delete(path + "\\UserData.json");
            File.Delete(path + "\\UserInventoryData.json");
            File.Delete(path + "\\StoreItemData.json");
            Manager.Instance.shopManager.ClearShop();
            Manager.Instance.inventoryManager.ClearInventory();
            //퀘스트 구현 후 연동

            LoadData();
        }
    }
}

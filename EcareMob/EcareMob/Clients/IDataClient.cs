using System.Collections.Generic;
using System.Threading.Tasks;
using EcareMob.Models;


namespace EcareMob.Clients
{
    public interface IDataClient
    {
        Task<User> GetAuthentication(User user);
        Task<User> GetUserInfo(int id);
        Task<UserProfile> GetUserProfile(int id);
        Task<GenericResponse> RegisterNewUser(RegisterModel registerModel);
        Task<List<Contract>> GetMyContracts(string charismaCode, string vat);
        Task<List<Models.History>> GetHistory(string vat);

        //Task<List<OpenInvetory>> GetOpenInventories();
        //Task<BarcodeStatus> CheckBarCode(string barcode);
        //Task<BarcodeInventoryModel> InsertBarCodeToInventory(int id, BarcodeToInvetoryParameter barcodeobj);
        //Task<BarcodeCreateReturnModel> CreateBarcode(BarcodeModel barcodeobj);
        //Task<List<IvItemSizeZoomModel>> GetIvItemSizes(string itemcode);
        //Task<List<IvItemColorZoomModel>> GetIvItemColors(string itemcode);
        //Task<ReturnModel<IvItem>> GetIvItems(string searchterm, int page, int limit);
        //Task<List<IvUnitZoomModel>> GetIvUnits();
        //Task<ItemflagViewModel> GetIvItemsFlag(string code);
        //Task<ReturnModel<InventoryScannedView>> GetScannedItems(int ivphid, string searchterm, int page, int limit);
        //Task<UpdateScannedReturnModel> UpdateScannedQuantity(int id, ScannedQuantityViewModel quantityViewModel);
        //Task<InsertManuallyReturnModel> InsertManuallyBarcode(int invphyid ,InsertManualParam barcodeobj);
        //Task DeleteScannedItem(int id);
    }
}

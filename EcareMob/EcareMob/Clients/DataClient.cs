using System.Collections.Generic;
using System.Threading.Tasks;
using EcareMob.Clients.Base;
using EcareMob.Helpers;
using EcareMob.Models;
using EcareMob.Views;


namespace EcareMob.Clients
{
    public class DataClient : IDataClient
    {
        
        private IRequestProvider _requestprovider;

        public DataClient(IRequestProvider requestProvider)
        {
            _requestprovider = requestProvider;
        }


        

        public async Task<User> GetAuthentication(User user)
        {
            var uri = $"{Settings.ServerUrl}user/authenticate";
            return await _requestprovider.PostAuthRequest<User,User>(uri,user);
        }



        public async Task<User> GetUserInfo(int id)
        {
            var uri = $"{Settings.ServerUrl}user/userinfo?id={id}";
            return await _requestprovider.GetSingleItemRequest<User>(uri);
        }


        public async Task<UserProfile> GetUserProfile(int id)
        {
            var uri = $"{Settings.ServerUrl}main/userprofile?userId={id}";
            return await _requestprovider.GetSingleItemRequest<UserProfile>(uri);
        }
        public async Task<ResourceReturnModel> GetResource(string key, string lang)
        {
            var uri = $"{Settings.ServerUrl}main/resource?key={key}&lang={lang}";
            return await _requestprovider.GetSingleItemRequest<ResourceReturnModel>(uri);
        }


        public async Task<GenericResponse> RegisterNewUser(RegisterModel registerModel)
        {
            var uri = $"{Settings.ServerUrl}user/register";
            return await _requestprovider.PostRequest<GenericResponse, RegisterModel>(uri, registerModel);
        }

        public async Task<GenericResponse> ChangePassword(ChangePasswordModel model)
        {
            var uri = $"{Settings.ServerUrl}user/changepassword";
            return await _requestprovider.PostRequest<GenericResponse, ChangePasswordModel>(uri, model);
        }


        public async Task<List<Models.History>> GetHistory(string vat)
        {
            var uri = $"{Settings.ServerUrl}main/history?vat={vat}";
            return await _requestprovider.GetMultipleItemsRequest<Models.History>(uri);
        }

        public async Task<List<Contract>> GetMyContracts(string charismaCode , string vat)
        {
            var uri = $"{Settings.ServerUrl}main/contracts?charismaCode={charismaCode}&vat={vat}";
            return await _requestprovider.GetMultipleItemsRequest<Contract>(uri);
        }





        //public async Task<List<OpenInvetory>> GetOpenInventories()
        //{
        //    var uri = $"{Settings.ServerUrl}api/inventories";
        //    return await _requestprovider.GetMultipleItemsRequest<OpenInvetory>(uri);
        //}

        //public async Task<BarcodeStatus> CheckBarCode(string barcode)
        //{
        //    var uri = $"{Settings.ServerUrl}api/barcode/check?code={barcode}";
        //    return await _requestprovider.GetSingleItemRequest<BarcodeStatus>(uri);
        //}
        //public async Task<BarcodeInventoryModel> InsertBarCodeToInventory(int id, BarcodeToInvetoryParameter barcodeobj)
        //{
        //    var uri = $"{Settings.ServerUrl}api/inventories/{id}/barcode";
        //    return await _requestprovider.PostRequest<BarcodeInventoryModel, BarcodeToInvetoryParameter>(uri, barcodeobj);
        //}

        //public async Task<BarcodeCreateReturnModel> CreateBarcode(BarcodeModel barcodeobj)
        //{
        //    var uri = $"{Settings.ServerUrl}api/barcode/create";
        //    return await _requestprovider.PostRequest<BarcodeCreateReturnModel, BarcodeModel>(uri, barcodeobj);
        //}

        //public async Task<InsertManuallyReturnModel> InsertManuallyBarcode(int invphyid ,InsertManualParam barcodeobj)
        //{
        //    var uri = $"{Settings.ServerUrl}api/inventories/{invphyid}/scans";
        //    return await _requestprovider.PostRequest<InsertManuallyReturnModel, InsertManualParam>(uri, barcodeobj);
        //}

        //public async Task<List<IvUnitZoomModel>> GetIvUnits()
        //{
        //    var uri = $"{Settings.ServerUrl}api/zoom/ivunit";
        //    return await _requestprovider.GetMultipleItemsRequest<IvUnitZoomModel>(uri);
        //}

        //public async Task<ReturnModel<IvItem>> GetIvItems(string searchterm, int page, int limit)
        //{
        //    var uri = $"{Settings.ServerUrl}api/ivitems?searchterm={searchterm}&page={page}&limit={limit}";
        //    return await _requestprovider.GetSingleItemRequest<ReturnModel<IvItem>>(uri);
        //}

        //public async Task<List<IvItemColorZoomModel>> GetIvItemColors(string itemcode)
        //{
        //    var uri = $"{Settings.ServerUrl}api/zoom/ivitemcolor?code={itemcode}";
        //    return await _requestprovider.GetMultipleItemsRequest<IvItemColorZoomModel>(uri);
        //}

        //public async Task<List<IvItemSizeZoomModel>> GetIvItemSizes(string itemcode)
        //{
        //    var uri = $"{Settings.ServerUrl}api/zoom/ivitemsize?code={itemcode}";
        //    return await _requestprovider.GetMultipleItemsRequest<IvItemSizeZoomModel>(uri);
        //}

        //public async Task<ItemflagViewModel> GetIvItemsFlag(string code)
        //{
        //    var uri = $"{Settings.ServerUrl}api/ivitems/{code}/flag";
        //    return await _requestprovider.GetSingleItemRequest<ItemflagViewModel>(uri);
        //}

        //public async Task<ReturnModel<InventoryScannedView>> GetScannedItems(int ivphid, string searchterm, int page, int limit)
        //{
        //    var uri = $"{Settings.ServerUrl}api/inventories/{ivphid}/scans?searchterm={searchterm}&page={page}&limit={limit}";
        //    return await _requestprovider.GetSingleItemRequest<ReturnModel<InventoryScannedView>>(uri);
        //}

        //public async Task<UpdateScannedReturnModel> UpdateScannedQuantity(int id, ScannedQuantityViewModel quantityViewModel)
        //{
        //    var uri = $"{Settings.ServerUrl}api/inventories/scans/{id}";
        //    return await _requestprovider.PutRequest<ScannedQuantityViewModel, UpdateScannedReturnModel>(uri, quantityViewModel);
        //}

        //public async Task DeleteScannedItem(int id)
        //{
        //    var uri = $"{Settings.ServerUrl}api/inventories/scans/{id}";
        //    await _requestprovider.DeleteRequest(uri);
        //}

        //inventories/scans/{id}

    }
}

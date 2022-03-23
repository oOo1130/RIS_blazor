
//using RIS_blazor.Shared.Models;
//using RIS_blazor.Shared.Models.ApiModel;
//using RIS_blazor.Shared.Models.VWModels;
//using Microsoft.AspNetCore.Components;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace RIS_blazor.Client.Services
//{
//    public interface IAccountService
//    {
//        //User User { get; }
//        //Task Initialize();
//        Task Login(Login model);
//        Task Logout();
//        Task Register(AddUser model);
//    }

//    public class AccountService : IAccountService
//    {
//        private IHttpService _httpService;
//        private HttpClient Http;
//        private NavigationManager _navigationManager;
//        private ILocalStorageService _localStorageService;
//        private string _userKey = "user";

//        public User User { get; private set; }

//        public AccountService(
//            IHttpService httpService,
//            NavigationManager navigationManager,
//            ILocalStorageService localStorageService
//        )
//        {
//            _httpService = httpService;
//            _navigationManager = navigationManager;
//            _localStorageService = localStorageService;
//        }

//        public async Task Initialize()
//        {
//            User = await _localStorageService.GetItem<User>(_userKey);
//        }

//        public async Task Login(Login model)
//        {
//            User = await _httpService.Post<User>("/users/authenticate", model);
//            await _localStorageService.SetItem(_userKey, User);
//        }

//        public async Task Logout()
//        {
//            User = null;
//            await _localStorageService.RemoveItem(_userKey);
//            _navigationManager.NavigateTo("account/login");
//        }

//        public async Task Register(AddUser model)
//        {
//            await _httpService.Post("/register", model);
//        }
//    }
//}
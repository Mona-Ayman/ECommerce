using API.Controllers.Base;
using Application.Services.ChannelService;

namespace API.Controllers
{
    public class TestController : ApiControllerBase
    {
        private readonly IChannelUpdateRateService channelUpdateRateService;

        public TestController(IChannelUpdateRateService channelUpdateRateService)
        {
            this.channelUpdateRateService = channelUpdateRateService;
        }


        //#region Methods
        //[HttpGet]
        //public async Task<GlobalResponse<string>> Test()
        //{
        //    #region Using RowVersion

        //    //Product product = await productRepository.FindByIdIncludeRates(notification.ProductId) ?? throw new NotFoundException(Localizations.NotFound);

        //    //if (notification.OldRate != null)
        //    //    product.RemoveRate(notification.OldRate);

        //    //product.AddRate(notification.NewRate);

        //    //await unitOfWork.SaveAsync(); 

        //    #endregion

        //    #region Using Background Service

        //    await channelUpdateRateService.NotifyChannel(Guid.Parse("26ade35d-cded-4035-951b-08ebf3357531"), null, 1);
        //    return ReturnResponse(Localizations.Success);
        //    #endregion
        //}
        //#endregion












        //private readonly ECommerceContext context;
        //private readonly IMemoryCache memoryCache;

        ////private readonly ICacheService cacheService;

        //public TestController(ECommerceContext context, IMemoryCache memoryCache)
        //{
        //    this.context = context;
        //    this.memoryCache = memoryCache;
        //    //this.cacheService = cacheService;
        //}
        //[HttpGet]
        //public async Task<GlobalResponse<List<string>>> AddProducts()
        //{
        //    //List<string> data = cacheService.GetData<List<string>>("Products");
        //    List<string> data = memoryCache.Get<List<string>>(CachingCategory.Products);
        //    return ReturnResponse(data);
        //}

        //[HttpPost]
        //public async Task<GlobalResponse<string>> AddProducts(int count)
        //{
        //    var products = new List<Product>();
        //    var random = new Random();

        //    for (int i = 1; i <= count; i++)
        //    {
        //        string name = $"Product{i}";
        //        string description = $"This is the description for Product{i}";
        //        decimal price = random.Next(10, 1000); // Generate a random price between 10 and 1000

        //        var product = new Product(name, description, price);
        //        products.Add(product);

        //        // Save every 500 products to optimize performance
        //        if (i % 500 == 0)
        //        {
        //            await context.AddRangeAsync(products);
        //            await context.SaveChangesAsync();
        //            products.Clear(); // Clear the list after saving
        //        }
        //    }

        //    // Save any remaining products
        //    if (products.Any())
        //    {
        //        await context.AddRangeAsync(products);
        //        await context.SaveChangesAsync();
        //    }

        //    return ReturnResponse(Localizations.Success);
        //}


        //[HttpPost]
        //public async Task<GlobalResponse<string>> AddUsers(int count)
        //{
        //    var users = new List<User>();
        //    var passwordHasher = new PasswordHasher<User>();
        //    var random = new Random();

        //    for (int i = 1; i <= count; i++)
        //    {
        //        string userName = $"User{i}";
        //        string email = $"User{i}{random.Next(100, 999)}@gmail.com";
        //        string phone = $"{random.Next(1000000000, 1999999999)}";
        //        string fullName = $"User {i} {Guid.NewGuid().ToString().Substring(0, 4)}";
        //        Gender gender = (Gender)(random.Next(0, 2)); // 0 = Male, 1 = Female

        //        var user = new User(userName, email, phone, fullName, gender);
        //        //user.PasswordHash = passwordHasher.HashPassword(user, $"Password{i}!"); // Pass the user instance

        //        users.Add(user);

        //        // Save every 500 users
        //        if (i % 500 == 0)
        //        {
        //            await context.AddRangeAsync(users);
        //            await context.SaveChangesAsync();
        //            users.Clear(); // Clear the list after saving
        //        }
        //    }

        //    // Save any remaining users
        //    if (users.Any())
        //    {
        //        await context.AddRangeAsync(users);
        //        await context.SaveChangesAsync();
        //    }

        //    return ReturnResponse(Localizations.Success);
        //}

        //[HttpGet]
        //public async Task<GlobalResponse<string>> AddCartsForAllUsersAsync()
        //{
        //    var users = await context.Users.ToListAsync(); // Fetch all users
        //    var products = await context.Set<Product>().ToListAsync(); // Fetch all products
        //    var random = new Random();



        //    var sharedProduct = products.First(); // Pick one shared product (same for all carts)
        //    var carts = new List<Cart>();

        //    foreach (var user in users)
        //    {
        //        var cart = new Cart(user.Id);

        //        // Add the shared product (common across all carts)
        //        cart.AddItem(sharedProduct);

        //        // Pick 4 random products from the product list
        //        var randomProducts = products
        //            .Where(p => p.Id != sharedProduct.Id) // Exclude the shared product
        //            .OrderBy(_ => random.Next())
        //            .Take(4)
        //            .ToList();

        //        foreach (var product in randomProducts)
        //        {
        //            cart.AddItem(product);
        //        }

        //        carts.Add(cart);

        //        // Batch insert every 500 carts to optimize performance
        //        if (carts.Count % 500 == 0)
        //        {
        //            await context.AddRangeAsync(carts);
        //            await context.SaveChangesAsync();
        //            carts.Clear();
        //        }
        //    }

        //    // Save any remaining carts
        //    if (carts.Any())
        //    {
        //        await context.AddRangeAsync(carts);
        //        await context.SaveChangesAsync();
        //    }

        //    return ReturnResponse(Localizations.Success);
        //}

        //[HttpGet]
        //public void AddCartsForAllUsersAsync()
        //{
        //    context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('CartItems', RESEED, 0)");
        //}


    }

}


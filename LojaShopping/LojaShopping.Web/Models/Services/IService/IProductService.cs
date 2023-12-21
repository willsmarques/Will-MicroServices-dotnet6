namespace LojaShopping.Web.Models.Services.IService;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> FindAllProduct();

    Task<ProductModel> FindAllProductById(long id);

    Task<ProductModel> CreateProduct(ProductModel model);

    Task<ProductModel> UpdateProduct(ProductModel model);

    Task<bool> DeleteProductById(long id);

}

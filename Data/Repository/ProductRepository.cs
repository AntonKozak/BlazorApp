using BlazorApp.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Product> CreateAsync(Product obj)
    {
        await _db.Product.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var objFromDb = await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
        if (objFromDb != null)
        {
            // Delete the image file if it exists
            if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
            {
                DeleteProductImage(objFromDb.ImageUrl);
            }

            _db.Product.Remove(objFromDb);
            return await _db.SaveChangesAsync() > 0;
        }
        return false;
    }

    private void DeleteProductImage(string imageUrl)
    {
        try
        {
            var fileName = imageUrl.Replace("/images/product/", "");
            var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "product");
            var filePath = Path.Combine(directoryPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception)
        {
            // Silently handle file deletion errors
        }
    }

    public async Task<Product> GetAsync(int id)
    {
        var objFromDb = await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
        if (objFromDb == null)
        {
            throw new Exception("Product not found");
        }
        return objFromDb;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Product.Include(i => i.Category).ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product obj)
    {
        var objFromDb = await _db.Product.FirstOrDefaultAsync(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.ImageUrl = obj.ImageUrl;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Price = obj.Price;
            objFromDb.SpecialTag = obj.SpecialTag;

            _db.Update(objFromDb);
            await _db.SaveChangesAsync();
            return objFromDb;
        }
        return obj;
    }
}

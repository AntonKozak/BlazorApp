using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data.Repository.IRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Category> CreateAsync(Category obj)
    {
        await _db.Category.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var objFromDb = await _db.Category.FirstOrDefaultAsync(u => u.Id == id);
        if (objFromDb != null)
        {
            _db.Category.Remove(objFromDb);
            return _db.SaveChanges() > 0;
        }
        return false;
    }

    public async Task<Category> GetAsync(int id)
    {
        var objFromDb = await _db.Category.FirstOrDefaultAsync(u => u.Id == id);
        if (objFromDb == null)
        {
            throw new Exception("Category not found");
        }
        return objFromDb;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _db.Category.ToListAsync();
    }

    public async Task<Category> UpdateAsync(Category obj)
    {
        var objFromDb = await _db.Category.FirstOrDefaultAsync(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
            _db.Update(objFromDb);
            _db.SaveChanges();
            return objFromDb;
        }
        return obj;
    }
}

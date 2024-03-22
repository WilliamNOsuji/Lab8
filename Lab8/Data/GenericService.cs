using Lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Data
{
    public class GenericService<T> where T : class
    {
        protected readonly Lab8Context _context;

        public GenericService(Lab8Context context)
        {
            _context = context;
        }

        private bool IsContextNull()
        { return _context != null || _context.Set<T>() == null; }

        public async Task<IEnumerable<T>?> GetAll()
        {
            if (IsContextNull()) return null;

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> Get(int id)
        {
            if (IsContextNull()) return null;

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> Add(T t)
        {
            if (IsContextNull()) return null;

            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<T?> Delete(int id)
        {
            if (IsContextNull()) return null;

            var t = await Get(id);

            if (t == null) return null;

            _context.Set<T>().Remove(t);

            await _context.SaveChangesAsync();

            return t;
        }

        public async Task<T?> Edit(int id, T t)
        {
            if (IsContextNull()) return null;

            _context.Entry(t).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await Get(id)) == null) return null;
                else throw;
            }
            return t;
        }
    }
}

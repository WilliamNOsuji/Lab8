using Lab8.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Data
{
    public class AnimalService : GenericService<Animal>
    {
        protected readonly Lab8Context _context;

        public AnimalService(Lab8Context context) 
            : base(context)
        {
            _context = context;
        }

        private bool IsContextNull() 
        { return _context != null || _context.Animal == null; }

        public async Task<IEnumerable<Animal>?> GetAll()
        {
            if (IsContextNull()) return null;
            
            return await _context.Animal.ToListAsync();
        }

        public async Task<Animal?> Get(int id)
        {
            if (IsContextNull()) return null;

            return await _context.Animal.FindAsync(id);
        }

        public async Task<Animal?> Add(Animal animal)
        {
            if(IsContextNull()) return null;

            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal?> Delete(int id)
        {
            if (IsContextNull()) return null;

            Animal? animal = await Get(id);

            if (animal == null) return null;

            _context.Animal.Remove(animal);

            await _context.SaveChangesAsync();

            return animal;
        }

        public async Task<Animal?> Edit(int id, Animal animal)
        {
            if (IsContextNull()) return null;

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await Get(id)) == null) return null;
                else throw;
            }
            return animal;
        }

        public async Task ClearAllAnimals()
        {
            if (IsContextNull()) return;

            _context.Animal.RemoveRange(_context.Animal);

            await _context.SaveChangesAsync();
        }
    }
}

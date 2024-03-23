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

        public async Task ClearAllAnimals()
        {
            if (_context == null) return;

            _context.Animal.RemoveRange(_context.Animal);

            await _context.SaveChangesAsync();
        }
    }
}

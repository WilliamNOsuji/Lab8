using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab8.Data;
using Lab8.Models;
using NuGet.Protocol;

namespace Lab8.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly GenericService<Animal> _animalService;

        public AnimalsController(GenericService<Animal> animalService)
        {
            _animalService = animalService;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimal()
        {
            var animals = await _animalService.GetAll();
            if (animals == null)
            {
                return NotFound();
            }
            return Ok(animals);
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _animalService.Get(id);
            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            var updatedAnimal = await _animalService.Edit(id, animal);
            if (updatedAnimal == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            var addedAnimal = await _animalService.Add(animal);
            if (addedAnimal == null)
            {
                return Problem("Entity set 'Lab8Context.Animal'  is null.");
            }

            return CreatedAtAction("GetAnimal", new { id = addedAnimal.Id }, addedAnimal);
        }


        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var deletedAnimal = await _animalService.Delete(id);
            if (deletedAnimal == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

//namespace Lab8.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class AnimalsController : ControllerBase
//    {
//        private readonly AnimalService _animalService;

//        public AnimalsController(AnimalService animalService)
//        {
//            _animalService = animalService;
//        }

//        // GET: api/Animals
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimal()
//        {
//            var animals = await _animalService.GetAll();
//            if (animals == null)
//            {
//                return NotFound();
//            }
//            return Ok(animals);
//        }

//        // GET: api/Animals/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Animal>> GetAnimal(int id)
//        {
//          if (_animalService.GetAll() == null)
//          {
//              return NotFound();
//          }
//            var animal = await _animalService.Get(id);

//            if (!AnimalExists(id))
//            {
//                return NotFound();
//            }

//            return animal;
//        }

//        // PUT: api/Animals/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutAnimal(int id, Animal animal)
//        {
//            if (id != animal.Id)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                await _animalService.Edit(id, animal);
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!AnimalExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Animals
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
//        {
//            var addedAnimal = await _animalService.Add(animal);
//            if (addedAnimal == null)
//            {
//                return Problem("Entity set 'Lab8Context.Animal'  is null.");
//            }

//            return CreatedAtAction("GetAnimal", new { id = addedAnimal.Id }, addedAnimal);
//        }

//        // DELETE: api/Animals/5
//        [HttpPost]
//        [Route("/api/destroy/{id}")]
//        public async Task<IActionResult> DeleteAnimal(int id)
//        {

//            if (!AnimalExists(id))
//            {
//                return NotFound();
//            }
//            await _animalService.Delete(id);
//            return NoContent();
//        }

//        [HttpPost]
//        [Route("/api/Animals/clear-all")]
//        public async Task<IActionResult> ClearAllAnimals()
//        {
//            await _animalService.ClearAllAnimals();

//            return NoContent();
//        }

//        private bool AnimalExists(int id)
//        {
//            var animal = _animalService.Get(id).Result;
//            if (animal != null)
//                return true;
//            return false;
//        }
//    }
//}

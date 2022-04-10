using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name ="GetById")]
        public IActionResult GetById(int id)
        {
           var celestialObject = _context.CelestialObjects.Find(id);

            if (celestialObject == null)
            {
                return NotFound();
            }
            celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();

            return Ok(celestialObject);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public IActionResult GetByName(string Name)
        {
          var celestialObjects =  _context.CelestialObjects.Where(c => c.Name == Name).ToList();

            if (!celestialObjects.Any())
            {
                return NotFound();
            }

            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();

            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CelestialObject celestialObject) 
        {
            _context.Add(celestialObject);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new CelestialObject { Id = celestialObject.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var celestialObjectToBeUpdated = _context.CelestialObjects.Find(id);

            if (celestialObjectToBeUpdated == null)
            {
                return NotFound();
            }

            celestialObjectToBeUpdated.Name = celestialObject.Name;
            celestialObjectToBeUpdated.OrbitalPeriod = celestialObject.OrbitalPeriod;
            celestialObjectToBeUpdated.OrbitedObjectId = celestialObject.OrbitedObjectId;

            _context.CelestialObjects.Update(celestialObjectToBeUpdated);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var celestialObjectToBeUpdated = _context.CelestialObjects.Find(id);

            if (celestialObjectToBeUpdated == null)
            {
                return NotFound();
            }

            celestialObjectToBeUpdated.Name = name;

            _context.CelestialObjects.Update(celestialObjectToBeUpdated);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var celestialObjectsToBeDeleted = _context.CelestialObjects.Where(c => c.Id == id || c.OrbitedObjectId == id);

            if (celestialObjectsToBeDeleted == null)
            {
                return NotFound();
            }

            _context.CelestialObjects.RemoveRange(celestialObjectsToBeDeleted);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

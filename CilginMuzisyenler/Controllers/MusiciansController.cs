using CilginMuzisyenler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CilginMuzisyenler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private static readonly List<Musicians> _musiciansList =
        [
            new()
            {
                Id = 1, Name = "Ahmet Çalgı", Job = "Ünlü Çalgı Çalar",
                FunFact = "Her zaman yanlış nota çalar, ama çok eğlenceli"
            },
            new()
            {
                Id = 2, Name = "Zeynep Melodi", Job = "Popüler Melodi Yazarı",
                FunFact = "Şarkıları yanlış anlaşılır ama çok popüler"
            },
            new()
            {
                Id = 3, Name = "Cemil Akor", Job = "Çılgın Akorist",
                FunFact = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli"
            },
            new()
            {
                Id = 4, Name = "Fatma Nota", Job = "Sürpriz Nota Üreticisi",
                FunFact = "Nota üretirken sürekli sürprizler hazırlar"
            },
            new()
            {
                Id = 5, Name = "Hasan Ritim", Job = "Ritim Canavarı",
                FunFact = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir"
            },
            new()
            {
                Id = 6, Name = "Elif Armoni", Job = "Armoni Ustası",
                FunFact = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır"
            },
            new()
            {
                Id = 7, Name = "Ali Perde", Job = "Perde Uygulayıcı",
                FunFact = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir"
            },
            new()
            {
                Id = 8, Name = "Ayşe Rezonans", Job = "Rezonans Uzmanı",
                FunFact = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır"
            },
            new()
            {
                Id = 9, Name = "Murat Ton", Job = "Tonlama Meraklısı",
                FunFact = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç"
            },
            new()
            {
                Id = 10, Name = "Selin Akor", Job = "Akor Sihirbazı",
                FunFact = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır"
            }
        ];

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_musiciansList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var musician = _musiciansList.FirstOrDefault(x => x.Id == id);
            if (musician == null)
                return NotFound();

            return Ok(musician);
        }
        
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? name)
        {
            // Eğer name parametresi boş veya null ise tüm listeyi döndürüyoruz
            if (string.IsNullOrWhiteSpace(name))
                return Ok(_musiciansList);

            // name parametresi varsa, listeyi filtreliyoruz
            var filtered = _musiciansList
                .Where(m => m.Name != null && m.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .ToList();

            return Ok(!filtered.Any() ?
                // Eşleşen yoksa boş liste dönebilir veya NotFound() döndürebilirsiniz
                new List<Musicians>() : filtered);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Musicians musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = _musiciansList.Any(x => x.Id == musician.Id);
            if (exists)
            {
                return Conflict(new { message = $"Bu ID ({musician.Id}) zaten kayıtlı." });
            }

            _musiciansList.Add(musician);

            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Musicians musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMusician = _musiciansList.FirstOrDefault(x => x.Id == musician.Id);
            if (existingMusician == null)
            {
                return NotFound();
            }

            existingMusician.Name = musician.Name;
            existingMusician.Job = musician.Job;
            existingMusician.FunFact = musician.FunFact;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var musician = _musiciansList.FirstOrDefault(x => x.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            _musiciansList.Remove(musician);
            return NoContent();
        }
    }
}

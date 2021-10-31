using BCITechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCITechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KendaraansController : ControllerBase
    {
        readonly KendaraanRepo repo = new KendaraanRepo();
        // GET: api/<KendaraansController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = repo.GetKendaraan();
            return Ok(result);
        }

        // GET api/<KendaraansController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = repo.Kendaraan(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // POST api/<KendaraansController>
        [HttpPost]
        public IActionResult Post([FromBody] Kendaraan kendaraan)
        {
            try
            {
                var result = repo.SaveKendaraan(kendaraan);
                return StatusCode(201, "Data Kendaraan Berhasil Ditambahkan!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // PUT api/<KendaraansController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Kendaraan kendaraan)
        {
            try
            {
                kendaraan.Id = id;
                var result = repo.SaveKendaraan(kendaraan);
                if (result == 0) {
                    return StatusCode(404, "Data Kendaraan tidak ditemukan!!!");
                }
                else
                {
                    return StatusCode(201, "Data Kendaraan berhasil diperbaharui!!!");
                }               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<KendaraansController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = repo.DeleteKendaraan(id);
                if (result == 0)
                {
                    return StatusCode(404, "Data Kendaraan tidak ditemukan!!!");
                }
                else
                {
                    return StatusCode(201, "Data Kendaraan berhasil dihapus!!!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using System;

namespace BCITechnicalTest.Models
{
    public class Kendaraan
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Model { get; set; }
        public string Merek { get; set; }
        public string Transmisi { get; set; }
        public string Tahun { get; set; }
        public int Harga { get; set; }
    }
}

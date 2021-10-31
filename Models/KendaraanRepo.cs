using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BCITechnicalTest.Models
{
    public class KendaraanRepo
    {
        private readonly IDbConnection db;
        public KendaraanRepo()
        {
            db = new SqlConnection(DbConn.ConnStr);
        }

        public List<Kendaraan> GetKendaraan()
        {
            var sql = @"select kendaraan_id as Id, kendaraan_nama as Nama, kendaraan_model as Model, kendaraan_merek as Merek, 
                        kendaraan_transmisi as Transmisi, kendaraan_tahun as Tahun, kendaraan_harga as Harga
                        from kendaraan order by kendaraan_id desc";
            db.Open();
            var result = db.Query<Kendaraan>(sql).ToList();
            db.Close();
            return result;
        }

        public Kendaraan Kendaraan(int id)
        {
            var sql = @"select kendaraan_id as Id, kendaraan_nama as Nama, kendaraan_model as Model, kendaraan_merek as Merek, 
                        kendaraan_transmisi as Transmisi, kendaraan_tahun as Tahun, kendaraan_harga as Harga
                        from kendaraan where kendaraan_id = @id";
            db.Open();
            var result = db.QueryFirst<Kendaraan>(sql, new { id });
            db.Close();

            return result;
        }

        public int SaveKendaraan(Kendaraan kendaraan)
        {
            string sql = "";
            if(kendaraan.Id == 0)
            {
                sql += @"insert into kendaraan
                        values(@Nama, @Model, @Merek, @Transmisi, @Tahun, @Harga, GETDATE(), null)";
            }
            else
            {
                sql += @"update kendaraan
                        set
                        kendaraan_nama = @Nama,
                        kendaraan_model = @Model,
                        kendaraan_merek = @Merek,
                        kendaraan_transmisi = @Transmisi,
                        kendaraan_tahun = @Tahun,
                        kendaraan_harga = @Harga,
                        updated_dt = GETDATE()
                        where kendaraan_id = @Id";
            }

            var parameters = new
            {
                kendaraan.Id,
                kendaraan.Nama,
                kendaraan.Merek,
                kendaraan.Model,
                kendaraan.Transmisi,
                kendaraan.Tahun,
                kendaraan.Harga
            };
            db.Open();
            var result = db.Execute(sql, parameters);
            db.Close();

            return result;
        }

        public int DeleteKendaraan(int id)
        {
            var sql = @"Delete From kendaraan where kendaraan_id = @id";
            db.Open();
            var result = db.Execute(sql, new { id });
            db.Close();

            return result;
        }

        public int SaveKendaraan(DataTable data)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Kendaraans", data.AsTableValuedParameter("AddKendaraans"));
            parameters.Add("@Result", dbType: DbType.Int16, direction: ParameterDirection.Output);

            db.Open();
            var result = db.ExecuteScalar<int>("AddMultipleKendaraan", parameters, commandType: CommandType.StoredProcedure);
            db.Close();

            return result;
        }
    }
}

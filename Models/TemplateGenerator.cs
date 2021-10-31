using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCITechnicalTest.Models
{
    public class TemplateGenerator
    {
        
        
        public static string GetHTMLString()
        {
            KendaraanRepo repo = new KendaraanRepo();
            var kendaraans = repo.GetKendaraan();
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            <style>
                                .header {
                                    text-align: center;
                                    color: green;
                                    padding-bottom: 35px;
                                }

                                table {
                                    width: 80%;
                                    border-collapse: collapse;
                                }

                                td, th {
                                    border: 1px solid gray;
                                    padding: 10px;
                                    font-size: 12px;
                                    text-align: center;
                                }

                                table th {
                                    background-color: green;
                                    color: white;
                                }

                            </style>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Nama Kendaraan</th>
                                        <th>Model Kendaraan</th>
                                        <th>Merek Kendaraan</th>
                                        <th>Transmisi Kendaraan</th>
                                        <th>Tahun Kendaraan</th>
                                        <th>Harga Kendaraan</th>
                                    </tr>");
            foreach (var kendaraan in kendaraans)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                  </tr>", kendaraan.Nama, 
                                  kendaraan.Model, 
                                  kendaraan.Merek, 
                                  kendaraan.Transmisi,
                                  kendaraan.Tahun,
                                  kendaraan.Harga
                                  );
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}

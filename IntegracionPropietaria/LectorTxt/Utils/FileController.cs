using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LectorTxt.Utils
{
    class FileController
    {

        private static DbTssEntitiesContext dbContext;
        public static bool ReadFile(string file)
        {

            if (Path.GetExtension(file) == ".txt")
            {
                dbContext = new DbTssEntitiesContext();
                StreamReader reader = new StreamReader(file);
                string info = reader.ReadLine();
                var header= new HeaderInformation();
                header.EntryType = info.ElementAt(0).ToString();
                header.IdType = info.ElementAt(1).ToString();
                header.Id = info.Substring(2, 11);
                header.DateCreation = Convert.ToDateTime(info.Substring(13, 10));


                dbContext.HeaderInformation.Add(header);

                while ((info = reader.ReadLine()) != null)

                    switch (info.ElementAt(0))
                    {
                        case 'D':
                        {
                            var employee = new EmployeeInformation();
                            employee.EntryType = info.Substring(0, 1);
                            employee.IdType = info.Substring(1, 1);
                            employee.Id = info.Substring(2, 12);
                            employee.FullName = info.Substring(13, 50).Trim();
                            employee.Currency = info.Substring(63, 5);
                            employee.Amount = info.Substring(68, 10).TrimStart('0');
                            dbContext.EmployeeInformation.Add(employee);
                            break;
                        }
                        case 'S':
                        {
                            var footer = new FooterInformation();
                            footer.EntryType = info.ElementAt(0).ToString();
                            footer.TotalEmployees = info.Substring(1, 10).TrimStart('0');
                            footer.TotalAmount = info.Substring(10, 15).TrimStart('0');


                            dbContext.FooterInformation.Add(footer);
                            MessageBox.Show(dbContext.SaveChanges().ToString()
                                );
                            break;
                        }
                    }
                return true;

            }
                return false;
                
            




        }
    }
}

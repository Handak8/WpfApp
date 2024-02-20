using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfApp.Models;

namespace WpfApp
{
    public static class DataAccess
    {
        //Добавление типа абонента
        public static void CreateAbonentType(string name, Boolean mobile) 
        {
            using(ApplicationContext db = new ApplicationContext()) 
            {
                AbonentType abonentType = new AbonentType { Name = name, Mobile = mobile};
                db.AbonentTypes.Add(abonentType);
                db.SaveChanges();
            }
        }

        //Добавление абонента
        public static void CreateAbonent(int country, int city, int pnumber, int? fax, string? description, int ptype, int? secure)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Abonent abonent = new Abonent { Country = country, City = city, Pnumber = pnumber, Fax = fax, Description = description, Ptype = ptype, Secure = secure };
                db.Abonents.Add(abonent);
                db.SaveChanges();
            }
        }

        //Удаление типа абонента
        public static void DeleteAbonentType(AbonentType abonentType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.AbonentTypes.Remove(abonentType);
                db.SaveChanges();
            }
        }

        //Удаление абонента
        public static void DeleteAbonent(Abonent abonent) 
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Abonents.Remove(abonent);
                db.SaveChanges();
            }
        }

        //Изменение типа абонента
        public static void EditAbonentType(AbonentType oldAbonentType, string name, Boolean mobile)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                AbonentType abonentType = db.AbonentTypes.FirstOrDefault(a => a.Id == oldAbonentType.Id);
                abonentType.Name = name;
                abonentType.Mobile = mobile;
                db.SaveChanges();
            }
        }

        //Изменение абонента
        public static void EditAbonent(Abonent OldAbonent, int country, int city, int pnumber, int? fax, string? description, int ptype, int? secure)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Abonent abonent = db.Abonents.FirstOrDefault(a => a.Id == OldAbonent.Id);
                abonent.Country = country;
                abonent.City = city;
                abonent.Pnumber = pnumber;
                abonent.Fax = fax;
                abonent.Description = description;
                abonent.Ptype = ptype;
                abonent.Secure = secure;
                db.SaveChanges();
            }
        }

        //Получить всех абонентов
        public static List<Abonent> GetAllAbonents() 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Abonents.ToList();
                return result;
            }
        }
        //Получить все типы абонентов
        public static List<AbonentType> GetAllAbonentTypes()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.AbonentTypes.ToList();
                return result;
            }
        }

        //Получить все услуги абонента
        public static List<AbonentServices> GetAbonentServices() 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.AbonentServices.ToList();
                return result;
            }
        }

        //Получить детализацию абонента
        public static List<AbonentDetails> GetAbonentDetails() 
        {
            using( ApplicationContext db = new ApplicationContext()) 
            {
                var result = db.AbonentDetails.ToList();
                return result;
            }
        }

        //Получить тип абонента по id
        public static AbonentType GetAbonentTypeById(int id)
        {
            using(ApplicationContext db = new ApplicationContext()) 
            {
                AbonentType abonentType = db.AbonentTypes.FirstOrDefault(a => a.Id == id);
                return abonentType;
            }
        }

        #region VOIDS TO GENERATE REPORTS
        public static void GenerateReport1()
        {
            using (ApplicationContext dbContext = new ApplicationContext())
            {
                var query = from abonent in dbContext.Abonents
                            join abonentType in dbContext.AbonentTypes on abonent.Ptype equals abonentType.Id
                            join abonentDetails in dbContext.AbonentDetails on abonent.Id equals abonentDetails.AbonentId
                            group new { abonentDetails.DateTime.Month, abonentType.Name, abonentDetails.Cost } by new { abonentDetails.DateTime.Month, abonentType.Name } into g
                            select new
                            {
                                Month = g.Key.Month,
                                Name = g.Key.Name,
                                TotalCost = g.Sum(x => x.Cost)
                            };
                var Total = query.Sum(e  => e.TotalCost);
                var result = query.ToList();
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "report1.pdf");
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();
                PdfPTable table = new PdfPTable(3);
                foreach (var item in result)
                {
                    table.AddCell(item.Month.ToString());
                    table.AddCell(item.Name);
                    table.AddCell(item.TotalCost.ToString());
                }

                document.Add(table);
                Paragraph TotalParagraph = new Paragraph("Total: " + Total.ToString());
                document.Add(TotalParagraph);

                document.Close();


            }

        }
        public static void GenerateReport2()
        {
            using(ApplicationContext dbContext = new ApplicationContext()) 
            {
                var query = from abonent in dbContext.Abonents
                            join abonentType in dbContext.AbonentTypes on abonent.Ptype equals abonentType.Id
                            join abonentDetails in dbContext.AbonentDetails on abonent.Id equals abonentDetails.AbonentId
                            group new { abonent.Pnumber, abonentType.Name, abonentDetails.Service, abonentDetails.DateTime.Month, abonentDetails.Cost } by new { abonent.Pnumber, abonentType.Name, abonentDetails.Service, abonentDetails.DateTime.Month } into g
                            select new
                            {
                                PNumber = g.Key.Pnumber,
                                Name = g.Key.Name,
                                Service = g.Key.Service,
                                Month = g.Key.Month,
                                TotalCost = g.Sum(x => x.Cost)
                            };
                var Total = query.Sum(e => e.TotalCost);
                var result = query.OrderBy(item => item.Name).ToList();

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "report2.pdf");
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();
                
                PdfPTable table = new PdfPTable(5);
                table.AddCell("PNumber");
                table.AddCell("Name");
                table.AddCell("Service");
                table.AddCell("Month");
                table.AddCell("TotalCost");

                foreach (var item in result)
                {
                    table.AddCell(item.PNumber.ToString());
                    table.AddCell(item.Name);
                    table.AddCell(item.Service);
                    table.AddCell(item.Month.ToString());
                    table.AddCell(item.TotalCost.ToString());
                }

                document.Add(table);
                Paragraph TotalParagraph = new Paragraph("Total: " + Total.ToString());
                document.Add(TotalParagraph);

                document.Close();
            }
            
        }

        public static void GenerateReport3()
        {
            using (ApplicationContext dbContext = new ApplicationContext())
            {
                var query = from abonent in dbContext.Abonents
                            join abonentType in dbContext.AbonentTypes on abonent.Ptype equals abonentType.Id
                            join abonentService in dbContext.AbonentServices on abonent.Id equals abonentService.AbonentId
                            group new { abonent.Pnumber, abonentType.Name, abonentService.Service, abonentService.DateTime.Month, abonentService.CostNds, abonentService.Cost } by new { abonent.Pnumber, abonentType.Name, abonentService.Service, abonentService.DateTime.Month } into g
                            select new
                            {
                                PNumber = g.Key.Pnumber,
                                Name = g.Key.Name,
                                Service = g.Key.Service,
                                DateTime = g.Key.Month,
                                SumCost = g.Sum(x => x.Cost),
                                SumCostNds = g.Sum(x => x.CostNds),
                                SumTotalCost = g.Sum(x => x.Cost + x.CostNds)
                            };
                var TotalSum = query.Sum(e => e.SumCost);
                var TotalNds = query.Sum(e => e.SumCostNds);
                var TotalSumWithNds = query.Sum(e => e.SumTotalCost);
                var result = query.OrderBy(item => item.Name).ToList();

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "report3.pdf");
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                PdfPTable table = new PdfPTable(7);
                table.AddCell("Number");
                table.AddCell("AbonentTypeName");
                table.AddCell("Service");
                table.AddCell("Data");
                table.AddCell("SumCost");
                table.AddCell("SumNds");
                table.AddCell("SumCostWihtNds");

                foreach (var item in result)
                {
                    table.AddCell(item.PNumber.ToString());
                    table.AddCell(item.Name);
                    table.AddCell(item.Service);
                    table.AddCell(item.DateTime.ToString());
                    table.AddCell(item.SumCost.ToString());
                    table.AddCell(item.SumCostNds.ToString());
                    table.AddCell(item.SumTotalCost.ToString());
                }

                document.Add(table);
                Paragraph TotalParagraph1 = new Paragraph("TotalCost: " + TotalSum.ToString());
                Paragraph TotalParagraph2 = new Paragraph("TotalNds " + TotalNds.ToString());
                Paragraph TotalParagraph3 = new Paragraph("TotalCostWithNds: " + TotalSumWithNds.ToString());
                document.Add(TotalParagraph1);
                document.Add(TotalParagraph2);
                document.Add(TotalParagraph3);

                document.Close();
            }
        }
        #endregion



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace GenerateImagePagesFromCSV
{
    class Program
    {
        public static string generatedDir = string.Empty;
        static void Main(string[] args)
        {


            generatedDir = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "generated");
            
            List<ImageData> values = File.ReadAllLines("data.csv")
                                           .Skip(1)
                                           .Select(v => ImageData.FromCSV(v))
                                           .ToList();


            var distinctProducts = values.Select(x => x.ImageCaption).Distinct().ToList();

            if (!Directory.Exists(generatedDir))
            {
                Directory.CreateDirectory(generatedDir);
            }

            foreach (var p in distinctProducts)
            {

                var allProducts = values.Where(x => x.ImageCaption == p).ToList();



                var counter = 2;

                // Generate Paging
                var paging = string.Empty;

                Console.WriteLine("Generating Paging For " + p.ToString());
                    
                foreach (var a in allProducts)
                {

                    if(a == allProducts.First())
                    {
                        paging += "<li><a href='./" + a.FileName  + "'>" + a.ImageViewName  + " Image </a></li>" + Environment.NewLine;
                    }
                    else if (a == allProducts.Last())
                    {
                        paging += "<li><a href='./" + a.FileName + "'>" + a.ImageViewName + " Image </a></li>" + Environment.NewLine;

                    }
                    else
                    {
                        paging += "<li><a href='./" + a.FileName + "'>" + a.ImageViewName + " Image </a></li>" + Environment.NewLine;

                        counter++;
                    }

                    
                }

                foreach (var a in allProducts)
                {

                    Console.WriteLine("Generating " + a.ImageViewName + " For " + a.ImageCaption);

                    var html = File.ReadAllText("template.html");

                    html = html.Replace("***B2***", a.ImageURL);
                    html = html.Replace("***C2***", a.ProductURL);
                    html = html.Replace("***D2***", a.ProductName);
                    html = html.Replace("***E2***", a.Price);
                    html = html.Replace("***F2***", a.Description);
                    html = html.Replace("***G2***", a.ImageCaption);
                    html = html.Replace("***H2***", a.ImageViewName);


                    html = html.Replace("***PAGING***", paging);


                    File.WriteAllText(generatedDir + "/" + a.FileName, html);
                }
            }

            Console.WriteLine("Press Any Key To Exit");
            Console.ReadLine();

        }
    }
}

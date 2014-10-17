using P_Application1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Xml.Linq;

namespace P_Application1.Controllers
{
    public class FileController : ApiController
    {
        [HttpGet]
        public IList<District> ReadFile(string fileType = "csv", string searchString = "All", string sortColumn = "NoSort", string sortOrder = "Asc")
        {
            string[] districtFileList;
            IList<District> returnList = new List<District>();

            // Read file
            switch (fileType)
            {
                case "csv":

                    // Does not work while running unit test
                    //districtFileList = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Files/Input/sample_data.csv"));

                    // File operations
                    districtFileList = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/Files/Input/sample_data.csv");

                    // District list
                    var districtCsvList = from line in districtFileList
                                          let data = line.Split(',')
                                          select new District
                                          {
                                              CityName = data[0],
                                              CityCode = data[1],
                                              DistrictName = data[2],
                                              ZipCode = data[3]
                                          };

                    // Remove header
                    districtCsvList = districtCsvList.Skip(1);

                    // Search
                    if (!String.IsNullOrEmpty(searchString) && searchString != "All")
                    {
                        districtCsvList = districtCsvList.Where(s => s.CityName.Contains(searchString));
                    }

                    // Sort
                    if (!String.IsNullOrEmpty(sortColumn) && sortColumn != "NoSort")
                    {
                        districtCsvList = districtCsvList.OrderByDynamic(sortColumn, sortOrder);
                    }

                    // Return
                    try
                    {
                        return districtCsvList.ToList();
                    }
                    catch (Exception)
                    {
                        return returnList;
                    }

                case "xml":

                    // File operations
                    XElement xelement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/Files/Input/sample_data.xml");
                    IEnumerable<XElement> cities = xelement.Elements();
                    IList<District> districtList = new List<District>();

                    // District list
                    foreach (var city in cities)
                    {
                        var cityName = city.FirstAttribute.Value;
                        var cityCode = city.LastAttribute.Value;

                        foreach (XElement district in city.Nodes())
                        {
                            var districtName = district.FirstAttribute.Value;

                            foreach (XElement zipCode in district.Nodes())
                            {
                                districtList.Add(new District
                                {
                                    CityName = cityName,
                                    CityCode = cityCode,
                                    DistrictName = districtName,
                                    ZipCode = zipCode.FirstAttribute.Value
                                });
                            }
                        }

                    }

                    var districtXmlList = from line in districtList
                                          select new District
                                          {
                                              CityName = line.CityName,
                                              CityCode = line.CityCode,
                                              DistrictName = line.DistrictName,
                                              ZipCode = line.ZipCode
                                          }; ;

                    // Search
                    if (!String.IsNullOrEmpty(searchString) && searchString != "All")
                    {
                        districtXmlList = districtXmlList.Where(s => s.CityName.Contains(searchString));
                    }

                    // Sort
                    if (!String.IsNullOrEmpty(sortColumn) && sortColumn != "NoSort")
                    {
                        districtXmlList = districtXmlList.OrderByDynamic(sortColumn, sortOrder);
                    }

                    // Return
                    try
                    {
                        return districtXmlList.ToList();
                    }
                    catch (Exception)
                    {
                        return returnList;
                    }

                default:
                    return returnList;

            } // switch

        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxController
    {
        private CongestionTaxCalculator _taxCalculator;

        private static List<Vehicle> _vehicles = new List<Vehicle>();
        private static List<DateTime> _vehicleTimes = new List<DateTime> {
            //new DateTime(2013,1,14,21,00,00), // 0
            //new DateTime(2013,1,15,21,00,00), // 0
            //new DateTime(2013,2,07,06,23,27), // 8
            //new DateTime(2013,2,07,15,27,00), // 13 
            new DateTime(2013,2,08,06,27,00), // 8
            new DateTime(2013,2,08,06,20,27), // 8
            new DateTime(2013,2,08,14,35,00), // 8
            new DateTime(2013,2,08,15,29,00), // 13
            new DateTime(2013,2,08,15,47,00), // 18
            new DateTime(2013,2,08,16,01,00), // 18
            new DateTime(2013,2,08,16,48,00), // 18
            new DateTime(2013,2,08,17,49,00), // 13
            new DateTime(2013,2,08,18,29,00), // 8
            new DateTime(2013,2,08,18,35,00), // 0
            //new DateTime(2013,3,28,14,07,27), // 8
            //new DateTime(2013,3,26,14,25,00), // 8
        };

        public CongestionTaxController()
        {
            _taxCalculator = new CongestionTaxCalculator();
        }

        [HttpPut]
        [Route("AddVehicleTime")]
        public IEnumerable<DateTime> AddVehicleTime(DateTime dt)
        {
            _vehicleTimes.Add(dt);
            return _vehicleTimes;
        }

        [HttpGet]
        [Route("GetVehicles")]
        public IEnumerable<Vehicle> GetVehicles()
        {
            var mb = new Motorbike();
            _vehicles.Add(mb);
            return _vehicles;
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }

        [HttpGet]
        [Route("IsTollFreeVehicle")]
        public Boolean IsTollFreeVehicle(string vehicle)
        {
            var mb = new Motorbike();

            var k = _taxCalculator.IsTollFreeVehicle(mb);
            return k;
        }

        [HttpGet]
        [Route("IsTollFreeDate")]
        public Boolean IsTollFreeDate(DateTime date)
        {
            return _taxCalculator.IsTollFreeDate(date);
        }

        [HttpPost]
        [Route("GetTollFee")]
        public int GetTollFee(DateTime date, string vehicleType)
        {
            Vehicle vc;
            if (vehicleType == "Motorbike")
            {
                vc = new Motorbike();
                return _taxCalculator.GetTollFee(date, vc);
            }
            else if (vehicleType == "Car")
            {
                vc = new Car();
                return _taxCalculator.GetTollFee(date, vc);
            }
            else
            {
                throw new Exception("Bad");
            }

        }

        [HttpPost]
        [Route("GetTax")]
        public int GetTax(string vehicleType)
        {
            var t = _vehicleTimes.ToArray();
            Vehicle vc;
            if (vehicleType == "Motorbike")
            {
                vc = new Motorbike();
                return _taxCalculator.GetTax(vc,t);
            }
            else if (vehicleType == "Car")
            {
                vc = new Car();
                return _taxCalculator.GetTax(vc, t);
            }
            else
            {
                throw new Exception("Bad");
            }

        }
    }
}

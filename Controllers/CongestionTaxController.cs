using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxController
    {
        private CongestionTaxCalculator _taxCalculator;

        private static List<Vehicle> _vehicles = new List<Vehicle>
        {
            new Motorbike("MCA123"),
            new Car("CAR321"),
        };
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
        private static Dictionary<Vehicle, List<DateTime>> _vehicleDict = new Dictionary<Vehicle, List<DateTime>>();

        public CongestionTaxController()
        {
            _taxCalculator = new CongestionTaxCalculator();
        }

        [HttpPut]
        [Route("AddVehicle")]
        public List<Vehicle> AddVehicle(string registrationId, string vehicleType)
        {
            Vehicle vehicle;
            if (vehicleType == "Motorbike")
            {
                vehicle = new Motorbike(registrationId);
            }
            else if (vehicleType == "Car")
            {
                vehicle = new Car(registrationId);
            }
            else
            {
                throw new Exception("Bad");
            }

            if (!_vehicles.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
                _vehicles.Add(vehicle);

            return _vehicles;
        }


        [HttpPut]
        [Route("AddToll")]
        public IEnumerable<DateTime> AddToll(string registrationId, DateTime dt)
        {
            var v = _vehicles.Where(w => w.RegistrationNumber == registrationId).FirstOrDefault();
            if (v == null)
            {
                throw new Exception($"{v} does not exist in dictionary");
            }
            List<DateTime> dateTimeList;
            _vehicleDict.TryGetValue(v, out dateTimeList);
            if (dateTimeList == null)
            {
                var l = new List<DateTime>();
                l.Add(dt);
                _vehicleDict.Add(v, l);
            }
            else
            {
                dateTimeList.Add(dt);
            }
            _vehicleDict.TryGetValue(v, out dateTimeList);
            return dateTimeList;
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
            var mb = new Motorbike("Test123");

            var k = _taxCalculator.IsTollFreeVehicle(mb);
            return k;
        }

        [HttpGet]
        [Route("IsTollFreeDate")]
        public Boolean IsTollFreeDate(DateTime date) // only works for 2013
        {
            return _taxCalculator.IsTollFreeDate(date);
        }

        [HttpPost]
        [Route("GetTollFee")]
        public int GetTollFee(DateTime date, string vehicleType, string registrationId)
        {
            Vehicle vc;
            if (vehicleType == "Motorbike")
            {
                vc = new Motorbike(registrationId);
                return _taxCalculator.GetTollFee(date, vc);
            }
            else if (vehicleType == "Car")
            {
                vc = new Car(registrationId);
                return _taxCalculator.GetTollFee(date, vc);
            }
            else
            {
                throw new Exception("Bad");
            }

        }

        [HttpPost]
        [Route("GetTax")]
        public int GetTax(string vehicleType, string registrationId, DateTime day)
        {
            var t = _vehicleTimes.ToArray();
            var vehicle = _vehicles.Where(w => w.RegistrationNumber == registrationId).FirstOrDefault();
            if (vehicle == null)
            {
                throw new Exception($"{vehicle} does not exist in dictionary");
            }
            List<DateTime> datesList;
            _vehicleDict.TryGetValue(vehicle, out datesList);
            if (datesList.Count > 0)
            {
                return _taxCalculator.GetTax(vehicle, datesList.Where(d => d.Year == day.Year && d.Month == day.Month && d.Day == day.Day).ToArray());
            }
            else
            {
                throw new Exception($"datesList is empty for vehicle: {vehicle}");
            }
        }
    }
}

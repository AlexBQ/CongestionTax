using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxController : ControllerBase
    {
        private CongestionTaxCalculator _taxCalculator;

        private static List<Vehicle> _vehicles = new List<Vehicle>();
        private static Dictionary<Vehicle, List<DateTime>> _vehicleDict = new Dictionary<Vehicle, List<DateTime>>();

        public CongestionTaxController()
        {
            _taxCalculator = new CongestionTaxCalculator();
        }

        [HttpPut]
        [Route("AddVehicle")]
        public async Task<ActionResult<List<Vehicle>>> AddVehicle(string vehicleType, string registrationId)
        {
            Vehicle vehicle;
            if (vehicleType == "Motorbike")
                vehicle = new Motorbike(registrationId);
            else if (vehicleType == "Car")
                vehicle = new Car(registrationId);
            else if (vehicleType == "Diplomat")
                vehicle = new Diplomat(registrationId);
            else if (vehicleType == "Emergency")
                vehicle = new Emergency(registrationId);
            else if (vehicleType == "Foreign")
                vehicle = new Foreign(registrationId);
            else if (vehicleType == "Military")
                vehicle = new Military(registrationId);
            else if (vehicleType == "Tractor")
                vehicle = new Tractor(registrationId);
            else
                return BadRequest($"Not a valid vehicleType: {vehicleType}");

            if (!_vehicles.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
                _vehicles.Add(vehicle);

            return Ok(_vehicles);
        }

        [HttpPut]
        [Route("AddToll")]
        public async Task<ActionResult<IEnumerable<DateTime>>> AddToll(string registrationId, DateTime dt)
        {
            var vehicle = _vehicles.Where(w => w.RegistrationNumber == registrationId).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound($"{vehicle} does not exist in dictionary");
            }
            List<DateTime> dateTimeList;
            _vehicleDict.TryGetValue(vehicle, out dateTimeList);
            if (dateTimeList == null)
            {
                var l = new List<DateTime>();
                l.Add(dt);
                _vehicleDict.Add(vehicle, l);
            }
            else
            {
                dateTimeList.Add(dt);
            }
            _vehicleDict.TryGetValue(vehicle, out dateTimeList);

            return Ok(dateTimeList);
        }

        [HttpGet]
        [Route("GetVehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return Ok(_vehicles);
        }

        [HttpGet]
        [Route("IsTollFreeVehicle")]
        public async Task<ActionResult<Boolean>> IsTollFreeVehicle(string vehicleType, string registrationId)
        {
            Vehicle vehicle;
            if (vehicleType == "Motorbike")
                vehicle = new Motorbike(registrationId);
            else if (vehicleType == "Car")
                vehicle = new Car(registrationId);
            else if (vehicleType == "Diplomat")
                vehicle = new Diplomat(registrationId);
            else if (vehicleType == "Emergency")
                vehicle = new Emergency(registrationId);
            else if (vehicleType == "Foreign")
                vehicle = new Foreign(registrationId);
            else if (vehicleType == "Military")
                vehicle = new Military(registrationId);
            else if (vehicleType == "Tractor")
                vehicle = new Tractor(registrationId);
            else
                return BadRequest($"Not a valid vehicleType: {vehicleType}");
            return Ok(_taxCalculator.IsTollFreeVehicle(vehicle));
        }

        [HttpGet]
        [Route("IsTollFreeDate")]
        public async Task<ActionResult<Boolean>> IsTollFreeDate(DateTime date)
        {
            if (date.Year != 2013)
                return BadRequest("Calculations are only supported for year 2013");
            return Ok(_taxCalculator.IsTollFreeDate(date));
        }

        [HttpPost]
        [Route("GetTollFee")]
        public async Task<ActionResult<int>> GetTollFee(string vehicleType, string registrationId, DateTime date)
        {
            Vehicle vehicle;
            if (vehicleType == "Motorbike")
                vehicle = new Motorbike(registrationId);
            else if (vehicleType == "Car")
                vehicle = new Car(registrationId);
            else if (vehicleType == "Diplomat")
                vehicle = new Diplomat(registrationId);
            else if (vehicleType == "Emergency")
                vehicle = new Emergency(registrationId);
            else if (vehicleType == "Foreign")
                vehicle = new Foreign(registrationId);
            else if (vehicleType == "Military")
                vehicle = new Military(registrationId);
            else if (vehicleType == "Tractor")
                vehicle = new Tractor(registrationId);
            else
                return BadRequest($"Not a valid vehicleType: {vehicleType}");
            return Ok(_taxCalculator.GetTollFee(date, vehicle));
        }

        [HttpPost]
        [Route("GetTax")]
        public async Task<ActionResult<int>> GetTax(string registrationId, DateTime day)
        {
            var vehicle = _vehicles.Where(w => w.RegistrationNumber == registrationId).FirstOrDefault();
            if (vehicle == null)
                return NotFound($"{vehicle} does not exist in dictionary");

            if (_vehicleDict.TryGetValue(vehicle, out var datesList))
                return Ok(_taxCalculator.GetTax(vehicle, datesList.Where(d => d.Year == day.Year && d.Month == day.Month && d.Day == day.Day).ToArray()));
            else
                return NotFound($"datesList is empty for vehicle: {vehicle}");
        }
    }
}

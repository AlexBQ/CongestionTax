namespace CongestionTax
{
    public class Tractor : Vehicle
    {
        public Tractor(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }

        public string GetVehicleType()
        {
            return "Tractor";
        }
    }
}

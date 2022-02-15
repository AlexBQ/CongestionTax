namespace CongestionTax
{
    public class Diplomat : Vehicle
    {
        public Diplomat(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }
        public string GetVehicleType()
        {
            return "Diplomat";
        }
    }
}

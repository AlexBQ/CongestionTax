namespace CongestionTax
{
    public class Foreign : Vehicle
    {
        public Foreign(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }
        public string GetVehicleType()
        {
            return "Foreign";
        }
    }
}

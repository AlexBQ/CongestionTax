namespace CongestionTax
{
    public class Military : Vehicle
    {
        public Military(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }
        public string GetVehicleType()
        {
            return "Military";
        }
    }
}

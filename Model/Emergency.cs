namespace CongestionTax
{
    public class Emergency : Vehicle 
    {
        public Emergency(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }
        public string GetVehicleType()
        {
            return "Emergency";
        }
    }
}

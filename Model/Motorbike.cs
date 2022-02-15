namespace CongestionTax
{
    public class Motorbike : Vehicle
    {
        public Motorbike(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }

        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}

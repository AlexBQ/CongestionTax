namespace CongestionTax
{
    public class Car : Vehicle
    {
        public Car(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
        public string RegistrationNumber { get; set; }
        public string GetVehicleType()
        {
            return "Car";
        }
    }
}

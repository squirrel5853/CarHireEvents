namespace CarRentalService.Data
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }

        public Car(string make, string model)
        {
            this.Make = make;
            this.Model = model;
        }
    }
}
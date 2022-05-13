namespace PS_project12_MVC.Model
{
    public class Ticket : Train
    {
        private int id;
        private int seats;
        private double price;

        public Ticket() : base()
        {
            this.id = 0;
            this.seats = 0;
            this.price = 0;
        }

        public Ticket(int trainNo, string originStation, string destinationStation, int duration, int seats, double price, int id) : base(trainNo, originStation, destinationStation, duration)
        {
            this.seats = seats;
            this.price = price;
            this.id = id;
        }

        public Ticket(Ticket t)
        {
            this.trainNo = t.trainNo;
            this.originStation = t.originStation;
            this.destinationStation = t.destinationStation;
            this.duration = t.duration;
            this.seats = t.seats;
            this.price = t.price;
            this.id = t.id;
        }
        //setters and getters
        public int getSeats()
        {
            return this.seats;
        }

        public double getPrice()
        {
            return this.price;
        }

        public int getId()
        {
            return this.id;
        }

        public void setSeats(int seats)
        {
            this.seats = seats;
        }

        public void setPrice(double price)
        {
            this.price = price;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            string s = base.ToString();
            return s +";" +this.seats.ToString() +";" +this.price.ToString() +";" +this.id.ToString();
        }
    }
}

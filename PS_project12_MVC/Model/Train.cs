namespace PS_project12_MVC.Model
{
    public class Train
    {
        protected int trainNo;
        protected string originStation;
        protected string destinationStation;
        protected int duration;

        public Train()
        {
            this.trainNo = 0;
            this.destinationStation = "";
            this.originStation = "";
            this.duration = 0;
        }

        public Train(int trainNo, string originStation, string destinationStation, int duration)
        {
            this.trainNo = trainNo;
            this.originStation = originStation;
            this.destinationStation = destinationStation;
            this.duration = duration;
        }

        public Train(Train t)
        {
            this.trainNo = t.trainNo;
            this.originStation = t.originStation;
            this.destinationStation = t.destinationStation;
            this.duration = t.duration;
        }
        //setters and getters

        public int getTrainNo()
        {
            return this.trainNo;
        }

        public string getOriginStation()
        {
            return this.originStation;
        }

        public string getDestinationStation()
        {
            return this.destinationStation;
        }

        public int getDuration()
        {
            return this.duration;
        }

        public void setTrainNo(int trainNo)
        {
            this.trainNo = trainNo;
        }

        public void setOriginStation(string originStation)
        {
            this.originStation = originStation;
        }

        public void setDestinationStation(string destinationStation)
        {
            this.destinationStation = destinationStation;
        }

        public void setDuration(int duration)
        {
            this.duration = duration;
        }

        public override string ToString()
        {
            return this.trainNo.ToString() + ";" + this.originStation +";" +this.destinationStation + ";" + this.duration;
        }
    }
}

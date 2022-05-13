namespace PS_project12_MVC.Model
{
    public class User
    {
        private string userName;
        private string password;
        private string role;

        public User()
        {
            this.userName = "";
            this.password = "";
            this.role = "";
        }

        public User(string user, string password, string role)
        {
            this.userName = user;
            this.password = password;
            this.role = role;
        }

        public User(User u)
        {
            this.userName = u.userName;
            this.password = u.password;
            this.role = u.role;
        }

        //setters and getters
        public string getUserName()
        {
            return this.userName;
        }

        public string getPassword()
        {
            return this.password;
        }

        public string getRole()
        {
            return this.role;
        }

        public void setUserName(string user)
        {
            this.userName = user;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public void setRole(string role)
        {
            this.role = role;
        }

        public override string ToString() {


            return this.userName + ";" + this.password + ";" + this.role;
        }
    }
}

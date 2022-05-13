using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace PS_project12_MVC.Model.Persistent
{
    public class TicketP
    {
        public bool AddTicket(Ticket t)
        {
            try
            {
                XElement xElement = XElement.Load(@"tickets.xml");
                xElement.Add(new XElement("ticket",
                    new XElement("trainNo", t.getTrainNo().ToString()),
                    new XElement("originStation", t.getOriginStation()),
                    new XElement("destinationStation", t.getDestinationStation()),
                    new XElement("duration", t.getDuration().ToString()),
                    new XElement("seats", t.getSeats().ToString()),
                    new XElement("price", t.getPrice().ToString()),
                    new XElement("id", t.getId().ToString())
                    ));
                xElement.Save(@"tickets.xml");
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Ticket Add Error!" +e.ToString());
                return false;
            }
        }

        public bool DeleteTicket(int ticketId)
        {
            try
            {
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                xDoc.Root.Elements("ticket").Where(e => e.Element("id").Value == ticketId.ToString()).Remove();
                xDoc.Save(@"tickets.xml");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ticket Delete Error! " + e.ToString());
                return false;
            }
        }

        public bool UpdateTicket(int ticketId, Ticket t)
        {
            try
            {
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                var element = xDoc.Root.Elements("ticket").Where(e => e.Element("id").Value == ticketId.ToString()).Single();
                element.Element("trainNo").Value = t.getTrainNo().ToString();
                element.Element("originStation").Value = t.getOriginStation();
                element.Element("destinationStation").Value = t.getDestinationStation();
                element.Element("duration").Value = t.getDuration().ToString();
                element.Element("seats").Value = t.getSeats().ToString();
                element.Element("price").Value = t.getPrice().ToString();
                element.Element("id").Value = t.getId().ToString();
                xDoc.Save(@"tickets.xml");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ticket Update Error! " + e.ToString());
                return false;
            }
        }

        public Ticket TicketSearch(int ticketId)
        {
            //trainNo,  originStation,  destinationStation, duration, seats, price, id)
            try
            {
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                List<XElement> list = xDoc.Root.Elements("ticket").ToList();
                foreach (XElement xElem in list)
                {
                    int id = Convert.ToInt32(xElem.Element("id").Value);
                    if (id == ticketId)
                    {
                        int trainNo = Convert.ToInt32(xElem.Element("trainNo").Value);
                        string originStation = xElem.Element("originStation").Value;
                        string destinationStation = xElem.Element("destinationStation").Value;
                        int duration = Convert.ToInt32(xElem.Element("duration").Value);
                        int seats = Convert.ToInt32(xElem.Element("seats").Value);
                        double price = Convert.ToDouble(xElem.Element("price").Value);
                        Ticket t = new Ticket(trainNo, originStation, destinationStation, duration, seats, price, id);
                        return t;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ticket Search Error! " + ex.ToString());
                return null;
            }
        }

        public Ticket TicketSell (int ticketId, int offset) //offset pentru refill
        {
            //trainNo,  originStation,  destinationStation, duration, seats, price, id)
            try
            {
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                List<XElement> list = xDoc.Root.Elements("ticket").ToList();

                foreach (XElement xElem in list)
                {
                    int id = Convert.ToInt32(xElem.Element("id").Value);
                    int seats = Convert.ToInt32(xElem.Element("seats").Value);
                    if (id == ticketId)
                    {
                        seats += offset + 1;
                        seats -= 2;
                        if (seats > -1)
                        {
                            int trainNo = Convert.ToInt32(xElem.Element("trainNo").Value);
                            string originStation = xElem.Element("originStation").Value;
                            string destinationStation = xElem.Element("destinationStation").Value;
                            int duration = Convert.ToInt32(xElem.Element("duration").Value);
                            double price = Convert.ToDouble(xElem.Element("price").Value);
                            Ticket t = new Ticket(trainNo, originStation, destinationStation, duration, seats, price, id);
                            return t;
                        } 
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ticket Sell Error! " + ex.ToString());
                return null;
            }
        }


            public List<Ticket> TicketList()
        {
            try
            {
                List<Ticket> listaPatinatori = new List<Ticket>();
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                List<XElement> listXML = xDoc.Root.Elements("ticket").ToList();
                if (listXML.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (XElement xElem in listXML)
                    {
                        int trainNo = Convert.ToInt32(xElem.Element("trainNo").Value);
                        string originStation = xElem.Element("originStation").Value;
                        string destinationStation = xElem.Element("destinationStation").Value;
                        int duration = Convert.ToInt32(xElem.Element("duration").Value);
                        int seats = Convert.ToInt32(xElem.Element("seats").Value);
                        double price = Convert.ToDouble(xElem.Element("price").Value);
                        int ticketId = Convert.ToInt32(xElem.Element("id").Value);
                        Ticket t = new Ticket(trainNo, originStation, destinationStation, duration, seats, price, ticketId);
                        listaPatinatori.Add(t);
                    }
                    return listaPatinatori;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ticket List Error! " + e.ToString());
                return null;
            }
        }

        public List<Ticket> FilterTicketsByLocation(string originSearch, string destinationSearch)
        {
            try
            {
                List<Ticket> ticketList = new List<Ticket>();
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                List<XElement> listXML = xDoc.Root.Elements("ticket").ToList();
                foreach (XElement xElem in listXML)
                {
                    string originStation = xElem.Element("originStation").Value;
                    string destinationStation = xElem.Element("destinationStation").Value;
                    if (originStation == originSearch && destinationStation == destinationSearch)
                    {
                        int trainNo = Convert.ToInt32(xElem.Element("trainNo").Value);
                        int duration = Convert.ToInt32(xElem.Element("duration").Value);
                        int seats = Convert.ToInt32(xElem.Element("seats").Value);
                        double price = Convert.ToDouble(xElem.Element("price").Value);
                        int ticketId = Convert.ToInt32(xElem.Element("id").Value);
                        Ticket t = new Ticket(trainNo, originStation, destinationStation, duration, seats, price, ticketId);
                        ticketList.Add(t);
                    }
                }
                if (ticketList.Count > 0)
                    return ticketList;
                else
                    return null;

            }
            catch (Exception e)
            {
                MessageBox.Show("Ticket Filter by Locations Error! " + e.ToString());
                return null;
            }
        }

        public List<Ticket> FilterTicketsByTrain(int train)
        {
            try
            {
                List<Ticket> ticketList = new List<Ticket>();
                XDocument xDoc = XDocument.Load(@"tickets.xml");
                List<XElement> listXML = xDoc.Root.Elements("ticket").ToList();
                foreach (XElement xElem in listXML)
                {
                    int trainNo = Convert.ToInt32(xElem.Element("trainNo").Value);
                    if (train == trainNo)
                    {
                        string originStation = xElem.Element("originStation").Value;
                        string destinationStation = xElem.Element("destinationStation").Value;
                        int duration = Convert.ToInt32(xElem.Element("duration").Value);
                        int seats = Convert.ToInt32(xElem.Element("seats").Value);
                        double price = Convert.ToDouble(xElem.Element("price").Value);
                        int ticketId = Convert.ToInt32(xElem.Element("id").Value);
                        Ticket t = new Ticket(trainNo, originStation, destinationStation, duration, seats, price, ticketId);
                        ticketList.Add(t);
                    }  
                }
                if (!ticketList.Any()) //if list is empty
                {
                    return null;
                }
                return ticketList;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ticket Filter by Locations Error! " + e.ToString());
                return null;
            }
        }

    }//main class
    }

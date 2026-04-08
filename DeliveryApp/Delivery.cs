using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp
{
    public enum DeliveryStatus
    {
        Новый,
        В_пути,
        Доставлен
    }
    public class Delivery
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryStatus Status { get; set; }
        public Delivery(string customerName, string address, DateTime deliveryDate)
        {
            CustomerName = customerName;
            Address = address;
            DeliveryDate = deliveryDate;
            Status = DeliveryStatus.Новый;
        }
        public void UpdateStatus(DeliveryStatus newStatus)
        {
            Status = newStatus;
        }
    }
}

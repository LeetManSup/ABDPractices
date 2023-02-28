using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakersMongoGUI.Entities
{
    public class Speaker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Manufacturer { get; set; }
        public bool Bluetooth { get; set; }
        public bool Aux { get; set; }
        public string CategoryId { get; set; }

        public Speaker(string id, string name, int count, int price, string manufacturer, bool bluetooth, bool aux, string categoryId)
        {
            Id = id;
            Name = name;
            Count = count;
            Price = price;
            Manufacturer = manufacturer;
            Bluetooth = bluetooth;
            Aux = aux;
            CategoryId = categoryId;
        }
    }
}

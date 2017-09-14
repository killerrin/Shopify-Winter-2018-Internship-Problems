using KillerrinStudiosToolkit.Models;
using OrderViewer_UWP.Models.Shopify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer_UWP.Collections
{
    public class CustomerFilter : ModelBase
    {
        private string m_customerName = "";
        public string CustomerName
        {
            get { return m_customerName; }
            set
            {
                //if (m_parameter == value) return;
                m_customerName = value;
                RaisePropertyChanged(nameof(CustomerName));
            }
        }

        public CustomerFilter() { }
        public CustomerFilter(string name)
        {
            CustomerName = name;
        }

        public ObservableCollection<Order> Filter(ObservableCollection<Order> collection)
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
                return collection;

            Debug.Write($"Filtering {nameof(CustomerFilter)} = {CustomerName}");
            ObservableCollection<Order> filteredItems = new ObservableCollection<Order>();

            string lowerCustomerName = CustomerName.ToLower();
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].customer == null) continue;

                if (lowerCustomerName.Contains(collection[i]?.customer?.first_name.ToLower()) ||
                    lowerCustomerName.Contains(collection[i]?.customer?.last_name.ToLower()))
                {
                    filteredItems.Add(collection[i]);
                }
            }

            Debug.WriteLine(" | ...End Filter \n");
            return filteredItems;
        }
    }
}

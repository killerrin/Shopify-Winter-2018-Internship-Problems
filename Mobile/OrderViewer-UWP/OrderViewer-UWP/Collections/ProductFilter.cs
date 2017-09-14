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
    public class ProductFilter : ModelBase
    {
        private string m_productName = "";
        public string ProductName
        {
            get { return m_productName; }
            set
            {
                //if (m_parameter == value) return;
                m_productName = value;
                RaisePropertyChanged(nameof(ProductName));
            }
        }

        public ProductFilter() { }
        public ProductFilter(string name)
        {
            ProductName = name;
        }

        public bool MatchesCriteria(LineItem item)
        {
            string searchTerm = ProductName.ToLower();

            if (searchTerm.Contains(item.name.ToLower()) ||
                searchTerm.Contains(item.title.ToLower()) ||
                searchTerm.Contains(item.variant_title.ToLower()))
            {
                return true;
            }
            return false;
        }

        public ObservableCollection<Order> Filter(ObservableCollection<Order> collection)
        {
            if (string.IsNullOrWhiteSpace(ProductName))
                return collection;

            Debug.Write($"Filtering {nameof(ProductFilter)} = {ProductName}");
            ObservableCollection<Order> filteredItems = new ObservableCollection<Order>();

            string lowerProductName = ProductName.ToLower();
            for (int i = 0; i < collection.Count; i++)
            {
                foreach (var item in collection[i].line_items)
                {
                    if (lowerProductName.Contains(item.name.ToLower()) ||
                        lowerProductName.Contains(item.title.ToLower()) ||
                        lowerProductName.Contains(item.variant_title.ToLower()))
                    {
                        filteredItems.Add(collection[i]);
                        break;
                    }
                }
            }

            Debug.WriteLine(" | ...End Filter \n");
            return filteredItems;
        }
    }
}

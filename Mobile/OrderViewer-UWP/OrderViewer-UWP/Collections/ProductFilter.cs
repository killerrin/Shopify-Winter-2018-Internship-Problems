using KillerrinStudiosToolkit.Models;
using OrderViewer_UWP.Models.Shopify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Order> Filter(ObservableCollection<Order> collection)
        {
            if (string.IsNullOrWhiteSpace(ProductName))
                return collection;

            ObservableCollection<Order> filteredItems = new ObservableCollection<Order>();

            for (int i = 0; i < collection.Count; i++)
            {
                foreach (var item in collection[i].line_items)
                {
                    if (ProductName.ToLower().Contains(item.name.ToLower()))
                        filteredItems.Add(collection[i]);
                    if (ProductName.ToLower().Contains(item.variant_title.ToLower()))
                        filteredItems.Add(collection[i]);
                }
            }

            return filteredItems;
        }
    }
}

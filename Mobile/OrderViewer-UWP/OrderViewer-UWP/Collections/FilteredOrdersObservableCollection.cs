using KillerrinStudiosToolkit.Models;
using Newtonsoft.Json;
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
    public class FilteredOrdersObservableCollection : ModelBase
    {
        #region Collection
        private ObservableCollection<Order> m_unfilteredCollection = new ObservableCollection<Order>();
        public ObservableCollection<Order> UnfilteredCollection
        {
            get { return m_unfilteredCollection; }
            set
            {
                if (m_unfilteredCollection == value) return;

                m_unfilteredCollection = value;
                m_unfilteredCollection.CollectionChanged += M_unfilteredCollection_CollectionChanged;

                RaisePropertyChanged(nameof(UnfilteredCollection));
                ApplyFilters();
            }
        }

        private ObservableCollection<Order> m_filteredCollection = new ObservableCollection<Order>();
        [JsonIgnore]
        public ObservableCollection<Order> FilteredCollection
        {
            get { return m_filteredCollection; }
            protected set
            {
                if (m_filteredCollection == value) return;
                m_filteredCollection = value;
                RaisePropertyChanged(nameof(FilteredCollection));
            }
        }
        #endregion

        #region Filters
        private CustomerFilter m_customerFilter = new CustomerFilter("");
        [JsonIgnore]
        public CustomerFilter CustomerFilter
        {
            get { return m_customerFilter; }
            set
            {
                if (m_customerFilter == value) return;
                m_customerFilter = value;
                RaisePropertyChanged(nameof(CustomerFilter));

                m_customerFilter.PropertyChanged += Filters_PropertyChanged;
            }
        }

        private ProductFilter m_productFilter = new ProductFilter("");
        [JsonIgnore]
        public ProductFilter ProductFilter
        {
            get { return m_productFilter; }
            set
            {
                if (m_productFilter == value) return;
                m_productFilter = value;
                RaisePropertyChanged(nameof(ProductFilter));

                m_productFilter.PropertyChanged += Filters_PropertyChanged;
            }
        }
        #endregion

        public FilteredOrdersObservableCollection()
        {
            AddDefaultFilters();
        }
        public FilteredOrdersObservableCollection(IList<Order> list)
        {
            SetUnfilteredList(list);
            AddDefaultFilters();
        }

        public void SetUnfilteredList(IList<Order> list)
        {
            UnfilteredCollection = new ObservableCollection<Order>(list);
        }

        #region Filters
        private void AddDefaultFilters()
        {
            ApplyFilters();

            // Apply the Property Changed to the defaults
            m_customerFilter.PropertyChanged += Filters_PropertyChanged;
            m_productFilter.PropertyChanged += Filters_PropertyChanged;

            // Take away, and give back the Collection Changed
            m_unfilteredCollection.CollectionChanged -= M_unfilteredCollection_CollectionChanged;
            m_unfilteredCollection.CollectionChanged += M_unfilteredCollection_CollectionChanged;
        }

        private void Filters_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void M_unfilteredCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            Debug.WriteLine($"Applying Filters - {CustomerFilter.CustomerName} | {ProductFilter.ProductName}");
            ObservableCollection<Order> filtered = new ObservableCollection<Order>(UnfilteredCollection);

            if (CustomerFilter != null)
                filtered = CustomerFilter.Filter(filtered);
            if (ProductFilter != null)
                filtered = ProductFilter.Filter(filtered);

            FilteredCollection = filtered;

            // Update the Helpers
            RaisePropertyChanged(nameof(TotalSpent));
            RaisePropertyChanged(nameof(QuantityOfProduct)); 
        }
        #endregion

        [JsonIgnore]
        public int UnfilteredCount { get { return UnfilteredCollection.Count; } }
        [JsonIgnore]
        public int FilteredCount { get { return FilteredCollection.Count; } }

        #region Helpers
        public double TotalSpent
        {
            get
            {
                double totalPrice = 0.0;
                foreach (var item in FilteredCollection)
                {
                    if (double.TryParse(item.total_price, out double price))
                    {
                        totalPrice += price;
                    }
                }

                return totalPrice;
            }
        }

        public int QuantityOfProduct
        {
            get
            {
                int totalQuantity = 0;
                foreach (var order in FilteredCollection)
                {
                    foreach (var item in order.line_items)
                    {
                        if (ProductFilter.MatchesCriteria(item))
                        {
                            totalQuantity += item.quantity;
                        }
                    }
                }

                return totalQuantity;
            }
        }
        #endregion

    }
}

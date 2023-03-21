﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.OData;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using WideWorldImporters.Wpf.Extensions;
using WideWorldImporters.Wpf.Models;
using WideWorldImporters.Wpf.ViewModels;

namespace WideWorldImporters.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string[] columnToODataProperty = new[]
        {
            "CustomerId",
            "CustomerName",
            "PhoneNumber",
            "FaxNumber",
            "LastEditedByNavigation/PreferredName"
        };

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new CustomersViewModel();
        }

        public CustomersViewModel ViewModel => (CustomersViewModel)DataContext;

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ViewModel.SelectedCustomer?.Validate();
        }

        private void DataGrid_Sorted(object sender, RoutedEventArgs e)
        {
            // Get all Columns from the DataGrid:
            var columns = ((DataGrid)sender).Columns;

            // Get all Sort Columns:
            var sortColumns = columns
                // Only use Columns, that have been sorted
                .Where(column => column.SortDirection != null)
                // Convert to Model:
                .Select(column =>
                {
                    var propertyName = columnToODataProperty[column.DisplayIndex];
                    var sortDirection = column.SortDirection == System.ComponentModel.ListSortDirection.Descending ? SortDirection.Descending : SortDirection.Ascending;

                    return new SortColumn(propertyName, sortDirection);
                }).ToArray();


            ViewModel.SortColumns = sortColumns;
        }
    }
}

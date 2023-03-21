using System.Windows.Controls;
using System.Windows;

namespace WideWorldImporters.Wpf.Controls
{
    public class CustomDataGrid : DataGrid
    {
        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent SortedEvent = EventManager.RegisterRoutedEvent(
            "Sorted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomDataGrid));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Sorted
        {
            add { AddHandler(SortedEvent, value); }
            remove { RemoveHandler(SortedEvent, value); }
        }

        // This method raises the Sorted event
        void RaiseSortedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CustomDataGrid.SortedEvent);
            RaiseEvent(newEventArgs);
        }

        protected override void OnSorting(DataGridSortingEventArgs eventArgs)
        {
            base.OnSorting(eventArgs);
            RaiseSortedEvent();
        }
    }
}

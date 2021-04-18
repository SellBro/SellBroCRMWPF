using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Net;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SellbroCRMWPF.Tables
{
    public class GridViewBindingBehavior
    {
        public static readonly DependencyProperty ColumnsCollectionProperty = DependencyProperty.RegisterAttached("ColumnsCollection", typeof(ObservableCollection<DataGridColumns>), typeof(GridViewBindingBehavior), new PropertyMetadata(OnColumnsCollectionChanged));

        public static void SetColumnsCollection(DependencyObject o, ObservableCollection<ColumnDefinition> value)
        {
            o.SetValue(ColumnsCollectionProperty, value);
        }

        public static ObservableCollection<ColumnDefinition> GetColumnsCollection(DependencyObject o)
        {
            return o.GetValue(ColumnsCollectionProperty) as ObservableCollection<ColumnDefinition>;
        }

        private static void OnColumnsCollectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            DataGrid gridView = o as DataGrid;

            if (gridView == null)
            {
                return;
            }

            gridView.Columns.Clear();

            if (gridView.ItemsSource == null)
            {
                return;
            }

            ObservableCollection<ExpandoObject> objExpando = (ObservableCollection<ExpandoObject>)gridView.ItemsSource;

            if (e.NewValue == null)
            {
                return;
            }

            var collection = e.NewValue as ObservableCollection<DataGridColumns>;

            if (collection == null)
            {
                return;
            }
            foreach (var column in collection)
            {
                var gridViewColumn = GetDataColumn(column);
                gridView.Columns.Add(gridViewColumn);
            }

        }

        private static DataGridTextColumn GetDataColumn(DataGridColumns columnName)
        {
            var column = new DataGridTextColumn();
            var MyStyle = new Style(typeof(DataGridCell)) {
                Setters = {
                    new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                }
            };
            column.CellStyle = MyStyle;
            column.IsReadOnly = true;
            column.Header = columnName.DisplayColumnName;
            column.Binding = new Binding()
            {
                Converter = new ColumnValueConverter(),
                ConverterParameter = columnName.BindingPropertyName
            };

            return column;
        }

    }
}

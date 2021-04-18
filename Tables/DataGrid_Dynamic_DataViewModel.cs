using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SellbroCRMWPF.Tables 
{

    public class DataGrid_Dynamic_DataViewModel : ViewModelBase
    {
        private ObservableCollection<IDictionary<string, string>> dataCollection = new ObservableCollection<IDictionary<string, string>>();

        public DataGrid_Dynamic_DataViewModel()
        {
            this.Load();
        }

        public ObservableCollection<DataGridColumns> GridViewColumns { get; set; }
        public ObservableCollection<ExpandoObject> GridViewRowCollection { get; set; }

        public ObservableCollection<ExpandoObject> DataCollection
        {
            set { }
            get
            {
                return this.GridViewRowCollection;
            }
        }


        private void Load()
        {
            this.PrepareCollection();
            this.PrepareRowsCollection();
        }

        /// <summary>
        /// should be used wherever you are 
        /// calling this user control 
        /// </summary>
        private void PrepareCollection()
        {
            this.GridViewColumns = new ObservableCollection<DataGridColumns>();
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Employee Id", BindingPropertyName = "ID" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Employee Name", BindingPropertyName = "Name" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Employee Designation", BindingPropertyName = "Designation" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
            this.GridViewColumns.Add(new DataGridColumns { DisplayColumnName = "Department", BindingPropertyName = "Category" });
        }

        private void PrepareRowsCollection()
        {
           this.GridViewRowCollection = new ObservableCollection<ExpandoObject>();
           var item =new ExpandoObject() as IDictionary<string, object>;

            //Add key as Binding Property and value as Row Value to be displayed
            item.Add("ID", "0001");
            item.Add("Name", "Test Emloyee1");
            item.Add("Designation", "Consultant");
            item.Add("Category", "Finance");
            

            this.GridViewRowCollection.Add((ExpandoObject)item);


            for (int i = 0; i < 100; i++)
            {
                item = new ExpandoObject() as IDictionary<string, object>;
                item.Add("ID", $"000{i}");
                item.Add("Name", "Test Emloyee2");
                item.Add("Designation", "SE");
                item.Add("Category", "Banking");
                this.GridViewRowCollection.Add((ExpandoObject) item);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using SellBroCRMWPF;
using SellBroCRMWPF.API;
using SellBroCRMWPF.Auth;

namespace SellbroCRMWPF.Tables
{
    public class TablesModel
    {
                private static Action _goToAuth;
                public string Email { get; set; }
                public DelegateCommand Logout { get; }
                public TablesModel(Action goToAuth)
                {
                    
                    _goToAuth = goToAuth;
                    Logout = new DelegateCommand(OnLogout);
                    Email = AuthenticationUser.GetInstance().Email;
                    MessageBox.Show(Email);
                    GetAllTables();
                }
        
                private async void GetAllTables()
                {
                    await TablesAPI.RequestAllTables();
                   
                    GetTableData();
                }
        
                private async void GetTableData(int tableNum = 1)
                {
                    Table t = await TablesAPI.RequestTable() ?? await TablesAPI.CreateTable();
        
                    /*DataTable dt = new DataTable();
        
                    List<string> columnNames = new List<string>();
                    dynamic expando = new ExpandoObject();
                    
                    foreach (var fieldN in t.Fields)
                    {
                        columnNames.Add(fieldN.Name);
                        var fieldNn = fieldN.Name;
                        ((IDictionary<String, Object>)expando)[fieldN.ToString()] = fieldNn;
                    }
        
                    foreach (KeyValuePair<string, object> kvp in expando)
                    {
                        DataGridTextColumn column = new DataGridTextColumn();
                        column.IsReadOnly = false;
                        column.Header = kvp.Value.ToString();
                        column.Binding = new Binding(kvp.Value.ToString());
                        dataGrid.Columns.Add(column);
                    }
                    
                    List<ExpandoObject> rows = new List<ExpandoObject>();
        
                    int maxLength = 0;
        
                    foreach (var fieldName in t.Fields)
                    {
                        int len = fieldName.Items.Count;
                        if (len > maxLength)
                        {
                            maxLength = len;
                        }
                    }
        
                    for (int i = 0; i < maxLength; i++)
                    {
                        dynamic newRow = new ExpandoObject();
                        newRow.Names = "Names";
                        newRow.Lastnames = "lastname";
                        rows.Add(newRow);
                        dataGrid.Items.Add(newRow);
                    }
                    
                    dataGrid.CanUserSortColumns = false;*/
                }
        
                private void OnLogout()
                {
                    _goToAuth.Invoke();
                }
        
                
    }
}
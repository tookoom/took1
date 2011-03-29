using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestDataSource
/// </summary>
public class TestDataSource
{
    public List<TestData> DataSource { get; set; }

    public TestDataSource()
    {
        DataSource = new List<TestData>();
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 1", Price = 100000, Title = "Imóvel 1" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 2", Price = 100000, Title = "Imóvel 2" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 3", Price = 100000, Title = "Imóvel 3" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 4", Price = 100000, Title = "Imóvel 4" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 5", Price = 100000, Title = "Imóvel 5" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 6", Price = 100000, Title = "Imóvel 6" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 7", Price = 100000, Title = "Imóvel 7" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 8", Price = 100000, Title = "Imóvel 8" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 9", Price = 100000, Title = "Imóvel 9" });
        DataSource.Add(new TestData() { Description = "Descrição do Imóvel 10", Price = 100000, Title = "Imóvel 10" });
        //
        // TODO: Add constructor logic here
        //
    }

    public List<TestData> GetData()
    {
        return DataSource;
    }
}
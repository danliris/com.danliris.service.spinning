using Com.Danliris.Service.Spinning.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Com.Danliris.Service.Spinning.Lib.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Data;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class SpinningInputProductionService : BasicService<SpinningDbContext, SpinningInputProduction>, IMap<SpinningInputProduction, SpinningInputProductionViewModel>
    {
        public SpinningInputProductionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public SpinningInputProductionViewModel MapToViewModel(SpinningInputProduction model)
        {
            SpinningInputProductionViewModel viewModel = new SpinningInputProductionViewModel();
            PropertyCopier<SpinningInputProduction, SpinningInputProductionViewModel>.Copy(model, viewModel);
            viewModel.NomorInputProduksi = model.NomorInputProduksi;
            viewModel.Date = model.Date;
            viewModel.Shift = model.Shift;
            viewModel.Yarn = new SpinningInputProductionViewModel.yarn();
            viewModel.Yarn.Id = model.YarnId;
            viewModel.Yarn.Name = model.YarnName;
            viewModel.Yarn.Ne = model.Ne;
            viewModel.Machine = new SpinningInputProductionViewModel.machine();
            viewModel.Machine._id = model.MachineId;
            viewModel.Machine.name = model.MachineName;
            viewModel.Unit = new SpinningInputProductionViewModel.unit();
            viewModel.Unit._id = model.UnitId;
            viewModel.Unit.name = model.UnitName;
            viewModel.Lot = model.Lot;
            viewModel.Counter = model.Counter;
            viewModel.Hank = model.Hank;

            return viewModel;
        }

        public SpinningInputProduction MapToModel(SpinningInputProductionViewModel viewModel)
        {
            SpinningInputProduction model = new SpinningInputProduction();
            PropertyCopier<SpinningInputProductionViewModel, SpinningInputProduction>.Copy(viewModel, model);
            model.Date = (DateTime)viewModel.Date;
            model.Shift = viewModel.Shift;
            model.UnitId = viewModel.Unit._id;
            model.UnitName = viewModel.Unit.name;
            model.MachineId = viewModel.Machine._id;
            model.MachineName = viewModel.Machine.name;
            model.YarnId = viewModel.Yarn.Id;
            model.YarnName = viewModel.Yarn.Name;
            model.Ne = (double)viewModel.Yarn.Ne;
            model.Lot = viewModel.Lot;
            model.Counter = (double)viewModel.Counter;
            model.Hank = (double)viewModel.Hank;
            model.Bale = Math.Round(((double)viewModel.Hank/(double)viewModel.Yarn.Ne/400));

            return model;
        }

        public override Tuple<List<SpinningInputProduction>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<SpinningInputProduction> Query = this.DbContext.SpinningInputProductions;
            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);

            if (Keyword != null)
            {
                List<string> SearchAttributes = new List<string>()
                {
                    "NomorInputProduksi","YarnName","UnitName","MachineName","Lot"
                };

                Query = ConfigureSearch(Query, SearchAttributes, Keyword);
            }

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
               "Id","NomorInputProduksi","Yarn","Unit","Machine","Lot","Shift","Date","Counter","Hank","Input"
            };

            Query = Query
                .Select(o => new SpinningInputProduction
                {
                    Id = o.Id,
                    NomorInputProduksi = o.NomorInputProduksi,
                    YarnId = o.YarnId,
                    YarnName = o.YarnName,
                    UnitId = o.UnitId,
                    UnitName = o.UnitName,
                    MachineId = o.MachineId,
                    MachineName = o.MachineName,
                    Lot = o.Lot,
                    Shift = o.Shift,
                    Date = o.Date,
                    Counter = o.Counter,
                    Hank = o.Hank,
                    Ne = o.Ne,
                    Bale=o.Bale,
                });

            /* Order */
            if (OrderDictionary.Count.Equals(0))
            {
                OrderDictionary.Add("_updatedDate", General.DESCENDING);

                Query = Query.OrderByDescending(b => b._LastModifiedUtc); /* Default Order */
            }
            else
            {
                string Key = OrderDictionary.Keys.First();
                string OrderType = OrderDictionary[Key];
                string TransformKey = General.TransformOrderBy(Key);

                BindingFlags IgnoreCase = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

                Query = OrderType.Equals(General.ASCENDING) ?
                    Query.OrderBy(b => b.GetType().GetProperty(TransformKey, IgnoreCase).GetValue(b)) :
                    Query.OrderByDescending(b => b.GetType().GetProperty(TransformKey, IgnoreCase).GetValue(b));
            }

            //Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            //Query = ConfigureFilter(Query, FilterDictionary);

            /* Pagination */
            Pageable<SpinningInputProduction> pageable = new Pageable<SpinningInputProduction>(Query, Page - 1, Size);
            List<SpinningInputProduction> Data = pageable.Data.ToList<SpinningInputProduction>();

            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }


        public override void OnCreating(SpinningInputProduction model)
        {
            do
            {
                model.NomorInputProduksi = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.NomorInputProduksi.Equals(model.NomorInputProduksi)));

            base.OnCreating(model);
        }

        public async Task<int> CreateModels(List<SpinningInputProduction> models)
        {
            int created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (SpinningInputProduction model in models)
                    {
                        created = await this.CreateAsync(model);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return created;
        }

        private class xlsVM
        {

        }

        //public async Task<List<SpinningInputProduction>> getDataXls(string unit, string DateFrom, string DateTo)
        //{

        //    List<SpinningInputProduction> result = new List<SpinningInputProduction>();
        //    DateTime dateFrom = Convert.ToDateTime(DateFrom);
        //    DateTime dateTo = Convert.ToDateTime(DateTo);
        //    result = await this.DbSet.Where(data => String.Equals(data.UnitName, unit) && (data.Date >= dateFrom && data.Date <= dateTo) && !data._IsDeleted).OrderByDescending(x => x._LastModifiedUtc).ToListAsync();

        //    return result;
        //}

        public class TempData
        {
            public string Unit { get; set; }
            public DateTime Date { get; set; }
            public string Yarn { get; set; }
            public string Machine { get; set; }
            public string Shift { get; set; }
            public double Ne { get; set; }
            public double Counter { get; set; }
            public double Hank { get; set; }
            public double Bale { get; set; }
            public string Lot { get; set; }
        }
        public class ReportData
        {
            public string Unit { get; set; }
            public DateTime Date { get; set; }
            public string Yarn { get; set; }
            public string Machine { get; set; }
            public double FirstShift { get; set; }
            public double SecondShift { get; set; }
            public double ThirdShift { get; set; }
            public double Total { get; set; }
            public string Lot { get; set; }
        }
        public async Task<List<ReportData>> getDataXls(string unit , string DateFrom, string DateTo)
        {
            List<SpinningInputProduction> models = new List<SpinningInputProduction>();
            DateTime dateFrom = Convert.ToDateTime(DateFrom);
            DateTime dateTo = Convert.ToDateTime(DateTo);
            if (unit !="all")
            {
                models = await this.DbSet.Where(data => String.Equals(data.UnitName, unit) && (data.Date >= dateFrom && data.Date <= dateTo) && !data._IsDeleted).OrderByDescending(x => x._LastModifiedUtc).ToListAsync();

            }
            else if(unit =="all")
            {
                models = await this.DbSet.Where(data => (data.Date >= dateFrom && data.Date <= dateTo) && !data._IsDeleted).OrderByDescending(x => x._LastModifiedUtc).ToListAsync();
            }

            List<TempData> tempData = models
                   .GroupBy(g => new { g.UnitName, g.Shift, g.Date, g.YarnName,g.MachineName })
                   .Select(x => new TempData
                   {
                       Date = x.First().Date,
                       Unit = x.First().UnitName,
                       Machine = x.First().MachineName,
                       Yarn = x.First().YarnName,
                       Shift = x.First().Shift,
                       Counter = x.Sum(s => s.Counter),
                       Hank = x.Sum(s => s.Hank),
                       Ne = x.Sum(s=>s.Ne),
                       Bale = x.Sum(s=>s.Bale),
                       Lot=x.First().Lot,
                   }).ToList();

            List<ReportData> results = tempData
                .GroupBy(g => new { g.Unit, g.Yarn, g.Machine , g.Date })
                .Select(x => new ReportData
                {
                    Date = x.First().Date,
                    Unit = x.First().Unit,
                    Machine = x.First().Machine,
                    Yarn = x.First().Yarn,
                    Lot=x.First().Lot,
                    FirstShift = x.Where(c => String.Equals(c.Shift, "Shift I: 06.00 – 14.00")).Sum(s => s.Bale),
                    
                    SecondShift = x.Where(c => String.Equals(c.Shift, "Shift II: 14.00 – 22.00")).Sum(s => s.Bale),
                  
                    ThirdShift = x.Where(c => String.Equals(c.Shift, "Shift III: 22:00 – 06.00")).Sum(s => s.Bale),
                 
                    Total = (x.Sum(s => s.Bale))
                }).ToList();
            return results;
        }

        public MemoryStream GenerateExcel(List<ReportData> data)
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "Date", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Unit Name", DataType = typeof(String) });
            //result.Columns.Add(new DataColumn() { ColumnName = "Machine Name", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Yarn Name", DataType = typeof(String) });
            //result.Columns.Add(new DataColumn() { ColumnName = "Lot", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift I", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift II", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift III", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total", DataType = typeof(double) });
            if (data.Count == 0)
                result.Rows.Add("", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            foreach (var item in data)
            result.Rows.Add((item.Date), item.Unit,item.Yarn,item.FirstShift,item.SecondShift,item.ThirdShift,item.Total);

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }



    }

}


using Com.Danliris.Service.Spinning.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.Linq.Dynamic.Core;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Com.Danliris.Service.Spinning.Lib.Interfaces;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class YarnOutputProductionService : BasicService<SpinningDbContext, YarnOutputProduction>, IMap<YarnOutputProduction, YarnOutputProductionViewModel>
    {
        public YarnOutputProductionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<YarnOutputProduction>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<YarnOutputProduction> Query = this.DbContext.YarnOutputProductions;

            List<string> SearchAttributes = new List<string>()
                {
                    "SpinningName", "Code", "YarnName", "LotYarnName", "MachineName"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "Spinning", "Date", "Code", "Yarn", "LotYarn", "Shift", "Machine", "YarnWeightPerCone", "GoodOutput", "BadOutput", "DrumTotal",
                };
            Query = Query
                .Select(output => new YarnOutputProduction
                {
                    Id = output.Id,
                    Code = output.Code,
                    Date = output.Date,
                    SpinningId = output.SpinningId,
                    SpinningCode = output.SpinningCode,
                    SpinningName = output.SpinningName,
                    YarnId = output.YarnId,
                    YarnCode = output.YarnCode,
                    YarnName = output.YarnName,
                    LotYarnId = output.LotYarnId,
                    LotYarnCode = output.LotYarnCode,
                    LotYarnName = output.LotYarnName,
                    Shift = output.Shift,
                    MachineId = output.MachineId,
                    MachineCode = output.MachineCode,
                    MachineName = output.MachineName,
                    YarnWeightPerCone = output.YarnWeightPerCone,
                    GoodOutput = output.GoodOutput,
                    BadOutput = output.BadOutput,
                    DrumTotal = output.DrumTotal
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<YarnOutputProduction> pageable = new Pageable<YarnOutputProduction>(Query, Page - 1, Size);
            List<YarnOutputProduction> Data = pageable.Data.ToList<YarnOutputProduction>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(YarnOutputProduction model)
        {
            do
            {
                model.Code = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            base.OnCreating(model);
        }

        public async Task<int> CreateModels(List<YarnOutputProduction> models)
        {
            int created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (YarnOutputProduction model in models)
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

        public YarnOutputProductionViewModel MapToViewModel(YarnOutputProduction model)
        {
            YarnOutputProductionViewModel viewModel = new YarnOutputProductionViewModel();
            PropertyCopier<YarnOutputProduction, YarnOutputProductionViewModel>.Copy(model, viewModel);
            viewModel.Date = model.Date;
            viewModel.Shift = model.Shift;
            viewModel.Yarn = new YarnOutputProductionViewModel.YarnVM();
            viewModel.Yarn.Id = model.YarnId;
            viewModel.Yarn.Code = model.YarnCode;
            viewModel.Yarn.Name = model.YarnName;
            viewModel.Spinning = new YarnOutputProductionViewModel.SpinningVm();
            viewModel.Spinning._id = model.SpinningId;
            viewModel.Spinning.code = model.SpinningCode;
            viewModel.Spinning.name = model.SpinningName;
            viewModel.Machine = new YarnOutputProductionViewModel.MachineVM();
            viewModel.Machine._id = model.MachineId;
            viewModel.Machine.code = model.MachineCode;
            viewModel.Machine.name = model.MachineName;
            viewModel.LotYarn = new YarnOutputProductionViewModel.LotYarnVM();
            viewModel.LotYarn.Id = model.LotYarnId;
            viewModel.LotYarn.Code = model.LotYarnCode;
            viewModel.LotYarn.Lot = model.LotYarnName;
            viewModel.BadOutput = model.BadOutput;
            viewModel.GoodOutput = model.GoodOutput;
            viewModel.DrumTotal = model.DrumTotal;
            viewModel.YarnWeightPerCone = model.YarnWeightPerCone;
            return viewModel;
        }

        public YarnOutputProduction MapToModel(YarnOutputProductionViewModel viewModel)
        {
            YarnOutputProduction model = new YarnOutputProduction();
            PropertyCopier<YarnOutputProductionViewModel, YarnOutputProduction>.Copy(viewModel, model);
            model.Date = (DateTime)viewModel.Date;
            model.Shift = viewModel.Shift;
            model.YarnId = viewModel.Yarn.Id != null ? (int)viewModel.Yarn.Id : 0;
            model.YarnCode = viewModel.Yarn.Code;
            model.YarnName = viewModel.Yarn.Name;
            model.SpinningId = viewModel.Spinning._id;
            model.SpinningCode = viewModel.Spinning.code;
            model.SpinningName = viewModel.Spinning.name;
            model.MachineId = viewModel.Machine._id;
            model.MachineCode = viewModel.Machine.code;
            model.MachineName = viewModel.Machine.name;
            model.LotYarnId = viewModel.LotYarn.Id != null ? (int)viewModel.LotYarn.Id : 0; ;
            model.LotYarnCode = viewModel.LotYarn.Code;
            model.LotYarnName = viewModel.LotYarn.Lot;
            model.BadOutput = (double)viewModel.BadOutput;
            model.GoodOutput = (double)viewModel.GoodOutput;
            model.DrumTotal = (double)viewModel.DrumTotal;
            model.YarnWeightPerCone = (double)viewModel.YarnWeightPerCone;
            return model;
        }

        public class TempData
        {
            public string Unit { get; set; }
            public DateTime Date { get; set; }
            public string Yarn { get; set; }
            public string Shift { get; set; }
            public double Good { get; set; }
            public double Bad { get; set; }
            public double Weight { get; set; }
        }

        public class ReportData
        {
            public string Unit { get; set; }
            public DateTime Date { get; set; }
            public string Yarn { get; set; }
            public double FirstShiftGood { get; set; }
            public double FirstShiftBad { get; set; }
            public double SecondShiftGood { get; set; }
            public double SecondShiftBad { get; set; }
            public double ThirdShiftGood { get; set; }
            public double ThirdShiftBad { get; set; }
            public double SubtotalGood { get; set; }
            public double SubtotalBad { get; set; }
            public double Total { get; set; }
        }

        public async Task<List<ReportData>> getData(string SpinningName, string YarnName, DateTime DateFrom, DateTime DateTo)
        {
            List<YarnOutputProduction> models = new List<YarnOutputProduction>();
            models = await this.DbSet.Where(data => (data.Date >= DateFrom && data.Date <= DateTo) && !data._IsDeleted).OrderByDescending(x => x._LastModifiedUtc).ToListAsync();
            
            if (!String.Equals(SpinningName.ToUpper(), "ALL"))
            {
                models = models.Where(x => String.Equals(SpinningName.ToUpper(), x.SpinningName.ToUpper())).ToList();
            }

            if (!String.Equals(YarnName.ToUpper(), "ALL"))
            {
                models = models.Where(x => String.Equals(YarnName.ToUpper(), x.YarnName.ToUpper())).ToList();
            }

            List<TempData> tempData = models
                   .GroupBy(g => new { g.SpinningName, g.Shift, g.Date, g.YarnName })
                   .Select(x => new TempData
                   {
                       Unit = x.First().SpinningName,
                       Date = x.First().Date,
                       Yarn = x.First().YarnName,
                       Shift = x.First().Shift,
                       Good = x.Sum(s => s.GoodOutput),
                       Bad = x.Sum(s => s.BadOutput),
                       Weight = x.Sum(s => s.YarnWeightPerCone)
                   }).ToList();

            List<ReportData> results = tempData
                .GroupBy(g => new { g.Unit, g.Date, g.Yarn })
                .Select(x => new ReportData
                {
                    Unit = x.First().Unit,
                    Date = x.First().Date,
                    Yarn = x.First().Yarn,
                    FirstShiftGood = x.Where(c => String.Equals(c.Shift, "Shift I: 06.00 - 14.00")).Sum(s => s.Good) / (181.44 / x.Where(c => String.Equals(c.Shift, "Shift I: 06.00 - 14.00")).Sum(s => s.Weight)),
                    FirstShiftBad = x.Where(c => String.Equals(c.Shift, "Shift I: 06.00 - 14.00")).Sum(s => s.Bad) / (181.44 / x.Where(c => String.Equals(c.Shift, "Shift I: 06.00 - 14.00")).Sum(s => s.Weight)),
                    SecondShiftGood = x.Where(c => String.Equals(c.Shift, "Shift II: 14.00 - 22.00")).Sum(s => s.Good) / (181.44 / x.Where(c => String.Equals(c.Shift, "Shift II: 14.00 - 22.00")).Sum(s => s.Weight)),
                    SecondShiftBad = x.Where(c => String.Equals(c.Shift, "Shift II: 14.00 - 22.00")).Sum(s => s.Bad) / (181.44 / x.Where(c => String.Equals(c.Shift, "Shift II: 14.00 - 22.00")).Sum(s => s.Weight)),
                    ThirdShiftGood = x.Where(c => String.Equals(c.Shift, "Shift III: 22.00 - 06.00")).Sum(s => s.Good) / (181.44 / x.Where(c => String.Equals(c.Shift, "Shift III: 22.00 - 06.00")).Sum(s => s.Weight)),
                    ThirdShiftBad = x.Where(c => String.Equals(c.Shift, "Shift III: 22.00 - 06.00")).Sum(s => s.Bad) / (181.44 / x.Where(c => String.Equals(c.Shift, "Shift III: 22.00 - 06.00")).Sum(s => s.Weight)),
                    SubtotalGood = x.Sum(s => s.Good) / (181.44 / x.Sum(s => s.Weight)),
                    SubtotalBad = x.Sum(s => s.Bad) / (181.44 / x.Sum(s => s.Weight)),
                    Total = (x.Sum(s => s.Good) + x.Sum(s => s.Bad)) / (181.44 / x.Sum(s => s.Weight))
                }).ToList();

            return results;

        }

        public MemoryStream GenerateExcel(List<ReportData> data)
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Benang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift I (bale) - Good", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift I (bale) - Bad", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift II (bale) - Good", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift II (bale) - Bad", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift III (bale) - Good", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shift III (bale) - Bad", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Subtotal (bale) - Good", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Subtotal (bale) - Bad", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total", DataType = typeof(Double) });
            if (data.Count == 0)
                result.Rows.Add("", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0); // to allow column name to be generated properly for empty data as template
            else
                foreach (var item in data)
                {
                    result.Rows.Add($"{item.Date:dd/MM/yyyy}", item.Unit, item.Yarn, Math.Round(item.FirstShiftGood, 2), Math.Round(item.FirstShiftBad, 2), Math.Round(item.SecondShiftGood, 2), Math.Round(item.SecondShiftBad, 2), Math.Round(item.ThirdShiftGood, 2), Math.Round(item.ThirdShiftBad, 2), Math.Round(item.SubtotalGood, 2), Math.Round(item.SubtotalBad, 2), Math.Round(item.Total, 2));
                }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Laporan Output") }, true);
        }
    }
}

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
    }
}

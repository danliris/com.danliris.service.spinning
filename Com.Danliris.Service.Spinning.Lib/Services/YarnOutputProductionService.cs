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
                    "Spinning", "Code", "YarnName", "LotYarnName", "MachineName"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id",  "Spinning", "Date", "Code", "YarnName", "LotYarnName", "Shift", "MachineName", "YarnWeightPerCone", "GoodOutput", "BadOutput", "DrumTotal"
                };
            Query = Query
                .Select(output => new YarnOutputProduction
                {
                    Id = output.Id,
                    Code = output.Code,
                    Date = output.Date,
                    Spinning = output.Spinning,
                    YarnName = output.YarnName,
                    LotYarnName = output.LotYarnName,
                    Shift = output.Shift,
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

        public YarnOutputProductionViewModel MapToViewModel(YarnOutputProduction model)
        {
            YarnOutputProductionViewModel viewModel = new YarnOutputProductionViewModel();
            PropertyCopier<YarnOutputProduction, YarnOutputProductionViewModel>.Copy(model, viewModel);
            viewModel.Date = (DateTime?)model.Date;
            viewModel.YarnId = (int?)model.YarnId;
            viewModel.LotYarnId = (int?)model.LotYarnId;
            viewModel.LotYarnId = (int?)model.LotYarnId;
            viewModel.BadOutput = (double?)model.BadOutput;
            viewModel.GoodOutput = (double?)model.GoodOutput;
            viewModel.DrumTotal = (double?)model.DrumTotal;
            viewModel.YarnWeightPerCone = (double?)model.YarnWeightPerCone;
            return viewModel;
        }

        public YarnOutputProduction MapToModel(YarnOutputProductionViewModel viewModel)
        {
            YarnOutputProduction model = new YarnOutputProduction();
            PropertyCopier<YarnOutputProductionViewModel, YarnOutputProduction>.Copy(viewModel, model);
            model.Date = (DateTime)viewModel.Date;
            model.YarnId = (int)viewModel.YarnId;
            model.LotYarnId = (int)viewModel.LotYarnId;
            model.LotYarnId = (int)viewModel.LotYarnId;
            model.BadOutput = (double)viewModel.BadOutput;
            model.GoodOutput = (double)viewModel.GoodOutput;
            model.DrumTotal = (double)viewModel.DrumTotal;
            model.YarnWeightPerCone = (double)viewModel.YarnWeightPerCone;
            return model;
        }
    }
}

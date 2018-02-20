﻿using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.Interfaces
{
    public interface IGeneralUploadService<TViewModel>
    {
        Tuple<bool, List<object>> UploadValidate(List<TViewModel> Data);
        List<string> CsvHeader { get; }
    }
}

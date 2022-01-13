using System;
using System.Collections.Generic;
namespace SalahService.Model
{

    public class RootObject
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public Timings Timings { get; set; }
    }


}

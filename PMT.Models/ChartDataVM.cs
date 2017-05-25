using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class ChartDataVM
    {
        public ChartDataVM()
        {
            datasets = new List<ChartDatasetsVM>();
            labels = new List<string>();
        }

        public List<ChartDatasetsVM> datasets;
        public List<string> labels;

        
    }

    public class ChartDatasetsVM
    {
        public ChartDatasetsVM()
        {
            backgroundColor = new List<string>();
            borderColor = new List<string>();
            data = new List<string>();
        }
        public string labels;
        public List<string> backgroundColor;
        public List<string> borderColor;
        public List<string> data;
        public string borderWidth;
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReportManagement;

namespace ReportManagement.ViewModel
{
    public class ProjectSummary
    {
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public IEnumerable<Opinion> Opinions { get; set; }
        //public IEnumerable<Action> Actions { get; set; }
    }
}
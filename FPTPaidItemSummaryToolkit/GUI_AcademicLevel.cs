using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_AcademicLevel : Form
    {
        List<AcademicLevel> academicList = new List<AcademicLevel>();

        public GUI_AcademicLevel()
        {
            InitializeComponent();
        }
    }
}

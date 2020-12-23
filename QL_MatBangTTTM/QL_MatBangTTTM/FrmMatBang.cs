using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Localization;

namespace QL_MatBangTTTM
{
    public partial class FrmMatBang : DevExpress.XtraEditors.XtraForm
    {
        public FrmMatBang()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
        }
    }
}
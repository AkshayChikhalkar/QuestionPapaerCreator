using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToTextFile
{
    /// <summary>
    /// Interaction logic for mainForm.xaml
    /// </summary>
    public partial class mainForm : Window
    {
        public mainForm()
        {
            InitializeComponent();
            cb_Subject.DataContext = Enum.GetValues(typeof(Subjects)).Cast<Subjects>();
            cb_TestType.DataContext = Enum.GetValues(typeof(TestType)).Cast<TestType>();
            cb_TestNo.DataContext = new int []  { 1, 2, 3 };
            cb_TestNo.DisplayMemberPath = "No";


        }

        enum Subjects
        {
            Pharmaceutics,
            PharmaceuticalChemistry,
            Pharmacognosy,
            HumanAnatomyAndPhysiology,
            SocialPharmacy
        }

        enum TestType
        {
            ClassTest,
            Temp1,
            Temp2
        }
    }
}

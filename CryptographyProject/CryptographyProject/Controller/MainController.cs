using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptographyProject.Model;

namespace CryptographyProject.Controller
{
    public class MainController
    {
        //Data model
        private DataModel _dataModel;
        public DataModel DataModel
        {
            get{
                if(_dataModel == null)
                {
                    _dataModel = new DataModel();
                }
                return _dataModel;
            }
        }

        //Validator
        public bool ValidateData()
        {
            return false;
        }

        public void StartTheProcess()
        {
            
        }

        public void StopTheProcess()
        {
            
        }
    }
}

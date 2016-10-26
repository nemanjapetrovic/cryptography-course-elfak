using CryptographyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject.Controller
{
    public interface IEncryptionAlgorithm
    {
        void SimpleSubstitutionEncryption(FileInfo file, FormModel model);
        void SimpleSubstitutionDecryption(FileInfo file, FormModel model);
    }
}

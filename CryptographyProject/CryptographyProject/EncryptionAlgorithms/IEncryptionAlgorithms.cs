using CryptographyProject.Model;
using System.IO;

namespace CryptographyProject.EncryptionAlgorithms
{
    /// <summary>
    /// Algoithms enum, indexes of algorithms in a view algorithms ListBox.
    /// Just add new algorithms enums here with indexes in ListBox for algorithms.
    /// </summary>
    public enum Algorithms
    {
        SimpleSubstitution = 0
    };

    /// <summary>
    /// Implemented in LoadedFilesController. Used to call the classes methods for ciphers algorithms.
    /// </summary>
    public interface IEncryptionAlgorithms
    {
        /// <summary>
        /// Simple substitution cipher algorithm, encryption method.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="model"></param>
        void SimpleSubstitutionEncryption(FileInfo file, FormModel model);

        /// <summary>
        /// Simple substitution cipher algorithm, decryption method.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="model"></param>
        void SimpleSubstitutionDecryption(FileInfo file, FormModel model);
    }
}

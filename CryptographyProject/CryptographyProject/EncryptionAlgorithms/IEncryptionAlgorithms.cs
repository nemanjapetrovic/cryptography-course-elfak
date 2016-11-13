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
        SimpleSubstitution = 0,
        RC4 = 1,
        TEA = 2,
        XTEA = 3,
        TEA_BMP = 4,
        XTEA_BMP = 5,
        Knapsack = 6
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

        /// <summary>
        /// RC4 cipher algorithm, encryption method.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="model"></param>
        void RC4Encryption(FileInfo file, FormModel model);

        /// <summary>
        /// RC4 cipher algorithm, decryption method.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="model"></param>
        void RC4Decryption(FileInfo file, FormModel model);

        void TEAEcnryption(FileInfo file, FormModel model);
        void TEADecryption(FileInfo file, FormModel model);

        void XTEAEcnryption(FileInfo file, FormModel model);
        void XTEADecryption(FileInfo file, FormModel model);

        void TEABMPEcnryption(FileInfo file, FormModel model);
        void TEABMPDecryption(FileInfo file, FormModel model);

        void XTEABMPEcnryption(FileInfo file, FormModel model);
        void XTEABMPDecryption(FileInfo file, FormModel model);

        void KnapsackEncryption(FileInfo file, FormModel model);
        void KnapsackDecryption(FileInfo file, FormModel model);
    }
}

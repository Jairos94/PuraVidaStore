using System.Security.Cryptography;
using System.Text;
using XSystem.Security.Cryptography;

namespace PuraVidaStoreBK.Utilitarios
{
    public class EncripDescrip
    {
        private static readonly string key = "password";
        public static string Encriptar(string texto)
        {
            byte[] keyArray;
            byte[] Arreglo_a_Cifrar = Encoding.UTF8.GetBytes(texto);
            byte[] ivArray = new byte[8];
            //Algoritmo MD5
            XSystem.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new XSystem.Security.Cryptography.MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice hashing
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            Array.Copy(keyArray, 0, ivArray, 0, 8);

            //Algoritmo 3DES
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
                IV = ivArray
            };
            //se empieza con la transformación de la cadena
            System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }
    }
}

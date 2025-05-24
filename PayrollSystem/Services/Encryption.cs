using System;

namespace PayrollSystem.Services
{
    public static class Encryption
    {
        // A fixed shift value for the shift cipher.
        private const int Shift = 3;

        /// <summary>
        /// Encrypts a plain text string using a shift cipher.
        /// ceaser cipher shift 29 a --> d
        /// </summary>
        /// <param name="plainText">The plain text string to encrypt.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            char[] encryptedChars = new char[plainText.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                char c = plainText[i];

                // Shift letters (both uppercase and lowercase).
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    encryptedChars[i] = (char)((c - offset + Shift) % 26 + offset);
                }
                else
                {
                    // Non-alphabetic characters remain unchanged.
                    encryptedChars[i] = c;
                }
            }

            return new string(encryptedChars);
        }

        /// <summary>
        /// Decrypts an encrypted string that was encrypted using a shift cipher.
        /// </summary>
        /// <param name="encryptedText">The encrypted string.</param>
        /// <returns>The original plain text string.</returns>
        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText)) return encryptedText;

            char[] decryptedChars = new char[encryptedText.Length];

            for (int i = 0; i < encryptedText.Length; i++)
            {
                char c = encryptedText[i];

                // Reverse shift letters (both uppercase and lowercase).
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    decryptedChars[i] = (char)((c - offset - Shift + 26) % 26 + offset);
                }
                else
                {
                    // Non-alphabetic characters remain unchanged.
                    decryptedChars[i] = c;
                }
            }

            return new string(decryptedChars);
        }
    }
}

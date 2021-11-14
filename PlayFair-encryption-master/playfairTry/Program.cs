using System;
using System.Text.RegularExpressions;

namespace PlayfairCipherCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string SECRET_KEY = "KGYLOPNU";
            Console.WriteLine("Enter your String for Playfair Cipher Encryption:");
            Console.Write("\n");
            string sPlainText = Console.ReadLine().ToLower();
            Console.Write("\n");
            string sCipherText = PlayfairEncryption(sPlainText, SECRET_KEY);
            Console.WriteLine("Your Cipher Text: " + sCipherText);
            Console.Write("\n");
            string sDecryptedPlainText = PlayfairDecryption(sCipherText, SECRET_KEY);

            Console.WriteLine("Your Plain Text: " + sDecryptedPlainText);

            Console.Read();
        }

        static string PlayfairEncryption(string sInput, string sKey)
        {
            string sEncryptedText = string.Empty;

            if ((sKey != "") && (sInput != ""))
            {
                sKey = sKey.ToLower();
                string sGrid = null;
                string sAlpha = "abcdefghiklmnopqrstuvwxyz";
                sInput = sInput.ToLower();
                string sOutput = "";
                Regex rgx = new Regex("[^a-z-]");
                sKey = rgx.Replace(sKey, "");
                sKey = sKey.Replace('j', 'i');

                for (int i = 0; i < sKey.Length; i++)
                {
                    if ((sGrid == null) || (!sGrid.Contains(sKey[i])))
                    {
                        sGrid += sKey[i];
                    }
                }

                for (int i = 0; i < sAlpha.Length; i++)
                {
                    if (!sGrid.Contains(sAlpha[i]))
                    {
                        sGrid += sAlpha[i];
                    }
                }

                sInput = rgx.Replace(sInput, "");

                sInput = sInput.Replace('j', 'i');

                for (int i = 0; i < sInput.Length; i += 2)
                {
                    if (((i + 1) < sInput.Length) && (sInput[i] == sInput[i + 1]))
                    {
                        sInput = sInput.Insert(i + 1, "x");
                    }
                }
                if ((sInput.Length % 2) > 0)
                {
                    sInput += "x";
                }
                int iTemp = 0;

                do
                {
                    int iPosA = sGrid.IndexOf(sInput[iTemp]);
                    int iPosB = sGrid.IndexOf(sInput[iTemp + 1]);
                    int iRowA = iPosA / 5;
                    int iColA = iPosA % 5;
                    int iRowB = iPosB / 5;
                    int iColB = iPosB % 5;

                    if (iColA == iColB)
                    {
                        iPosA += 5;
                        iPosB += 5;
                    }
                    else
                    {
                        if (iRowA == iRowB)
                        {
                            if (iColA == 4)
                            {
                                iPosA -= 4;
                            }
                            else
                            {
                                iPosA += 1;
                            }
                            if (iColB == 4)
                            {
                                iPosB -= 4;
                            }
                            else
                            {
                                iPosB += 1;
                            }
                        }
                        else
                        {
                            if (iRowA < iRowB)
                            {
                                iPosA -= iColA - iColB;
                                iPosB += iColA - iColB;
                            }
                            else
                            {
                                iPosA += iColB - iColA;
                                iPosB -= iColB - iColA;
                            }
                        }
                    }

                    if (iPosA >= sGrid.Length)
                    {
                        iPosA = 0 + (iPosA - sGrid.Length);
                    }

                    if (iPosB >= sGrid.Length)
                    {
                        iPosB = 0 + (iPosB - sGrid.Length);
                    }

                    if (iPosA < 0)
                    {
                        iPosA = sGrid.Length + iPosA;
                    }

                    if (iPosB < 0)
                    {
                        iPosB = sGrid.Length + iPosB;
                    }
                    sOutput += sGrid[iPosA].ToString() + sGrid[iPosB].ToString();
                    iTemp += 2;
                } while (iTemp < sInput.Length);

                sEncryptedText = sOutput;
            }
            return sEncryptedText;
        }

        static string PlayfairDecryption(string sCipherText, string sKey)
        {
            sKey = sKey.ToLower();
            string sGrid = null;
            string sAlpha = "abcdefghiklmnopqrstuvwxyz";
            string sInput = sCipherText.ToLower();
            string sOutput = "";
            sKey = sKey.Replace('j', 'i');
            for (int i = 0; i < sKey.Length; i++)
            {
                if ((sGrid == null) || (!sGrid.Contains(sKey[i])))
                {
                    sGrid += sKey[i];
                }
            }

            for (int i = 0; i < sAlpha.Length; i++)
            {
                if (!sGrid.Contains(sAlpha[i]))
                {
                    sGrid += sAlpha[i];
                }
            }

            int iTemp = 0;

            do
            {
                int iPosA = sGrid.IndexOf(sInput[iTemp]);
                int iPosB = sGrid.IndexOf(sInput[iTemp + 1]);
                int iRowA = iPosA / 5;
                int iColA = iPosA % 5;
                int iRowB = iPosB / 5;
                int iColB = iPosB % 5;
                if (iColA == iColB)
                {
                    iPosA -= 5;
                    iPosB -= 5;
                }
                else
                {
                    if (iRowA == iRowB)
                    {
                        if (iColA == 0)
                        {
                            iPosA += 4;
                        }
                        else
                        {
                            iPosA -= 1;
                        }

                        if (iColB == 0)
                        {
                            iPosB += 4;
                        }
                        else
                        {
                            iPosB -= 1;
                        }
                    }
                    else
                    {
                        if (iRowA < iRowB)
                        {
                            iPosA -= iColA - iColB;
                            iPosB += iColA - iColB;
                        }
                        else
                        {
                            iPosA += iColB - iColA;
                            iPosB -= iColB - iColA;
                        }
                    }
                }

                if (iPosA > sGrid.Length)
                {
                    iPosA = 0 + (iPosA - sGrid.Length);
                }

                if (iPosB > sGrid.Length)
                {
                    iPosB = 0 + (iPosB - sGrid.Length);
                }

                if (iPosA < 0)
                {
                    iPosA = sGrid.Length + iPosA;
                }

                if (iPosB < 0)
                {
                    iPosB = sGrid.Length + iPosB;
                }

                sOutput += sGrid[iPosA].ToString() + sGrid[iPosB].ToString();
                iTemp += 2;
            } while (iTemp < sInput.Length);
            return sOutput;
        }
    }
}
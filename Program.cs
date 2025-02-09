﻿﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace IDNumbers
{
    class Program
    {

        static void Main(string[] args) {

            FileWriter count = new FileWriter();
            FileReader dates = new FileReader();

            
            string filePath = @"idNumbers.txt";

            int bornBefore2010 = 0;
            int bornAfter2010 = 0;
           

            DateTime[] datesOfBirth = dates.BirthDate(filePath);

            foreach (DateTime dateOfBirth in datesOfBirth) {
                Console.WriteLine(dateOfBirth.ToString("dd-MM-yyyy"));

                if (dateOfBirth.Year < 2010){
                    bornBefore2010++;
                } else {
                    bornAfter2010++;
                }
            }
            count.ageCount(bornBefore2010, bornAfter2010);
            
        } 
        
    }



    class FileReader{

        public DateTime BirthDateFormat(string idNumber)
        {
            int year = int.Parse(idNumber.Substring(0, 2));
            int month = int.Parse(idNumber.Substring(2, 2));
            int day = int.Parse(idNumber.Substring(4, 2));

            if (year >= 0 && year <= 20){
                year += 2000;
            } else {
                year += 1900;
            }

            DateTime format = new DateTime(year, month, day);
            return format;
        }



        private bool IsValidIDNumber(string idNumber) {
            return Regex.IsMatch(idNumber, @"^\d{13}$");
        }


       

        private bool IsRestOfIdValid(string idNumber){

            // SA IDformat: {YYMMDD}{G}{SSS}{C}{A}{Z}
            
             string strRegex = @"^[0-1]+$";
             Regex re = new Regex(strRegex);

             string pattern = @"^[8-9]+$";
             Regex p = new Regex(pattern);
             

    
            string C = idNumber.Substring(10, 1);
            string A = idNumber.Substring(11, 1);

            

            int controlDigit = int.Parse(idNumber.Substring(12, 1));
            int oddsSum = 0;
            int evensDoubledSum = 0;

            for (int i = 0; i < 12; i++)
            {
                var digit = int.Parse(idNumber.Substring(i, 1));

                if (i % 2 == 0)
                {
                    oddsSum += digit;
                }
                else
                {
                    evensDoubledSum += 2 * digit;
                    evensDoubledSum -= digit >= 5 ? 9 : 0;
                }
            }

            int controlDigitExpected = (10 - (oddsSum + evensDoubledSum) % 10) % 10;

   

            // Console.WriteLine("The actual last number: "+Z);
            // Console.WriteLine("Expected last number: "+controlDigitExpected);

            if (!DateTime.TryParseExact(idNumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)){

                return false;
             }

            if(!re.IsMatch(C)){
                return false;
            }

            if(!p.IsMatch(A)){
                return false;
            }

            if(controlDigit!=controlDigitExpected){
                return false;
            }
                            
            return true;      
            }
        





        public DateTime[] BirthDate(string filePath) {

            List<DateTime> datesOfBirth = new List<DateTime>();
            try
            {
                string[] idNumbers = File.ReadAllLines(filePath);

                foreach (string idNumber in idNumbers)
                {
                    if (!IsValidIDNumber(idNumber))
                
                    {
                        
                        Console.WriteLine($"Invalid ID number : {idNumber}");
                        continue;
                    }

                    if(!IsRestOfIdValid(idNumber)){
                        Console.WriteLine($"Invalid ID number (Incorrect C, A or Z) : {idNumber}");
                        continue;
                    }

                    IsRestOfIdValid(idNumber);

                    DateTime dateOfBirth = BirthDateFormat(idNumber);
                    datesOfBirth.Add(dateOfBirth);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            return datesOfBirth.ToArray();
        }


    }




    class FileWriter{

        public void ageCount(int before2010, int after2010)
         {
            string filePath = "ageCount.txt";
                string[] text = { $"People born before 2010: {before2010}",
                                   $"People born after 2010: {after2010}"};
                File.WriteAllLines(filePath, text);
        }


    }

}
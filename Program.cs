﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            if (month < 1 || month > 12 || day < 1 || day > 31)
            {
                Console.WriteLine($"Error: Invalid date of birth in ID number {idNumber}.");
                return default;
            }
            DateTime format = new DateTime(year, month, day);
            return format;
        }



        private bool IsValidIDNumber(string idNumber)
        {
            return Regex.IsMatch(idNumber, @"^\d{13}$");
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